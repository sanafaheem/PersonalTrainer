using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PersonalTrainer.API.Controllers;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;
using PersonalTrainer.API.Services.AI;

namespace PersonalTrainer.Tests.Controllers;

public class UserWorkoutProfileControllerTests
{
    private readonly IUserWorkoutProfileService            _profileService = Substitute.For<IUserWorkoutProfileService>();
    private readonly ICurrentUserService                   _currentUser    = Substitute.For<ICurrentUserService>();
    private readonly IWorkoutGenerationAgent               _workoutAgent   = Substitute.For<IWorkoutGenerationAgent>();
    private readonly IWorkoutPlanService                   _workoutPlan    = Substitute.For<IWorkoutPlanService>();
    private readonly ILogger<UserWorkoutProfileController> _logger         = Substitute.For<ILogger<UserWorkoutProfileController>>();

    private readonly UserWorkoutProfileController _controller;

    public UserWorkoutProfileControllerTests()
    {
        _controller = new UserWorkoutProfileController(
            _profileService,
            _currentUser,
            _workoutAgent,
            _workoutPlan,
            _logger)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };
    }

    // ── GetByUserId ───────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByUserId_WhenProfileExists_ReturnsOkWithProfile()
    {
        var profile = new UserWorkoutProfileResponse { Id = 1, UserId = "user-1", FirstName = "John" };
        _profileService.GetByUserIdAsync("user-1").Returns(profile);

        var result = await _controller.GetByUserId("user-1");

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(profile, ok.Value);
    }

    [Fact]
    public async Task GetByUserId_WhenProfileNotFound_ReturnsNotFound()
    {
        _profileService.GetByUserIdAsync(Arg.Any<string>()).Returns((UserWorkoutProfileResponse?)null);

        var result = await _controller.GetByUserId("unknown-user");

        Assert.IsType<NotFoundResult>(result);
    }

    // ── GetPlanById ───────────────────────────────────────────────────────────

    [Fact]
    public async Task GetPlanById_WhenPlanExists_ReturnsOkWithPlan()
    {
        var plan = new WorkoutPlanResponse { Id = 42, Title = "Chest Day" };
        _workoutPlan.GetByIdAsync(42).Returns(plan);

        var result = await _controller.GetPlanById(42);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(plan, ok.Value);
    }

    [Fact]
    public async Task GetPlanById_WhenPlanNotFound_ReturnsNotFound()
    {
        _workoutPlan.GetByIdAsync(Arg.Any<int>()).Returns((WorkoutPlanResponse?)null);

        var result = await _controller.GetPlanById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    // ── GetMyPlans ────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetMyPlans_WhenNotLoggedIn_ReturnsUnauthorized()
    {
        _currentUser.IsLoggedIn.Returns(false);

        var result = await _controller.GetMyPlans();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetMyPlans_WhenLoggedIn_AndProfileNotFound_ReturnsEmptyList()
    {
        _currentUser.IsLoggedIn.Returns(true);
        _currentUser.UserId.Returns("user-1");
        _profileService.GetByUserIdAsync("user-1").Returns((UserWorkoutProfileResponse?)null);

        var result = await _controller.GetMyPlans();

        var ok   = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);
        Assert.Empty(list);
    }

    [Fact]
    public async Task GetMyPlans_WhenLoggedIn_AndHasProfile_ReturnsPlanList()
    {
        var profile = new UserWorkoutProfileResponse { Id = 10, UserId = "user-1" };
        var plans   = new List<WorkoutPlanSummaryResponse>
        {
            new() { Id = 1, Title = "Plan A" },
            new() { Id = 2, Title = "Plan B" }
        };
        _currentUser.IsLoggedIn.Returns(true);
        _currentUser.UserId.Returns("user-1");
        _profileService.GetByUserIdAsync("user-1").Returns(profile);
        _workoutPlan.GetAllByProfileIdAsync(10).Returns(plans);

        var result = await _controller.GetMyPlans();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(plans, ok.Value);
    }

    // ── Generate ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Generate_WhenNotLoggedIn_CallsAgentAndReturnsOk()
    {
        var request = BuildRequest();
        var plan    = new WorkoutPlanResponse { Title = "Guest Workout" };
        _currentUser.IsLoggedIn.Returns(false);
        _workoutAgent.GenerateAsync(request).Returns(plan);

        var result = await _controller.Generate(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(plan, ok.Value);
        await _profileService.DidNotReceive().SaveAsync(Arg.Any<UserWorkoutProfileRequest>());
    }

    [Fact]
    public async Task Generate_WhenLoggedIn_AndExistingPlanFound_ReturnsExistingPlanWithoutCallingAgent()
    {
        var request      = BuildRequest();
        var profile      = new UserWorkoutProfileResponse { Id = 5 };
        var existingPlan = new WorkoutPlanResponse { Id = 99, Title = "Cached Plan" };
        _currentUser.IsLoggedIn.Returns(true);
        _currentUser.UserId.Returns("user-1");
        _profileService.SaveAsync(Arg.Any<UserWorkoutProfileRequest>()).Returns(profile);
        _workoutPlan.GetLatestByProfileIdAsync(5).Returns(existingPlan);

        var result = await _controller.Generate(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(existingPlan, ok.Value);
        await _workoutAgent.DidNotReceive().GenerateAsync(Arg.Any<UserWorkoutProfileRequest>());
    }

    [Fact]
    public async Task Generate_WhenLoggedIn_AndNoPlanFound_GeneratesAndSavesPlan()
    {
        var request   = BuildRequest();
        var profile   = new UserWorkoutProfileResponse { Id = 5 };
        var newPlan   = new WorkoutPlanResponse { Title = "New Plan" };
        var savedPlan = new WorkoutPlanResponse { Id = 10, Title = "New Plan" };
        _currentUser.IsLoggedIn.Returns(true);
        _currentUser.UserId.Returns("user-1");
        _profileService.SaveAsync(Arg.Any<UserWorkoutProfileRequest>()).Returns(profile);
        _workoutPlan.GetLatestByProfileIdAsync(5).Returns((WorkoutPlanResponse?)null);
        _workoutAgent.GenerateAsync(Arg.Any<UserWorkoutProfileRequest>()).Returns(newPlan);
        _workoutPlan.SaveAsync(newPlan, 5).Returns(savedPlan);

        var result = await _controller.Generate(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(savedPlan, ok.Value);
    }

    [Fact]
    public async Task Generate_WhenAgentThrowsRateLimitException_Returns429()
    {
        var request = BuildRequest();
        _currentUser.IsLoggedIn.Returns(false);
        _workoutAgent
            .GenerateAsync(Arg.Any<UserWorkoutProfileRequest>())
            .ThrowsAsync(new InvalidOperationException("Gemini rate limit exceeded."));

        var result = await _controller.Generate(request);

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(429, statusResult.StatusCode);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static UserWorkoutProfileRequest BuildRequest() => new()
    {
        FirstName       = "John",
        Age             = 30,
        FitnessLevel    = "Beginner",
        Goal            = "MuscleGain",
        FocusArea       = "Core",
        DurationMinutes = 30,
        Equipment       = ["Mat"]
    };
}

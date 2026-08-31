using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;
using PersonalTrainer.Tests.Infrastructure;

namespace PersonalTrainer.Tests.Services;

/// <summary>
/// Uses SQLite in-memory via TestDatabase.
/// Each test class gets its own fresh database — full isolation between classes.
/// Tests within this class share the same DB but each builds its own data in Arrange.
/// </summary>
public class UserWorkoutProfileServiceTests : IDisposable
{
    private readonly TestDatabase            _db      = new();
    private readonly UserWorkoutProfileService _service;

    public UserWorkoutProfileServiceTests()
    {
        _service = new UserWorkoutProfileService(_db.Context);
    }

    // ── GetByUserIdAsync ──────────────────────────────────────────────────────

    [Fact]
    public async Task GetByUserIdAsync_WhenProfileExists_ReturnsProfile()
    {
        await SeedProfileAsync("user-1", "Alice");

        var result = await _service.GetByUserIdAsync("user-1");

        Assert.NotNull(result);
        Assert.Equal("Alice", result.FirstName);
        Assert.Equal("user-1", result.UserId);
    }

    [Fact]
    public async Task GetByUserIdAsync_WhenProfileDoesNotExist_ReturnsNull()
    {
        var result = await _service.GetByUserIdAsync("nonexistent-user");

        Assert.Null(result);
    }

    // ── SaveAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task SaveAsync_WithNewUser_CreatesProfileAndReturnsIt()
    {
        var request = BuildRequest("user-new", "Bob");

        var result = await _service.SaveAsync(request);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Bob", result.FirstName);
        Assert.Equal("Beginner", result.FitnessLevel);
        Assert.Equal("Core", result.FocusArea);
        Assert.Contains("Mat", result.Equipment);
    }

    [Fact]
    public async Task SaveAsync_WithExistingUser_UpdatesProfileInsteadOfCreatingNew()
    {
        await SeedProfileAsync("user-update", "Original");

        var updateRequest = BuildRequest("user-update", "Updated");
        var result = await _service.SaveAsync(updateRequest);

        // Should still be only one profile for this user
        var count = _db.Context.UserWorkoutProfiles.Count(p => p.UserId == "user-update");
        Assert.Equal(1, count);
        Assert.Equal("Updated", result.FirstName);
    }

    [Fact]
    public async Task SaveAsync_WithExistingUser_UpdatesEquipmentList()
    {
        await SeedProfileAsync("user-equip", "Jane", equipment: [Equipment.Mat]);

        var updateRequest = BuildRequest("user-equip", "Jane", equipment: ["Dumbbells", "Mat"]);
        var result = await _service.SaveAsync(updateRequest);

        Assert.Equal(2, result.Equipment.Count);
        Assert.Contains("Dumbbells", result.Equipment);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private async Task SeedProfileAsync(string userId, string firstName, List<Equipment>? equipment = null)
    {
        _db.Context.UserWorkoutProfiles.Add(new UserWorkoutProfile
        {
            UserId          = userId,
            FirstName       = firstName,
            Age             = 30,
            FitnessLevel    = FitnessLevel.Beginner,
            Goal            = WorkoutGoal.MuscleGain,
            FocusArea       = FocusArea.Core,
            DurationMinutes = 30,
            Equipment       = equipment ?? [Equipment.Mat]
        });
        await _db.Context.SaveChangesAsync();
    }

    private static UserWorkoutProfileRequest BuildRequest(
        string userId, string firstName, List<string>? equipment = null) => new()
    {
        UserId          = userId,
        FirstName       = firstName,
        Age             = 30,
        FitnessLevel    = "Beginner",
        Goal            = "MuscleGain",
        FocusArea       = "Core",
        DurationMinutes = 30,
        Equipment       = equipment ?? ["Mat"]
    };

    public void Dispose() => _db.Dispose();
}

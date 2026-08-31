using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PersonalTrainer.API.Controllers;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services;

namespace PersonalTrainer.Tests.Controllers;

public class ExerciseControllerTests
{
    private readonly IExerciseQueryService _exerciseService = Substitute.For<IExerciseQueryService>();
    private readonly ExerciseController    _controller;

    public ExerciseControllerTests()
    {
        _controller = new ExerciseController(_exerciseService);
    }

    [Fact]
    public async Task Query_WithValidParameters_ReturnsOk()
    {
        _exerciseService
            .GetExercisesForProfileAsync(Arg.Any<FocusArea>(), Arg.Any<List<Equipment>>())
            .Returns([new ExerciseTemplateResult { Name = "Plank Hold" }]);

        var result = await _controller.Query(FocusArea.Core, [Equipment.Mat]);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Query_ReturnsCorrectExerciseCount()
    {
        _exerciseService
            .GetExercisesForProfileAsync(Arg.Any<FocusArea>(), Arg.Any<List<Equipment>>())
            .Returns([
                new() { Name = "Plank Hold" },
                new() { Name = "Crunches"   },
                new() { Name = "Dead Bug"   }
            ]);

        var result = await _controller.Query(FocusArea.Core, [Equipment.Mat]);

        var ok    = Assert.IsType<OkObjectResult>(result);
        var count = (int)ok.Value!.GetType().GetProperty("count")!.GetValue(ok.Value)!;
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task Query_WithNoMatchingExercises_ReturnsZeroCount()
    {
        _exerciseService
            .GetExercisesForProfileAsync(Arg.Any<FocusArea>(), Arg.Any<List<Equipment>>())
            .Returns([]);

        var result = await _controller.Query(FocusArea.Core, [Equipment.Dumbbells]);

        var ok    = Assert.IsType<OkObjectResult>(result);
        var count = (int)ok.Value!.GetType().GetProperty("count")!.GetValue(ok.Value)!;
        Assert.Equal(0, count);
    }
}

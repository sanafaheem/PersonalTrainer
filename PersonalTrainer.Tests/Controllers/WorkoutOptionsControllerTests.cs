using Microsoft.AspNetCore.Mvc;
using PersonalTrainer.API.Controllers;
using PersonalTrainer.API.Enums;

namespace PersonalTrainer.Tests.Controllers;

public class WorkoutOptionsControllerTests
{
    private readonly WorkoutOptionsController _controller = new();

    [Fact]
    public void GetOptions_ReturnsOkResult()
    {
        var result = _controller.GetOptions();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetOptions_ResponseContainsAllFourCategories()
    {
        var value      = (Assert.IsType<OkObjectResult>(_controller.GetOptions())).Value!;
        var properties = value.GetType().GetProperties().Select(p => p.Name);

        Assert.Contains("fitnessLevels", properties);
        Assert.Contains("workoutGoals",  properties);
        Assert.Contains("focusAreas",    properties);
        Assert.Contains("equipment",     properties);
    }

    [Fact]
    public void GetOptions_FitnessLevels_CountMatchesEnum()
    {
        var value         = (Assert.IsType<OkObjectResult>(_controller.GetOptions())).Value!;
        var fitnessLevels = (IEnumerable<object>)value.GetType().GetProperty("fitnessLevels")!.GetValue(value)!;

        Assert.Equal(Enum.GetValues<FitnessLevel>().Length, fitnessLevels.Count());
    }

    [Fact]
    public void GetOptions_Equipment_CountMatchesEnum()
    {
        var value     = (Assert.IsType<OkObjectResult>(_controller.GetOptions())).Value!;
        var equipment = (IEnumerable<object>)value.GetType().GetProperty("equipment")!.GetValue(value)!;

        Assert.Equal(Enum.GetValues<Equipment>().Length, equipment.Count());
    }
}

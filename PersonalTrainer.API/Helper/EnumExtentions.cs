using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersonalTrainer.API.Helpers;

public static class EnumExtentions
{
    public static string GetDisplayName(this Enum value ){
        return value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttribute<DisplayAttribute>()
            ?.Name ?? value.ToString();
    }
}
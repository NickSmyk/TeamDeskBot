using System.ComponentModel;
using System.Reflection;
using TeamDeskBot.Models.Enums;

namespace TeamDeskBot.Extensions;

public static class CommandsExtensions
{
    public static string GetValue(this Commands command)
    {
        return command.ToString();
    }
    
    public static string GetDescription(this Commands command)
    {
        string description = "No description provided to the command";
        FieldInfo? field = command.GetType().GetField(command.ToString());

        if (field is null)
        {
            return description;
        }
        
        object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attrs.Length == 0)
        {
            return description;
        }

        description = ((DescriptionAttribute)attrs[0]).Description;
        return description;
    }
}
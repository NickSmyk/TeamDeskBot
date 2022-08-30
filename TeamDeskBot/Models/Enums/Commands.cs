using System.ComponentModel;

namespace TeamDeskBot.Models.Enums;

public enum Commands
{
    [Description("Get list of all commands")]
    Commands,
    [Description("Get list of all the users")]
    GetUsers,
    [Description("Starts the process of adding user")]
    AddUser,
    [Description("Delete user by id")]
    DeleteUser,
    [Description("Delete user by id")]
    CancelStage,
}
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
    [Description("Starts the process of editing user")]
    EditUser,
    [Description("Delete user by id")]
    DeleteUser,
    [Description("Go to the previous stage of the interaction")]
    Back,
    [Description("Cancel the current interaction")]
    Cancel,
}
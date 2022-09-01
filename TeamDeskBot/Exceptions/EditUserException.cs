namespace TeamDeskBot.Exceptions;

public class EditUserException : BaseException
{
    public EditUserException(int userId, string message) : base(
        $"Error while editing the user with id {userId}. Message : {message}",
        "Error occured while editing the users") { }
}
namespace TeamDeskBot.Exceptions;

public class GetUserException : BaseException
{
    public GetUserException(int userId, string message) : base(
        $"Error while getting the user with id {userId}. Message : {message}",
        "Error occured while requesting the user") { }
}
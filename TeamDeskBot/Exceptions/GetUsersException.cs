namespace TeamDeskBot.Exceptions;

public class GetUsersException : BaseException
{
    public GetUsersException(string message) : base(
        $"Error while getting users. Message : {message}",
        "Error occured while requesting the users") { }
}
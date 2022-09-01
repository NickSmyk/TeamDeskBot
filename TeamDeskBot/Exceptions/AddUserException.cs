namespace TeamDeskBot.Exceptions;

public class AddUserException : BaseException
{
    public AddUserException(string responseCode) : base(
        $"Error while adding new user, the api request returned with code \"{responseCode}\"",
        "Error occured while adding new user") { }
}
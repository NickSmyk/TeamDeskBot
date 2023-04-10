namespace TeamDeskBot.Exceptions;

public class NoInteractionException : BaseException
{
    public NoInteractionException(string user) : base(
        $"No interactions found for user {user}",
        "No interactions were found for the user") { }
}
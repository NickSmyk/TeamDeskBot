namespace TeamDeskBot.Exceptions;

public class BaseException : Exception
{
    private const string DEFAULT_MESSAGE = "An error occured during the execution";
    public readonly string DisplayMessage;

    protected BaseException(string message, string displayMessage = DEFAULT_MESSAGE) : base(message)
    {
        DisplayMessage = displayMessage;
    }
}
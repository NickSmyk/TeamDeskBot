namespace TeamDeskBot.Exceptions;

public class RestRequestException : BaseException
{
    public RestRequestException(string message) : base(
        $"Something went wrong during rest request. Message : {message}",
        "An error occured while sending a request") { }
    
}
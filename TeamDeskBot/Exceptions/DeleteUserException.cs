namespace TeamDeskBot.Exceptions;

public class DeleteUserException: BaseException
{
    public DeleteUserException(int userId, string message) : base(
        $"Error while deleting a user with id {userId}. Message: {message}",
        "Error occured while deleting the user") { }
}
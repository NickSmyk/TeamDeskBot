namespace TeamDeskBot.Exceptions;

public class ParsingException : BaseException
{
    public ParsingException(string value, string variableName) : base(
        $"Was unable to parse value {value} of the variable {variableName}",
        "Error occured while converting data") { }
}
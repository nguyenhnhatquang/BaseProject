namespace BaseProject.Domain.Shares;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided.");

    public static implicit operator Result(Error error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);

    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public string Code { get; }

    public string Description { get; }
}
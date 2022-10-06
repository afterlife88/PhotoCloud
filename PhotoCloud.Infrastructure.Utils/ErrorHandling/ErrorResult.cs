namespace PhotoCloud.Infrastructure.Utils.ErrorHandling;

public class ErrorResult
{
    public string RequestId { get; set; } = null!;

    public IEnumerable<string> Errors { get; set; }

    public ErrorResult(IEnumerable<string> errors, string requestId)
    {
        Errors = errors;
        RequestId = requestId;
    }

    public ErrorResult(string error) => Errors = new string[1]
    {
        error
    };

    public override string ToString() => string.Join(Environment.NewLine, Errors);
}
namespace HttpClients;

public class ErrorResult
{
    public string RequestId { get; set; }

    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

    public ErrorResult(IEnumerable<string> errors, string requestId)
    {
        Errors = errors;
        RequestId = requestId;
    }

    public ErrorResult()
    {
    }

    public ErrorResult(string error) => Errors = new string[1]
    {
        error
    };

    public override string ToString() => string.Join(Environment.NewLine, Errors);
}
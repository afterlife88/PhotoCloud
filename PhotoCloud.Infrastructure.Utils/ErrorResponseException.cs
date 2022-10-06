using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace HttpClients;

public class ErrorResponseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public ErrorResult ErrorResult { get; }

    public override string Message
    {
        get
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
            interpolatedStringHandler.AppendLiteral("Http status: ");
            interpolatedStringHandler.AppendFormatted(StatusCode);
            interpolatedStringHandler.AppendLiteral("; ");
            interpolatedStringHandler.AppendFormatted(ErrorResult);
            return interpolatedStringHandler.ToStringAndClear();
        }
    }

    public ErrorResponseException(ErrorResult errorResult, HttpStatusCode statusCode)
    {
        ErrorResult = errorResult;
        StatusCode = statusCode;
    }

    public ErrorResponseException()
    {
    }

    public ErrorResponseException(string message)
        : base(message)
    {
    }

    public ErrorResponseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected ErrorResponseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
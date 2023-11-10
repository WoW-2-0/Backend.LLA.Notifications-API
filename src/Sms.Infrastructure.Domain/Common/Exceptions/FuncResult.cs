namespace Sms.Infrastructure.Domain.Common.Exceptions;

public class ActionResult<T>
{
    public T Data { get; init; }

    public Exception? Exception { get; }

    public bool IsSuccess => Exception is null;

    public ActionResult(T data) => Data = data;

    public ActionResult(Exception exception) => Exception = exception;
}
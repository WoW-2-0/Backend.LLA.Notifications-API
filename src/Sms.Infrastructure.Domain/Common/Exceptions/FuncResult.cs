namespace Sms.Infrastructure.Domain.Common.Exceptions;

// Options pattern - Data yoki hech nima

// Result pattern - Data? yoki Exception

// notification - success, failed yoki timeout


public class FuncResult<T>
{
    public T Data { get; init; }

    public Exception? Exception { get; }

    public bool IsSuccess => Exception is null;

    public FuncResult(T data) => Data = data;

    public FuncResult(Exception exception) => Exception = exception;
}
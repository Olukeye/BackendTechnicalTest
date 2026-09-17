namespace BackendTechnicalTest.Application.Common.Results;

public sealed class Result<T>
{
    private Result(
        bool isSuccess,
        T? value,
        ResultError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public ResultError? Error { get; }

    public static Result<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>(
            isSuccess: true,
            value: value,
            error: null);
    }

    public static Result<T> Failure(ResultError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result<T>(
            isSuccess: false,
            value: default,
            error: error);
    }
}
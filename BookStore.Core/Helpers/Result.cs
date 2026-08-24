namespace BookStore.Core.Helpers;

public class Result<T>(bool isSuccess, T? value, string? error)
{
    public bool IsSuccess { get; } = isSuccess;
    public T? Value { get; } = value;

    public string? Error { get; set; } = error;

    public static Result<T> Success(T value) => new(true, value, null);

    public static Result<T> Failure(string error) => new(false, default, error);
}

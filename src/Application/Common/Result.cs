using System.Diagnostics.CodeAnalysis;

namespace Application.Common;

public sealed class Result<T>
{
    // ReSharper disable once UnusedMember.Global
    public Result()
    {
    }

    public Result(T data)
    {
        Data = data;
        IsSuccessful = true;
    }

    public Result(string[] errors)
    {
        Errors = errors;
        IsSuccessful = false;
    }

    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(Errors))]
    public bool IsSuccessful { get; set; }

    public T? Data { get; set; }

    public string[]? Errors { get; set; }
}

public static class Result
{
    public static Result<T> Success<T>(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure<T>(string[] errors)
    {
        return new Result<T>(errors);
    }

    public static Result<T> Failure<T>(string error)
    {
        return new Result<T>(new[] { error });
    }
}
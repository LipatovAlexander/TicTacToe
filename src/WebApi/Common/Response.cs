using System.Diagnostics.CodeAnalysis;

namespace WebApi.Common;

public class Response<T>
{
    public Response(T data)
    {
        Data = data;
        IsSuccessful = true;
    }

    public Response(string[] errors)
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

public static class Response
{
    public static Response<T> Success<T>(T data)
    {
        return new Response<T>(data);
    }

    public static Response<T> Failure<T>(string[] errors)
    {
        return new Response<T>(errors);
    }
}
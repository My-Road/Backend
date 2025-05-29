using ErrorOr;
namespace MyRoad.API.Common;

public static class ErrorOrExtensions
{
    public static ErrorOr<TDestination> ToContract<TSource, TDestination>(
        this ErrorOr<TSource> result,
        Func<TSource, TDestination> mapFunc)
    {
        return result.IsError ? result.Errors : mapFunc(result.Value);
    }
}
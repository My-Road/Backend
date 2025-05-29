using MyRoad.Domain.Common.Entities;
using ErrorOr;

namespace MyRoad.API.Common;

public static class PaginationExtensions
{
    private static PaginatedResponse<TDestination> MapItems<TSource, TDestination>(
        this PaginatedResponse<TSource> source,
        Func<TSource, TDestination> mapFunc)
    {
        return new PaginatedResponse<TDestination>
        {
            Items = source.Items.Select(mapFunc).ToList(),
            Page = source.Page,
            PageSize = source.PageSize,
            TotalCount = source.TotalCount
        };
    }

    public static ErrorOr<PaginatedResponse<TDestination>> ToContractPaginatedList<TSource, TDestination>(
        this ErrorOr<PaginatedResponse<TSource>> result,
        Func<TSource, TDestination> mapFunc)
    {
        if (result.IsError)
        {
            return result.Errors;
        }

        var mapped = result.Value.MapItems(mapFunc);
        return mapped;
    }

}
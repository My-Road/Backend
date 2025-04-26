namespace MyRoad.API.Common;

public class RetrievalRequest
{
    private const string DefaultSort = "Id";
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;
    public string Filters { get; set; } = string.Empty;

    public string Sorts { get; set; } = DefaultSort;

    public int Page { get; set; } = DefaultPage;

    public int PageSize { get; set; } = DefaultPageSize;
}
namespace MyRoad.Domain.Common;

public class NotFoundException(string id) : Exception
{
    public string Id { get; set; } = id;
}
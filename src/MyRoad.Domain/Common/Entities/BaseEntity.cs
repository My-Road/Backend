namespace MyRoad.Domain.Common.Entities;

public abstract class BaseEntity<TId> where TId : notnull
{
    public TId Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
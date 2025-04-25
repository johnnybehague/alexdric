namespace Alexdric.Application.Queries;

/// <summary>
/// GetById Query
/// </summary>
public record BaseGetByIdQuery
{
    /// <summary>
    /// Id of the entity to get
    /// </summary>
    public int Id { get; set; }
}

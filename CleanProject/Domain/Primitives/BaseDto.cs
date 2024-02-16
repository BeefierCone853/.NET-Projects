namespace Domain.Primitives;

/// <summary>
/// Base record for all DTOs.
/// </summary>
/// <param name="Id">The unique identifier of an entity.</param>
public abstract record BaseDto(int Id);
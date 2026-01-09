namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object for upserting a person.
/// </summary>
public class PeopleDtoUpsert
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public string PersonName { get; private set; } = default!;

    /// <summary>
    /// The age of the person.
    /// </summary>
    public int Age { get; private set; }
}

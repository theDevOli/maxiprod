namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object for upserting a person.
/// </summary>
public class PersonDtoUpsert
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public string PersonName { get; set; } = default!;

    /// <summary>
    /// The age of the person.
    /// </summary>
    public int Age { get; set; }
}

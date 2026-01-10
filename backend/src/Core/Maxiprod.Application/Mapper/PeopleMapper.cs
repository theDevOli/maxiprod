using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.Mapper;

/// <summary>
/// Mapper class for Person entity and PersonDtoUpsert DTO.
/// </summary>
public static class PersonMapper
{
    /// <summary>
    /// Converts PersonDtoUpsert DTO to Person entity.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the person name and age.
    /// </param>
    /// <returns>
    /// A new Person entity with the specified values.
    /// </returns>
    public static Person ToEntity(this PersonDtoUpsert dto)
        => new Person(dto.PersonName, dto.Age);

    /// <summary>
    /// Converts PersonDtoUpsert DTO to Person entity with a specified ID.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the person name and age.
    /// </param>
    /// <param name="personId">
    /// The ID of the person to be created.
    /// </param>
    /// <returns>
    /// A new Person entity with the specified ID.
    /// </returns>
    public static Person ToEntity(this PersonDtoUpsert dto, int personId)
        => new Person(personId, dto.PersonName, dto.Age);

    /// <summary>
    /// Converts Person entity to PersonDtoUpsert DTO.
    /// </summary>
    /// <param name="entity">
    /// The Person entity to be converted.
    /// </param>
    /// <returns>
    /// A new PersonDtoUpsert DTO with the specified values.
    /// </returns>
    public static PersonDtoUpsert ToDto(this Person entity)
    => new PersonDtoUpsert
    {
        PersonName = entity.PersonName,
        Age = entity.Age
    };
}

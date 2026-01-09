using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.Mapper;

/// <summary>
/// Mapper class for People entity and PeopleDtoUpsert DTO.
/// </summary>
public static class PeopleMapper
{
    /// <summary>
    /// Converts PeopleDtoUpsert DTO to People entity.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static People ToEntity(this PeopleDtoUpsert dto)
        => new People(dto.PersonName, dto.Age);

    /// <summary>
    /// Converts PeopleDtoUpsert DTO to People entity with a specified ID.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the person name and age.
    /// </param>
    /// <param name="peopleId">
    /// The ID of the people to be created.
    /// </param>
    /// <returns>
    /// A new People entity with the specified ID.
    /// </returns>
    public static People ToEntity(this PeopleDtoUpsert dto, int peopleId)
        => new People(peopleId, dto.PersonName, dto.Age);

    /// <summary>
    /// Converts People entity to PeopleDtoUpsert DTO.
    /// </summary>
    /// <param name="entity">
    /// The People entity to be converted.
    /// </param>
    /// <returns>
    /// A new PeopleDtoUpsert DTO with the specified values.
    /// </returns>
    public static PeopleDtoUpsert ToDto(this People entity)
    => new PeopleDtoUpsert
    {
        PersonName = entity.PersonName,
        Age = entity.Age
    };
}

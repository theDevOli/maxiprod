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
/// Converts People entity to PeopleDtoUpsert DTO.
/// </summary>
/// <param name="entity"></param>
/// <returns></returns>
    public static PeopleDtoUpsert ToDto(this People entity)
    => new PeopleDtoUpsert
    {
        PersonName = entity.PersonName,
        Age = entity.Age
    };
}

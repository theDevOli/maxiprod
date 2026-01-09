using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.Mapper;

/// <summary>
/// Mapper class for Category entity and CategoryDtoUpsert.
/// </summary>
public static class CategoryMapper
{
    /// <summary>
    /// Converts a CategoryDtoUpsert to a Category entity.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static Category ToEntity(this CategoryDtoUpsert dto)
    => new Category(dto.CategoryDescription, dto.CategoryGoal);

    /// <summary>
    /// Converts a Category entity to a CategoryDtoUpsert.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static CategoryDtoUpsert ToDto(this Category entity)
    => new CategoryDtoUpsert
    {
        CategoryDescription = entity.CategoryDescription,
        CategoryGoal = entity.CategoryGoal
    };

}

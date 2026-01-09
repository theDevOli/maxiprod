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
    /// <param name="dto">
    /// The DTO containing the category description and goal.
    /// </param>
    /// <returns>
    /// A new Category entity with the specified values.
    /// </returns>

    public static Category ToEntity(this CategoryDtoUpsert dto)
    => new Category(dto.CategoryDescription, dto.CategoryGoal);

    /// <summary>
    /// Converts a CategoryDtoUpsert to a Category entity with a specified ID.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the category description and goal.
    /// </param>
    /// <param name="categoryId">
    /// The ID of the category to be created.
    /// </param>
    /// <returns>
    /// A new Category entity with the specified ID.
    /// </returns>
    public static Category ToEntity(this CategoryDtoUpsert dto, int categoryId)
    => new Category(categoryId, dto.CategoryDescription, dto.CategoryGoal);

    /// <summary>
    /// Converts a Category entity to a CategoryDtoUpsert.
    /// </summary>
    /// <param name="entity">
    /// 
    /// </param>
    /// <returns>
    /// A new CategoryDtoUpsert DTO with the specified values.
    /// </returns>
    public static CategoryDtoUpsert ToDto(this Category entity)
    => new CategoryDtoUpsert
    {
        CategoryDescription = entity.CategoryDescription,
        CategoryGoal = entity.CategoryGoal
    };

}

using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.CategoryService;

/// <summary>
/// Service to update a category
/// </summary>
/// <param name="categoryRepository">
/// The repository used to update a category
/// </param>
public class CategoryUpdatableService(ICategoryRepository categoryRepository) : ICategoryUpdatableService
{
    /// <summary>
    /// Updates a category
    /// </summary>
    /// <param name="categoryId">
    /// The ID of the category to update
    /// </param>
    /// <param name="dto">
    /// The DTO containing the updated category information
    /// </param>
    /// <returns>
    /// True if the category was updated successfully, false otherwise.
    /// </returns>
    public async Task<bool> UpdateCategoryAsync(int categoryId, CategoryDtoUpsert dto)
    {
        var category = dto.ToEntity(categoryId);

        var isUpdated = await categoryRepository.UpdateCategoryAsync(category);

        return isUpdated;
    }
}

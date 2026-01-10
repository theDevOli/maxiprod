using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.CategoryService;

/// <summary>
/// Service to delete a category
/// </summary>
/// <param name="categoryRepository">
/// The repository used to delete a category
/// </param>
public class CategoryDeletionService(ICategoryRepository categoryRepository) : ICategoryDeletionService
{
    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="categoryId">
    /// The ID of the category to delete.
    /// </param>
    /// <returns>
    /// True if the category was deleted successfully, false otherwise.
    /// </returns>
    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var isDeleted = await categoryRepository.DeleteCategoryAsync(categoryId);

        return isDeleted;
    }
}

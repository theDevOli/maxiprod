using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.CategoryService;

/// <summary>
/// Service to get a category by its identifier
/// </summary>
/// <param name="categoryRepository">
/// The repository used to retrieve a category by its identifier
/// </param>
public class CategoryGetterByIdService(ICategoryRepository categoryRepository) : ICategoryGetterByIdService
{
    /// <summary>
    /// Gets a category by its identifier
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category to retrieve
    /// </param>
    /// <returns>
    /// The category if found, otherwise null
    /// </returns>
    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    => await categoryRepository.GetCategoryByIdAsync(categoryId);
}

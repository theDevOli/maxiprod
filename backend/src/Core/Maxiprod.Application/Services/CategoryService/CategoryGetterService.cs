using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.CategoryService;

/// <summary>
/// Service to get all categories
/// </summary>
/// <param name="categoryRepository">
/// Repository for category operations
/// </param>
public class CategoryGetterService(ICategoryRepository categoryRepository) : ICategoryGetterService
{
/// <summary>
/// Gets all categories
/// </summary>
/// <returns>
/// A list of all categories
/// </returns>
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    => await categoryRepository.GetAllCategoriesAsync();
}

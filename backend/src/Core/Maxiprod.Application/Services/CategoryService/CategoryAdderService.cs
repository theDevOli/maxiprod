using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.CategoryService;
/// <summary>
/// Service to add a new category
/// </summary>
/// <param name="categoryRepository">
/// The repository used to persist the category.
/// </param>
public class CategoryAdderService(ICategoryRepository categoryRepository) : ICategoryAdderService
{
    /// <summary>
    /// Adds a new category.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the category data to add.
    /// </param>
    /// <returns>The ID of the newly created category.</returns>
    public async Task<int> AddCategoryAsync(CategoryDtoUpsert dto)
    {
        var category = dto.ToEntity();

        var categoryId = await categoryRepository.CreateCategoryAsync(category);

        return categoryId;
    }
}

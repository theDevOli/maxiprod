using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for updating a category.
/// </summary>
public interface ICategoryUpdatableService
{
/// <summary>
/// Updates a category.
/// </summary>
/// <param name="categoryId">
/// The ID of the category to be updated.
/// </param>
/// <param name="categoryDto">
/// The DTO containing the updated category information.
/// </param>
/// <returns>
/// True if the category was successfully updated; otherwise, false.
/// </returns>
    public Task<bool> UpdateCategoryAsync(int categoryId, CategoryDtoUpsert categoryDto);
}

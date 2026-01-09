using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for adding a new category.
/// </summary>
public interface ICategoryAdderService
{
    /// <summary>
    /// Adds a new category based on the provided DTO.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the category information to add.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, returning the ID of the newly added category.
    /// </returns>
    public Task<int> AddCategoryAsync(CategoryDtoUpsert dto);
}

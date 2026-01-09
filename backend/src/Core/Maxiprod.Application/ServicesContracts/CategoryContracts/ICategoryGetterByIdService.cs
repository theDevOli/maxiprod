using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for getting a category by its ID.
/// </summary>
public interface ICategoryGetterByIdService
{
    /// <summary>
    /// Gets a category by its ID.
    /// </summary>
    /// <param name="categoryId">
    /// The ID of the category to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, returning the category if found, or null if not found.
    /// </returns>
public Task<Category?> GetCategoryByIdAsync(int categoryId);
}

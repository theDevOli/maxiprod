using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for updating a category.
/// </summary>
public interface ICategoryUpdatableService
{
    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="category">
    /// The category to be updated.
    /// </param>
    /// <returns>
    /// A boolean indicating whether the update was successful.
    /// </returns>
    public Task<bool> UpdateCategoryAsync(Category category);
}

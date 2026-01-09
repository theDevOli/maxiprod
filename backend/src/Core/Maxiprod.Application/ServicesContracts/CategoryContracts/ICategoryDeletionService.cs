namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for deleting a category.
/// </summary>
public interface ICategoryDeletionService
{
    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="categoryId">
    /// The ID of the category to delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, returning a boolean indicating whether the deletion was successful.
    /// </returns>
    public Task<bool> DeleteCategoryAsync(int categoryId);
}

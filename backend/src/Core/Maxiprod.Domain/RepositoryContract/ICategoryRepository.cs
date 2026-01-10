using Maxiprod.Domain.Entity;

namespace Maxiprod.Domain.RepositoryContract;
/// <summary>
/// Defines the contract for category repository operations.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves all categories from the data source.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Category>> GetAllCategoriesAsync();

    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public Task<Category?> GetCategoryByIdAsync(int categoryId);

    /// <summary>
    /// Creates a new category in the data source.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public Task<int> CreateCategoryAsync(Category category);

    /// <summary>
    /// Updates an existing category in the data source.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public Task<bool> UpdateCategoryAsync(Category category);

    /// <summary>
    /// Deletes a category by its unique identifier.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public Task<bool> DeleteCategoryAsync(int categoryId);

    /// <summary>
    /// Checks whether a category exists in the system by its ID.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category to check.</param>
    /// <returns>Returns true if the category exists; otherwise, false.</returns>
    public Task<bool> DoesCategoryExistsAsync(int categoryId);

    /// <summary>
    /// Checks whether a specific category exists in the system.
    /// </summary>
    /// <param name="category">The category object to check for existence.</param>
    /// <returns>Returns true if the category exists; otherwise, false.</returns>
    public Task<bool> DoesCategoryExistsAsync(Category category);

    /// <summary>
    /// Determines whether the given category is unique.
    /// </summary>
    /// <param name="category">The category object.</param>
    /// <returns>Returns true if the category is unique; otherwise, false.</returns>
    public Task<bool> IsCategoryUniqueAsync(Category category);
}

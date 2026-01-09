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
}

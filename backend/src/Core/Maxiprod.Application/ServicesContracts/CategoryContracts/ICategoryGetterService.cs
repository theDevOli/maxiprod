using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.CategoryContracts;

/// <summary>
/// Service contract for getting all categories.
/// </summary>
public interface ICategoryGetterService
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation, returning an enumerable of all categories.
    /// </returns>
    public Task<IEnumerable<Category>> GetAllCategoriesAsync();
}

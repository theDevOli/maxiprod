namespace Maxiprod.Domain.RepositoryContract;

/// <summary>
/// Repository contract for retrieving financial balance data from the database.
/// </summary>
/// <remarks>
/// This interface provides methods to get balances per person, per category, 
/// and overall totals. It is intended for read-only operations and used by 
/// the application layer for reporting purposes.
/// </remarks>
public interface IBalanceRepository
{
    /// <summary>
    /// Gets the balance information for all people.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the results to.</typeparam>
    /// <returns>A collection of balances per person.</returns>
    public Task<IEnumerable<T>> GetPeopleBalance<T>();

    /// <summary>
    /// Gets the balance information for all categories.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the results to.</typeparam>
    /// <returns>A collection of balances per category.</returns>
    public Task<IEnumerable<T>> GetCategoriesBalance<T>();

    /// <summary>
    /// Gets the overall total balance across all people and categories.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the result to.</typeparam>
    /// <returns>The total balance, or null if no data exists.</returns>
    public Task<T?> GetTotalBalance<T>();
}

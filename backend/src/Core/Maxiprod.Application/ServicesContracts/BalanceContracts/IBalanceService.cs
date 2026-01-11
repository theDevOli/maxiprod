using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.BalanceContracts;

/// <summary>
/// Service contract for retrieving aggregated financial balances.
/// </summary>
public interface IBalanceService
{
    /// <summary>
    /// Gets the aggregated balances for categories, people, and total statistics.
    /// </summary>
    /// <returns>
    /// A <see cref="BalanceListDto"/> containing all balances.
    /// </returns>
    public Task<BalanceListDto> GetBalanceAsync();
}

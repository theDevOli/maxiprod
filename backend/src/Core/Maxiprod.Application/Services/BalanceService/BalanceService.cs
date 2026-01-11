using Maxiprod.Application.DTO;
using Maxiprod.Application.ServicesContracts.BalanceContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.BalanceService;

/// <summary>
/// Service for retrieving aggregated financial balances.
/// </summary>
public class BalanceService(IBalanceRepository balanceRepository) : IBalanceService
{
    /// <summary>
    /// Gets balances for categories, people, and total statistics.
    /// </summary>
    /// <returns>A <see cref="BalanceListDto"/> with all balances.</returns>
    public async Task<BalanceListDto> GetBalanceAsync()
    {
        var category = balanceRepository.GetCategoriesBalance<BalanceDtoCategory>();
        var people = balanceRepository.GetPeopleBalance<BalanceDtoPerson>();
        var total = balanceRepository.GetTotalBalance<BalanceDto>();

        await Task.WhenAll(category, people, total);

        return new BalanceListDto()
        {
            Categories = category.Result,
            People = people.Result,
            TotalStatistic = total.Result!
        };
    }
}

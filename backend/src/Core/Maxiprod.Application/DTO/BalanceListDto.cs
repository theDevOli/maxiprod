namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object representing a comprehensive financial balance report.
/// </summary>
/// <remarks>
/// This DTO aggregates financial information for both categories and people,
/// including totals and overall statistics. It is used for read operations
/// and reporting purposes. No business logic or domain rules are included.
/// </remarks>
public class BalanceListDto
{
    /// <summary>
    /// Collection of category-specific balance information.
    /// </summary>
    public IEnumerable<BalanceDtoCategory> Categories { get; set; } = default!;

    /// <summary>
    /// Collection of person-specific balance information.
    /// </summary>
    public IEnumerable<BalanceDtoPerson> People { get; set; } = default!;

    /// <summary>
    /// Overall total statistics for income, expenses, and net balance.
    /// </summary>
    public BalanceDto TotalStatistic { get; set; } = default!;
}

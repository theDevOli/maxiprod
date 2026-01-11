namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object that represents a financial balance.
/// </summary>
/// <remarks>
/// This DTO is used as a read model to expose aggregated financial data,
/// such as total income, total expense, and the resulting balance.
/// It does not represent a domain entity and contains no business rules.
/// </remarks>
public class BalanceDto
{
    /// <summary>
    /// Total amount of income.
    /// </summary>
    public decimal Income { get; set; }

    /// <summary>
    /// Total amount of expense.
    /// </summary>
    public decimal Expense { get; set; }

    /// <summary>
    /// Net balance calculated as Income minus Expense.
    /// </summary>
    public decimal Balance { get; set; }
}

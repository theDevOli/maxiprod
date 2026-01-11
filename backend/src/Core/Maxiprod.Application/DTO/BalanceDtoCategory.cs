namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object representing the financial balance of a specific category.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BalanceDto"/> to include income, expense, and net balance.
/// Contains the category description for identification.
/// This DTO is intended for read operations and reporting; no business rules are applied.
/// </remarks>
public class BalanceDtoCategory : BalanceDto
{
    /// <summary>
    /// The descriptive name of the category.
    /// </summary>
    public string CategoryDescription { get; set; } = default!;
}

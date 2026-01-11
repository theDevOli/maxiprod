namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object representing the financial balance of a specific person.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BalanceDto"/> to include income, expense, and net balance.
/// Contains the person's name for identification.
/// This DTO is intended for read operations and reporting; no business rules are applied.
/// </remarks>
public class BalanceDtoPerson : BalanceDto
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public string PersonName { get; set; } = default!;
}

using Maxiprod.Domain.Enum;

namespace Maxiprod.Application.DTO;

/// <summary>
/// Data Transfer Object for upserting a transaction.
/// </summary>
public class TransactionDtoUpsert
{

    /// <summary>
    /// The description of the transaction.
    /// This value is required and cannot be null or empty.
    /// </summary>
    public string TransactionDescription { get; set; } = default!;

    /// <summary>
    /// The amount of the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of the transaction ('despesa' or 'receita').
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// The identifier of the category linked with this transaction.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// The identifier of the person who owns this transaction.
    /// </summary>
    public int PeopleId { get; set; }
}

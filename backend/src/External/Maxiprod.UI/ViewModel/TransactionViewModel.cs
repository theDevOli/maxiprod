using System.ComponentModel.DataAnnotations;
using Maxiprod.Domain.Entity;

namespace Maxiprod.UI.ViewModel;

public class TransactionViewModel
{

    /// <summary>
    /// The unique identifier of the transaction.
    /// </summary>
    [Required]
    public int TransactionId { get; set; }

    /// <summary>
    /// The description of the transaction.
    /// This value is required and cannot be null or empty.
    /// </summary>
    [Required]
    public string TransactionDescription { get; set; } = default!;

    /// <summary>
    /// The amount of the transaction.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of the transaction ('despesa' or 'receita').
    /// </summary>
    [Required]
    public string TransactionType { get; set; } = default!;

    /// <summary>
    /// The identifier of the category linked with this transaction.
    /// </summary>
    [Required]
    public int CategoryId { get; set; }

    /// <summary>
    /// The identifier of the person who owns this transaction.
    /// </summary>
    [Required]
    public int PersonId { get; set; }

    /// <summary>
    /// Convert a Transaction entity to TransactionViewModel
    /// </summary>
    /// <param name="transaction">
    /// Transaction Entity
    /// </param>
    /// <returns>
    /// A Transaction ModelView
    /// </returns>
    public static TransactionViewModel? FromTransaction(Transaction? transaction)
    {
        if (transaction is null) return null;

        return new TransactionViewModel()
        {
            TransactionId = transaction.TransactionId,
            TransactionDescription = transaction.TransactionDescription,
            Amount = transaction.Amount,
            TransactionType = transaction.TransactionType.ToString(),
            CategoryId = transaction.CategoryId,
            PersonId = transaction.PersonId
        };
    }

    /// <summary>
    /// Convert a Collection of Transaction to a Collection of TransactionViewModel
    /// </summary>
    /// <param name="transactions">
    /// List of Transaction Entity.
    /// </param>
    /// <returns>
    /// List of TransactionModelView
    /// </returns>
    public static IEnumerable<TransactionViewModel> FromTransactions(IEnumerable<Transaction> transactions)
    {
        foreach (var transaction in transactions)
        {
            if (transaction is null) continue;

            yield return FromTransaction(transaction)!;
        }
    }
}

using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for getting transactions.
/// </summary>
public interface ITransactionGetterService
{
    /// <summary>
    /// Gets all transactions asynchronously.
    /// </summary>
    /// <returns>
    /// A collection of all transactions.
    /// </returns>
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
}

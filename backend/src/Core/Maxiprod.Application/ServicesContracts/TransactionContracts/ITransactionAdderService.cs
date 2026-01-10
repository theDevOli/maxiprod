using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for adding transaction.
/// </summary>
public interface ITransactionAdderService
{

    /// <summary>
    /// Adds a new transaction asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to add.
    /// </param>
    /// <returns>
    /// The ID of the newly added transaction.
    /// </returns>
    public Task<int> AddTransactionAsync(Transaction transaction);
}

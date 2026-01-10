using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for updating transactions.
/// </summary>
public interface ITransactionUpdatableService
{
    /// <summary>
    /// Updates an existing transaction asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to update.
    /// </param>
    /// <returns>
    /// True if the transaction was updated successfully, otherwise false.
    /// </returns>
    public Task<bool> UpdateTransactionAsync(Transaction transaction);
}

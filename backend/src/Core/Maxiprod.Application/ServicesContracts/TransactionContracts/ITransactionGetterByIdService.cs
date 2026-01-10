using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for getting transaction by ID.
/// </summary>
public interface ITransactionGetterByIdService
{
    /// <summary>
    /// Gets a transaction by its ID asynchronously.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to retrieve.
    /// </param>
    /// <returns>
    /// The transaction if found, otherwise null.
    /// </returns>
    public Task<Transaction?> GetTransactionByIdAsync(int transactionId);
}

using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for updating transactions.
/// </summary>
public interface ITransactionUpdatableService
{

    /// <summary>
    /// Updates an existing transaction.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to update.
    /// </param>
    /// <param name="dto">
    /// The DTO containing the updated transaction data.
    /// </param>
    /// <returns>
    /// True if the transaction was updated successfully, otherwise false.
    /// </returns>
    public Task<bool> UpdateTransactionAsync(int transactionId, TransactionDtoUpsert dto);
}

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for deleting transaction.
/// </summary>
public interface ITransactionDeletionService
{
    /// <summary>
    /// Deletes a transaction asynchronously.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to delete.
    /// </param>
    /// <returns>
    /// True if the transaction was deleted successfully, false otherwise.
    /// </returns>
    public Task<bool> DeleteTransactionAsync(int transactionId);
}

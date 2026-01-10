using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionContract;

/// <summary>
/// Service responsible for deleting transactions.
/// </summary>
/// <param name="transactionRepository">
/// The repository used to delete transactions.
/// </param>
public class TransactionDeletionService(ITransactionRepository transactionRepository) : ITransactionDeletionService
{
    /// <summary>
    /// Deletes a transaction by its ID.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to delete.
    /// </param>
    /// <returns>
    /// A boolean indicating whether the transaction was successfully deleted.
    /// </returns>
    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        var isDeleted = await transactionRepository.DeleteTransactionAsync(transactionId);

        return isDeleted;
    }
}

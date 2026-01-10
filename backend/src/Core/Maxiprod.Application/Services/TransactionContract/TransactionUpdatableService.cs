

using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionContract;

/// <summary>
/// Service for updating transactions.
/// </summary>
/// <param name="transactionRepository">
/// The repository for managing transaction data.
/// </param>
public class TransactionUpdatableService(ITransactionRepository transactionRepository) : ITransactionUpdatableService
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
    public async Task<bool> UpdateTransactionAsync(int transactionId, TransactionDtoUpsert dto)
    {
        var exists = await transactionRepository.DoesTransactionExistsAsync(dto.ToEntity(transactionId));

        if (!exists)
            return false;

        var transaction = dto.ToEntity(transactionId);

        var isUpdated = await transactionRepository.UpdateTransactionAsync(transaction);

        return isUpdated;
    }
}

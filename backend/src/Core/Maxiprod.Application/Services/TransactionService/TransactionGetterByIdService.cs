

using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionService;

/// <summary>
/// Service to get a transaction by its identifier
/// </summary>
/// <param name="transactionRepository">
/// The repository used to retrieve transactions.
/// </param>
public class TransactionGetterByIdService(ITransactionRepository transactionRepository) : ITransactionGetterByIdService
{
    /// <summary>
    /// Gets a transaction by its identifier.
    /// </summary>
    /// <param name="transactionId">
    /// The identifier of the transaction to retrieve.
    /// </param>
    /// <returns>
    /// The transaction if found, otherwise null.
    /// </returns>
    public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
    => await transactionRepository.GetTransactionByIdAsync(transactionId);
}

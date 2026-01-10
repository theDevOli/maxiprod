

using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionContract;

/// <summary>
/// Service to get all transactions
/// </summary>
/// <param name="transactionRepository">
/// Repository for transaction operations
/// </param>
public class TransactionGetterService(ITransactionRepository transactionRepository) : ITransactionGetterService
{
    
/// <summary>
/// Gets all transactions.
/// </summary>
/// <returns>
/// A list of all transactions.
/// </returns>
    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    => await transactionRepository.GetAllTransactionsAsync();
}

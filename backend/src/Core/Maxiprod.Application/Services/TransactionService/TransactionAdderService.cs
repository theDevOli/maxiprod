using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionService;

/// <summary>
/// Service for adding transactions.
/// </summary>
public class TransactionAdderService(ITransactionRepository transactionRepository) : ITransactionAdderService
{
    /// <summary>
    /// Adds a new transaction.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the transaction data.
    /// </param>
    /// <returns>
    /// The ID of the newly added transaction.
    /// </returns>
    public async Task<int> AddTransactionAsync(TransactionDtoUpsert dto)
    {
        var transaction = dto.ToEntity();

        var isUnique = await transactionRepository.IsTransactionUniqueAsync(transaction);
        
        if (!isUnique)
            return -1;

        var transactionId = await transactionRepository.CreateTransactionAsync(transaction);

        return transactionId;
    }
}

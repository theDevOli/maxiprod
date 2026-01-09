using System.Transactions;

namespace Maxiprod.Domain.RepositoryContract;

/// <summary>
/// Defines the contract for transaction repository operations.
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Retrieves all transactions from the data source.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

    /// <summary>
    /// Retrieves a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    public Task<Transaction?> GetTransactionByIdAsync(int transactionId);

    /// <summary>
    /// Creates a new transaction in the data source.
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public Task<int> CreateTransactionAsync(Transaction transaction);

    /// <summary>
    /// Updates an existing transaction in the data source.
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public Task<bool> UpdateTransactionAsync(Transaction transaction);

    /// <summary>
    /// Deletes a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    public Task<bool> DeleteTransactionAsync(int transactionId);
}



using Maxiprod.Domain.Entity;

namespace Maxiprod.Domain.RepositoryContract;

/// <summary>
/// Defines the contract for transaction repository operations.
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Retrieves all transactions from the data source.
    /// </summary>
    /// <returns>
    /// A collection of all transactions.
    /// </returns>
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

    /// <summary>
    /// Retrieves a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction to retrieve.
    /// </param>
    /// <returns>
    /// The transaction if found; otherwise, null.
    /// </returns>
    public Task<Transaction?> GetTransactionByIdAsync(int transactionId);

    /// <summary>
    /// Checks if a transaction exists in the data source.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to check.
    /// </param>
    /// <returns>
    /// True if the transaction exists; otherwise, false.
    /// </returns>
    public Task<bool> DoesTransactionExistsAsync(Transaction transaction);
    /// <summary>
    /// Checks if a transaction exists in the data source by its ID.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to check.
    /// </param>
    /// <returns>
    /// True if the transaction exists; otherwise, false.
    /// </returns>
    public Task<bool> DoesTransactionExistsAsync(int transactionId);

    /// <summary>
    /// Checks if a transaction is unique in the data source, based on
    /// TransactionDescription, Amount, TransactionType, CategoryId, and PersonId.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to check.
    /// </param>
    /// <returns>
    /// True if the transaction is unique; otherwise, false.
    /// </returns>
    public Task<bool> IsTransactionUniqueAsync(Transaction transaction);

    /// <summary>
    /// Creates a new transaction in the data source.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to create.
    /// </param>
    /// <returns>
    /// The ID of the newly created transaction.
    /// </returns>
    public Task<int> CreateTransactionAsync(Transaction transaction);

    /// <summary>
    /// Updates an existing transaction in the data source.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to update.
    /// </param>
    /// <returns>
    /// True if the transaction was successfully updated; otherwise, false.
    /// </returns>
    public Task<bool> UpdateTransactionAsync(Transaction transaction);

    /// <summary>
    /// Deletes a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction to delete.
    /// </param>
    /// <returns>
    /// True if the transaction was successfully deleted; otherwise, false.
    /// </returns>
    public Task<bool> DeleteTransactionAsync(int transactionId);
}

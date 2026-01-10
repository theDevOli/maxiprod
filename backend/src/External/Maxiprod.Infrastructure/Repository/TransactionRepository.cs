using System.Data;
using Dapper;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Infrastructure.DbContext;

namespace Maxiprod.Infrastructure.Repository;

/// <summary>
/// Implements the transaction repository for managing transactions in the data source.
/// </summary>
/// <param name="dapper">
/// The data context for database operations.
/// </param>
public class TransactionRepository(DataContext dapper) : ITransactionRepository
{
    #region SQL Queries
    /// <summary>
    /// SQL query to get all transactions.
    /// </summary>
    private readonly string _getAllTransactionsQuery =
    $"""
        SELECT
            transaction_id AS {nameof(Transaction.TransactionId)},
            transaction_description AS {nameof(Transaction.TransactionDescription)},
            amount AS {nameof(Transaction.Amount)},
            transaction_type AS {nameof(Transaction.TransactionType)},
            category_id AS {nameof(Transaction.CategoryId)},
            person_id AS {nameof(Transaction.PersonId)}
        FROM
            transaction;
    """;

    /// <summary>
    /// SQL query to get a transaction by its ID.
    /// </summary>
    private readonly string _getTransactionByTransactionIdQuery =
    $"""
        SELECT
            transaction_id AS {nameof(Transaction.TransactionId)},
            transaction_description AS {nameof(Transaction.TransactionDescription)},
            amount AS {nameof(Transaction.Amount)},
            transaction_type AS {nameof(Transaction.TransactionType)},
            category_id AS {nameof(Transaction.CategoryId)},
            person_id AS {nameof(Transaction.PersonId)}
        FROM
            transaction
        WHERE
            transaction_id = @{nameof(Transaction.TransactionId)};
    """;

    /// <summary>
    /// SQL query to get a unique transaction based on its properties.
    /// </summary>
    private readonly string _getUniqueTransactionQuery =
    $"""
        SELECT
            transaction_id AS {nameof(Transaction.TransactionId)},
            transaction_description AS {nameof(Transaction.TransactionDescription)},
            amount AS {nameof(Transaction.Amount)},
            transaction_type AS {nameof(Transaction.TransactionType)},
            category_id AS {nameof(Transaction.CategoryId)},
            person_id AS {nameof(Transaction.PersonId)}
        FROM
            transaction
        WHERE
            transaction_description = @{nameof(Transaction.TransactionDescription)} AND
            amount = @{nameof(Transaction.Amount)} AND
            transaction_type = @{nameof(Transaction.TransactionType)} AND
            category_id = @{nameof(Transaction.CategoryId)} AND
            person_id = @{nameof(Transaction.PersonId)};
    """;

    /// <summary>
    /// SQL query to create a new transaction.
    /// </summary>
    private readonly string _createTransactionQuery =
    $"""
        INSERT INTO transaction
        (
            transaction_description,
            amount,
            transaction_type,
            category_id,
            person_id
        )
        VALUES
        (
            @{nameof(Transaction.TransactionDescription)},
            @{nameof(Transaction.Amount)},
            @{nameof(Transaction.TransactionType)},
            @{nameof(Transaction.CategoryId)},
            @{nameof(Transaction.PersonId)}
        )
        RETURNING transaction_id;
    """;

    /// <summary>
    /// SQL query to update an existing transaction.
    /// </summary>
    private readonly string _updateTransactionQuery =
    $"""
        UPDATE transaction
        SET
            transaction_description = @{nameof(Transaction.TransactionDescription)},
            amount = @{nameof(Transaction.Amount)},
            transaction_type = @{nameof(Transaction.TransactionType)},
            category_id = @{nameof(Transaction.CategoryId)},
            person_id = @{nameof(Transaction.PersonId)}
        WHERE
            transaction_id = @{nameof(Transaction.TransactionId)};
    """;

    /// <summary>
    /// SQL query to delete a transaction by its ID.
    /// </summary>
    private readonly string _deleteTransactionQuery =
    $"""
        DELETE FROM transaction
        WHERE
            transaction_id = @{nameof(Transaction.TransactionId)};
    """;
    #endregion

    /// <summary>
    /// Creates a new transaction asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to create.
    /// </param>
    /// <returns>
    /// The ID of the created transaction.
    /// </returns>
    public async Task<int> CreateTransactionAsync(Transaction transaction)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Transaction.TransactionDescription), transaction.TransactionDescription, DbType.String);
        parameters.Add(nameof(Transaction.Amount), transaction.Amount, DbType.Decimal);
        parameters.Add(nameof(Transaction.TransactionType), transaction.TransactionType.ToString(), DbType.String);
        parameters.Add(nameof(Transaction.CategoryId), transaction.CategoryId, DbType.Int32);
        parameters.Add(nameof(Transaction.PersonId), transaction.PersonId, DbType.Int32);

        return await dapper.ExecuteScalarSqlAsync<int>(_createTransactionQuery, parameters);
    }

    /// <summary>
    /// Deletes a transaction asynchronously.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction to delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public Task<bool> DeleteTransactionAsync(int transactionId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Transaction.TransactionId), transactionId, DbType.Int32);

        return dapper.ExecuteSqlAsync(_deleteTransactionQuery, parameters);
    }

    /// <summary>
    /// Checks if a transaction exists asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to check.
    /// </param>
    /// <returns>
    /// True if the transaction exists; otherwise, false.
    /// </returns>
    public async Task<bool> DoesTransactionExistsAsync(Transaction transaction)
    => await GetTransactionByIdAsync(transaction.TransactionId) is not null;

    public async Task<bool> DoesTransactionExistsAsync(int transactionId)
    => await GetTransactionByIdAsync(transactionId) is not null;

    /// <summary>
    /// Gets all transactions asynchronously.
    /// </summary>
    /// <returns>
    /// A list of all transactions.
    /// </returns>
    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    => await dapper.LoadDataAsync<Transaction>(_getAllTransactionsQuery);

    /// <summary>
    /// Gets a transaction by its ID asynchronously.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction to retrieve.
    /// </param>
    /// <returns>
    /// The transaction if found; otherwise, null.
    /// </returns>
    public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Transaction.TransactionId), transactionId, DbType.Int32);

        return await dapper.LoadDataSingleAsync<Transaction>(_getTransactionByTransactionIdQuery, parameters);
    }

    /// <summary>
    /// Checks if a transaction is unique asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to check for uniqueness.
    /// </param>
    /// <returns>
    /// True if the transaction is unique; otherwise, false.
    /// </returns>
    public async Task<bool> IsTransactionUniqueAsync(Transaction transaction)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Transaction.TransactionDescription), transaction.TransactionDescription, DbType.String);
        parameters.Add(nameof(Transaction.Amount), transaction.Amount, DbType.Decimal);
        parameters.Add(nameof(Transaction.TransactionType), transaction.TransactionType.ToString(), DbType.String);
        parameters.Add(nameof(Transaction.CategoryId), transaction.CategoryId, DbType.Int32);
        parameters.Add(nameof(Transaction.PersonId), transaction.PersonId, DbType.Int32);

        return await dapper.LoadDataSingleAsync<Transaction>(_getUniqueTransactionQuery, parameters) is null;
    }

    /// <summary>
    /// Updates an existing transaction asynchronously.
    /// </summary>
    /// <param name="transaction">
    /// The transaction to update.
    /// </param>
    /// <returns>
    /// True if the transaction was updated successfully, otherwise false.
    /// </returns>
    public async Task<bool> UpdateTransactionAsync(Transaction transaction)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Transaction.TransactionId), transaction.TransactionId, DbType.Int32);
        parameters.Add(nameof(Transaction.TransactionDescription), transaction.TransactionDescription, DbType.String);
        parameters.Add(nameof(Transaction.Amount), transaction.Amount, DbType.Decimal);
        parameters.Add(nameof(Transaction.TransactionType), transaction.TransactionType.ToString(), DbType.String);
        parameters.Add(nameof(Transaction.CategoryId), transaction.CategoryId, DbType.Int32);
        parameters.Add(nameof(Transaction.PersonId), transaction.PersonId, DbType.Int32);

        return await dapper.ExecuteSqlAsync(_updateTransactionQuery, parameters);
    }
}

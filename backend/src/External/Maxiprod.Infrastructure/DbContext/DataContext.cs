using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Maxiprod.Infrastructure.DbContext;

/// <summary>
/// DataContext class for managing database connections and operations.
/// </summary>
public class DataContext
{
    /// <summary>
    /// Configuration instance for accessing configuration settings.
    /// </summary>
    private readonly IConfiguration? _config;

    /// <summary>
    /// Creates and returns a new database connection.
    /// </summary>
    /// <returns>
    /// A new database connection.
    /// </returns>
    private IDbConnection CreateConnection()
    {

        var connectionString = _config?.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }

    /// <summary>
    /// Initializes a new instance of the DataContext class (Dapper purse only).
    /// </summary>
    public DataContext() { }

    /// <summary>
    /// Initializes a new instance of the DataContext class with the specified configuration.
    /// </summary>
    /// <param name="config">
    /// The configuration instance used to retrieve connection strings.
    /// </param>
    public DataContext(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Loads data from the database based on the provided SQL query and parameters.
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to return.
    /// </typeparam>
    /// <param name="sql">
    /// The SQL query to execute.
    /// </param>
    /// <param name="parameters">
    /// The parameters to pass to the SQL query.
    /// </param>
    /// <param name="transaction">
    /// The transaction to use for the query.
    /// </param>
    /// <returns>
    /// A collection of objects of type T.
    /// </returns>
    public async Task<IEnumerable<T>> LoadDataAsync<T>(string sql, DynamicParameters? parameters = null, IDbTransaction? transaction = null)
    {
        using var dbConnection = CreateConnection();
        dbConnection.Open();
        return await dbConnection.QueryAsync<T>(sql, parameters, transaction);
    }

    /// <summary>
    /// Loads a single record from the database based on the provided SQL query and parameters.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to return.
    /// </typeparam>
    /// <param name="sql">
    /// The SQL query to execute.
    /// </param>
    /// <param name="parameters">
    /// The parameters to pass to the SQL query.
    /// </param>
    /// <param name="transaction">
    /// The transaction to use for the query.
    /// </param>
    /// <returns>
    /// A single object of type T, or null if no record is found.
    /// </returns>
    public async Task<T?> LoadDataSingleAsync<T>(string sql, DynamicParameters? parameters = null, IDbTransaction? transaction = null)
    {
        using var dbConnection = CreateConnection();
        dbConnection.Open();

        return await dbConnection.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction);
    }

    /// <summary>
    /// Executes a SQL command that does not return a result set.
    /// </summary>
    /// <param name="sql">SQL query to execute.</param>
    /// <param name="parameters">Query parameters (optional).</param>
    /// <param name="transaction">Transaction to use (optional).</param>
    /// <returns>True if any rows were affected; otherwise, false.</returns>

    public async Task<bool> ExecuteSqlAsync(string sql, DynamicParameters? parameters = null, IDbTransaction? transaction = null)
    {
        using var dbConnection = CreateConnection();
        dbConnection.Open();

        return await dbConnection.ExecuteAsync(sql, parameters, transaction) > 0;
    }

    /// <summary>
    /// Executes a SQL query that returns a single scalar value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the scalar value to return.
    /// </typeparam>
    /// <param name="sql">
    /// The SQL query to execute.
    /// </param>
    /// <param name="parameters">
    /// The parameters to pass to the SQL query.
    /// </param>
    /// <param name="transaction">
    /// The transaction to use for the query.
    /// </param>
    /// <returns>
    /// A single scalar value of type T, or null if no value is returned.
    /// </returns>
    public async Task<T?> ExecuteScalarSqlAsync<T>(string sql, DynamicParameters? parameters = null, IDbTransaction? transaction = null)
    {
        using var dbConnection = CreateConnection();
        dbConnection.Open();

        return await dbConnection.ExecuteScalarAsync<T>(sql, parameters, transaction);
    }

    /// <summary>
    /// Executes a SQL query that returns the number of affected rows.
    /// </summary>
    /// <param name="sql">
    /// The SQL query to execute.
    /// </param>
    /// <param name="parameters">
    /// The parameters to pass to the SQL query.
    /// </param>
    /// <param name="transaction">
    /// The transaction to use for the query.
    /// </param>
    /// <returns>
    /// The number of affected rows.
    /// </returns>
    public async Task<int> ExecuteSqlWithRowCountAsync(string sql, DynamicParameters? parameters = null, IDbTransaction? transaction = null)
    {
        using var dbConnection = CreateConnection();
        dbConnection.Open();

        return await dbConnection.ExecuteAsync(sql, parameters, transaction);
    }
}

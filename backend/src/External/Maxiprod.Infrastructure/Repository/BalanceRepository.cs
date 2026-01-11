using System.Data;
using Dapper;
using Maxiprod.Application.DTO;
using Maxiprod.Domain.ObjectValues;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Infrastructure.DbContext;

namespace Maxiprod.Infrastructure.Repository;

/// <summary>
/// Repository for retrieving financial balances by categories, people, and total statistics.
/// Uses Dapper and SQL queries to aggregate transaction data.
/// </summary>
public class BalanceRepository(DataContext dapper) : IBalanceRepository
{
    #region SQL Commands

    /// <summary>
    /// SQL query to calculate balances for each category.
    /// </summary>
    private readonly string _getCategoriesBalanceQuery =
        $"""
        SELECT
            c.category_description AS {nameof(BalanceDtoCategory.CategoryDescription)},

            SUM(CASE WHEN t.transaction_type = @income THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoCategory.Income)},
            SUM(CASE WHEN t.transaction_type = @expense THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoCategory.Expense)},
            SUM(CASE WHEN t.transaction_type = @income THEN t.amount ELSE 0 END)
            - 
            SUM(CASE WHEN t.transaction_type = @expense THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoCategory.Balance)}

        FROM 
            category AS c
        LEFT JOIN 
            transaction AS t 
            ON t.category_id = c.category_id
        GROUP BY 
            c.category_description
        ORDER BY 
            c.category_description;
        """;

    /// <summary>
    /// SQL query to calculate balances for each person.
    /// </summary>
    public readonly string _getPeopleBalanceQuery =
        $"""
        SELECT
            p.person_name AS {nameof(BalanceDtoPerson.PersonName)},

            SUM(CASE WHEN t.transaction_type = @income THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoPerson.Income)},
            SUM(CASE WHEN t.transaction_type = @expense THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoPerson.Expense)},
            SUM(CASE WHEN t.transaction_type = @income THEN t.amount ELSE 0 END)
            - 
            SUM(CASE WHEN t.transaction_type = @expense THEN t.amount ELSE 0 END) AS {nameof(BalanceDtoPerson.Balance)}

        FROM person AS p
        LEFT JOIN transaction AS t ON t.person_id = p.person_id
        GROUP BY p.person_name
        ORDER BY p.person_name;
        """;

    /// <summary>
    /// SQL query to calculate the total balance across all transactions.
    /// </summary>
    public readonly string _GetTotalBalanceQuery =
        $"""
        SELECT
            SUM(CASE WHEN transaction_type = @income THEN amount ELSE 0 END) AS {nameof(BalanceDto.Income)},
            SUM(CASE WHEN transaction_type = @expense THEN amount ELSE 0 END) AS {nameof(BalanceDto.Expense)},
            SUM(CASE WHEN transaction_type = @income THEN amount ELSE 0 END)
            - 
            SUM(CASE WHEN transaction_type = @expense THEN amount ELSE 0 END) AS {nameof(BalanceDto.Balance)}
        FROM transaction;
        """;

    #endregion

    /// <summary>
    /// Retrieves the balance for each category.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the results to.</typeparam>
    /// <returns>A collection of category balances.</returns>
    public async Task<IEnumerable<T>> GetCategoriesBalance<T>()
    {
        var parameters = new DynamicParameters();
        parameters.Add("income", CategoryGoal.receita.ToString(), DbType.String);
        parameters.Add("expense", CategoryGoal.despesa.ToString(), DbType.String);

        return await dapper.LoadDataAsync<T>(_getCategoriesBalanceQuery, parameters);
    }

    /// <summary>
    /// Retrieves the balance for each person.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the results to.</typeparam>
    /// <returns>A collection of people balances.</returns>
    public async Task<IEnumerable<T>> GetPeopleBalance<T>()
    {
        var parameters = new DynamicParameters();
        parameters.Add("income", CategoryGoal.receita.ToString(), DbType.String);
        parameters.Add("expense", CategoryGoal.despesa.ToString(), DbType.String);

        return await dapper.LoadDataAsync<T>(_getPeopleBalanceQuery, parameters);
    }

    /// <summary>
    /// Retrieves the total balance across all categories and people.
    /// </summary>
    /// <typeparam name="T">The DTO type to map the result to.</typeparam>
    /// <returns>The total balance.</returns>
    public async Task<T?> GetTotalBalance<T>()
    {
        var parameters = new DynamicParameters();
        parameters.Add("income", CategoryGoal.receita.ToString(), DbType.String);
        parameters.Add("expense", CategoryGoal.despesa.ToString(), DbType.String);

        return await dapper.LoadDataSingleAsync<T>(_GetTotalBalanceQuery, parameters);
    }
}

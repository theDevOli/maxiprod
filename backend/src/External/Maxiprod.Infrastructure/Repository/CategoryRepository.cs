using System.Data;
using Dapper;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Infrastructure.DbContext;

namespace Maxiprod.Infrastructure.Repository;

/// <summary>
/// Repository responsible for managing <see cref="Category"/> persistence.
/// Implements data access logic using Dapper.
/// </summary>
/// <remarks>
/// This repository communicates directly with the database and should not contain business logic.
/// </remarks>
public class CategoryRepository(DataContext dapper) : ICategoryRepository
{
    #region SQL Queries

    /// <summary>
    /// SQL query used to retrieve all categories ordered by goal and description.
    /// </summary>
    private readonly string _getAllCategoriesQuery =
    $"""
    SELECT
        category_id AS {nameof(Category.CategoryId)},
        category_description AS {nameof(Category.CategoryDescription)},
        goal AS {nameof(Category.CategoryGoal)}
    FROM
        category
    ORDER BY
        goal, category_description;
    """;

    /// <summary>
    /// SQL query used to retrieve a category by its unique identifier.
    /// </summary>
    private readonly string _getCategoryByCategoryIdQuery =
    $"""
    SELECT
        category_id AS {nameof(Category.CategoryId)},
        category_description AS {nameof(Category.CategoryDescription)},
        goal AS {nameof(Category.CategoryGoal)}
    FROM
        category
    WHERE
        category_id = @{nameof(Category.CategoryId)};
    """;

    /// <summary>
    /// SQL query used to verify category uniqueness based on description and goal.
    /// </summary>
    private readonly string _getUniqueCategoryIdQuery =
    $"""
    SELECT
        category_id AS {nameof(Category.CategoryId)},
        category_description AS {nameof(Category.CategoryDescription)},
        goal AS {nameof(Category.CategoryGoal)}
    FROM
        category
    WHERE
        category_description = @{nameof(Category.CategoryDescription)} AND
        goal = @{nameof(Category.CategoryGoal)};
    """;

    /// <summary>
    /// SQL query used to insert a new category and return its generated identifier.
    /// </summary>
    private readonly string _createCategoryQuery =
    $"""
    INSERT INTO category 
        (category_description, goal) 
    VALUES 
        (
            @{nameof(Category.CategoryDescription)},
            @{nameof(Category.CategoryGoal)}
        )
    RETURNING category_id;
    """;

    /// <summary>
    /// SQL query used to update an existing category.
    /// </summary>
    private readonly string _updateCategoryQuery =
    $"""
    UPDATE category 
    SET
        category_description = @{nameof(Category.CategoryDescription)},
        goal = @{nameof(Category.CategoryGoal)}
    WHERE
        category_id = @{nameof(Category.CategoryId)};
    """;

    /// <summary>
    /// SQL query used to delete a category by its identifier.
    /// </summary>
    private readonly string _deleteCategoryQuery =
    $"""
    DELETE FROM category
    WHERE
        category_id = @{nameof(Category.CategoryId)};
    """;

    #endregion

    /// <summary>
    /// Creates a new category in the database.
    /// </summary>
    /// <param name="category">
    /// The <see cref="Category"/> entity to be persisted.
    /// </param>
    /// <returns>
    /// The identifier of the newly created category.
    /// </returns>
    public async Task<int> CreateCategoryAsync(Category category)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Category.CategoryDescription), category.CategoryDescription, DbType.String);
        parameters.Add(nameof(Category.CategoryGoal), category.CategoryGoal.ToString(), DbType.String);

        return await dapper.ExecuteScalarSqlAsync<int>(_createCategoryQuery, parameters);
    }

    /// <summary>
    /// Deletes a category from the database.
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category to delete.
    /// </param>
    /// <returns>
    /// <c>true</c> if the category was deleted; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Category.CategoryId), categoryId, DbType.Int32);

        return await dapper.ExecuteSqlAsync(_deleteCategoryQuery, parameters);
    }

    /// <summary>
    /// Checks whether a category exists by its identifier.
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category.
    /// </param>
    /// <returns>
    /// <c>true</c> if the category exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DoesCategoryExistsAsync(int categoryId)
        => await GetCategoryByIdAsync(categoryId) is not null;

    /// <summary>
    /// Checks whether a category exists based on its identifier.
    /// </summary>
    /// <param name="category">
    /// The category entity to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if the category exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DoesCategoryExistsAsync(Category category)
        => await GetCategoryByIdAsync(category.CategoryId) is not null;

    /// <summary>
    /// Retrieves all categories from the database.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Category"/> entities.
    /// </returns>
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        => await dapper.LoadDataAsync<Category>(_getAllCategoriesQuery);

    /// <summary>
    /// Retrieves a category by its identifier.
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category.
    /// </param>
    /// <returns>
    /// A <see cref="Category"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Category.CategoryId), categoryId, DbType.Int32);

        return await dapper.LoadDataSingleAsync<Category>(_getCategoryByCategoryIdQuery, parameters);
    }

    /// <summary>
    /// Determines whether a category is unique based on description and goal.
    /// </summary>
    /// <param name="category">
    /// The category to validate.
    /// </param>
    /// <returns>
    /// <c>true</c> if no category with the same description and goal exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> IsCategoryUniqueAsync(Category category)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Category.CategoryDescription), category.CategoryDescription, DbType.String);
        parameters.Add(nameof(Category.CategoryGoal), category.CategoryGoal.ToString(), DbType.String);

        return await dapper.LoadDataSingleAsync<Category>(_getUniqueCategoryIdQuery, parameters) is null;
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="category">
    /// The category entity containing updated data.
    /// </param>
    /// <returns>
    /// <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Category.CategoryDescription), category.CategoryDescription, DbType.String);
        parameters.Add(nameof(Category.CategoryGoal), category.CategoryGoal.ToString(), DbType.String);
        parameters.Add(nameof(Category.CategoryId), category.CategoryId, DbType.Int32);

        return await dapper.ExecuteSqlAsync(_updateCategoryQuery, parameters);
    }
}

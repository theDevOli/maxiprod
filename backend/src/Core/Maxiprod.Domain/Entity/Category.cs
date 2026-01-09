using Maxiprod.Domain.Enum;

namespace Maxiprod.Domain.Entity;

/// <summary>
/// Represents a category used to classify financial transactions.
/// A category defines the allowed purpose of a transaction
/// ('despesa','receita' or 'ambas).
/// </summary>
public class Category
{
    /// <summary>
    /// The unique identifier of the category.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// The category description.
    /// This field is required and cannot be null or empty.
    /// </summary>
    public string CategoryDescription { get; private set; } = default!;

    /// <summary>
    /// The category goal, defining whether it can be used for
    /// expenses, income, or both.
    /// </summary>
    public CategoryGoal CategoryGoal { get; private set; }

    /// <summary>
    /// Private constructor required by Dapper.
    /// </summary>
    private Category() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// This constructor enforces the domain invariants.
    /// </summary>
    /// <param name="categoryDescription">
    /// The textual description of the category.
    /// Must not be null, empty, or whitespace.
    /// </param>
    /// <param name="categoryGoal">
    /// The goal of the category ('despesa','receita' or 'ambas).
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="categoryDescription"/> is null or empty.
    /// </exception>
    public Category(string categoryDescription, CategoryGoal categoryGoal)
    {
        ChangeDescription(categoryDescription);
        CategoryGoal = categoryGoal;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class with an identifier.
    /// </summary>
    /// <param name="categoryId">
    /// The unique identifier of the category
    /// </param>
    /// <param name="categoryDescription">
    /// The description of the category.
    /// </param>
    /// <param name="categoryGoal">
    /// The goal of the category ('despesa','receita' or 'ambas).
    /// </param>
    public Category(int categoryId, string categoryDescription, CategoryGoal categoryGoal)
    {
        CategoryId = categoryId;
        ChangeDescription(categoryDescription);
        CategoryGoal = categoryGoal;
    }

    /// <summary>
    /// Changes the description of the category.
    /// </summary>
    /// <param name="categoryDescription">
    /// The new description to assign to the category.
    /// Must not be null, empty, or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="categoryDescription"/> is invalid.
    /// </exception>
    public void ChangeDescription(string categoryDescription)
    {
        if (string.IsNullOrWhiteSpace(categoryDescription))
            throw new ArgumentException("Category name cannot be null or empty");

        CategoryDescription = categoryDescription;
    }
}

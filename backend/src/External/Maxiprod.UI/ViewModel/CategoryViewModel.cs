using System.ComponentModel.DataAnnotations;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.ObjectValues;

namespace Maxiprod.UI.ViewModel;

/// <summary>
/// View model used to expose category data to the UI layer.
/// Represents a simplified and validated version of the Category domain entity.
/// </summary>
public class CategoryViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    [Required(ErrorMessage = "CategoryId is required")]
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the category description.
    /// This field is required and cannot be null or empty.
    /// </summary>
    [Required(ErrorMessage = "CategoryDescription is required")]
    public string CategoryDescription { get; set; } = default!;

    /// <summary>
    /// Gets or sets the category goal.
    /// Indicates whether the category is intended for expenses, income, or both.
    /// </summary>
    [Required(ErrorMessage = "CategoryGoal is required")]
    [EnumDataType(typeof(CategoryGoal), ErrorMessage = $"Invalid category goal!")]
    public string CategoryGoal { get; set; } = default!;

    /// <summary>
    /// Creates a <see cref="CategoryViewModel"/> from a domain <see cref="Category"/> entity.
    /// </summary>
    /// <param name="category">The category domain entity.</param>
    /// <returns>A populated <see cref="CategoryViewModel"/> instance.</returns>
    public static CategoryViewModel? FromCategory(Category? category)
    {
        if (category is null) return null;

        return new CategoryViewModel
        {
            CategoryId = category.CategoryId,
            CategoryDescription = category.CategoryDescription,
            CategoryGoal = category.CategoryGoal.ToString()
        };
    }

    /// <summary>
    /// Converts a collection of <see cref="Category"/> domain entities
    /// into a collection of <see cref="CategoryViewModel"/>.
    /// </summary>
    /// <param name="categories">The collection of category domain entities.</param>
    /// <returns>An enumerable collection of <see cref="CategoryViewModel"/>.</returns>
    public static IEnumerable<CategoryViewModel> FromCategories(IEnumerable<Category> categories)
    {
        foreach (var category in categories)
        {
            if (category is null) continue;

            yield return FromCategory(category)!;
        }
    }
}

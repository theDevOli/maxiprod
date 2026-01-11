using System.ComponentModel.DataAnnotations;
using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.ObjectValues;

namespace Maxiprod.UI.ViewModel;

/// <summary>
/// ViewModel used for creating or updating a category.
/// Encapsulates validation rules and conversion logic to domain and DTO models.
/// </summary>
public class CategoryUpsertViewModel
{
    /// <summary>
    /// Gets or sets the category description.
    /// This value represents the human-readable name of the category.
    /// </summary>
    /// <remarks>
    /// This field is mandatory and cannot be null or empty.
    /// </remarks>
    [Required(ErrorMessage = "CategoryDescription is required")]
    public string CategoryDescription { get; set; } = default!;

    /// <summary>
    /// Gets or sets the category goal.
    /// </summary>
    /// <remarks>
    /// Indicates the purpose of the category, such as expense or income.
    /// The value must match one of the defined <see cref="CategoryGoal"/> enum values.
    /// </remarks>
    [Required(ErrorMessage = "CategoryGoal is required")]
    public string CategoryGoal { get; set; } = default!;

    /// <summary>
    /// Converts the current <see cref="CategoryUpsertViewModel"/> into a domain <see cref="CategoryViewModel"/>.
    /// </summary>
    /// <param name="catalogId">
    /// The identifier of the catalog to which the category belongs.
    /// </param>
    /// <param name="viewModel">
    /// The view model containing category data.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="Category"/> populated with validated data.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <see cref="CategoryGoal"/> cannot be parsed into a valid enum value.
    /// </exception>
    public static CategoryViewModel ToCategoryViewModel(int catalogId, CategoryUpsertViewModel viewModel)
        => new CategoryViewModel()
        {
            CategoryId = catalogId,
            CategoryDescription = viewModel.CategoryDescription,
            CategoryGoal = viewModel.CategoryGoal
        };

    /// <summary>
    /// Converts the current <see cref="CategoryUpsertViewModel"/> into a <see cref="CategoryDtoUpsert"/>.
    /// </summary>
    /// <param name="viewModel">
    /// The view model containing category data.
    /// </param>
    /// <returns>
    /// A <see cref="CategoryDtoUpsert"/> instance ready for application layer processing.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <see cref="CategoryGoal"/> cannot be parsed into a valid enum value.
    /// </exception>
    public static CategoryDtoUpsert ToCategoryDtoUpsert(CategoryUpsertViewModel viewModel)
        => new CategoryDtoUpsert
        {
            CategoryDescription = viewModel.CategoryDescription,
            CategoryGoal = Enum.Parse<CategoryGoal>(viewModel.CategoryGoal)
        };
}

using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Maxiprod.UI.Controllers;

/// <summary>
/// Controller responsible for managing category resources.
/// Provides endpoints for creating, retrieving, updating, and deleting categories.
/// </summary>
/// <remarks>
/// This controller acts as an entry point for HTTP requests and delegates
/// business logic execution to the application services layer.
/// </remarks>
[Route("v1/api/[controller]")]
[ApiController]
public class CategoriesController
(
    ICategoryGetterService categoryGetterService,
    ICategoryGetterByIdService categoryGetterByIdService,
    ICategoryAdderService categoryAdderService,
    ICategoryUpdatableService categoryUpdatableService,
    ICategoryDeletionService categoryDeletionService
)
: ControllerBase
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of categories.
    /// </returns>
    /// <response code="200">Categories retrieved successfully.</response>
    [HttpGet("")]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var categories = await categoryGetterService.GetAllCategoriesAsync();
        return Ok(CategoryViewModel.FromCategories(categories));
    }

    /// <summary>
    /// Retrieves a category by its identifier.
    /// </summary>
    /// <param name="categoryId">
    /// The unique identifier of the category.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the category if found.
    /// </returns>
    /// <response code="200">Category found.</response>
    /// <response code="404">Category not found.</response>
    [HttpGet("{categoryId}", Name = "GetCategoryById")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
    {
        var category = await categoryGetterByIdService.GetCategoryByIdAsync(categoryId);

        if (category is null)
            return NotFound();

        return Ok(CategoryViewModel.FromCategory(category));
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="viewModel">
    /// The category data to be created.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the created category.
    /// </returns>
    /// <response code="201">Category created successfully.</response>
    /// <response code="409">A category with the same properties already exists.</response>
    /// <response code="400">Invalid request payload.</response>
    [HttpPost("")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryUpsertViewModel viewModel)
    {
        var dto = CategoryUpsertViewModel.ToCategoryDtoUpsert(viewModel);

        var categoryId = await categoryAdderService.AddCategoryAsync(dto);

        if (categoryId == -1)
            return Conflict();

        return CreatedAtRoute(
            "GetCategoryById",
            new { categoryId },
            CategoryUpsertViewModel.ToCategoryViewModel(categoryId, viewModel)
        );
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category to update.
    /// </param>
    /// <param name="viewModel">
    /// The updated category data.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation.
    /// </returns>
    /// <response code="204">Category updated successfully.</response>
    /// <response code="404">Category not found.</response>
    /// <response code="400">Invalid request payload.</response>
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategoryAsync(
        [FromRoute] int categoryId,
        [FromBody] CategoryUpsertViewModel viewModel)
    {
        var dto = CategoryUpsertViewModel.ToCategoryDtoUpsert(viewModel);

        var isUpdated = await categoryUpdatableService.UpdateCategoryAsync(categoryId, dto);

        if (!isUpdated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Deletes a category by its identifier.
    /// </summary>
    /// <param name="categoryId">
    /// The identifier of the category to delete.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation.
    /// </returns>
    /// <response code="204">Category deleted successfully.</response>
    /// <response code="404">Category not found.</response>
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int categoryId)
    {
        var isDeleted = await categoryDeletionService.DeleteCategoryAsync(categoryId);

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}

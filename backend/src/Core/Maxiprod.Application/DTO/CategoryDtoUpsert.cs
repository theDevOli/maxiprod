using Maxiprod.Domain.Enum;

namespace Maxiprod.Application.DTO;
/// <summary>
/// Data Transfer Object for upserting a category.
/// </summary>
public class CategoryDtoUpsert
{
    /// <summary>
    /// The description of the category.
    /// </summary>
    public string CategoryDescription { get; set; } = default!;

    /// <summary>
    /// The goal of the category ('despesa', 'receita', or 'ambos').
    /// </summary>
    public CategoryGoal CategoryGoal { get; set; }
}

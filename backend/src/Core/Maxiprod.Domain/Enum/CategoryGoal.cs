namespace Maxiprod.Domain.Enum;
/// <summary>
/// Defines the goals for a category.
/// </summary>
[Flags]
public enum CategoryGoal
{
    despesa = 0,
    receita = 1 << 0,
    ambas = 1 << 1
}

namespace Maxiprod.Domain.Enum;

[Flags]
public enum CategoryGoal
{
    despesa = 0,
    receita = 1 << 0,
    ambas = 1 << 1
}

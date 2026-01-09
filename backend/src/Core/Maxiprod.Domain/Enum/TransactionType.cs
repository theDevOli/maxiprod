namespace Maxiprod.Domain.Enum;

[Flags]
public enum TransactionType
{
    despesa = 0,
    receita = 1 << 1
}

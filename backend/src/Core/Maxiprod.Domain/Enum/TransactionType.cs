namespace Maxiprod.Domain.Enum;
/// <summary>
/// Defines the types of transactions.
/// </summary>
[Flags]
public enum TransactionType
{
    despesa = 0,
    receita = 1 << 1
}

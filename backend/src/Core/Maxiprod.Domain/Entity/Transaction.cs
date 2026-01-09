using Maxiprod.Domain.Enum;

namespace Maxiprod.Domain.Entity;

/// <summary>
/// Represents a financial transaction.
/// </summary>
public class Transaction
{
    /// <summary>
    /// The unique identifier of the transaction.
    /// </summary>
    public int TransactionId { get; private set; }

    /// <summary>
    /// The description of the transaction.
    /// This value is required and cannot be null or empty.
    /// </summary>
    public string TransactionDescription { get; private set; } = default!;

    /// <summary>
    /// The amount of the transaction.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// The type of the transaction ('despesa' or 'receita').
    /// </summary>
    public TransactionType TransactionType { get; private set; }

    /// <summary>
    /// The identifier of the category linked with this transaction.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// The identifier of the person who owns this transaction.
    /// </summary>
    public int PeopleId { get; private set; }

    /// <summary>
    /// Required by Dapper for object materialization.
    /// </summary>
    private Transaction() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class with the required data.
    /// </summary>
    /// <param name="transactionDescription">Description of the transaction.</param>
    /// <param name="amount">Monetary amount of the transaction.</param>
    /// <param name="transactionType">Type of the transaction (expense or revenue).</param>
    /// <param name="categoryId">Identifier of the related category.</param>
    /// <param name="peopleId">Identifier of the related person.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the transaction description is null or empty.
    /// </exception>
    public Transaction(
        string transactionDescription,
        decimal amount,
        TransactionType transactionType,
        int categoryId,
        int peopleId)
    {
        ChangeTransactionDescription(transactionDescription);
        Amount = amount;
        TransactionType = transactionType;
        CategoryId = categoryId;
        PeopleId = peopleId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class with an identifier.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction.
    /// </param>
    /// <param name="transactionDescription">
    /// Description of the transaction.
    /// </param>
    /// <param name="amount">
    /// The monetary amount of the transaction.
    /// </param>
    /// <param name="transactionType">
    /// The type of the transaction.
    /// </param>
    /// <param name="categoryId">
    /// The identifier of the category linked with this transaction.
    /// </param>
    /// <param name="peopleId">
    /// The identifier of the person who owns this transaction.
    /// </param>
    public Transaction(
        int transactionId,
        string transactionDescription,
        decimal amount,
        TransactionType transactionType,
        int categoryId,
        int peopleId)
    {
        TransactionId = transactionId;
        ChangeTransactionDescription(transactionDescription);
        Amount = amount;
        TransactionType = transactionType;
        CategoryId = categoryId;
        PeopleId = peopleId;
    }

    /// <summary>
    /// Changes the description of the transaction.
    /// </summary>
    /// <param name="transactionDescription"></param>
    /// <exception cref="ArgumentException"></exception>
    public void ChangeTransactionDescription(string transactionDescription)
    {
        if (string.IsNullOrWhiteSpace(transactionDescription))
            throw new ArgumentException("Transaction description cannot be null or empty.");

        TransactionDescription = transactionDescription;
    }
}

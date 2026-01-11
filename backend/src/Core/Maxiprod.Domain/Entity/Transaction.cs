using Maxiprod.Domain.ObjectValues;

namespace Maxiprod.Domain.Entity;

/// <summary>
/// Represents a financial transaction in the domain.
/// A transaction can be an expense or income and must obey
/// domain business rules.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Gets the unique identifier of the transaction.
    /// </summary>
    public int TransactionId { get; private set; }

    /// <summary>
    /// Gets the description of the transaction.
    /// This value is required and cannot be null or empty.
    /// </summary>
    public string TransactionDescription { get; private set; } = default!;

    /// <summary>
    /// Gets the monetary amount of the transaction.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// Gets the type of the transaction (expense or income).
    /// </summary>
    public TransactionType TransactionType { get; private set; }

    /// <summary>
    /// Gets the identifier of the category associated with this transaction.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// Gets the identifier of the person who owns this transaction.
    /// </summary>
    public int PersonId { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// Required by Dapper for object materialization.
    /// </summary>
    private Transaction() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class
    /// with the required data.
    /// </summary>
    /// <param name="transactionDescription">
    /// Description of the transaction.
    /// </param>
    /// <param name="amount">
    /// Monetary amount of the transaction.
    /// </param>
    /// <param name="transactionType">
    /// Type of the transaction (expense or income).
    /// </param>
    /// <param name="categoryId">
    /// Identifier of the related category.
    /// </param>
    /// <param name="personId">
    /// Identifier of the related person.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the description is null or empty,
    /// or when the amount or transaction type is invalid.
    /// </exception>
    public Transaction(
        string transactionDescription,
        decimal amount,
        TransactionType transactionType,
        int categoryId,
        int personId)
    {
        ChangeTransactionDescription(transactionDescription);
        ChangeAmount(amount);
        ChangeTransactionType(transactionType);
        CategoryId = categoryId;
        PersonId = personId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class
    /// with an existing identifier.
    /// </summary>
    /// <param name="transactionId">
    /// The unique identifier of the transaction.
    /// </param>
    /// <param name="transactionDescription">
    /// Description of the transaction.
    /// </param>
    /// <param name="amount">
    /// Monetary amount of the transaction.
    /// </param>
    /// <param name="transactionType">
    /// Type of the transaction.
    /// </param>
    /// <param name="categoryId">
    /// Identifier of the related category.
    /// </param>
    /// <param name="personId">
    /// Identifier of the related person.
    /// </param>
    public Transaction
    (
        int transactionId,
        string transactionDescription,
        decimal amount,
        TransactionType transactionType,
        int categoryId,
        int personId
    )
    {
        TransactionId = transactionId;
        ChangeTransactionDescription(transactionDescription);
        ChangeAmount(amount);
        ChangeTransactionType(transactionType);
        CategoryId = categoryId;
        PersonId = personId;
    }

    /// <summary>
    /// Changes the transaction description.
    /// </summary>
    /// <param name="transactionDescription">
    /// The new description to assign.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the description is null, empty, or whitespace.
    /// </exception>
    public void ChangeTransactionDescription(string transactionDescription)
    {
        if (string.IsNullOrWhiteSpace(transactionDescription))
            throw new ArgumentException("Transaction description cannot be null or empty.");

        TransactionDescription = transactionDescription;
    }

    /// <summary>
    /// Changes the transaction amount.
    /// </summary>
    /// <param name="amount">
    /// The new monetary amount.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the amount is negative.
    /// </exception>
    public void ChangeAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Transaction amount cannot be negative.");

        Amount = amount;
    }

    /// <summary>
    /// Changes the transaction type.
    /// </summary>
    /// <param name="transactionType">
    /// The new transaction type.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the transaction type is not a valid enum value.
    /// </exception>
    public void ChangeTransactionType(TransactionType transactionType)
    {
        if (!Enum.IsDefined(typeof(TransactionType), transactionType))
            throw new ArgumentException("Transaction type is invalid.");

        TransactionType = transactionType;
    }

    /// <summary>
    /// Creates a transaction while enforcing domain business rules.
    /// </summary>
    /// <param name="description">
    /// Description of the transaction.
    /// </param>
    /// <param name="amount">
    /// Monetary amount of the transaction.
    /// </param>
    /// <param name="transactionType">
    /// Type of the transaction.
    /// </param>
    /// <param name="person">
    /// The person associated with the transaction.
    /// </param>
    /// <param name="categoryId">
    /// Identifier of the category.
    /// </param>
    /// <returns>
    /// A valid <see cref="Transaction"/> instance.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a minor attempts to create an income transaction.
    /// </exception>
    public static Transaction CreateTransaction(
        string description,
        decimal amount,
        TransactionType transactionType,
        Person person,
        Category category)
    {
        if (person.Age < 18 && transactionType == TransactionType.receita)
            throw new ArgumentException("Only adults (Age >= 18) are allowed to insert income transactions.");

        if (category.CategoryGoal != CategoryGoal.ambas && category.CategoryGoal != (CategoryGoal)transactionType)
            throw new ArgumentException($"Category goal ({category.CategoryGoal}) differs from transaction type ({transactionType})");

        return new Transaction(description, amount, transactionType, category.CategoryId, person.PersonId);
    }
}

using System.ComponentModel.DataAnnotations;
using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.ObjectValues;

namespace Maxiprod.UI.ViewModel;

public class TransactionUpsertViewModel
{
    /// <summary>
    /// The description of the transaction.
    /// This value is required and cannot be null or empty.
    /// </summary>
    [Required(ErrorMessage = "Transaction description is required.")]
    public string TransactionDescription { get; set; } = default!;

    /// <summary>
    /// The amount of the transaction.
    /// </summary>
    [Required(ErrorMessage = "Amount is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of the transaction ('despesa' or 'receita').
    /// </summary>
    [Required(ErrorMessage = "Transaction type is required.")]
    public string TransactionType { get; set; } = default!;

    /// <summary>
    /// The identifier of the category linked with this transaction.
    /// </summary>
    [Required(ErrorMessage = "CategoryId is required.")]
    public int CategoryId { get; set; }

    /// <summary>
    /// The identifier of the person who owns this transaction.
    /// </summary>
    [Required(ErrorMessage = "PersonId is required.")]
    public int PersonId { get; set; }

    /// <summary>
    /// Convert a Transaction entity to TransactionViewModel
    /// </summary>
    /// <param name="transaction">
    /// Transaction Entity
    /// </param>
    /// <returns>
    /// A Transaction ModelView
    /// </returns>
    public static Transaction ToTransaction(TransactionUpsertViewModel viewModel)
    => new Transaction
        (
            viewModel.TransactionDescription,
            viewModel.Amount,
            Enum.Parse<TransactionType>(viewModel.TransactionType),
            viewModel.CategoryId,
            viewModel.PersonId
        );

    /// <summary>
    /// Converts a <see cref="TransactionUpsertViewModel"/> into a <see cref="TransactionDtoUpsert"/>.
    /// </summary>
    /// <param name="viewModel">The ViewModel object containing the transaction data provided by the user.</param>
    /// <returns>
    /// A <see cref="TransactionDtoUpsert"/> with data mapped from the ViewModel, 
    /// including conversion of the transaction type from string to <see cref="TransactionType"/>.
    /// </returns>
    public static TransactionDtoUpsert ToTransactionDto(TransactionUpsertViewModel viewModel)
        => new TransactionDtoUpsert()
        {
            TransactionDescription = viewModel.TransactionDescription,
            Amount = viewModel.Amount,
            TransactionType = Enum.Parse<TransactionType>(viewModel.TransactionType),
            CategoryId = viewModel.CategoryId,
            PersonId = viewModel.PersonId,
        };

    /// <summary>
    /// Converts a <see cref="TransactionUpsertViewModel"/> into a <see cref="Transaction"/> entity,
    /// assigning a specific <paramref name="transactionId"/>.
    /// </summary>
    /// <param name="transactionId">
    /// The ID of the transaction to be assigned to the entity.
    /// </param>
    /// <param name="viewModel">
    /// The ViewModel object containing the transaction data provided by the user.
    /// </param>
    /// <returns>
    /// An instance of <see cref="Transaction"/> containing the data from the ViewModel and the provided ID, 
    /// including conversion of the transaction type from string to <see cref="TransactionType"/>.
    /// </returns>
    public static Transaction ToTransaction(int transactionId, TransactionUpsertViewModel viewModel)
        => new Transaction
            (
                transactionId,
                viewModel.TransactionDescription,
                viewModel.Amount,
                Enum.Parse<TransactionType>(viewModel.TransactionType),
                viewModel.CategoryId,
                viewModel.PersonId
            );

}

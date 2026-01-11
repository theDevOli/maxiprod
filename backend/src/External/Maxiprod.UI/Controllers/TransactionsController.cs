using Maxiprod.Application.DTO;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxiprod.UI.Controllers;

/// <summary>
/// Provides endpoints for managing financial transactions.
/// Supports creating, retrieving, updating, and deleting transactions.
/// </summary>
[Route("v1/api/[controller]")]
[ApiController]
[AllowAnonymous]
public class TransactionsController
(
    ITransactionAdderService transactionAdderService,
    ITransactionDeletionService transactionDeletionService,
    ITransactionGetterByIdService transactionGetterByIdService,
    ITransactionGetterService transactionGetterService,
    ITransactionUpdatableService transactionUpdatableService
)
: ControllerBase
{
    /// <summary>
    /// Retrieves all transactions.
    /// </summary>
    /// <returns>
    /// Returns a list of all transactions.
    /// </returns>
    /// <response code="200">Transactions retrieved successfully.</response>
    [HttpGet("")]
    public async Task<IActionResult> GetTransactionsAsync()
    {
        var transactions = await transactionGetterService.GetAllTransactionsAsync();
        return Ok(TransactionViewModel.FromTransactions(transactions));
    }

    /// <summary>
    /// Retrieves a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId">The unique identifier of the transaction.</param>
    /// <returns>
    /// Returns the transaction if found.
    /// </returns>
    /// <response code="200">Transaction retrieved successfully.</response>
    /// <response code="404">Transaction not found.</response>
    [HttpGet("{transactionId}", Name = "GetTransactionById")]
    public async Task<IActionResult> GetTransactionByIdAsync([FromRoute] int transactionId)
    {
        var transaction = await transactionGetterByIdService.GetTransactionByIdAsync(transactionId);

        if (transaction is null)
            return NotFound();

        return Ok(TransactionViewModel.FromTransaction(transaction));
    }

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <param name="viewModel">The transaction data used to create a new transaction.</param>
    /// <returns>
    /// Returns the newly created transaction along with its location.
    /// </returns>
    /// <response code="201">Transaction created successfully.</response>
    /// <response code="409">A transaction with the same details already exists.</response>
    [HttpPost("")]
    public async Task<IActionResult> AddTransactionAsync([FromBody] TransactionUpsertViewModel viewModel)
    {
        var dto = TransactionUpsertViewModel.ToTransactionDto(viewModel);
        var transactionId = await transactionAdderService.AddTransactionAsync(dto);
        
        // FIXME: I would try to avoid unnecessary data copy on the db, but it's out of scope.
        // if (transactionId == -1)
        //     return Conflict("A transaction with the same details already exists.");

        return CreatedAtRoute(
            "GetTransactionById",
            new { transactionId },
            TransactionUpsertViewModel.ToTransaction(transactionId, viewModel)
        );
    }

    /// <summary>
    /// Updates an existing transaction.
    /// </summary>
    /// <param name="transactionId">The unique identifier of the transaction to update.</param>
    /// <param name="dto">The updated transaction data.</param>
    /// <returns>
    /// Returns no content if the update is successful.
    /// </returns>
    /// <response code="204">Transaction updated successfully.</response>
    /// <response code="404">Transaction not found.</response>
    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransactionAsync(
        [FromRoute] int transactionId,
        [FromBody] TransactionDtoUpsert dto)
    {
        var isUpdated = await transactionUpdatableService.UpdateTransactionAsync(transactionId, dto);

        if (!isUpdated)
            return NotFound($"Transaction with ID {transactionId} not found.");

        return NoContent();
    }

    /// <summary>
    /// Deletes an existing transaction.
    /// </summary>
    /// <param name="transactionId">The unique identifier of the transaction to delete.</param>
    /// <returns>
    /// Returns no content if the deletion is successful.
    /// </returns>
    /// <response code="204">Transaction deleted successfully.</response>
    /// <response code="404">Transaction not found.</response>
    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransactionAsync([FromRoute] int transactionId)
    {
        var isDeleted = await transactionDeletionService.DeleteTransactionAsync(transactionId);

        if (!isDeleted)
            return NotFound($"Transaction with ID {transactionId} not found.");

        return NoContent();
    }
}

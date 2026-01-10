using Maxiprod.Application.DTO;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxiprod.UI.Controllers;

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
    [HttpGet("")]
    public async Task<IActionResult> GetTransactionsAsync()
    {
        var transactions = await transactionGetterService.GetAllTransactionsAsync();

        return Ok(TransactionViewModel.FromTransactions(transactions));
    }

    [HttpGet("{transactionId}", Name = "GetTransactionById")]
    public async Task<IActionResult> GetTransactionByIdAsync([FromRoute] int transactionId)
    {
        var transaction = await transactionGetterByIdService.GetTransactionByIdAsync(transactionId);

        if (transaction is null)
            return NotFound();

        return Ok(TransactionViewModel.FromTransaction(transaction));
    }

    [HttpPost("")]
    public async Task<IActionResult> AddTransactionAsync([FromBody] TransactionUpsertViewModel viewModel)
    {
        var dto = TransactionUpsertViewModel.ToTransactionDto(viewModel);
        var transactionId = await transactionAdderService.AddTransactionAsync(dto);

        if (transactionId == -1)
            return Conflict("A transaction with the same details already exists.");


        return CreatedAtRoute
            (
                "GetTransactionById",
                new { transactionId },
                TransactionUpsertViewModel.ToTransaction(transactionId, viewModel)
            );
    }

    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransactionAsync([FromRoute] int transactionId, [FromBody] TransactionDtoUpsert dto)
    {
        var isUpdated = await transactionUpdatableService.UpdateTransactionAsync(transactionId, dto);

        if (!isUpdated)
            return NotFound($"Transaction with ID {transactionId} not found.");

        return NoContent();
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransactionAsync([FromRoute] int transactionId)
    {
        var isDeleted = await transactionDeletionService.DeleteTransactionAsync(transactionId);
        if (!isDeleted)
            return NotFound($"Transaction with ID {transactionId} not found.");
        return NoContent();
    }
}
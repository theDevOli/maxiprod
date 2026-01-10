using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.TransactionContracts;

/// <summary>
/// Service contract for adding transaction.
/// </summary>
public interface ITransactionAdderService
{

    /// <summary>
    /// Adds a new transaction.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the transaction data.
    /// </param>
    /// <returns>
    /// The ID of the newly added transaction.
    /// </returns>
    public Task<int> AddTransactionAsync(TransactionDtoUpsert dto);
}

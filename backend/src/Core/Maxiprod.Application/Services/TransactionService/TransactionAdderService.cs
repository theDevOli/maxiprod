using Maxiprod.Application.DTO;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.TransactionService;

/// <summary>
/// Service for adding transactions.
/// </summary>
public class TransactionAdderService
(
    ITransactionRepository transactionRepository,
    IPersonRepository personRepository,
    ICategoryRepository categoryRepository
)
: ITransactionAdderService
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
    public async Task<int> AddTransactionAsync(TransactionDtoUpsert dto)
    {
        // FIXME: I would try to avoid unnecessary data copy on the db, but it's out of scope.
        // var isUnique = transactionRepository.IsTransactionUniqueAsync(tempTransaction);
        var personTask = personRepository.GetPersonByIdAsync(dto.PersonId);
        var categoryTask = categoryRepository.GetCategoryByIdAsync(dto.CategoryId);

        // await Task.WhenAll(isUnique, personTask, categoryTask);
        await Task.WhenAll( personTask, categoryTask);

        var category = categoryTask.Result;
        var person = personTask.Result;

        // if (!isUnique.Result)
        //     return -1;

        if (category is null)
            throw new ArgumentException($"There is no such category with ID: {dto.CategoryId} no the data base!");

        if (person is null)
            throw new ArgumentException($"There is no such person with ID: {dto.PersonId} no the data base!");
        var transaction = Transaction.CreateTransaction(dto.TransactionDescription, dto.Amount, dto.TransactionType, person, category);

        var transactionId = await transactionRepository.CreateTransactionAsync(transaction);

        return transactionId;
    }
}

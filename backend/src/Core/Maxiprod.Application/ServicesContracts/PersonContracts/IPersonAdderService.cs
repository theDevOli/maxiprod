using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service contract for adding person.
/// </summary>
public interface IPersonAdderService
{
/// <summary>
/// Adds a new person asynchronously.
/// </summary>
/// <param name="person">
/// The person to add.
/// </param>
/// <returns>
/// The ID of the newly added person.
/// </returns>
    public Task<int> AddPersonAsync(Person person);
}

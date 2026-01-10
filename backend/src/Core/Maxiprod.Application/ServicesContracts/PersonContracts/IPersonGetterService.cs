using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service interface for getting person.
/// </summary>
public interface IPersonGetterService
{
    /// <summary>
    /// Gets all people asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the list of all people.
    /// </returns>
    public Task<IEnumerable<Person>> GetAllPeopleAsync();
}

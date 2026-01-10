using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service interface for getting person.
/// </summary>
public interface IPersonGetterService
{
    /// <summary>
    /// Gets a person by ID asynchronously.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the person if found, otherwise null.
    /// </returns>
    public Task<Person?> GetPersonByIdAsync(int personId);
}

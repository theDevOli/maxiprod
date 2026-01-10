using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service contract for getting person by ID.
/// </summary>
public interface IPersonGetterByIdService
{
    /// <summary>
    /// Gets a person by their ID asynchronously.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to retrieve.
    /// </param>
    /// <returns>
    /// The person with the specified ID, or null if not found.
    /// </returns>
    public Task<Person?> GetPersonByIdAsync(int personId);
}

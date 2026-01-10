using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.PersonService;

/// <summary>
/// Service for getting a person by ID.
/// </summary>
/// <param name="personRepository">
/// The repository used to retrieve person data.
/// </param>
public class PersonGetterByIdService(IPersonRepository personRepository) : IPersonGetterByIdService
{
    /// <summary>
    /// Gets a person by their ID.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the person if found, otherwise null.
    /// </returns>
    public async Task<Person?> GetPersonByIdAsync(int personId)
    => await personRepository.GetPersonByIdAsync(personId);
}

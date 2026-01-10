using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.PersonService;

/// <summary>
/// Service for getting all people.
/// </summary>
/// <param name="personRepository">
/// The repository used to retrieve people.
/// </param>
public class PersonGetterService(IPersonRepository personRepository) : IPersonGetterService
{
    /// <summary>
    /// Gets all people asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the list of all people.
    /// </returns>
    public async Task<IEnumerable<Person>> GetAllPeopleAsync()
    => await personRepository.GetAllPeopleAsync();
}

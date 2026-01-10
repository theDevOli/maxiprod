using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;

namespace Maxiprod.Application.Services.PersonService;

/// <summary>
/// Service for adding a new person.
/// </summary>
/// <param name="personRepository">
/// The repository for managing person entities.
/// </param>
public class PersonAdderService(IPersonRepository personRepository) : IPersonAdderService
{
    /// <summary>
    /// Adds a new person.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the person's information.
    /// </param>
    /// <returns>
    /// The ID of the newly added person.
    /// </returns>
    public async Task<int> AddPersonAsync(PersonDtoUpsert dto)
    {
        var person = dto.ToEntity();
        var isAdded = await personRepository.CreatePersonAsync(person);

        return isAdded;
    }
}

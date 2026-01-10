using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.PersonService;
/// <summary>
/// Service for updating a person.
/// </summary>
/// <param name="personRepository">
/// The repository for managing person data.
/// </param>
public class PersonUpdatableService(IPersonRepository personRepository) : IPersonUpdatableService
{
    /// <summary>
    /// Updates a person's information asynchronously.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to update.
    /// </param>
    /// <param name="dto">
    /// The DTO containing the updated person information.
    /// </param>
    /// <returns>
    /// True if the person was successfully updated; otherwise, false.
    /// </returns>
    public async Task<bool> UpdatePersonAsync(int personId, PersonDtoUpsert dto)
    {
        var person = dto.ToEntity(personId);

        var isUpdated = await personRepository.UpdatePersonAsync(person);

        return isUpdated;
    }
}

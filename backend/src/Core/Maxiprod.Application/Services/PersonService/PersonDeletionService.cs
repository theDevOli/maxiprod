using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Domain.RepositoryContract;

namespace Maxiprod.Application.Services.PersonService;

/// <summary>
/// Service to handle person deletion operations.
/// </summary>
/// <param name="personRepository">
/// The repository used to delete a person.
/// </param>
public class PersonDeletionService(IPersonRepository personRepository) : IPersonDeletionService
{
    /// <summary>
    /// Deletes a person by their ID.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to delete.
    /// </param>
    /// <returns>
    /// True if the person was deleted successfully; otherwise, false.
    /// </returns>
    public async Task<bool> DeletePersonAsync(int personId)
    {
        var isDeleted = await personRepository.DeletePersonAsync(personId);

        return isDeleted;
    }
}

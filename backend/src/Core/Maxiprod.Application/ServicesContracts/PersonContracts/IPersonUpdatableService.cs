using Maxiprod.Application.DTO;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service interface for updating person.
/// </summary>
public interface IPersonUpdatableService
{
    /// <summary>
    /// Updates a person asynchronously.
    /// </summary>
    /// <param name="personId">
    /// The ID of the person to update.
    /// </param>
    /// <param name="dto">
    /// The DTO containing the updated person information.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the person was updated successfully, otherwise false.
    /// </returns>
    public Task<bool> UpdatePersonAsync(int personId, PersonDtoUpsert dto);
}

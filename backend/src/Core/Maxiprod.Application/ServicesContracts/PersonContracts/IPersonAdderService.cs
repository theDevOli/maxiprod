using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service contract for adding person.
/// </summary>
public interface IPersonAdderService
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
    public Task<int> AddPersonAsync(PersonDtoUpsert dto);
}

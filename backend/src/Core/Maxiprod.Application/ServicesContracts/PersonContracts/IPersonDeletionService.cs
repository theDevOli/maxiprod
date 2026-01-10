namespace Maxiprod.Application.ServicesContracts.PersonContracts;

/// <summary>
/// Service contract for deleting person.
/// </summary>
public interface IPersonDeletionService
{
/// <summary>
/// Deletes a person asynchronously.
/// </summary>
/// <param name="personId">
/// The ID of the person to delete.
/// </param>
/// <returns>
/// True if the person was deleted successfully, false otherwise.
/// </returns>
    public Task<bool> DeletePersonAsync(int personId);
}

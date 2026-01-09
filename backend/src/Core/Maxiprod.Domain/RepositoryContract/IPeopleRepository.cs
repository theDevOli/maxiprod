using Maxiprod.Domain.Entity;

namespace Maxiprod.Domain.RepositoryContract;
/// <summary>
/// Defines the contract for people repository operations.
/// </summary>
public interface IPeopleRepository
{
    /// <summary>
    /// Retrieves all people from the data source.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<People>> GetAllPeopleAsync();

    /// <summary>
    /// Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public Task<People?> GetPeopleByIdAsync(int personId);

    /// <summary>
    /// Creates a new person in the data source.
    /// </summary>
    /// <param name="people"></param>
    /// <returns></returns>
    public Task<int> CreatePeopleAsync(People people);

    /// <summary>
    /// Updates an existing person in the data source.
    /// </summary>
    /// <param name="people"></param>
    /// <returns></returns>
    public Task<bool> UpdatePeopleAsync(People people);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public Task<bool> DeletePeopleAsync(int personId);
}

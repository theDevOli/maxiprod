using Maxiprod.Domain.Entity;

namespace Maxiprod.Domain.RepositoryContract;
/// <summary>
/// Defines the contract for person repository operations.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Retrieves all person from the data source.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Person>> GetAllPersonAsync();

    /// <summary>
    /// Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the person if found, otherwise null.
    /// </returns>
    public Task<Person?> GetPersonByIdAsync(int personId);

    /// <summary>
    /// Creates a new person in the data source.
    /// </summary>
    /// <param name="person">
    /// The person to create.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the identifier of the created person.
    /// </returns>
    public Task<int> CreatePersonAsync(Person person);

    /// <summary>
    /// Updates an existing person in the data source.
    /// </summary>
    /// <param name="person">
    /// The person to update.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a boolean indicating whether the update was successful.
    /// </returns>
    public Task<bool> UpdatePersonAsync(Person person);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a boolean indicating whether the deletion was successful.
    /// </returns>
    public Task<bool> DeletePersonAsync(int personId);
}

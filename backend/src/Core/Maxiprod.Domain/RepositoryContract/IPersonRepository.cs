using Maxiprod.Domain.Entity;

namespace Maxiprod.Domain.RepositoryContract;

/// <summary>
/// Defines the contract for person repository operations.
/// Provides methods for creating, retrieving, updating, and deleting people,
/// as well as checking for existence.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Retrieves all people from the data source.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a collection of all people.
    /// </returns>
    Task<IEnumerable<Person>> GetAllPeopleAsync();

    /// <summary>
    /// Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the person if found; otherwise, <c>null</c>.
    /// </returns>
    Task<Person?> GetPersonByIdAsync(int? personId);

    /// <summary>
    /// Creates a new person in the data source.
    /// </summary>
    /// <param name="person">
    /// The person entity to create.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the unique identifier of the created person.
    /// </returns>
    Task<int> CreatePersonAsync(Person person);

    /// <summary>
    /// Updates an existing person in the data source.
    /// </summary>
    /// <param name="person">
    /// The person entity containing updated values.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains <c>true</c> if the update was successful;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdatePersonAsync(Person person);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains <c>true</c> if the deletion was successful;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<bool> DeletePersonAsync(int personId);

    /// <summary>
    /// Determines whether a person exists by their unique identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to check.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains <c>true</c> if the person exists;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<bool> DoesPersonExistAsync(int? personId);

    /// <summary>
    /// Determines whether a given person entity exists in the data source.
    /// </summary>
    /// <param name="person">
    /// The person entity to check.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains <c>true</c> if the person exists;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<bool> DoesPersonExistAsync(Person person);
}

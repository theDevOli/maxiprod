using System.Data;
using Dapper;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Infrastructure.DbContext;

namespace Maxiprod.Infrastructure.Repository;

/// <summary>
/// Repository responsible for managing <see cref="Person"/> persistence using Dapper.
/// Implements basic CRUD operations and database queries related to people.
/// </summary>
public class PersonRepository(DataContext dapper) : IPersonRepository
{
     #region SQL Commands

    /// <summary>
    /// SQL query to retrieve all people ordered by name.
    /// </summary>
    private readonly string _getAllPeopleQuery =
    $"""
    SELECT
        person_id AS {nameof(Person.PersonId)},
        person_name AS {nameof(Person.PersonName)},
        age AS {nameof(Person.Age)}
    FROM person
    ORDER BY person_name;
    """;

    /// <summary>
    /// SQL query to retrieve a person by its identifier.
    /// </summary>
    private readonly string _getPersonByPersonIdQuery =
    $"""
    SELECT
        person_id AS {nameof(Person.PersonId)},
        person_name AS {nameof(Person.PersonName)},
        age AS {nameof(Person.Age)}
    FROM person
    WHERE person_id = @{nameof(Person.PersonId)};
    """;

    /// <summary>
    /// SQL command to insert a new person and return its generated identifier.
    /// </summary>
    private readonly string _createPersonQuery =
    $"""
    INSERT INTO person (person_name, age)
    VALUES (@{nameof(Person.PersonName)}, @{nameof(Person.Age)})
    RETURNING person_id;
    """;

    /// <summary>
    /// SQL command to update an existing person's data.
    /// </summary>
    private readonly string _updatePersonQuery =
    $"""
    UPDATE person
    SET
        person_name = @{nameof(Person.PersonName)},
        age = @{nameof(Person.Age)}
    WHERE person_id = @{nameof(Person.PersonId)};
    """;

    /// <summary>
    /// SQL command to delete a person by identifier.
    /// </summary>
    private readonly string _deletePersonQuery =
    $"""
    DELETE FROM person
    WHERE person_id = @{nameof(Person.PersonId)};
    """;

    #endregion

    /// <summary>
    /// Creates a new person in the database.
    /// </summary>
    /// <param name="person">Person entity to be created.</param>
    /// <returns>The generated identifier of the new person.</returns>
    public async Task<int> CreatePersonAsync(Person person)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Person.PersonName), person.PersonName, DbType.String);
        parameters.Add(nameof(Person.Age), person.Age, DbType.Int32);

        return await dapper.ExecuteScalarSqlAsync<int>(_createPersonQuery, parameters);
    }

    /// <summary>
    /// Deletes a person from the database by its identifier.
    /// </summary>
    /// <param name="personId">Identifier of the person to be deleted.</param>
    /// <returns>
    /// <c>true</c> if the deletion was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DeletePersonAsync(int personId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Person.PersonId), personId, DbType.Int32);

        return await dapper.ExecuteSqlAsync(_deletePersonQuery, parameters);
    }

    /// <summary>
    /// Checks whether a person exists in the database using its identifier.
    /// </summary>
    /// <param name="personId">Identifier of the person.</param>
    /// <returns>
    /// <c>true</c> if the person exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DoesPersonExistAsync(int? personId)
        => await GetPersonByIdAsync(personId) is not null;

    /// <summary>
    /// Checks whether a person exists in the database using a <see cref="Person"/> entity.
    /// </summary>
    /// <param name="person">Person entity.</param>
    /// <returns>
    /// <c>true</c> if the person exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DoesPersonExistAsync(Person person)
        => await GetPersonByIdAsync(person.PersonId) is not null;

    /// <summary>
    /// Retrieves all people from the database.
    /// </summary>
    /// <returns>A collection of <see cref="Person"/>.</returns>
    public async Task<IEnumerable<Person>> GetAllPeopleAsync()
        => await dapper.LoadDataAsync<Person>(_getAllPeopleQuery);

    /// <summary>
    /// Retrieves a person by its identifier.
    /// </summary>
    /// <param name="personId">Identifier of the person.</param>
    /// <returns>
    /// The <see cref="Person"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Person?> GetPersonByIdAsync(int? personId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Person.PersonId), personId, DbType.Int32);

        return await dapper.LoadDataSingleAsync<Person>(_getPersonByPersonIdQuery, parameters);
    }

    /// <summary>
    /// Updates an existing person's data.
    /// </summary>
    /// <param name="person">Person entity containing updated values.</param>
    /// <returns>
    /// <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdatePersonAsync(Person person)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(Person.PersonName), person.PersonName, DbType.String);
        parameters.Add(nameof(Person.Age), person.Age, DbType.Int32);
        parameters.Add(nameof(Person.PersonId), person.PersonId, DbType.Int32);

        return await dapper.ExecuteSqlAsync(_updatePersonQuery, parameters);
    }
}

using Maxiprod.Application.DTO;
using Maxiprod.Application.Mapper;
using Maxiprod.Application.ServicesContracts.PersonContracts;
using Microsoft.AspNetCore.Mvc;

namespace Maxiprod.UI.Controllers;

/// <summary>
/// API controller responsible for managing people.
/// Provides endpoints for creating, retrieving, updating and deleting people.
/// </summary>
[Route("v1/api/[controller]")]
[ApiController]
public class PeopleController
(
    IPersonGetterService personGetterService,
    IPersonGetterByIdService personGetterByIdService,
    IPersonAdderService personAdderService,
    IPersonUpdatableService personUpdatableService,
    IPersonDeletionService personDeletionService
)
: ControllerBase
{
  /// <summary>
    /// Retrieves all registered people.
    /// </summary>
    /// <returns>
    /// A list of people.
    /// </returns>
    /// <response code="200">Returns the list of people.</response>
    [HttpGet("")]
    public async Task<IActionResult> GetAllPeopleAsync()
    {
        var people = await personGetterService.GetAllPeopleAsync();
        return Ok(people);
    }

    /// <summary>
    /// Retrieves a person by its identifier.
    /// </summary>
    /// <param name="personId">Identifier of the person.</param>
    /// <returns>
    /// The person if found.
    /// </returns>
    /// <response code="200">Returns the requested person.</response>
    /// <response code="404">Person not found.</response>
    [HttpGet("{personId}", Name = "GetPersonById")]
    public async Task<IActionResult> GetPersonByIdAsync([FromRoute] int personId)
    {
        var person = await personGetterByIdService.GetPersonByIdAsync(personId);

        if (person is null)
            return NotFound();

        return Ok(person);
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="dto">Data transfer object containing person data.</param>
    /// <returns>
    /// The created person with its generated identifier.
    /// </returns>
    /// <response code="201">Person successfully created.</response>
    /// <response code="400">Invalid input data.</response>
    [HttpPost("")]
    public async Task<IActionResult> CreatePersonAsync([FromBody] PersonDtoUpsert dto)
    {
        var personId = await personAdderService.AddPersonAsync(dto);

        return CreatedAtRoute(
            "GetPersonById",
            new { personId },
            dto.ToEntity(personId)
        );
    }

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="personId">Identifier of the person to update.</param>
    /// <param name="dto">Data transfer object containing updated data.</param>
    /// <returns>
    /// No content if the update is successful.
    /// </returns>
    /// <response code="204">Person successfully updated.</response>
    /// <response code="404">Person not found.</response>
    /// <response code="400">Invalid input data.</response>
    [HttpPut("{personId}")]
    public async Task<IActionResult> UpdatePersonIdAsync(
        [FromRoute] int personId,
        [FromBody] PersonDtoUpsert dto)
    {
        var isUpdated = await personUpdatableService.UpdatePersonAsync(personId, dto);

        if (!isUpdated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Deletes a person by its identifier.
    /// </summary>
    /// <param name="personId">Identifier of the person to delete.</param>
    /// <returns>
    /// No content if the deletion is successful.
    /// </returns>
    /// <response code="204">Person successfully deleted.</response>
    /// <response code="404">Person not found.</response>
    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePersonAsync([FromRoute] int personId)
    {
        var isDeleted = await personDeletionService.DeletePersonAsync(personId);

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}


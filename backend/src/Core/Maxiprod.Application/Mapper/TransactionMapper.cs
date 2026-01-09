using Maxiprod.Application.DTO;
using Maxiprod.Domain.Entity;

namespace Maxiprod.Application.Mapper;

/// <summary>
/// Mapper class for Transaction entity and TransactionDtoUpsert DTO.
/// </summary>
public static class TransactionMapper
{
    /// <summary>
    /// Converts TransactionDtoUpsert DTO to Transaction entity.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the transaction description, amount, type, category ID, and people ID.
    /// </param>
    /// <returns>
    /// A new Transaction entity with the specified values.
    /// </returns>
    public static Transaction ToEntity(this TransactionDtoUpsert dto)
        => new Transaction(dto.TransactionDescription, dto.Amount, dto.TransactionType, dto.CategoryId, dto.PeopleId);

    /// <summary>
    /// Converts TransactionDtoUpsert DTO to Transaction entity with a specified ID.
    /// </summary>
    /// <param name="dto">
    /// The DTO containing the transaction description, amount, type, category ID, and people ID.
    /// </param>
    /// <param name="transactionId">
    /// The ID of the transaction to be created.
    /// </param>
    /// <returns>
    /// A new Transaction entity with the specified values.
    /// </returns>
    public static Transaction ToEntity(this TransactionDtoUpsert dto, int transactionId)
        => new Transaction(dto.TransactionDescription, dto.Amount, dto.TransactionType, dto.CategoryId, dto.PeopleId);

    /// <summary>
    /// Converts Transaction entity to TransactionDtoUpsert DTO.
    /// </summary>
    /// <param name="entity">
    /// The Transaction entity to be converted.
    /// </param>
    /// <returns>
    /// A new TransactionDtoUpsert DTO with the specified values.
    /// </returns>
    public static TransactionDtoUpsert ToDto(this Transaction entity)
    => new TransactionDtoUpsert
    {
        TransactionDescription = entity.TransactionDescription,
        Amount = entity.Amount,
        TransactionType = entity.TransactionType,
        CategoryId = entity.CategoryId,
        PeopleId = entity.PeopleId
    };
}

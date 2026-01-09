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
    /// <param name="dto"></param>
    /// <returns></returns>
    public static Transaction ToEntity(this TransactionDtoUpsert dto)
        => new Transaction(dto.TransactionDescription, dto.Amount, dto.TransactionType, dto.CategoryId, dto.PeopleId);

/// <summary>
/// Converts Transaction entity to TransactionDtoUpsert DTO.
/// </summary>
/// <param name="entity"></param>
/// <returns></returns>
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

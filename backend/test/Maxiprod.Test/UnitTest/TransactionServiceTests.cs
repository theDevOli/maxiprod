using Maxiprod.Application.DTO;
using Maxiprod.Application.Services.TransactionService;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.ObjectValues;
using Maxiprod.Domain.RepositoryContract;
using Moq;
using Xunit.Sdk;

namespace Maxiprod.Test.UnitTest;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _repositoryMock;
    private readonly TransactionAdderService _adderService;
    private readonly TransactionDeletionService _deletionService;
    private readonly TransactionGetterByIdService _getterByIdService;
    private readonly TransactionGetterService _getterService;
    private readonly TransactionUpdatableService _updatableService;

    public TransactionServiceTests()
    {
        _repositoryMock = new Mock<ITransactionRepository>();

        _adderService = new TransactionAdderService(_repositoryMock.Object);
        _deletionService = new TransactionDeletionService(_repositoryMock.Object);
        _getterByIdService = new TransactionGetterByIdService(_repositoryMock.Object);
        _getterService = new TransactionGetterService(_repositoryMock.Object);
        _updatableService = new TransactionUpdatableService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldReturnTransactionId_WhenTransactionIsCreated()
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = 100,
            TransactionType = TransactionType.despesa,
            CategoryId = 1,
            PersonId = 1
        };

        _repositoryMock
            .Setup(repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(1);

        // Act
        var result = await _adderService.AddTransactionAsync(dto);

        // Assert
        Assert.Equal(1, result);

        _repositoryMock.Verify(
            repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()),
            Times.Once
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Once
        );
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldReturnMinusOne_WhenTransitionIsNotUnique()
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = 100,
            TransactionType = TransactionType.despesa,
            CategoryId = 1,
            PersonId = 1
        };

        _repositoryMock
            .Setup(repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(false);

        // Act
        var result = await _adderService.AddTransactionAsync(dto);

        //Assert
        Equals(-1, result);

        _repositoryMock.Verify(
            repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()),
            Times.Once
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );
    }
    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = 100,
            TransactionType = TransactionType.despesa,
            CategoryId = 1,
            PersonId = 1
        };

        _repositoryMock
            .Setup(repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _adderService.AddTransactionAsync(dto)
        );


        _repositoryMock.Verify(
            repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()),
            Times.Once
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Once
        );
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddTransactionAsync_ShouldThrowException_WhenTransactionDescriptionIsInvalid(string? transactionDescription)
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = transactionDescription!,
            Amount = 100,
            TransactionType = TransactionType.despesa,
            CategoryId = 1,
            PersonId = 1
        };

        // Act
        _repositoryMock
            .Setup(repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        //  Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddTransactionAsync(dto)
        );


        _repositoryMock.Verify(
            repo => repo.IsTransactionUniqueAsync(It.IsAny<Transaction>()),
            Times.Never
        );
        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenAmountIsInvalid()
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = -100,
            TransactionType = TransactionType.despesa,
            CategoryId = 1,
            PersonId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddTransactionAsync(dto)
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );

        _repositoryMock.Verify(
        repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
        Times.Never
);
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenTransactionTypeIsInvalid()
    {
        // Arrange
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = 100,
            TransactionType = (TransactionType)25,
            CategoryId = 1,
            PersonId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddTransactionAsync(dto)
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );

        _repositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldReturnTrue_WhenTransactionIsDeleted()
    {
        // Arrange
        int transactionId = 1;

        _repositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ReturnsAsync(true);

        // Act
        var result = await _deletionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteTransactionAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldReturnFalse_WhenTransactionDoesNotExist()
    {
        // Arrange
        int transactionId = 2;

        _repositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        _repositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _deletionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteTransactionAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        int transactionId = 3;

        _repositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _deletionService.DeleteTransactionAsync(transactionId));

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteTransactionAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldReturnTransaction_WhenTransactionExists()
    {
        // Arrange
        int transactionId = 1;
        var transaction = new Transaction(transactionId, "Sample Transaction", 100, TransactionType.despesa, 1, 1);

        _repositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync(transaction);

        // Act
        var result = await _getterByIdService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transactionId, result!.TransactionId);

        _repositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldReturnNull_WhenTransactionDoesNotExist()
    {
        // Arrange
        int transactionId = 2;
        _repositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync((Transaction?)null);

        // Act
        var result = await _getterByIdService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        int transactionId = 3;
        _repositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _getterByIdService.GetTransactionByIdAsync(transactionId));
        _repositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ShouldReturnTransactions_WhenTransactionsExist()
    {
        // Arrange
        var transactions = new List<Transaction>
            {
                new Transaction(1, "Sample Transaction 1", 100, TransactionType.despesa, 1, 1),
                new Transaction(2, "Sample Transaction 2", 200, TransactionType.receita, 1, 1)
            };

        _repositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ReturnsAsync(transactions);

        // Act
        var result = await _getterService.GetAllTransactionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, ((List<Transaction>)result).Count);
        _repositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ShouldReturnEmptyList_WhenNoTransactionsExist()
    {
        // Arrange
        var transactions = new List<Transaction>();
        _repositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ReturnsAsync(transactions);

        // Act
        var result = await _getterService.GetAllTransactionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _repositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _getterService.GetAllTransactionsAsync());
        _repositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ShouldReturnTrue_WhenTransactionIsUpdated()
    {
        // Arrange
        int transactionId = 1;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Updated transaction",
            Amount = 100,
            TransactionType = TransactionType.receita,
            CategoryId = 2,
            PersonId = 1
        };

        _repositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        // Act
        var result = await _updatableService.UpdateTransactionAsync(transactionId, dto);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ShouldReturnFalse_WhenTransactionIsNotUpdated()
    {
        // Arrange
        int transactionId = 2;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Updated transaction",
            Amount = 100,
            TransactionType = TransactionType.receita,
            CategoryId = 2,
            PersonId = 1
        };

        _repositoryMock
                    .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
                    .ReturnsAsync(false);

        _repositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(false);

        // Act
        var result = await _updatableService.UpdateTransactionAsync(transactionId, dto);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateTransactionAsync_ShouldThrowException_WhenTransactionDescriptionIsInvalid(string? transactionDescription)
    {
        // Arrange
        int transactionId = 3;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = transactionDescription!,
            Amount = 100,
            TransactionType = TransactionType.receita,
            CategoryId = 2,
            PersonId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _updatableService.UpdateTransactionAsync(transactionId, dto));

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
    }
    [Fact]
    public async Task UpdateTransactionAsync_ShouldThrowException_WhenAmountIsInvalid()
    {
        // Arrange
        int transactionId = 3;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Test Transaction",
            Amount = -100,
            TransactionType = TransactionType.receita,
            CategoryId = 2,
            PersonId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _updatableService.UpdateTransactionAsync(transactionId, dto));

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ShouldThrowException_WhenTransactionTypeIsInvalid()
    {
        // Arrange
        int transactionId = 3;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Updated transaction",
            Amount = 100,
            TransactionType = (TransactionType)999,
            CategoryId = 2,
            PersonId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _updatableService.UpdateTransactionAsync(transactionId, dto));

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        int transactionId = 3;
        var dto = new TransactionDtoUpsert
        {
            TransactionDescription = "Updated transaction",
            Amount = 100,
            TransactionType = TransactionType.receita,
            CategoryId = 2,
            PersonId = 1
        };

        _repositoryMock
                .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _updatableService.UpdateTransactionAsync(transactionId, dto));

        _repositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
    }
}

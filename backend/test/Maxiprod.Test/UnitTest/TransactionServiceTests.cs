using Maxiprod.Application.DTO;
using Maxiprod.Application.Services.TransactionService;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.ObjectValues;
using Maxiprod.Domain.RepositoryContract;
using Moq;

namespace Maxiprod.Test.UnitTest;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly TransactionAdderService _adderService;
    private readonly TransactionDeletionService _deletionService;
    private readonly TransactionGetterByIdService _getterByIdService;
    private readonly TransactionGetterService _getterService;
    private readonly TransactionUpdatableService _updatableService;

    public TransactionServiceTests()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();

        _adderService = new TransactionAdderService
                            (
                                _transactionRepositoryMock.Object,
                                _personRepositoryMock.Object,
                                _categoryRepositoryMock.Object
                            );

        _deletionService = new TransactionDeletionService(_transactionRepositoryMock.Object);
        _getterByIdService = new TransactionGetterByIdService(_transactionRepositoryMock.Object);
        _getterService = new TransactionGetterService(_transactionRepositoryMock.Object);
        _updatableService = new TransactionUpdatableService(_transactionRepositoryMock.Object);
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldReturnTransactionId_WhenSuccess()
    {
        // Arrange
        var dto = new TransactionDtoUpsert()
        {
            TransactionDescription = "Salário",
            Amount = 3000,
            TransactionType = TransactionType.receita,
            PersonId = 1,
            CategoryId = 1
        };

        var adultPerson = new Person(1, "João", 30);

        var validCategory = new Category(1, "Salário", CategoryGoal.receita);

        _personRepositoryMock
            .Setup(r => r.GetPersonByIdAsync(dto.PersonId))
            .ReturnsAsync(adultPerson);

        _categoryRepositoryMock
            .Setup(r => r.GetCategoryByIdAsync(dto.CategoryId))
            .ReturnsAsync(validCategory);

        _transactionRepositoryMock
            .Setup(r => r.CreateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(10);

        // Act
        var result = await _adderService.AddTransactionAsync(dto);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var dto = new TransactionDtoUpsert()
        {
            TransactionDescription = "Salário",
            Amount = 3000,
            TransactionType = TransactionType.receita,
            PersonId = 1,
            CategoryId = 1
        };

        var adultPerson = new Person(1, "João", 30);

        _personRepositoryMock
            .Setup(r => r.GetPersonByIdAsync(dto.PersonId))
            .ReturnsAsync(adultPerson);

        _categoryRepositoryMock
            .Setup(r => r.GetCategoryByIdAsync(dto.CategoryId))
            .ReturnsAsync((Category?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _adderService.AddTransactionAsync(dto));
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenPersonDoesNotExist()
    {
        // Arrange
        var dto = new TransactionDtoUpsert()
        {
            TransactionDescription = "Salário",
            Amount = 3000,
            TransactionType = TransactionType.receita,
            PersonId = 1,
            CategoryId = 1
        };

        var validCategory = new Category(1, "Salário", CategoryGoal.receita);

        _personRepositoryMock
            .Setup(r => r.GetPersonByIdAsync(dto.PersonId))
            .ReturnsAsync((Person?)null);

        _categoryRepositoryMock
            .Setup(r => r.GetCategoryByIdAsync(dto.CategoryId))
            .ReturnsAsync(validCategory);

        //    Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _adderService.AddTransactionAsync(dto));
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenMinorAddsIncome()
    {
        // Arrange
        var dto = new TransactionDtoUpsert()
        {
            TransactionDescription = "Salário",
            Amount = 3000,
            TransactionType = TransactionType.receita,
            PersonId = 1,
            CategoryId = 1
        };
        var minor = new Person(1, "Pedro", 16);
        var validCategory = new Category(1, "Salário", CategoryGoal.receita);

        _personRepositoryMock
            .Setup(r => r.GetPersonByIdAsync(dto.PersonId))
            .ReturnsAsync(minor);

        _categoryRepositoryMock
            .Setup(r => r.GetCategoryByIdAsync(dto.CategoryId))
            .ReturnsAsync(validCategory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _adderService.AddTransactionAsync(dto));
    }

    [Fact]
    public async Task AddTransactionAsync_ShouldThrowException_WhenCategoryGoalDiffersFromTransactionType()
    {
        // Arrange
        var dto = new TransactionDtoUpsert()
        {
            TransactionDescription = "Salário",
            Amount = 3000,
            TransactionType = TransactionType.receita,
            PersonId = 1,
            CategoryId = 1
        };
        var adultPerson = new Person(1, "João", 30);
        var category = new Category(1, "Alimentação", CategoryGoal.despesa);

        _personRepositoryMock
            .Setup(r => r.GetPersonByIdAsync(dto.PersonId))
            .ReturnsAsync(adultPerson);

        _categoryRepositoryMock
            .Setup(r => r.GetCategoryByIdAsync(dto.CategoryId))
            .ReturnsAsync(category);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _adderService.AddTransactionAsync(dto));
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

        //  Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddTransactionAsync(dto)
        );

        _transactionRepositoryMock.Verify(
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

        _transactionRepositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );

        _transactionRepositoryMock.Verify(
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

        _transactionRepositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );

        _transactionRepositoryMock.Verify(
            repo => repo.CreateTransactionAsync(It.IsAny<Transaction>()),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldReturnTrue_WhenTransactionIsDeleted()
    {
        // Arrange
        int transactionId = 1;

        _transactionRepositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _transactionRepositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ReturnsAsync(true);

        // Act
        var result = await _deletionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.True(result);
        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.DeleteTransactionAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldReturnFalse_WhenTransactionDoesNotExist()
    {
        // Arrange
        int transactionId = 2;

        _transactionRepositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        _transactionRepositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _deletionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.False(result);
        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.DeleteTransactionAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        int transactionId = 3;

        _transactionRepositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _transactionRepositoryMock
            .Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _deletionService.DeleteTransactionAsync(transactionId));

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<int>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.DeleteTransactionAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldReturnTransaction_WhenTransactionExists()
    {
        // Arrange
        int transactionId = 1;
        var transaction = new Transaction(transactionId, "Sample Transaction", 100, TransactionType.despesa, 1, 1);

        _transactionRepositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync(transaction);

        // Act
        var result = await _getterByIdService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transactionId, result!.TransactionId);

        _transactionRepositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldReturnNull_WhenTransactionDoesNotExist()
    {
        // Arrange
        int transactionId = 2;
        _transactionRepositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync((Transaction?)null);

        // Act
        var result = await _getterByIdService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.Null(result);
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        int transactionId = 3;
        _transactionRepositoryMock
            .Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _getterByIdService.GetTransactionByIdAsync(transactionId));
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
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

        _transactionRepositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ReturnsAsync(transactions);

        // Act
        var result = await _getterService.GetAllTransactionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, ((List<Transaction>)result).Count);
        _transactionRepositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ShouldReturnEmptyList_WhenNoTransactionsExist()
    {
        // Arrange
        var transactions = new List<Transaction>();
        _transactionRepositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ReturnsAsync(transactions);

        // Act
        var result = await _getterService.GetAllTransactionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _transactionRepositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        _transactionRepositoryMock
            .Setup(repo => repo.GetAllTransactionsAsync())
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _getterService.GetAllTransactionsAsync());
        _transactionRepositoryMock.Verify(repo => repo.GetAllTransactionsAsync(), Times.Once);
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

        _transactionRepositoryMock
            .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        _transactionRepositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(true);

        // Act
        var result = await _updatableService.UpdateTransactionAsync(transactionId, dto);

        // Assert
        Assert.True(result);

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
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

        _transactionRepositoryMock
                    .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
                    .ReturnsAsync(false);

        _transactionRepositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(false);

        // Act
        var result = await _updatableService.UpdateTransactionAsync(transactionId, dto);

        // Assert
        Assert.False(result);

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
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

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
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

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
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

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Never);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Never);
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

        _transactionRepositoryMock
                .Setup(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(true);

        _transactionRepositoryMock
            .Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _updatableService.UpdateTransactionAsync(transactionId, dto));

        _transactionRepositoryMock.Verify(repo => repo.DoesTransactionExistsAsync(It.IsAny<Transaction>()), Times.Once);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
    }
}

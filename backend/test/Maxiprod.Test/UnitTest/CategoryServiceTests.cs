using Maxiprod.Application.DTO;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Domain.ObjectValues;
using Moq;
using Maxiprod.Domain.Entity;
using Maxiprod.Application.Services.CategoryService;

namespace Maxiprod.Test;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly CategoryAdderService _adderService;
    private readonly CategoryDeletionService _deletionService;
    private readonly CategoryGetterByIdService _getterByIdService;
    private readonly CategoryGetterService _getterService;
    private readonly CategoryUpdatableService _updatableService;

    public CategoryServiceTests()
    {
        _repositoryMock = new Mock<ICategoryRepository>();

        _adderService = new CategoryAdderService(_repositoryMock.Object);
        _deletionService = new CategoryDeletionService(_repositoryMock.Object);
        _getterByIdService = new CategoryGetterByIdService(_repositoryMock.Object);
        _getterService = new CategoryGetterService(_repositoryMock.Object);
        _updatableService = new CategoryUpdatableService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldReturnCategoryId_WhenCategoryIsCreated()
    {
        // Arrange
        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "Test Category",
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
            .ReturnsAsync(1);

        // Act
        var result = await _adderService.AddCategoryAsync(dto);

        // Assert
        Assert.Equal(1, result);

        _repositoryMock.Verify(
            repo => repo.CreateCategoryAsync(It.IsAny<Category>()),
            Times.Once
        );
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "Test Category",
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _adderService.AddCategoryAsync(dto));

        _repositoryMock.Verify(
            repo => repo.CreateCategoryAsync(It.IsAny<Category>()),
            Times.Once
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddCategoryAsync_ShouldThrowException_WhenCategoryDescriptionIsInvalid(string? categoryDescription)
    {
        // Arrange
        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = categoryDescription!,
            CategoryGoal = CategoryGoal.despesa
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _adderService.AddCategoryAsync(dto));

        _repositoryMock.Verify(
            repo => repo.CreateCategoryAsync(It.IsAny<Category>()),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldReturnTrue_WhenCategoryIsDeleted()
    {
        // Arrange
        var categoryId = 1;

        _repositoryMock
            .Setup(repo => repo.DeleteCategoryAsync(categoryId))
            .ReturnsAsync(true);

        // Act
        var result = await _deletionService.DeleteCategoryAsync(categoryId);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            repo => repo.DeleteCategoryAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = 999;

        _repositoryMock
            .Setup(repo => repo.DeleteCategoryAsync(categoryId))
            .ReturnsAsync(false);

        // Act
        var result = await _deletionService.DeleteCategoryAsync(categoryId);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            repo => repo.DeleteCategoryAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var categoryId = 1;

        _repositoryMock
            .Setup(repo => repo.DeleteCategoryAsync(categoryId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _deletionService.DeleteCategoryAsync(categoryId)
        );

        _repositoryMock.Verify(
            repo => repo.DeleteCategoryAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = 1;

        var category = new Category(categoryId, "Test Category", CategoryGoal.ambas);


        _repositoryMock
            .Setup(repo => repo.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(category);

        // Act
        var result = await _getterByIdService.GetCategoryByIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result!.CategoryId);
        Assert.Equal("Test Category", result.CategoryDescription);

        _repositoryMock.Verify(
            repo => repo.GetCategoryByIdAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = 999;

        _repositoryMock
            .Setup(repo => repo.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync((Category?)null);

        // Act
        var result = await _getterByIdService.GetCategoryByIdAsync(categoryId);

        // Assert
        Assert.Null(result);

        _repositoryMock.Verify(
            repo => repo.GetCategoryByIdAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var categoryId = 1;

        _repositoryMock
            .Setup(repo => repo.GetCategoryByIdAsync(categoryId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _getterByIdService.GetCategoryByIdAsync(categoryId)
        );

        _repositoryMock.Verify(
            repo => repo.GetCategoryByIdAsync(categoryId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnAllCategories_WhenCategoriesExist()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category(1, "Food", CategoryGoal.despesa),
            new Category(2, "Transport", CategoryGoal.despesa)
        };

        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ReturnsAsync(categories);

        // Act
        var result = await _getterService.GetAllCategoriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Collection(result,
            c => Assert.Equal("Food", c.CategoryDescription),
            c => Assert.Equal("Transport", c.CategoryDescription)
        );

        _repositoryMock.Verify(
            repo => repo.GetAllCategoriesAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ReturnsAsync(new List<Category>());

        // Act
        var result = await _getterService.GetAllCategoriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        _repositoryMock.Verify(
            repo => repo.GetAllCategoriesAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _getterService.GetAllCategoriesAsync()
        );

        _repositoryMock.Verify(
            repo => repo.GetAllCategoriesAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateCategoryAsync_ShouldReturnTrue_WhenCategoryIsUpdated()
    {
        // Arrange
        var categoryId = 1;

        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "Updated Category",
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()))
            .ReturnsAsync(true);

        // Act
        var result = await _updatableService.UpdateCategoryAsync(categoryId, dto);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            repo => repo.UpdateCategoryAsync(
                It.Is<Category>(c =>
                    c.CategoryId == categoryId &&
                    c.CategoryDescription == dto.CategoryDescription &&
                    c.CategoryGoal == dto.CategoryGoal
                )
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateCategoryAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = 999;

        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "Non-existing Category",
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()))
            .ReturnsAsync(false);

        // Act
        var result = await _updatableService.UpdateCategoryAsync(categoryId, dto);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            repo => repo.UpdateCategoryAsync(It.IsAny<Category>()),
            Times.Once
        );
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateCategoryAsync_ShouldReturnFalse_WhenCategoryDescriptionIsInvalid(string? categoryDescription)
    {
        // Arrange
        var categoryId = 1;

        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = categoryDescription!,
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _updatableService.UpdateCategoryAsync(categoryId, dto));

        _repositoryMock.Verify(
            repo => repo.UpdateCategoryAsync(It.IsAny<Category>()),
            Times.Never
        );
    }

    [Fact]
    public async Task UpdateCategoryAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var categoryId = 1;

        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "Updated Category",
            CategoryGoal = CategoryGoal.despesa
        };

        _repositoryMock
            .Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _updatableService.UpdateCategoryAsync(categoryId, dto)
        );

        _repositoryMock.Verify(
            repo => repo.UpdateCategoryAsync(It.IsAny<Category>()),
            Times.Once
        );
    }
}

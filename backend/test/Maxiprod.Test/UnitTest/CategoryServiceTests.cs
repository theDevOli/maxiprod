using Maxiprod.Application.DTO;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Domain.Enum;
using Moq;
using Maxiprod.Domain.Entity;
using Maxiprod.Application.Services.CategoryService;

namespace Maxiprod.Test;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly CategoryAdderService _adderService;

    public CategoryServiceTests()
    {
        _repositoryMock = new Mock<ICategoryRepository>();
        _adderService = new CategoryAdderService(_repositoryMock.Object);
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

    [Fact]
    public async Task AddCategoryAsync_ShouldThrowException_WhenCategoryDescriptionIsInvalid()
    {
        // Arrange
        var dto = new CategoryDtoUpsert
        {
            CategoryDescription = "",
            CategoryGoal = CategoryGoal.despesa
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _adderService.AddCategoryAsync(dto));

        _repositoryMock.Verify(
            repo => repo.CreateCategoryAsync(It.IsAny<Category>()),
            Times.Never
        );
    }
}

using Maxiprod.Application.DTO;
using Maxiprod.Application.Services.PersonService;
using Maxiprod.Domain.Entity;
using Maxiprod.Domain.RepositoryContract;
using Moq;

namespace Maxiprod.Test.UnitTest;

public class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _repositoryMock;
    private readonly PersonAdderService _adderService;
    private readonly PersonDeletionService _deletionService;
    private readonly PersonGetterByIdService _getterByIdService;
    private readonly PersonGetterService _getterService;
    private readonly PersonUpdatableService _updatableService;

    public PersonServiceTests()
    {
        _repositoryMock = new Mock<IPersonRepository>();

        _adderService = new PersonAdderService(_repositoryMock.Object);
        _deletionService = new PersonDeletionService(_repositoryMock.Object);
        _getterByIdService = new PersonGetterByIdService(_repositoryMock.Object);
        _getterService = new PersonGetterService(_repositoryMock.Object);
        _updatableService = new PersonUpdatableService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddPersonAsync_ShouldReturnPersonId_WhenPersonIsCreated()
    {
        // Arrange
        var personId = 1;
        var personName = "John Doe";
        var age = 31;

        var dto = new PersonDtoUpsert
        {
            PersonName = personName,
            Age = age
        };

        _repositoryMock
            .Setup(repo => repo.CreatePersonAsync(It.IsAny<Person>()))
            .ReturnsAsync(personId);

        // Act
        var result = await _adderService.AddPersonAsync(dto);

        // Assert
        Assert.Equal(personId, result);

        _repositoryMock.Verify(
            repo => repo.CreatePersonAsync(It.Is<Person>(p =>
                p.PersonName.Equals(personName) && p.Age == age
            )),
            Times.Once
        );
    }

    [Fact]
    public async Task AddPersonAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var personName = "John Doe";
        var age = 31;

        var dto = new PersonDtoUpsert
        {
            PersonName = personName,
            Age = age
        };


        _repositoryMock
            .Setup(repo => repo.CreatePersonAsync(It.IsAny<Person>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _adderService.AddPersonAsync(dto)
        );

        _repositoryMock.Verify(
            repo => repo.CreatePersonAsync(It.IsAny<Person>()),
            Times.Once
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddPersonAsync_ShouldThrowException_WhenPersonNameIsInvalid(string? personName)
    {
        // Arrange
        var age = 31;

        var dto = new PersonDtoUpsert
        {
            PersonName = personName!,
            Age = age
        };


        _repositoryMock
            .Setup(repo => repo.CreatePersonAsync(It.IsAny<Person>()));


        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddPersonAsync(dto)
        );

        _repositoryMock.Verify(
            repo => repo.CreatePersonAsync(It.IsAny<Person>()),
            Times.Never
        );
    }

    [Fact]
    public async Task AddPersonAsync_ShouldThrowException_WhenAgeIsInvalid()
    {
        // Arrange
        var personName = "John Doe";
        var age = -1;
        var dto = new PersonDtoUpsert
        {
            PersonName = personName,
            Age = age
        };


        _repositoryMock
            .Setup(repo => repo.CreatePersonAsync(It.IsAny<Person>()));


        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _adderService.AddPersonAsync(dto)
        );

        _repositoryMock.Verify(
            repo => repo.CreatePersonAsync(It.IsAny<Person>()),
            Times.Never
        );
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldReturnTrue_WhenPersonIsDeleted()
    {
        // Arrange
        var personId = 1;

        _repositoryMock
            .Setup(repo => repo.DoesPersonExistAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.DeletePersonAsync(personId))
            .ReturnsAsync(true);

        // Act
        var result = await _deletionService.DeletePersonAsync(personId);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(repo => repo.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeletePersonAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldReturnFalse_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = 999;

        _repositoryMock
            .Setup(repo => repo.DoesPersonExistAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _deletionService.DeletePersonAsync(personId);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(repo => repo.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeletePersonAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var personId = 1;

        _repositoryMock
            .Setup(repo => repo.DoesPersonExistAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(repo => repo.DeletePersonAsync(personId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _deletionService.DeletePersonAsync(personId)
        );

        _repositoryMock.Verify(repo => repo.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeletePersonAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task GetPersonByIdAsync_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var personId = 1;
        var personName = "John Doe";
        var age = 31;

        var person = new Person(personId, personName, age);

        _repositoryMock
            .Setup(repo => repo.GetPersonByIdAsync(personId))
            .ReturnsAsync(person);

        // Act
        var result = await _getterByIdService.GetPersonByIdAsync(personId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(personId, result!.PersonId);
        Assert.Equal(personName, result.PersonName);
        Assert.Equal(age, result.Age);

        _repositoryMock.Verify(
            repo => repo.GetPersonByIdAsync(personId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetPersonByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = 999;

        _repositoryMock
            .Setup(repo => repo.GetPersonByIdAsync(personId))
            .ReturnsAsync((Person?)null);

        // Act
        var result = await _getterByIdService.GetPersonByIdAsync(personId);

        // Assert
        Assert.Null(result);

        _repositoryMock.Verify(
            repo => repo.GetPersonByIdAsync(personId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetPersonByIdAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var personId = 1;

        _repositoryMock
            .Setup(repo => repo.GetPersonByIdAsync(personId))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _getterByIdService.GetPersonByIdAsync(personId)
        );

        _repositoryMock.Verify(
            repo => repo.GetPersonByIdAsync(personId),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllPeopleAsync_ShouldReturnAllPeople_WhenPeopleExist()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person (1,"John Doe",30),
            new Person (2,"Jane Doe",25)
        };

        _repositoryMock
            .Setup(repo => repo.GetAllPeopleAsync())
            .ReturnsAsync(people);

        // Act
        var result = await _getterService.GetAllPeopleAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Collection(result,
            p => Assert.Equal("John Doe", p.PersonName),
            p => Assert.Equal("Jane Doe", p.PersonName)
        );

        _repositoryMock.Verify(
            repo => repo.GetAllPeopleAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllPeopleAsync_ShouldReturnEmptyList_WhenNoPeopleExist()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAllPeopleAsync())
            .ReturnsAsync(new List<Person>());

        // Act
        var result = await _getterService.GetAllPeopleAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        _repositoryMock.Verify(
            repo => repo.GetAllPeopleAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task GetAllPeopleAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAllPeopleAsync())
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => _getterService.GetAllPeopleAsync()
        );

        _repositoryMock.Verify(
            repo => repo.GetAllPeopleAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {
        // Arrange
        var personId = 1;
        var dto = new PersonDtoUpsert
        {
            PersonName = "John Doe",
            Age = 31
        };

        _repositoryMock
            .Setup(r => r.DoesPersonExistAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        _repositoryMock
            .Setup(r => r.UpdatePersonAsync(It.IsAny<Person>()))
            .ReturnsAsync(true);

        // Act
        var result = await _updatableService.UpdatePersonAsync(personId, dto);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(r => r.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>()), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdatePersonAsync_ShouldReturnFalse_WhenPersonNameIsInvalid(string? personName)
    {
        // Arrange
        var personId = 1;
        var dto = new PersonDtoUpsert
        {
            PersonName = personName!,
            Age = 31
        };

        _repositoryMock
          .Setup(r => r.DoesPersonExistAsync(It.IsAny<int>()))
          .ReturnsAsync(true);
        _repositoryMock
            .Setup(r => r.UpdatePersonAsync(It.IsAny<Person>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _updatableService.UpdatePersonAsync(personId, dto)
        );

        _repositoryMock.Verify(r => r.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>()), Times.Never);
    }
    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnFalse_WhenAgeIsInvalid()
    {
        // Arrange
        var personId = 1;
        var dto = new PersonDtoUpsert
        {
            PersonName = "John Doe",
            Age = -31
        };

        _repositoryMock
          .Setup(r => r.DoesPersonExistAsync(It.IsAny<int>()))
          .ReturnsAsync(true);
        _repositoryMock
            .Setup(r => r.UpdatePersonAsync(It.IsAny<Person>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _updatableService.UpdatePersonAsync(personId, dto)
        );

        _repositoryMock.Verify(r => r.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>()), Times.Never);
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnFalse_WhenUpdateFails()
    {
        // Arrange
        var personId = 1;
        var dto = new PersonDtoUpsert
        {
            PersonName = "John Doe",
            Age = 31
        };

        _repositoryMock
          .Setup(r => r.DoesPersonExistAsync(It.IsAny<int>()))
          .ReturnsAsync(false);

        // Act
        var result = await _updatableService.UpdatePersonAsync(personId, dto);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(r => r.DoesPersonExistAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>()), Times.Never);
    }
}

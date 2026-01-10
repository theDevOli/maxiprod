namespace Maxiprod.Domain.Entity;

/// <summary>
/// Represents a person in the system.
/// </summary>
public class Person
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    public int PersonId { get; private set; }

    /// <summary>
    /// Gets the person's name.
    /// This value is required and cannot be null or empty.
    /// </summary>
    public string PersonName { get; private set; } = default!;

    /// <summary>
    /// Gets the person's age.
    /// The age must be greater than or equal to zero.
    /// </summary>
    public int Age { get; private set; }

    public bool IsAdult => Age >= 18;

    /// <summary>
    /// Private constructor required by Dapper.
    /// </summary>
    private Person() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class
    /// enforcing all domain invariants.
    /// </summary>
    /// <param name="personName">
    /// The person's name. Must not be null, empty, or whitespace.
    /// </param>
    /// <param name="age">
    /// The person's age. Must be greater than zero.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="personName"/> is invalid or
    /// <paramref name="age"/> is less than or equal to zero.
    /// </exception>
    public Person(string personName, int age)
    {
        ChangeName(personName);
        ChangeAge(age);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class with an identifier.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person.
    /// </param>
    /// <param name="personName">
    /// The person's name.
    /// </param>
    /// <param name="age">
    /// The person's age.
    /// </param>
    public Person(int personId, string personName, int age)
    {
        PersonId = personId;
        ChangeName(personName);
        ChangeAge(age);
    }

    /// <summary>
    /// Changes the person's name.
    /// </summary>
    /// <param name="personName">
    /// The new name to assign to the person.
    /// Must not be null, empty, or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="personName"/> is invalid.
    /// </exception>
    public void ChangeName(string personName)
    {
        if (string.IsNullOrWhiteSpace(personName))
            throw new ArgumentException("Person's name cannot be null or empty");

        PersonName = personName;
    }

    /// <summary>
    /// Changes the person's age.
    /// </summary>
    /// <param name="age">
    /// The new age to assign to the person.
    /// Must be greater than zero.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="age"/> is less than or equal to zero.
    /// </exception>
    public void ChangeAge(int age)
    {
        if (age <= 0)
            throw new ArgumentException("Age cannot be less than or equal to zero");

        Age = age;
    }
}

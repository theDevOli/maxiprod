import type { IPerson } from "../types/IPerson.interface"

/**
 * Represents a person entity.
 */
export class Person implements IPerson {
    private readonly _personId: number
    private readonly _personName: string
    private readonly _age: number

    /**
     * Creates a new Person instance.
     *
     * @param personId - Unique identifier of the person
     * @param personName - Name of the person
     * @param age - Age of the person
     */
    constructor(personId: number, personName: string, age: number) {
        this._personId = personId
        this._personName = personName
        this._age = age
    }

    /**
     * Returns the person's unique identifier.
     */
    public get personId(): number {
        return this._personId
    }

    /**
     * Returns the person's name.
     */
    public get personName(): string {
        return this._personName
    }

    /**
     * Returns the person's age.
     */
    public get age(): number {
        return this._age
    }

    /**
     * Factory that creates a Person instance from an
     * IPerson object.
     *
     * @param person - Object containing person data
     * @returns A Person instance
     */
    public static fromSingleInterface(person: IPerson): Person {
        return new Person(person.personId, person.personName, person.age)
    }

    /**
     * Converts an array of IPerson objects into
     * an array of Person instances.
     *
     * @param people - Array of person objects
     * @returns Array of Person instances
     */
    public static fromBulkInterface(people: IPerson[]): Person[] {
        return people.map((p) => this.fromSingleInterface(p))
    }
}

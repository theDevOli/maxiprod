import type { IPeopleBalance } from "../types/IPeopleBalance.interface"
import { Balance } from "./Balance"

/**
 * Represents the financial balance of a specific person.
 */
export class PeopleBalance extends Balance {
    private _personName: string

    /**
     * Creates a new PeopleBalance instance.
     *
     * @param personName - Name of the person
     * @param income - Total income amount for the person
     * @param expense - Total expense amount for the person
     * @param balance - Final balance amount for the person
     */
    constructor(
        personName: string,
        income: number,
        expense: number,
        balance: number
    ) {
        super(income, expense, balance)
        this._personName = personName
    }

    /**
     * Returns the person's name.
     */
    public get personName(): string {
        return this._personName
    }

    /**
     * Factory that creates a PeopleBalance instance from an
     * IPeopleBalance object.
     *
     * @param peopleBalance - Object containing people balance data
     * @returns A PeopleBalance instance
     */
    public static fromSingleInterface(
        peopleBalance: IPeopleBalance
    ): PeopleBalance {
        return new PeopleBalance(
            peopleBalance.personName,
            peopleBalance.income,
            peopleBalance.expense,
            peopleBalance.balance
        )
    }


    /**
     * Converts an array of IPeopleBalance objects into
     * an array of PeopleBalance instances.
     *
     * @param peopleBalance - Array of people balance objects
     * @returns Array of PeopleBalance instances
     */
    public static fromBulkInterface(
        peopleBalance: IPeopleBalance[]
    ): PeopleBalance[] {
        return peopleBalance.map((p) => this.fromSingleInterface(p))
    }
}

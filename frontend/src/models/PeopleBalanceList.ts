import type { IPeopleBalanceList } from "../types/IPeopleBalanceList.interface"
import { Balance } from "./Balance"
import { PeopleBalance } from "./PeopleBalance"

/**
 * Represents a list of people balances along with
 * financial statistics.
 */
export class PeopleBalanceList implements IPeopleBalanceList {
    private _people: PeopleBalance[]
    private _totalStatistic: Balance

    /**
     * Creates a new PeopleBalanceList instance.
     *
     * @param people - List of people balance instances
     * @param totalStatistic - Financial statistics
     */
    constructor(people: PeopleBalance[], totalStatistic: Balance) {
        this._people = people
        this._totalStatistic = totalStatistic
    }

    /**
     * Returns the list of people balances.
     */
    public get people(): PeopleBalance[] {
        return this._people
    }

    /**
     * Returns the financial statistics.
     */
    public get totalStatistic(): Balance {
        return this._totalStatistic
    }

        /**
     * Factory that creates a PeopleBalanceList instance from an
     * IPeopleBalanceList object.
     *
     * @param peopleBalanceList - Object containing people balance list data
     * @returns A PeopleBalanceList instance
     */
    public static fromSingleInterface(
        peopleBalanceList: IPeopleBalanceList
    ): PeopleBalanceList {
        const people = PeopleBalance.fromBulkInterface(peopleBalanceList.people)
        const totalStatistic = Balance.fromSingleInterface(
            peopleBalanceList.totalStatistic
        )

        return new PeopleBalanceList(people, totalStatistic)
    }
}

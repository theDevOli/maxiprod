import type { IBalance } from "./IBalance.interface"
import type { IPeopleBalance } from "./IPeopleBalance.interface"
/**
 * Represents a summarized list of balances grouped by person.
 */
export interface IPeopleBalanceList {
    people: IPeopleBalance[]
    totalStatistic: IBalance
}

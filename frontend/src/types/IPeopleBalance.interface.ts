import type { IBalance } from "./IBalance.interface"

/**
 * Represents the balance associated with a specific person.
 */
export interface IPeopleBalance extends IBalance {
    personName: string
}

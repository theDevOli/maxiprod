import type { IBalance } from "./IBalance.interface"
import type { ICategoryBalance } from "./ICategoryBalance.interface"

/**
 * Represents a collection of category balances.
 *
 */
export interface ICategoryBalanceList {
    categories: ICategoryBalance[]
    totalStatistic: IBalance
}

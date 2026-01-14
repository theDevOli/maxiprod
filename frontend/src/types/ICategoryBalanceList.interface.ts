import type { IBalance } from "./IBalance.interface"
import type { ICategoryBalance } from "./ICategoryBalance.interface"

export interface ICategoryBalanceList {
    categories: ICategoryBalance[]
    totalStatistic: IBalance
}

import type { IBalance } from "./IBalance.interface"
import type { IPeopleBalance } from "./IPeopleBalance.interface"

export interface IPeopleBalanceList {
    people: IPeopleBalance[]
    totalStatistic: IBalance
}

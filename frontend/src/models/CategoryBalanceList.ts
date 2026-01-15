import type { ICategoryBalanceList } from "../types/ICategoryBalanceList.interface"
import { Balance } from "./Balance"
import { CategoryBalance } from "./CategoryBalance"

/**
 * Represents a collection of category balances along with
 * aggregated financial statistics.
 */
export class CategoryBalanceList implements ICategoryBalanceList {
    private _categories: CategoryBalance[]
    private _totalStatistic: Balance

    /**
     * Creates a new CategoryBalanceList instance.
     *
     * @param categories - List of category balance domain objects
     * @param totalStatistic - Aggregated financial statistics
     */
    constructor(categories: CategoryBalance[], totalStatistic: Balance) {
        this._categories = categories
        this._totalStatistic = totalStatistic
    }

    /**
     * Returns the list of category balances.
     */
    public get categories(): CategoryBalance[] {
        return this._categories
    }

    /**
     * Returns the aggregated total statistics.
     */
    public get totalStatistic(): Balance {
        return this._totalStatistic
    }

    /**
     * Creates a CategoryBalanceList instance from an
     * ICategoryBalanceList object.
     * @param categoryBalanceList - Category balance list data
     * @returns CategoryBalanceList instance
     */
    public static fromSingleInterface(
        categoryBalanceList: ICategoryBalanceList
    ): CategoryBalanceList {
        const categories = CategoryBalance.fromBulkInterface(
            categoryBalanceList.categories
        )
        const totalStatistic = Balance.fromSingleInterface(
            categoryBalanceList.totalStatistic
        )

        return new CategoryBalanceList(categories, totalStatistic)
    }
}

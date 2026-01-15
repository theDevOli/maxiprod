import type { ICategoryBalance } from "../types/ICategoryBalance.interface"
import { Balance } from "./Balance"

/**
 * Represents the financial balance of a specific category.
 */
export class CategoryBalance extends Balance {
    private _categoryDescription: string

    /**
     * Creates a new CategoryBalance instance.
     *
     * @param categoryDescription - Category description
     * @param income - Total income amount for the category
     * @param expense - Total expense amount for the category
     * @param balance - Final balance amount for the category
     */
    constructor(
        categoryDescription: string,
        income: number,
        expense: number,
        balance: number
    ) {
        super(income, expense, balance)
        this._categoryDescription = categoryDescription
    }

    /**
     * Returns the category description.
     */
    public get categoryDescription(): string {
        return this._categoryDescription
    }

        /**
     * Factory to create a CategoryBalance instance from an
     * ICategoryBalance object.
     *
     * @param categoryBalance - Object containing category balance data
     * @returns A CategoryBalance instance
     */
    public static fromSingleInterface(
        categoryBalance: ICategoryBalance
    ): CategoryBalance {
        return new CategoryBalance(
            categoryBalance.categoryDescription,
            categoryBalance.income,
            categoryBalance.expense,
            categoryBalance.balance
        )
    }
    
    /**
     * Converts an array ICategoryBalance objects into
     * an array of CategoryBalance instances.
     *
     * @param categoryBalance - Array of category balance objects
     * @returns Array of CategoryBalance instances
     */
    public static fromBulkInterface(
        categoryBalance: ICategoryBalance[]
    ): CategoryBalance[] {
        return categoryBalance.map((c) => this.fromSingleInterface(c))
    }
}

import type { IBalance } from "../types/IBalance.interface"
import { cashFormatter } from "../utils/cashFormatter"

/**
 * Class representing a financial balance.
 *
 * This class implements the `IBalance` interface and provides both numeric
 * and formatted string representations of income, expense, and balance values.
 *
 * @implements {IBalance}
 */
export class Balance implements IBalance {
    private _income: number
    private _expense: number
    private _balance: number

    /**
     * Creates a new Balance instance.
     *
     * @param {number} income - Total income amount.
     * @param {number} expense - Total expense amount.
     * @param {number} balance - Net balance amount.
     */
    constructor(income: number, expense: number, balance: number) {
        this._income = income
        this._expense = expense
        this._balance = balance
    }

    /**
     * Gets the numeric income value.
     * @returns {number} Income amount.
     */
    public get income(): number {
        return this._income
    }

    /**
     * Gets the numeric expense value.
     * @returns {number} Expense amount.
     */
    public get expense(): number {
        return this._expense
    }

    /**
     * Gets the numeric balance value.
     * @returns {number} Balance amount.
     */
    public get balance(): number {
        return this._balance
    }

    /**
     * Returns the formatted income as a string in a currency format.
     * @returns {string} Formatted income.
     */
    public getIncome(): string {
        return cashFormatter(this._income)
    }

    /**
     * Returns the formatted expense as a string in a currency format.
     * @returns {string} Formatted expense.
     */
    public getExpense(): string {
        return cashFormatter(this._expense)
    }

    /**
     * Returns the formatted balance as a string in a currency format.
     * @returns {string} Formatted balance.
     */
    public getBalance(): string {
        return cashFormatter(this._balance)
    }

    /**
     * Factory method to create a Balance instance from an existing IBalance object.
     *
     * @param {IBalance} balance - Object implementing IBalance interface.
     * @returns {Balance} New Balance instance.
     */
    public static fromSingleInterface(balance: IBalance): Balance {
        return new Balance(balance.income, balance.expense, balance.balance)
    }
}

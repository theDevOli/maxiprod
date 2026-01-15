import type { ICategory } from "../types/ICategory.interface"
import type { IPerson } from "../types/IPerson.interface"
import type { ITransaction } from "../types/ITransaction.interface"
import type { TransactionType } from "../types/TransactionType.type"
import { cashFormatter } from "../utils/cashFormatter"

/**
 * Represents a financial transaction.
 */
export class Transaction {
    private _transactionId: number
    private _transactionDescription: string
    private _amount: number
    private _transactionType: TransactionType
    private _category: string
    private _person: string

    /**
     * Creates a new Transaction instance.
     *
     * @param transactionId - Unique identifier of the transaction
     * @param transactionDescription - Description of the transaction
     * @param amount - Transaction amount
     * @param transactionType - Type of the transaction ('redeita' or 'despesa')
     * @param category - Category description
     * @param person - Person name associated with the transaction
     */
    constructor(
        transactionId: number,
        transactionDescription: string,
        amount: number,
        transactionType: TransactionType,
        category: string,
        person: string
    ) {
        this._transactionId = transactionId
        this._transactionDescription = transactionDescription
        this._amount = amount
        this._transactionType = transactionType
        this._category = category
        this._person = person
    }

    /**
     * Returns the transaction identifier.
     */
    public get transactionId(): number {
        return this._transactionId
    }

    /**
     * Returns the transaction description.
     */
    public get transactionDescription(): string {
        return this._transactionDescription
    }

    /**
     * Returns the transaction amount.
     */
    public get amount(): number {
        return this._amount
    }

    /**
     * Returns the transaction type.
     */
    public get transactionType(): TransactionType {
        return this._transactionType
    }

    /**
     * Returns the category description.
     */
    public get category(): string {
        return this._category
    }

    /**
     * Returns the person name.
     */
    public get person(): string {
        return this._person
    }

    /**
     * Returns the transaction type formatted with
     * the first letter capitalized.
     */
    public get type(): string {
        const type = String(this._transactionType)
        const len = type.length

        return `${type.substring(0, 1).toLocaleUpperCase()}${type.substring(
            1,
            len
        )}`
    }

    /**
     * Returns the transaction amount formatted as currency.
     */
    public get cash(): string {
        return cashFormatter(this._amount)
    }

    /**
     * Factory that creates a Transaction instance from an
     * ITransaction object and related person and category data.
     *
     * @param transaction - Transaction data object
     * @param person - List of people used to resolve the person name
     * @param category - List of categories used to resolve the category description
     * @returns A Transaction instance
     */
    public static fromSingleInterface(
        transaction: ITransaction,
        person: IPerson[],
        category: ICategory[]
    ): Transaction {
        const foundCategory = category.find(
            (c) => c.categoryId === transaction.categoryId
        )!.categoryDescription

        const foundPerson = person.find(
            (p) => p.personId === transaction.personId
        )!.personName

        return new Transaction(
            transaction.transactionId,
            transaction.transactionDescription,
            transaction.amount,
            transaction.transactionType,
            foundCategory,
            foundPerson
        )
    }

    /**
     * Converts an array of ITransaction objects into
     * an array of Transaction instances.
     *
     * @param transactions - Array of transaction objects
     * @param person - List of people used to resolve person names
     * @param category - List of categories used to resolve category descriptions
     * @returns Array of Transaction instances
     */
    public static fromBulkInterface(
        transactions: ITransaction[],
        person: IPerson[],
        category: ICategory[]
    ): Transaction[] {
        return transactions.map((t) =>
            this.fromSingleInterface(t, person, category)
        )
    }
}

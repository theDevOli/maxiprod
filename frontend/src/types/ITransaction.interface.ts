import type { TransactionType } from "./TransactionType.type"

/**
 * Represents a financial transaction.
 */
export interface ITransaction {
    transactionId: number
    transactionDescription: string
    amount: number
    transactionType: TransactionType
    categoryId: number
    personId: number
}

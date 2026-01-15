import type { ITransaction } from "../types/ITransaction.interface"
import { api } from "./api"

/**
 * Service responsible for handling transaction-related API requests.
 *
 * This service provides CRUD operations for financial transactions,
 */
export const transactionServices = {
    /**
     * Retrieves all transactions.
     *
     * Endpoint:
     * GET /Transactions
     *
     * @returns {Promise<ITransaction[]>}
     * An array of transaction objects.
     */
    getAll: async (): Promise<ITransaction[]> => {
        const response = await api.get("/Transactions")
        return response.data
    },

    /**
     * Retrieves a transaction by its unique identifier.
     *
     * Endpoint:
     * GET /Transactions/{id}
     *
     * NOTE: To be consumed in the future.
     *
     * @param {number} transactionId - The ID of the transaction to retrieve
     * @returns {Promise<ITransaction>}
     * The requested transaction object.
     */
    getById: async (transactionId: number): Promise<ITransaction> => {
        const response = await api.get(`/Transactions/${transactionId}`)
        return response.data
    },

    /**
     * Creates a new transaction.
     *
     * Endpoint:
     * POST /Transactions
     *
     * @param {Omit<ITransaction, "transactionId" | "transactionType"> & { transactionType: string }}
     * bodyRequest - Data required to create a transaction
     *
     * @returns {Promise<ITransaction>}
     * The newly created transaction object.
     */
    create: async (
        bodyRequest: Omit<ITransaction, "transactionId" | "transactionType"> & {
            transactionType: string
        }
    ): Promise<ITransaction> => {
        const response = await api.post("/Transactions", bodyRequest)
        return response.data
    },

    /**
     * Updates an existing transaction.
     *
     * Endpoint:
     * PUT /Transactions/{id}
     *
     * NOTE: To be consumed in the future.
     *
     * @param {number} transactionId - The ID of the transaction to update
     * @param {Omit<ITransaction, "transactionId" | "transactionType"> & {transactionType: string}} bodyRequest - Updated transaction data
     *
     * @returns {Promise<ITransaction>}
     * The updated transaction object.
     */
    update: async (
        transactionId: number,
        bodyRequest: Omit<ITransaction, "transactionId" | "transactionType"> & {
            transactionType: string
        }
    ): Promise<ITransaction> => {
        const response = await api.put(
            `/Transactions/${transactionId}`,
            bodyRequest
        )
        return response.data
    },

    /**
     * Deletes a transaction by its unique identifier.
     *
     * Endpoint:
     * DELETE /Transactions/{id}
     *
     * @param {number} transactionId - The ID of the transaction to delete
     *
     * @returns {Promise<void>}
     */
    delete: async (transactionId: number): Promise<void> => {
        await api.delete(`/Transactions/${transactionId}`)
    },
}

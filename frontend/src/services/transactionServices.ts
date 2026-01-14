import type { ITransaction } from "../types/ITransaction.interface"
import { api } from "./api"

export const transactionServices = {
    // GET /api/Transactions
    getAll: async (): Promise<ITransaction[]> => {
        const response = await api.get("/Transactions")
        return response.data
    },

    // GET /api/Transactions/{id}
    getById: async (transactionId: number): Promise<ITransaction> => {
        const response = await api.get(`/Transactions/${transactionId}`)
        return response.data
    },

    // POST /api/Transactions
    create: async (
        bodyRequest: Omit<ITransaction, "transactionId" | "transactionType"> & {
            transactionType: string
        }
    ): Promise<ITransaction> => {
        const response = await api.post("/Transactions", bodyRequest)
        return response.data
    },

    // PUT /api/Transactions/{id}
    update: async (
        transactionId: number,
        bodyRequest: ITransaction
    ): Promise<ITransaction> => {
        const response = await api.put(
            `/Transactions/${transactionId}`,
            bodyRequest
        )
        return response.data
    },

    // DELETE /api/Transactions/{id}
    delete: async (transactionId: number): Promise<void> => {
        await api.delete(`/Transactions/${transactionId}`)
    },
}

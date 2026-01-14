import type { ICategory } from "../types/ICategory.interface"
import { api } from "./api"

export const categoryServices = {
    // GET /api/Categories
    getAll: async (): Promise<ICategory[]> => {
        const response = await api.get("/Categories")
        return response.data
    },

    // GET /api/Categories/{id}
    getById: async (categoryId: number): Promise<ICategory> => {
        const response = await api.get(`/Categories/${categoryId}`)
        return response.data
    },

    // POST /api/Categories
    create: async (
        bodyRequest: Omit<ICategory, "categoryId" | "categoryGoal"> & {
            categoryGoal: string
        }
    ): Promise<ICategory> => {
        const response = await api.post("/Categories", bodyRequest)
        return response.data
    },

    // PUT /api/Categories/{id}
    update: async (
        categoryId: number,
        bodyRequest: ICategory
    ): Promise<ICategory> => {
        const response = await api.put(`/Categories/${categoryId}`, bodyRequest)
        return response.data
    },

    // DELETE /api/Categories/{id}
    delete: async (categoryId: number): Promise<void> => {
        await api.delete(`/Categories/${categoryId}`)
    },
}

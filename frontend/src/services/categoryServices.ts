import type { ICategory } from "../types/ICategory.interface"
import { api } from "./api"

/**
 * Service responsible for handling category-related API requests.
 *
 * This service provides CRUD operations for categories,
 */
export const categoryServices = {
    /**
     * Retrieves all categories.
     *
     * Endpoint:
     * GET /Categories
     *
     * @returns {Promise<ICategory[]>}
     * An array of category objects.
     */
    getAll: async (): Promise<ICategory[]> => {
        const response = await api.get("/Categories")
        return response.data
    },

    /**
     * Retrieves a category by its unique identifier.
     *
     * Endpoint:
     * GET /Categories/{id}
     *
     * NOTE: To be consumed in the future
     *
     * @param {number} categoryId - The ID of the category to retrieve
     * @returns {Promise<ICategory>}
     * The requested category object.
     */
    getById: async (categoryId: number): Promise<ICategory> => {
        const response = await api.get(`/Categories/${categoryId}`)
        return response.data
    },

    /**
     * Creates a new category.
     *
     * Endpoint:
     * POST /Categories
     *
     * @param {Object} bodyRequest - Data required to create the category
     * @param {string} bodyRequest.categoryDescription - Category description
     * @param {string} bodyRequest.categoryGoal - Category goal (e.g. income, expense, both)
     *
     * @returns {Promise<ICategory>}
     * The newly created category object.
     */
    create: async (
        bodyRequest: Omit<ICategory, "categoryId" | "categoryGoal"> & {
            categoryGoal: string
        }
    ): Promise<ICategory> => {
        const response = await api.post("/Categories", bodyRequest)
        return response.data
    },

    /**
     * Updates an existing category.
     *
     * Endpoint:
     * PUT /Categories/{id}
     *
     * NOTE: To be consumed in the future
     *
     * @param {number} categoryId - The ID of the category to update
     * @param {Object} bodyRequest - Updated category data
     *
     * @returns {Promise<ICategory>}
     * The updated category object.
     */
    update: async (
        categoryId: number,
        bodyRequest: Omit<ICategory, "categoryId" | "categoryGoal"> & {
            categoryGoal: string
        }
    ): Promise<ICategory> => {
        const response = await api.put(`/Categories/${categoryId}`, bodyRequest)
        return response.data
    },

    /**
     * Deletes a category by its unique identifier.
     *
     * Endpoint:
     * DELETE /Categories/{id}
     *
     * @param {number} categoryId - The ID of the category to delete
     *
     * @returns {Promise<void>}
     */
    delete: async (categoryId: number): Promise<void> => {
        await api.delete(`/Categories/${categoryId}`)
    },
}

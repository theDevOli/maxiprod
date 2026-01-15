import type { IPerson } from "../types/IPerson.interface"
import { api } from "./api"

/**
 * Service responsible for handling person-related API requests.
 *
 * This service provides CRUD operations for people,
 */
export const personServices = {
    /**
     * Retrieves all people.
     *
     * Endpoint:
     * GET /People
     *
     * @returns {Promise<IPerson[]>}
     * An array of people objects.
     */
    getAll: async (): Promise<IPerson[]> => {
        const response = await api.get("/People")
        return response.data
    },

    /**
     * Retrieves a person by their unique identifier.
     *
     * Endpoint:
     * GET /People/{id}
     *
     * NOTE: To be consumed in the future.
     *
     * @param {number} personId - The ID of the person to retrieve
     * @returns {Promise<IPerson>}
     * The requested person object.
     */
    getById: async (personId: number): Promise<IPerson> => {
        const response = await api.get(`/People/${personId}`)
        return response.data
    },

    /**
     * Creates a new person.
     *
     * Endpoint:
     * POST /People
     *
     * @param {Omit<IPerson, "personId">} bodyRequest - Data required to create a person
     * @returns {Promise<IPerson>}
     * The newly created person object.
     */
    create: async (
        bodyRequest: Omit<IPerson, "personId">
    ): Promise<IPerson> => {
        const response = await api.post("/People", bodyRequest)
        return response.data
    },

    /**
     * Updates an existing person.
     *
     * Endpoint:
     * PUT /People/{id}
     *
     * NOTE: To be consumed in the future.
     *
     * @param {number} personId - The ID of the person to update
     * @param {Omit<IPerson, "personId"} bodyRequest - Updated person data
     *
     * @returns {Promise<IPerson>}
     * The updated person object.
     */
    update: async (
        personId: number,
        bodyRequest: Omit<IPerson, "personId">
    ): Promise<IPerson> => {
        const response = await api.put(`/People/${personId}`, bodyRequest)
        return response.data
    },

    /**
     * Deletes a person by their unique identifier.
     *
     * Endpoint:
     * DELETE /People/{id}
     *
     * @param {number} personId - The ID of the person to delete
     *
     * @returns {Promise<void>}
     */
    delete: async (personId: number): Promise<void> => {
        await api.delete(`/People/${personId}`)
    },
}

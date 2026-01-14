

import type { IPerson } from "../types/IPerson.interface"
import { api } from "./api"

export const personServices = {
    // GET /api/People
    getAll: async (): Promise<IPerson[]> => {
        const response = await api.get("/People")
        return response.data
    },

    // GET /api/People/{id}
    getById: async (personId: number): Promise<IPerson> => {
        const response = await api.get(`/People/${personId}`)
        return response.data
    },

    // POST /api/People
    create: async (bodyRequest: Omit<IPerson, "personId">): Promise<IPerson> => {
        const response = await api.post("/People", bodyRequest)
        return response.data
    },

    // PUT /api/People/{id}
    update: async (personId: number, bodyRequest: IPerson): Promise<IPerson> => {
        const response = await api.put(`/People/${personId}`, bodyRequest)
        return response.data
    },

    // DELETE /api/People/{id}
    delete: async (personId: number): Promise<void> => {
        await api.delete(`/People/${personId}`)
    },
}

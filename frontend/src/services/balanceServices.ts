import type { ICategoryBalanceList } from "../types/ICategoryBalanceList.interface"
import type { IPeopleBalanceList } from "../types/IPeopleBalanceList.interface"
import { api } from "./api"

/**
 * Service responsible for retrieving balance statistics from the backend.
 */
export const balanceServices = {
    /**
     * Retrieves balance statistics grouped by categories.
     *
     * Endpoint:
     * GET /Balances/Categories
     *
     * @returns {Promise<ICategoryBalanceList>}
     * A list containing category balances and overall totals.
     */
    getAllCategoriesBalance: async (): Promise<ICategoryBalanceList> => {
        const response = await api.get("/Balances/Categories")
        return response.data
    },

    /**
     * Retrieves balance statistics grouped by people.
     *
     * Endpoint:
     * GET /Balances/People
     *
     * @returns {Promise<IPeopleBalanceList>}
     * A list containing people balances and overall totals.
     */
    getAllPeopleBalance: async (): Promise<IPeopleBalanceList> => {
        const response = await api.get("/Balances/People")
        return response.data
    },
}

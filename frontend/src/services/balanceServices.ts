
import type { ICategoryBalanceList } from "../types/ICategoryBalanceList.interface"
import type { IPeopleBalanceList } from "../types/IPeopleBalanceList.interface"
import { api } from "./api"

export const balanceServices = {
    // GET /api/Balances/Categories
    getAllCategoriesBalance: async (): Promise<ICategoryBalanceList> => {
        const response = await api.get("/Balances/Categories")
        return response.data
    },

    // GET /api/Balances/People
    getAllPeopleBalance: async (): Promise<IPeopleBalanceList> => {
        const response = await api.get("/Balances/People")
        return response.data
    },

}
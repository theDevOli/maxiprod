import axios, { type AxiosInstance, type AxiosResponse } from "axios"

const API_BASE = "http://localhost:5000/v1/api"

/**
 * Pre-configured Axios instance used throughout the application.
 */
export const api: AxiosInstance = axios.create({
    baseURL: API_BASE,
    headers: {
        "Content-Type": "application/json",
    },
    timeout: 10000,
})

/**
 * Axios response interceptor.
 */
api.interceptors.response.use(
    (response: AxiosResponse) => {
        return response
    },
    (error) => {
        console.error("❌ Response error:", {
            url: error.config?.url,
            status: error.response?.status,
            message: error.response?.data?.message || error.message,
        })

        if (error.response?.status === 404) {
            console.warn("⚠️ Endpoint não encontrado:", error.config.url)
        }

        return Promise.reject(error)
    }
)

export default api

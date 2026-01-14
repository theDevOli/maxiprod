// src/services/api.ts
import axios, {
    type AxiosInstance,
    type AxiosRequestConfig,
    type AxiosResponse,
} from "axios"

// URL direta da API
const API_BASE = "http://localhost:5000/v1/api"

// Interface para respostas da sua API
export interface ApiResponse<T = any> {
    data: T
    message?: string
    success: boolean
    statusCode: number
}

export const api: AxiosInstance = axios.create({
    baseURL: API_BASE,
    headers: {
        "Content-Type": "application/json",
    },
    timeout: 10000,
})

// Interceptor para responses
api.interceptors.response.use(
    (response: AxiosResponse) => {
        console.log(`✅ ${response.status} ${response.config.url}`)
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

// Função helper para fazer requests tipadas
export async function apiRequest<T = any>(
    config: AxiosRequestConfig
): Promise<ApiResponse<T>> {
    try {
        const response = await api(config)
        return {
            data: response.data,
            message: response.data?.message,
            success: true,
            statusCode: response.status,
        }
    } catch (error: any) {
        return {
            data: null as any,
            message: error.response?.data?.message || error.message,
            success: false,
            statusCode: error.response?.status || 500,
        }
    }
}

export default api

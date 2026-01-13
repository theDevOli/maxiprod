import {
    createContext,
    useContext,
    useState,
    type JSX,
    type ReactNode,
} from "react"

interface LoadingContextType {
    isLoading: boolean
    setIsLoading: (isLoading: boolean) => void
    startLoading: () => void
    stopLoading: () => void
}

const LoadingContext = createContext<LoadingContextType | undefined>(undefined)

export function useLoading(): LoadingContextType {
    const context = useContext(LoadingContext)
    if (!context) {
        throw new Error("useLoading must be used within LoadingProvider")
    }
    return context
}

interface LoadingProviderProps {
    children: ReactNode
}

export function LoadingProvider({
    children,
}: LoadingProviderProps): JSX.Element {
    const [isLoading, setIsLoading] = useState(false)

    const startLoading = () => setIsLoading(true)
    const stopLoading = () => setIsLoading(false)

    return (
        <LoadingContext.Provider
            value={{ isLoading, setIsLoading, startLoading, stopLoading }}>
            {children}
        </LoadingContext.Provider>
    )
}

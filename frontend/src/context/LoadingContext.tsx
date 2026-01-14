import {
    createContext,
    useContext,
    useState,
    type JSX,
    type ReactNode,
} from "react"

/**
 * Type definition for the LoadingContext.
 *
 * @typedef {Object} LoadingContextType
 * @property {boolean} isLoading - Indicates whether a loading process is active.
 * @property {(isLoading: boolean) => void} setIsLoading - Directly sets the loading state.
 * @property {() => void} startLoading - Sets `isLoading` to true.
 * @property {() => void} stopLoading - Sets `isLoading` to false.
 */
interface LoadingContextType {
    isLoading: boolean
    setIsLoading: (isLoading: boolean) => void
    startLoading: () => void
    stopLoading: () => void
}

const LoadingContext = createContext<LoadingContextType | undefined>(undefined)

/**
 * Custom hook to access the LoadingContext.
 *
 * This hook provides an easy way to read and modify the loading state
 * anywhere in the component tree wrapped by `LoadingProvider`.
 *
 * @throws Will throw an error if used outside a `LoadingProvider`.
 * @returns {LoadingContextType} The context object with loading state and helper functions.
 */
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

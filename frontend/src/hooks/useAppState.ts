import { useEffect, useCallback } from "react"
import { useNavigate, useLocation } from "react-router-dom"
import { LocalStorageService } from "../services/LocalStorageService"

/**
 * Custom hook for managing and restoring the application's state across routes.
 * It internally uses `LocalStorageService` to persist state in the browser's local storage.
 * @returns {Object} An object containing methods to interact with app state:
 * @property {() => {link: string, formData: any} | null} restoreState - Restores saved state if applicable and navigates to the saved route. Returns the restored state or null.
 * @property {(formData: any) => void} saveFormData - Saves form data to persistent storage.
 * @property {() => void} clearState - Clears all saved state from local storage.
 * @property {() => {link: string, formData: any} | null} getCurrentState - Returns the currently saved state without modifying it.
 */
export function useAppState() {
    const navigate = useNavigate()
    const location = useLocation()

    const storageService = new LocalStorageService()

    useEffect(() => {
        const route = location.pathname

        if (route === "/") return

        storageService.saveCurrentRoute(route)
    }, [location.pathname])

    /**
     * Restores the previously saved route and form data if restoration
     * conditions are met.
     *
     * When restored, navigation replaces the current history entry
     * and injects the saved form data into route state.
     *
     * @returns The restored state if navigation occurs, otherwise null.
     */
    const restoreState = useCallback(() => {
        const savedState = storageService.getState()

        if (storageService.shouldRestore() && savedState) {
            navigate(savedState.link, {
                replace: true,
                state: { formData: savedState.formData },
            })
            return savedState
        }
        storageService.markSession()

        return null
    }, [navigate, location.pathname])

    /**
     * Save form data to local storage.
     *
     * @param formData - Arbitrary form data to be saved.
     */
    const saveFormData = useCallback((formData: any) => {
        storageService.saveFormData(formData)
    }, [])

    /**
     * Clears all persisted application state from local storage.
     * NOTE: To be used in the future when login is implemented.
     */
    const clearState = useCallback(() => {
        storageService.clearState()
    }, [])

    return {
        restoreState,
        saveFormData,
        clearState,
        getCurrentState: storageService.getState.bind(storageService),
    }
}

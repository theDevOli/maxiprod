import type { IStorage } from "../types/IStorage.interface"

export class LocalStorageService {
    private readonly CACHE_KEY = "app_cache"
    private readonly SESSION_FLAG = "app_session"

    public saveState(value: IStorage): void {
        localStorage.setItem(this.CACHE_KEY, JSON.stringify(value))
    }

    public markSession(): void {
        if (!sessionStorage.getItem(this.SESSION_FLAG)) {
            sessionStorage.setItem(this.SESSION_FLAG, Date.now().toString())
        }
    }

    public getState(): IStorage | null {
        const item = localStorage.getItem(this.CACHE_KEY)
        return item ? JSON.parse(item) : null
    }

    public clearState(): void {
        localStorage.removeItem(this.CACHE_KEY)
        sessionStorage.removeItem(this.SESSION_FLAG)
    }

    public shouldRestore(): boolean {
        const session = sessionStorage.getItem(this.SESSION_FLAG)

        const hasSavedState = localStorage.getItem(this.CACHE_KEY) !== null

        return session === null && hasSavedState
    }

    public saveFormData(formData: any): void {
        const current = this.getState() || { link: "/", formData: null }

        current.formData = formData
        this.saveState(current)
    }

    public saveCurrentRoute(route: string): void {
        const current = this.getState() || { link: route, formData: null }

        current.link = route
        this.saveState(current)
    }
}

import "bootstrap/dist/css/bootstrap.min.css"
import NavBar from "./components/NavBar"
import Footer from "./components/Fotter"
import { Outlet } from "react-router-dom"
import { Spinner } from "./components/Spiner"
import { useLoading } from "./context/LoadingContext"
import { useEffect } from "react"
import { useAppState } from "./hooks/useAppState"

function App() {
    const { isLoading } = useLoading()
    const { restoreState } = useAppState()

    useEffect(() => {
        restoreState()
    }, [])

    return (
        <>
            {isLoading && <Spinner />}
            <div className="d-flex flex-column min-vh-100">
                <NavBar />

                <main className="flex-grow-1 w-100 p-5">
                    <Outlet />
                </main>

                <Footer />
            </div>
        </>
    )
}

export default App

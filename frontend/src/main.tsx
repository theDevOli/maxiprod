import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"

import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import "./styles/index.css"

import { createBrowserRouter, RouterProvider } from "react-router-dom"
import NewCategory from "./pages/Category/NewCategory.tsx"
import App from "./App.tsx"
import CategoryDashboard from "./pages/Category/CategoryDashboard.tsx"
import NewPerson from "./pages/Person/NewPerson.tsx"
import Home from "./pages/Home/Home.tsx"
import PersonDashboard from "./pages/Person/PersonDashboard.tsx"
import NewTransaction from "./pages/Transaction/NewTransaction.tsx"
import TransactionDashboard from "./pages/Transaction/TransactionDashboard.tsx"
import CategoryBalance from "./pages/Transaction/CategoryBalance.tsx"
import PersonBalance from "./pages/Transaction/PersonBalance.tsx"
import { NotFound } from "./pages/Error/NotFound.tsx"
import { LoadingProvider } from "./context/LoadingContext.tsx"

const router = createBrowserRouter([
    {
        element: <App />,
        children: [
            {
                path: "/",
                element: <Home />,
            },
            {
                path: "new-category",
                element: <NewCategory />,
            },
            {
                path: "category-dashboard",
                element: <CategoryDashboard />,
            },
            {
                path: "new-person",
                element: <NewPerson />,
            },
            {
                path: "person-dashboard",
                element: <PersonDashboard />,
            },
            {
                path: "new-transaction",
                element: <NewTransaction />,
            },
            {
                path: "transaction-dashboard",
                element: <TransactionDashboard />,
            },
            {
                path: "person-balance",
                element: <PersonBalance />,
            },
            {
                path: "category-balance",
                element: <CategoryBalance />,
            },
            {
                path: "*",
                element: <NotFound />,
            },
        ],
    },
])
createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <LoadingProvider>
            <RouterProvider router={router} />
        </LoadingProvider>
    </StrictMode>
)

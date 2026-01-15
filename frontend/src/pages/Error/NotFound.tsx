import { Link } from "react-router-dom"
import "../../styles/index.css"

/**
 * Displays a 404 - Not Found page.
 * It provides a message and a link to return
 * to the home page.
 */
export function NotFound() {
    return (
        <div className="not-found-container">
            <h1 className="not-found-code">404</h1>
            <h2 className="not-found-title">Página não encontrada</h2>
            <p className="not-found-text">
                A página que você tentou acessar não existe ou foi removida.
            </p>

            <Link to="/" className="not-found-link fs-4">
                Voltar para a página inicial
            </Link>
        </div>
    )
}

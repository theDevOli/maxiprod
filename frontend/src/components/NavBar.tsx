import { Link } from "react-router-dom"

/**
 * NavBar component for the application.
 *
 * This component renders a responsive navigation bar with dropdown menus
 * for Pessoas, Categorias, and Transações. 
 * Menu Structure:
 * - Pessoas
 *   - Cadastro → /new-person
 *   - Relatório de Pessoas → /person-dashboard
 * - Categorias
 *   - Cadastro → /new-category
 *   - Relatório de Categorias → /category-dashboard
 * - Transações
 *   - Cadastro → /new-transaction
 *   - Relatório de Transações → /transaction-dashboard
 *   - Balanço Por Pessoas → /person-balance
 *   - Balanço Por Categorias → /category-balance
 *
 * @component
 * @returns {JSX.Element} The navigation bar element for the top of the page.
 */
export default function NavBar() {
    return (
        <header>
            <nav className="nav-bar navbar navbar-expand-lg bg-body-tertiary sticky-top">
                <div className="logo">
                    <span role="img">🍿</span>
                    <h1>Popcorn</h1>
                </div>
                <div className="container-fluid">
                    <button
                        className="navbar-toggler"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div
                        className="collapse navbar-collapse"
                        id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <li className="nav-item dropdown">
                                <a
                                    className="nav-link dropdown-toggle fs-3 mx-3"
                                    role="button"
                                    data-bs-toggle="dropdown"
                                    aria-expanded="false">
                                    Pessoas
                                </a>
                                <ul className="dropdown-menu">
                                    <li>
                                        <Link
                                            to="new-person"
                                            className="dropdown-item fs-4">
                                            Cadastro
                                        </Link>
                                    </li>
                                    <li>
                                        <Link
                                            to="person-dashboard"
                                            className="dropdown-item fs-4">
                                            Relatório de Pessoas
                                        </Link>
                                    </li>
                                </ul>
                            </li>
                            <li className="nav-item dropdown">
                                <a
                                    className="nav-link dropdown-toggle fs-3 mx-3"
                                    role="button"
                                    data-bs-toggle="dropdown"
                                    aria-expanded="false">
                                    Categorias
                                </a>
                                <ul className="dropdown-menu">
                                    <li>
                                        <Link
                                            to="new-category"
                                            className="dropdown-item fs-4">
                                            Cadastro
                                        </Link>
                                    </li>
                                    <li>
                                        <Link
                                            to="category-dashboard"
                                            className="dropdown-item fs-4">
                                            Relatório de Categorias
                                        </Link>
                                    </li>
                                </ul>
                            </li>
                            <li className="nav-item dropdown">
                                <a
                                    className="nav-link dropdown-toggle fs-3 mx-3"
                                    role="button"
                                    data-bs-toggle="dropdown"
                                    aria-expanded="false">
                                    Transações
                                </a>
                                <ul className="dropdown-menu">
                                    <li>
                                        <Link
                                            to="new-transaction"
                                            className="dropdown-item fs-4">
                                            Cadastro
                                        </Link>
                                    </li>
                                    <li>
                                        <Link
                                            to="transaction-dashboard"
                                            className="dropdown-item fs-4">
                                            Relatório de Transações
                                        </Link>
                                    </li>
                                    <li>
                                        <Link
                                            to="person-balance"
                                            className="dropdown-item fs-4">
                                            Balanço Por Pessoas
                                        </Link>
                                    </li>
                                    <li>
                                        <Link
                                            to="category-balance"
                                            className="dropdown-item fs-4">
                                            Balanço Por Categorias
                                        </Link>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    )
}

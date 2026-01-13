export default function NavBar() {
    return (
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
                                href="#"
                                role="button"
                                data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Pessoas
                            </a>
                            <ul className="dropdown-menu">
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="new-person">
                                        Cadastro
                                    </a>
                                </li>
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="person-dashboard">
                                        Relatório de Pessoas
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li className="nav-item dropdown">
                            <a
                                className="nav-link dropdown-toggle fs-3 mx-3"
                                href="#"
                                role="button"
                                data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Categorias
                            </a>
                            <ul className="dropdown-menu">
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="new-category">
                                        Cadastro
                                    </a>
                                </li>
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="category-dashboard">
                                        Relatório de Categorias
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li className="nav-item dropdown">
                            <a
                                className="nav-link dropdown-toggle fs-3 mx-3"
                                href="#"
                                role="button"
                                data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Transações
                            </a>
                            <ul className="dropdown-menu">
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="new-transaction">
                                        Cadastro
                                    </a>
                                </li>
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="transaction-dashboard">
                                        Relatório de Transações
                                    </a>
                                </li>
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="lista-saldo">
                                        Balanço Por Pessoas
                                    </a>
                                </li>
                                <li>
                                    <a
                                        className="dropdown-item fs-4"
                                        href="person-balance">
                                        Balanço Por Categorias
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    )
}

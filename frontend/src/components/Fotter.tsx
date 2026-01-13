export default function Footer() {
    return (
        <footer className="footer bg-body-tertiary fixed-bottom text-center text-md-start py-4 mt-auto">
            <div className="container">
                <div className="row align-items-center">
                    <div className="col-md-6 mb-3 mb-md-0">
                        <span className="text-body-secondary">
                            © {new Date().getFullYear()} Popcorn
                        </span>
                    </div>

                    <div className="col-md-6 text-md-end">
                        <a href="#" className="text-body-secondary me-3">
                            Pessoas
                        </a>
                        <a href="#" className="text-body-secondary me-3">
                            Categorias
                        </a>
                        <a href="#" className="text-body-secondary">
                            Transações
                        </a>
                    </div>
                </div>
            </div>
        </footer>
    )
}

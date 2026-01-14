/**
 * Footer component for the application.
 *
 * This component renders a responsive footer.
 * @component
 * @returns {JSX.Element} The footer element to render at the bottom of the page.
 */
export default function Footer() {
    return (
        <footer className="bg-light text-center py-3 mt-auto border-top">
            <div className="container">
                <div className="row align-items-center">
                    <div className="col-md-6 mb-3 mb-md-0">
                        <span className="text-body-secondary">
                            © {new Date().getFullYear()} Popcorn
                        </span>
                    </div>
                </div>
            </div>
        </footer>
    )
}

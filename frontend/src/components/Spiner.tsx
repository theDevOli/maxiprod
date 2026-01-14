/**
 * Spinner component.
 *
 * This component renders a simple loading spinner.
 * @component
 * @returns {JSX.Element} The loading spinner element.
 */
export function Spinner() {
    return (
        <div className="loading-overlay">
            <span className="loader"></span>
        </div>
    )
}

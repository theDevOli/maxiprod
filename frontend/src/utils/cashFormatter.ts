/**
 * Formats a numeric value to Brazilian currency.
 * @param amount - Monetary value to be formatted.
 * @returns The formatted currency string (e.g. "R$ 1.234,56").
 */
export function cashFormatter(amount: number): string {
    return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
    }).format(amount)
}

import type { IBalance } from "./IBalance.interface"

/**
 * Represents the balance of a specific category.
 *
 * Extends the generic balance contract by adding
 * category-specific identification data.
 */
export interface ICategoryBalance extends IBalance {
    categoryDescription: string
}

/**
 * Represents the allowed goals for a category.
 *
 * - "despesa": Expense only
 * - "receita": Income only
 * - "ambas": Both income and expense
 */
export type CategoryGoal = {
    categoryGoal: "despesa" | "receita" | "ambas"
}

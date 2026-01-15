/**
 * Represents a Category domain contract.
 */
import type { CategoryGoal } from "./CategoryGoal.type"

export interface ICategory {
    categoryId: number
    categoryDescription: string
    categoryGoal: CategoryGoal
}

import type { CategoryGoal } from "../types/CategoryGoal.type"
import type { ICategory } from "../types/ICategory.interface"

/**
 * Class representing a Category.
 *
 * This class implements the `ICategory` interface and encapsulates
 * category information including ID, description, and goal.
 *
 * @implements {ICategory}
 */
export class Category implements ICategory {
    private readonly _categoryId: number
    private readonly _categoryDescription: string
    private readonly _categoryGoal: CategoryGoal

    /**
     * Creates a new Category instance.
     *
     * @param {number} categoryId - Unique identifier for the category.
     * @param {string} categoryDescription - Description of the category.
     * @param {CategoryGoal} categoryGoal - Goal type for the category.
     */
    constructor(
        categoryId: number,
        categoryDescription: string,
        categoryGoal: CategoryGoal
    ) {
        this._categoryId = categoryId
        this._categoryDescription = categoryDescription
        this._categoryGoal = categoryGoal
    }

    /**
     * Gets the category ID.
     * @returns {number} The category's unique identifier.
     */
    public get categoryId(): number {
        return this._categoryId
    }

    /**
     * Gets the category description.
     * @returns {string} The category description.
     */
    public get categoryDescription(): string {
        return this._categoryDescription
    }

    /**
     * Gets the category goal as the raw value.
     * @returns {CategoryGoal} The category goal.
     */
    public get categoryGoal(): CategoryGoal {
        return this._categoryGoal
    }

    /**
     * Gets the category goal as a formatted string with the first letter capitalized.
     *
     * @returns {string} Formatted category goal.
     */
    public get goal(): string {
        const goal = String(this._categoryGoal)
        const len = goal.length

        return `${goal.substring(0, 1).toLocaleUpperCase()}${goal.substring(
            1,
            len
        )}`
    }

    /**
     * Factory method to create a Category instance from a single ICategory object.
     *
     * @param {ICategory} category - An object implementing the ICategory interface.
     * @returns {Category} A new Category instance.
     */
    public static fromSingleInterface(category: ICategory): Category {
        return new Category(
            category.categoryId,
            category.categoryDescription,
            category.categoryGoal
        )
    }

    /**
     * Factory method to create multiple Category instances from an array of ICategory objects.
     *
     * @param {ICategory[]} categories - Array of objects implementing the ICategory interface.
     * @returns {Category[]} Array of Category instances.
     */
    public static fromBulkInterface(categories: ICategory[]): Category[] {
        return categories.map((c) => this.fromSingleInterface(c))
    }
}

import { useEffect, useState } from "react"
import { useLoading } from "../../context/LoadingContext"
import { balanceServices } from "../../services/balanceServices"
import { CategoryBalanceList } from "../../models/CategoryBalanceList"
import type { IPieDataItem } from "../../types/IPieDataItem.interface"
import { PieChart } from "../../components/PieChart"

/**
 * Displays balance information grouped by category.
 */
export default function CategoryBalance() {
    const { startLoading, stopLoading } = useLoading()
    const [categories, setCategories] = useState<CategoryBalanceList>()
    const [incomes, setIncomes] = useState<IPieDataItem[]>([])
    const [expenses, setExpenses] = useState<IPieDataItem[]>([])

    useEffect(() => {
        getCategories()
    }, [])

    /**
     * Retrieves balance data grouped by category, converts it
     * into domain models, and prepares chart data.
     */
    async function getCategories(): Promise<void> {
        try {
            startLoading()

            const tempBalance = await balanceServices.getAllCategoriesBalance()
            const data = CategoryBalanceList.fromSingleInterface(tempBalance)

            const tempIncomes: IPieDataItem[] = data.categories.map((c) => {
                const value = (c.income / data.totalStatistic.income) * 100
                return {
                    label: c.categoryDescription,
                    value,
                }
            })

            const tempExpenses: IPieDataItem[] = data.categories.map((c) => {
                const value = (c.expense / data.totalStatistic.expense) * 100
                return {
                    label: c.categoryDescription,
                    value,
                }
            })

            setCategories(data)
            setIncomes(tempIncomes)
            setExpenses(tempExpenses)
        } catch (err: any) {
            console.error(err)
            window.alert(
                "Error ao pegar dados do banco de dados. Por favor tente novamente!"
            )
        } finally {
            stopLoading()
        }
    }
    return (
        <div className="container-fluid p-4">
            <div className="row g-4 align-items-stretch">
                <div className="col-12 col-lg-4">
                    <div className="d-flex flex-column gap-4">
                        <div className="bg-secondary shadow rounded-4 h-100 d-flex flex-column align-items-center fs-3 fw-bold p-4">
                            <h1 className="mb-4 text-center">
                                Receita (%) por Categoria
                            </h1>
                            <PieChart data={incomes} />
                        </div>

                        <div className="bg-secondary shadow rounded-4 h-100 d-flex flex-column align-items-center fs-3 fw-bold p-4">
                            <h1 className="mb-4 text-center">
                                Despesa (%) por Categoria
                            </h1>
                            <PieChart data={expenses} />
                        </div>
                    </div>
                </div>

                <div className="col-12 col-lg-8">
                    <div className="bg-secondary shadow rounded-4 h-100 d-flex flex-column align-items-center fs-3 fw-bold p-4">
                        <h1 className="mb-4 text-center">
                            Lista de Saldo por Categoria
                        </h1>

                        <table className="table table-dark table-striped table-hover w-100">
                            <thead>
                                <tr>
                                    <th scope="col">Nome</th>
                                    <th scope="col">Receita</th>
                                    <th scope="col">Despesa</th>
                                    <th scope="col">Saldo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {categories?.categories.map((c, i) => (
                                    <tr key={i}>
                                        <td>{c.categoryDescription}</td>
                                        <td>{c.getIncome()}</td>
                                        <td>{c.getExpense()}</td>
                                        <td>{c.getBalance()}</td>
                                    </tr>
                                ))}
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th scope="col">Total</th>
                                    <th scope="col">
                                        {categories?.totalStatistic.getIncome()}
                                    </th>
                                    <th scope="col">
                                        {categories?.totalStatistic.getExpense()}
                                    </th>
                                    <th scope="col">
                                        {categories?.totalStatistic.getBalance()}
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    )
}

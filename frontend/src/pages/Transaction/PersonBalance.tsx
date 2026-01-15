import { useEffect, useState } from "react"
import { useLoading } from "../../context/LoadingContext"
import { balanceServices } from "../../services/balanceServices"
import type { IPieDataItem } from "../../types/IPieDataItem.interface"
import { PieChart } from "../../components/PieChart"
import { PeopleBalanceList } from "../../models/PeopleBalanceList"

/**
 * Displays financial balance statistics grouped by people.
 */
export default function PersonBalance() {
    const { startLoading, stopLoading } = useLoading()
    const [people, setPeople] = useState<PeopleBalanceList>()
    const [incomes, setIncomes] = useState<IPieDataItem[]>([])
    const [expenses, setExpenses] = useState<IPieDataItem[]>([])

    useEffect(() => {
        getPeople()
    }, [])

    /**
     * Fetches balance data grouped by people from the backend
     * and prepares data for pie chart visualization.
     */
    async function getPeople(): Promise<void> {
        try {
            startLoading()

            const tempBalance = await balanceServices.getAllPeopleBalance()
            const data = PeopleBalanceList.fromSingleInterface(tempBalance)
            console.log(data)

            const tempIncomes: IPieDataItem[] = data.people.map((p) => {
                const value = (p.income / data.totalStatistic.income) * 100
                return {
                    label: p.personName,
                    value,
                }
            })

            const tempExpenses: IPieDataItem[] = data.people.map((p) => {
                const value = (p.expense / data.totalStatistic.expense) * 100
                return {
                    label: p.personName,
                    value,
                }
            })

            setPeople(data)
            setIncomes(tempIncomes)
            setExpenses(tempExpenses)
        } catch (err: any) {
            console.error(err)
            window.alert("Erro ao pegar dados do banco de dados!")
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
                                Receita (%) por pessoas
                            </h1>
                            <PieChart data={incomes} />
                        </div>

                        <div className="bg-secondary shadow rounded-4 h-100 d-flex flex-column align-items-center fs-3 fw-bold p-4">
                            <h1 className="mb-4 text-center">
                                Despesa (%) por pessoa
                            </h1>
                            <PieChart data={expenses} />
                        </div>
                    </div>
                </div>

                <div className="col-12 col-lg-8">
                    <div className="bg-secondary shadow rounded-4 h-100 d-flex flex-column align-items-center fs-3 fw-bold p-4">
                        <h1 className="mb-4 text-center">
                            Lista de Saldo por Pessoas
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
                                {people?.people.map((p, i) => (
                                    <tr key={i}>
                                        <td>{p.personName}</td>
                                        <td>{p.getIncome()}</td>
                                        <td>{p.getExpense()}</td>
                                        <td>{p.getBalance()}</td>
                                    </tr>
                                ))}
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th scope="col">Total</th>
                                    <th scope="col">
                                        {people?.totalStatistic.getIncome()}
                                    </th>
                                    <th scope="col">
                                        {people?.totalStatistic.getExpense()}
                                    </th>
                                    <th scope="col">
                                        {people?.totalStatistic.getBalance()}
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

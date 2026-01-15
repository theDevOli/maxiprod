import { useEffect, useState } from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faTrashCan } from "@fortawesome/free-solid-svg-icons"
import { Link } from "react-router-dom"
import { useLoading } from "../../context/LoadingContext"
import { transactionServices } from "../../services/transactionServices"
import { Transaction } from "../../models/Transaction"
import { personServices } from "../../services/personServices"
import { categoryServices } from "../../services/categoryServices"
import { cashFormatter } from "../../utils/cashFormatter"

/**
 * Displays a dashboard with all registered financial transactions.
 */
export default function TransactionDashboard() {
    const [transactions, setTransactions] = useState<Transaction[]>([])
    const { startLoading, stopLoading } = useLoading()

    const totalIncome: number = transactions.filter(
        (i) => i.type.toLocaleLowerCase() === "receita"
    ).length
    const totalExpense: number = transactions.filter(
        (i) => i.type.toLocaleLowerCase() === "despesa"
    ).length

    const incomeAvg: number =
        transactions.reduce((sum, i) => {
            if (i.type.toLocaleLowerCase() === "receita") return sum + i.amount

            return sum
        }, 0) / totalIncome

    const expenseAvg: number =
        transactions.reduce((sum, e) => {
            if (e.type.toLocaleLowerCase() === "despesa") return sum + e.amount

            return sum
        }, 0) / totalExpense

    useEffect(() => {
        getTransaction()
    }, [])

    /**
     * Fetches transactions, people, and categories from the backend
     * and converts them into Transaction model instances.
     */
    async function getTransaction(): Promise<void> {
        try {
            startLoading()
            const [transactions, people, categories] = await Promise.all([
                transactionServices.getAll(),
                personServices.getAll(),
                categoryServices.getAll(),
            ])

            const data = Transaction.fromBulkInterface(
                transactions,
                people,
                categories
            )

            setTransactions(data)
        } catch (err: any) {
            console.error(err)
            window.alert("Error ao pegar dados do banco de dados!")
        } finally {
            stopLoading()
        }
    }

    async function deleteTransaction(transactionId: number): Promise<void> {
        try {
            startLoading()
            await transactionServices.delete(transactionId)

            setTransactions((transaction) =>
                transaction.filter((t) => t.transactionId != transactionId)
            )
            window.alert("Transação removida do banco de dados com sucesso!")
        } catch (err: any) {
            window.alert("Error ao deletar transação do banco de dados!")
            console.error(err)
        } finally {
            stopLoading()
        }
    }

    if (transactions.length === 0)
        return (
            <>
                <h1>Sem dados no banco de dados.</h1>
                <h2 className="my-5">
                    Por favor insira uma nova transação em:
                </h2>
                <Link
                    to="/new-transaction"
                    className="not-found-link fs-5 my-5">
                    Cadastro
                </Link>
            </>
        )

    return (
        <>
            <h1 className="mb-4 text-center">Lista de Transações</h1>
            <table className="table table-dark table-striped table-hover text-center align-middle">
                <thead>
                    <tr>
                        <th scope="col">Descrição</th>
                        <th scope="col">Valor</th>
                        <th scope="col">Tipo de Transação</th>
                        <th scope="col">Categoria</th>
                        <th scope="col">Pessoa</th>
                        <th scope="col">Ação</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((t) => (
                        <tr key={t.transactionId}>
                            <td>{t.transactionDescription}</td>
                            <td>{t.cash}</td>
                            <td>{t.type}</td>
                            <td>{t.category}</td>
                            <td>{t.person}</td>
                            <td>
                                <button
                                    className="btn btn-danger btn-sm mx-2"
                                    onClick={() =>
                                        deleteTransaction(t.transactionId)
                                    }>
                                    <FontAwesomeIcon icon={faTrashCan} />
                                    Excluir
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="row mt-4">
                <div className="col-md-4 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">Total de Transações</h5>
                            <p className="card-text display-6">
                                {transactions.length}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">
                                Valor Médio de Despesas
                            </h5>
                            <p className="card-text display-6">
                                {transactions.length > 0
                                    ? cashFormatter(expenseAvg)
                                    : cashFormatter(0)}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">
                                Valor Médio de Receita
                            </h5>
                            <p className="card-text display-6">
                                {transactions.length > 0
                                    ? cashFormatter(incomeAvg)
                                    : cashFormatter(0)}
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

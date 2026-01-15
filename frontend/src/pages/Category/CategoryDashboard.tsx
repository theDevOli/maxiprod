import { useEffect, useState } from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faTrashCan } from "@fortawesome/free-solid-svg-icons"
import { Link } from "react-router-dom"
import { useLoading } from "../../context/LoadingContext"
import { categoryServices } from "../../services/categoryServices"
import { Category } from "../../models/Category"

/**
 * Displays the category dashboard.
 *
 * Responsible for fetching categories, rendering the category table,
 * handling deletions, and showing summary statistics.
 */
export default function CategoryDashboard() {
    const [categories, setCategories] = useState<Category[]>([])
    const { startLoading, stopLoading } = useLoading()

    useEffect(() => {
        getCategories()
    }, [])

    /**
     * Fetches all categories from the API and maps them
     * into Category domain instances.
     */
    async function getCategories(): Promise<void> {
        try {
            startLoading()
            const tempCategories = await categoryServices.getAll()
            const data = Category.fromBulkInterface(tempCategories)
            console.log(data)
            setCategories(data)
        } catch (err: any) {
            console.error(err)
            window.alert(
                "Error em pegar os dados do banco de dados. Faça o reload da página, por favor. "
            )
        } finally {
            stopLoading()
        }
    }

    /**
     * Deletes a category by its identifier and updates
     * the local state accordingly.
     *
     * @param categoryId - Identifier of the category to delete
     */
    async function deleteCategory(categoryId: number): Promise<void> {
        try {
            startLoading()
            await categoryServices.delete(categoryId)

            setCategories((category) =>
                category.filter((c) => c.categoryId != categoryId)
            )

            window.alert("Operação concluida com sucesso!")
        } catch (err: any) {
            window.alert("Erro ao deletar usuário!")
            console.error(err)
        } finally {
            stopLoading()
        }
    }

    if (categories.length === 0)
        return (
            <>
                <h1 className="mt-5">Sem dados no banco de dados.</h1>
                <h2 className="my-5">Por favor insira categorieas em:</h2>
                <Link to="/new-category" className="not-found-link fs-5 my-5">
                    Cadastro
                </Link>
            </>
        )

    return (
        <>
            <h1 className="mb-4 text-center">Lista de Categorias</h1>
            <table className="table table-dark table-striped table-hover text-center align-middle">
                <thead>
                    <tr>
                        <th scope="col">Descrição</th>
                        <th scope="col">Finalidade</th>
                        <th scope="col">Ação</th>
                    </tr>
                </thead>
                <tbody>
                    {categories.map((c) => (
                        <tr key={c.categoryId}>
                            <td>{c.categoryDescription}</td>
                            <td>{c.goal}</td>
                            <td>
                                <button
                                    className="btn btn-danger btn-sm mx-2"
                                    onClick={() =>
                                        deleteCategory(c.categoryId)
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
                            <h5 className="card-title">Total de Categorias</h5>
                            <p className="card-text display-6">
                                {categories.length}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">
                                Quantidade de Despesas
                            </h5>
                            <p className="card-text display-6">
                                {categories.length > 0
                                    ? categories.reduce((sum, c) => {
                                          if (
                                              c.goal.toLocaleLowerCase() ===
                                                  "ambas" ||
                                              c.goal.toLocaleLowerCase() ===
                                                  "despesa"
                                          )
                                              return sum + 1

                                          return sum
                                      }, 0)
                                    : 0}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">
                                Quantidade de Receita
                            </h5>
                            <p className="card-text display-6">
                                {categories.length > 0
                                    ? categories.reduce((sum, c) => {
                                          if (
                                              c.goal.toLocaleLowerCase() ===
                                                  "ambas" ||
                                              c.goal.toLocaleLowerCase() ===
                                                  "receita"
                                          )
                                              return sum + 1

                                          return sum
                                      }, 0)
                                    : 0}
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

import { useEffect, useState } from "react"
import { personServices } from "../../services/personServices"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faTrashCan } from "@fortawesome/free-solid-svg-icons"
import { Link } from "react-router-dom"
import { useLoading } from "../../context/LoadingContext"
import { Person } from "../../models/Person"

/**
 * Dashboard component responsible for listing and managing people.
 */
export default function PersonDashboard() {
    const [person, setPerson] = useState<Person[]>([])
    const { startLoading, stopLoading } = useLoading()

    useEffect(() => {
        getPeople()
    }, [])

    /**
     * Retrieves all people from the backend API and maps them
     * into domain models.
     */
    async function getPeople(): Promise<void> {
        try {
            startLoading()

            const tempPeople = await personServices.getAll()
            const data = Person.fromBulkInterface(tempPeople)

            setPerson(data)
        } catch (err: any) {
            window.alert(
                "Não foi possivel pegar os dados do banco de dados. Por favor tente mais tarde!"
            )
            console.error(err)
        } finally {
            stopLoading()
        }
    }

    /**
     * Deletes a person by ID and updates the local state.
     *
     * @param personId - Unique identifier of the person to be deleted.
     */
    async function deletePerson(personId: number): Promise<void> {
        try {
            startLoading()
            await personServices.delete(personId)

            setPerson((person) => person.filter((p) => p.personId != personId))
            window.alert("Usuário removido do banco de dados com sucesso!")
        } catch (err: any) {
            window.alert(
                "Não foi possível remover o usuário do banco de dados!"
            )
            console.error(err)
        } finally {
            stopLoading()
        }
    }

    if (person.length === 0)
        return (
            <>
                <h1>Sem dados no banco de dados.</h1>
                <h2 className="my-5">Por favor insira pessoas em:</h2>
                <Link to="/new-person" className="not-found-link fs-5 my-5">
                    Cadastro
                </Link>
            </>
        )

    return (
        <>
            <h1 className="mb-4 text-center">Lista de Pessoas</h1>
            <table className="table table-dark table-striped table-hover text-center align-middle">
                <thead>
                    <tr>
                        <th scope="col">Nome</th>
                        <th scope="col">Idade</th>
                        <th scope="col">Ação</th>
                    </tr>
                </thead>
                <tbody>
                    {person.map((p) => (
                        <tr key={p.personId}>
                            <td>{p.personName}</td>
                            <td>{p.age}</td>
                            <td>
                                <button
                                    className="btn btn-danger btn-sm mx-2"
                                    onClick={() => deletePerson(p.personId)}>
                                    <FontAwesomeIcon icon={faTrashCan} />
                                    Excluir
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="row mt-4">
                <div className="col-md-3 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">Total de Pessoas</h5>
                            <p className="card-text display-6">
                                {person.length}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-3 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">Média de Idade</h5>
                            <p className="card-text display-6">
                                {person.length > 0
                                    ? Math.round(
                                          person.reduce(
                                              (sum, p) => sum + p.age,
                                              0
                                          ) / person.length
                                      )
                                    : 0}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-3 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">Mais Novo</h5>
                            <p className="card-text display-6">
                                {person.length > 0
                                    ? Math.min(...person.map((p) => p.age))
                                    : 0}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="col-md-3 mb-3">
                    <div className="card border-dark">
                        <div className="card-body text-center">
                            <h5 className="card-title">Mais Velho</h5>
                            <p className="card-text display-6">
                                {person.length > 0
                                    ? Math.max(...person.map((p) => p.age))
                                    : 0}
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div />
        </>
    )
}

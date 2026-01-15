import { useEffect, useState } from "react"
import type { IPerson } from "../../types/IPerson.interface"
import { personServices } from "../../services/personServices"
import { useLoading } from "../../context/LoadingContext"
import { useAppState } from "../../hooks/useAppState"

/**
 * Returns the initial state for the person form.
 *
 * @returns An object containing empty form fields.
 */
function getInitialState(): FormData {
    return { personName: "", age: "" }
}

/**
 * Returns the initial state for the person form.
 *
 * @returns An object containing empty form fields.
 */
type FormData = { personName: string; age: string }

/**
 * Component responsible for registering a new person.
 */
export default function NewPerson() {
    const { saveFormData, getCurrentState } = useAppState()

    const storedForm = getCurrentState()?.formData

    const { startLoading, stopLoading } = useLoading()
    const [formData, setFormData] = useState<FormData>(
        storedForm ?? getInitialState()
    )

    useEffect(() => {
        if (formData !== null) {
            saveFormData(formData)
        }
    }, [formData, saveFormData])

    /**
     * Handles changes for all form input fields.
     *
     * @param e - Input change event.
     */
    function handleInputsChange(e: React.ChangeEvent<HTMLInputElement>): void {
        const { name, value } = e.target

        setFormData((prev) => {
            return {
                ...prev,
                [name]: value,
            }
        })

        saveFormData(formData)
    }

    /**
     * Handles form submission and sends the person data to the API.
     *
     * @param e - Form submit event.
     */
    async function handlePost(
        e: React.FormEvent<HTMLFormElement>
    ): Promise<void> {
        e.preventDefault()

        const payload: Omit<IPerson, "personId"> = {
            ...formData,
            age: Number(formData.age),
        }

        setFormData(getInitialState())

        try {
            startLoading()
            await personServices.create(payload)
            saveFormData(formData)
            window.alert(
                `${payload.personName} foi adicionada com sucesso no banco de dados!`
            )
        } catch (err: any) {
            console.error(err)
            window.alert(
                `Não foi possivel adicionar ${payload.personName} ao banco de dados!`
            )
        } finally {
            stopLoading()
        }
    }

    /**
     * Resets the form to its initial state.
     */
    function handleCancel(): void {
        setFormData(getInitialState())
    }

    return (
        <form className="container-fluid mt-5" onSubmit={(e) => handlePost(e)}>
            <div className="row justify-content-center">
                <div className="col-12 col-lg-10 col-xl-9 col-xxl-8">
                    <div className="box p-5 p-md-5 shadow-lg rounded-4">
                        <h1 className="text-center mb-5 fw-bold">
                            Cadastro de Pessoa
                        </h1>
                        <div className="mb-5">
                            <label
                                htmlFor="personName"
                                className="form-label fs-4 fw-semibold">
                                Nome
                            </label>
                            <input
                                value={formData.personName}
                                onChange={(e) => handleInputsChange(e)}
                                type="text"
                                className="form-control form-control-lg py-4 fs-5"
                                id="personName"
                                name="personName"
                                placeholder="Digite o nome"
                                required
                                minLength={3}
                                maxLength={60}
                                pattern="^[A-Za-zÀ-ÿ\s]+$"
                                title="O nome deve conter apenas letras e espaços"
                            />
                        </div>
                        <div className="mb-5">
                            <label
                                htmlFor="age"
                                className="form-label fs-4 fw-semibold">
                                Idade
                            </label>
                            <input
                                value={formData.age}
                                onChange={(e) => handleInputsChange(e)}
                                type="number"
                                className="form-control form-control-lg py-4 fs-5"
                                id="age"
                                name="age"
                                placeholder="Digite a idade"
                                required
                                min={0}
                                max={120}
                            />
                        </div>
                        <div className="d-flex gap-4 mt-4">
                            <button
                                type="submit"
                                className="btn btn-primary btn-lg w-100 py-3 fs-4">
                                Salvar
                            </button>
                            <button
                                type="button"
                                onClick={handleCancel}
                                className="btn btn-danger btn-lg w-100 py-3 fs-4">
                                Cancelar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    )
}

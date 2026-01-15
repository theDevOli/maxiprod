import { useEffect, useState } from "react"
import { useLoading } from "../../context/LoadingContext"
import { categoryServices } from "../../services/categoryServices"
import { useAppState } from "../../hooks/useAppState"

/**
 * Returns the initial state for the category form.
 *
 * @returns An object containing empty category form fields.
 */
function getInitialState() {
    return {
        categoryDescription: "",
        categoryGoal: "",
    }
}

/**
 * Represents the form data structure for creating a category.
 */
type FormData = { categoryDescription: string; categoryGoal: string }

/**
 * Component responsible for creating a new category.
 */
export default function NewCategory() {
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
     * Handles changes for all form inputs.
     * @param e - Input or select change event.
     */
    function handleInputsChange(
        e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
    ): void {
        const { name, value } = e.target

        setFormData((prev) => {
            return {
                ...prev,
                [name]: value,
            }
        })
    }

    /**
     * Handles form submission and sends the category data to the API.
     * @param e - Form submit event.
     */
    async function handlePost(
        e: React.FormEvent<HTMLFormElement>
    ): Promise<void> {
        e.preventDefault()

        const payload = { ...formData }

        setFormData(getInitialState())

        try {
            startLoading()
            await categoryServices.create(payload)
            window.alert("Categoria criada com sucesso!")
        } catch (err: any) {
            window.alert(
                `Não foi possível criar a categoria ${payload.categoryDescription}!`
            )
            console.error(err)
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
                <div className="col-12 col-md-8 col-lg-6 col-xl-5">
                    <div className="box p-5 p-md-5 shadow-lg rounded-4">
                        <h1 className="text-center mb-5 fw-bold">
                            Cadastro de Categoria
                        </h1>
                        <div className="mb-5">
                            <label
                                htmlFor="categoryDescription"
                                className="form-label fs-4 fw-semibold">
                                Categoria
                            </label>
                            <input
                                value={formData.categoryDescription}
                                onChange={(e) => handleInputsChange(e)}
                                type="text"
                                className="form-control form-control-lg py-4 fs-3"
                                id="categoryDescription"
                                name="categoryDescription"
                                placeholder="Digite a categoria"
                                required
                                minLength={3}
                                maxLength={60}
                                pattern="^[A-Za-zÀ-ÿ\s]+$"
                                title="A categoria deve conter apenas letras e espaços"
                            />
                        </div>
                        <div className="mb-5">
                            <label
                                htmlFor="categoryGoal"
                                className="form-label fs-4 fw-semibold">
                                Finalidade
                            </label>
                            <select
                                value={formData.categoryGoal}
                                onChange={(e) => handleInputsChange(e)}
                                className="form-control form-control-lg py-4 fs-3"
                                id="categoryGoal"
                                name="categoryGoal"
                                required>
                                <option value={""}>
                                    Selecione a finalidade da categoria!
                                </option>
                                <option value={"ambas"}>Ambas</option>
                                <option value={"receita"}>Receita</option>
                                <option value={"despesa"}>Despesa</option>
                            </select>
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

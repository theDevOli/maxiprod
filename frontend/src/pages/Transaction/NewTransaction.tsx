import { useEffect, useState } from "react"
import { useLoading } from "../../context/LoadingContext"
import { personServices } from "../../services/personServices"
import { categoryServices } from "../../services/categoryServices"
import { Category } from "../../models/Category"
import type { IPerson } from "../../types/IPerson.interface"
import { transactionServices } from "../../services/transactionServices"
import { useAppState } from "../../hooks/useAppState"

/**
 * Returns the initial empty state for the transaction form.
 */
function getInitialState() {
    return {
        transactionDescription: "",
        amount: "",
        transactionType: "",
        categoryId: "",
        personId: "",
    }
}

/**
 * Returns the initial empty state for the transaction form.
 */
type FormData = {
    transactionDescription: string
    amount: string
    transactionType: string
    categoryId: string
    personId: string
}

/**
 * Transaction creation page.
 */
export default function NewTransaction() {
    const { saveFormData, getCurrentState } = useAppState()

    const storedForm = getCurrentState()?.formData

    const { startLoading, stopLoading } = useLoading()
    const [formData, setFormData] = useState<FormData>(
        storedForm ?? getInitialState()
    )

    const [people, setPeople] = useState<IPerson[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const ADULT_AGE = 18

    useEffect(() => {
        if (formData !== null) {
            saveFormData(formData)
        }
    }, [formData, saveFormData])

    useEffect(() => {
        getEntities()
    }, [])

    /**
     * Fetches people and categories concurrently.
     */
    async function getEntities(): Promise<void> {
        try {
            startLoading()
            const [tempPeople, tempCategories] = await Promise.all([
                personServices.getAll(),
                categoryServices.getAll(),
            ])

            setPeople(tempPeople)
            setCategories(Category.fromBulkInterface(tempCategories))
        } catch (err: any) {
            console.error(err)
            window.alert(
                "Error para pegar todas as categorias no banco de dados. Por favor, tente novamente!"
            )
        } finally {
            stopLoading()
        }
    }

    /**
     * Returns the age of a person based on their ID.
     * Defaults to adult age if not found.
     */
    function getAge(personId: number): number {
        const age = people.find((p) => p.personId === personId)?.age

        if (!age) return ADULT_AGE

        return age
    }

    /**
     * Handles updates for all form inputs and selects.
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
     * Submits the transaction form to the backend.
     */
    async function handlePost(
        e: React.FormEvent<HTMLFormElement>
    ): Promise<void> {
        e.preventDefault()

        const payload = {
            ...formData,
            amount: Number(formData.amount),
            categoryId: Number(formData.categoryId),
            personId: Number(formData.personId),
        }
        try {
            await transactionServices.create(payload)
            window.alert("Tansação adicionada com sucesso ao banco de dados!")
        } catch (err: any) {
            console.error(err)
            window.alert(
                "Erro ao adicionar transação ao banco de dados. Por favor, tente novamente!"
            )
        } finally {
            stopLoading()
        }
    }

    /**
     * Handles updates for all form inputs and selects.
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
                            Cadastro de Transações
                        </h1>

                        <div className="mb-5">
                            <label
                                htmlFor="transactionDescription"
                                className="form-label fs-5 fw-semibold">
                                Descrição da Transição
                            </label>
                            <input
                                value={formData.transactionDescription}
                                onChange={(e) => handleInputsChange(e)}
                                type="text"
                                className="form-control form-control-lg py-4 fs-3"
                                id="transactionDescription"
                                name="transactionDescription"
                                placeholder="Digite a descrição da transação"
                                required
                                minLength={3}
                                maxLength={60}
                                pattern="^[A-Za-zÀ-ÿ\s]+$"
                                title="A transação deve conter apenas letras e espaços"
                            />
                        </div>

                        <div className="mb-5">
                            <label
                                htmlFor="amount"
                                className="form-label fs-5 fw-semibold">
                                Valor da Transação
                            </label>
                            <input
                                value={formData.amount}
                                onChange={(e) => handleInputsChange(e)}
                                type="number"
                                className="form-control form-control-lg py-4 fs-3"
                                id="amount"
                                name="amount"
                                placeholder="Digite o valor da transação"
                                required
                                min={0}
                            />
                        </div>

                        <div className="mb-5">
                            <label
                                htmlFor="transactionType"
                                className="form-label fs-5 fw-semibold">
                                Tipo de Transição
                            </label>
                            <select
                                value={formData.transactionType}
                                onChange={(e) => handleInputsChange(e)}
                                className="form-control form-control-lg py-4 fs-3"
                                id="transactionType"
                                name="transactionType"
                                required>
                                <option value={""}>
                                    Selecione um tipo de transição!
                                </option>
                                {getAge(Number(formData?.personId)) >=
                                    ADULT_AGE && (
                                    <option value={"receita"}>Receita</option>
                                )}
                                <option value={"despesa"}>Despesa</option>
                            </select>
                        </div>

                        <div className="mb-5">
                            <label
                                htmlFor="categoryId"
                                className="form-label fs-5 fw-semibold">
                                Categoria
                            </label>
                            <select
                                value={formData.categoryId}
                                onChange={(e) => handleInputsChange(e)}
                                className="form-control form-control-lg py-4 fs-3"
                                id="categoryId"
                                name="categoryId"
                                required>
                                <option value={""}>
                                    Selecione uma categoria!
                                </option>
                                {categories
                                    .filter((c) => {
                                        return (
                                            c.goal.toLocaleLowerCase() ===
                                                formData.transactionType.toLocaleLowerCase() ||
                                            c.goal.toLocaleLowerCase() ===
                                                "ambas"
                                        )
                                    })
                                    .map((c) => (
                                        <option
                                            value={c.categoryId}
                                            key={c.categoryId}>
                                            {c.categoryDescription}
                                        </option>
                                    ))}
                            </select>
                        </div>

                        <div className="mb-5">
                            <label
                                htmlFor="personId"
                                className="form-label fs-5 fw-semibold">
                                Pessoa
                            </label>
                            <select
                                value={formData.personId}
                                onChange={(e) => handleInputsChange(e)}
                                className="form-control form-control-lg py-4 fs-3"
                                id="personId"
                                name="personId"
                                required>
                                <option value={""}>Selecione uma pessoa</option>
                                {people.map((p) => (
                                    <option value={p.personId}>
                                        {p.personName}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className="d-flex gap-4 mt-4">
                            <button
                                type="submit"
                                className="btn btn-primary btn-lg w-100 py-3 fs-5">
                                Salvar
                            </button>
                            <button
                                type="button"
                                onClick={handleCancel}
                                className="btn btn-danger btn-lg w-100 py-3 fs-5">
                                Cancelar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    )
}

/**
 * Home page of the application.
 * This component presents an overview of the Popcorn application,
 * explaining its purpose and main features.
 */
export default function Home() {
    return (
        <div className="text-center mb-5">
            <h1 className="mb-4">🍿 Popcorn</h1>

            <p>
                O <strong>Popcorn</strong> é uma aplicação desenvolvida para
                facilitar o controle e a gestão de transações financeiras de
                forma simples, organizada e eficiente.
            </p>

            <p>
                Com o Popcorn, você pode cadastrar categorias, pessoas e
                transações, acompanhar saldos e manter uma visão clara da sua
                movimentação financeira, ajudando na tomada de decisões e no
                controle do dia a dia.
            </p>

            <p className="mt-4">
                Utilize o menu acima para começar a gerenciar suas informações.
            </p>
        </div>
    )
}

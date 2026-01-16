# 🍿 Popcorn

**Popcorn** é um sistema de **controle de gastos residenciais**, criado para auxiliar no gerenciamento financeiro do dia a dia da residência.

---

## 🚀 Funcionalidades

O sistema permite:

-   O cadastro de **pessoas**, **categorias** e **transações**;
-   A listagem de **pessoas**, **categorias** e **transações**;
-   A exclusão de **pessoas**;
-   Relatório de saldo por **pessoa**;
-   Relatório de saldo por **categoria**;

---

## 🧩 Modelagem das Entidades

### 👤 Pessoa

-   **Identificador**: valor único gerado automaticamente
-   **Nome**: texto
-   **Idade**: número inteiro positivo

---

### 🗂️ Categoria

-   **Identificador**: valor único gerado automaticamente
-   **Descrição**: texto
-   **Finalidade**:
    -   `despesa`
    -   `receita`
    -   `ambas`

---

### 💸 Transação

-   **Identificador**: valor único gerado automaticamente
-   **Descrição**: texto
-   **Valor**: número decimal positivo
-   **Tipo**:
    -   `despesa`
    -   `receita`
-   **Categoria**: identificador da categoria cadastrada
-   **Pessoa**: identificador da pessoa cadastrada

---

## 🛠️ Tecnologias

-   Frontend: React + Vite
-   Backend: (ASP.NET)
-   Banco de Dados: (PostgreSQL)
-   Containerização: Docker e Docker Compose

---

## ▶️ Como executar o projeto

### Pré-requisitos

-   Docker
-   Docker Compose

### Execução

Na raiz do projeto, execute:

```bash
sudo docker compose up
```

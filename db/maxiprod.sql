DROP TABLE IF EXISTS transaction;
DROP TABLE IF EXISTS person;
DROP TABLE IF EXISTS category;

DROP SEQUENCE IF EXISTS person_id_seq  CASCADE;
CREATE SEQUENCE person_id_seq START 1;

CREATE TABLE person (
    person_id               INT PRIMARY KEY DEFAULT nextval('person_id_seq'),
    person_name             VARCHAR(150) NOT NULL,
    age                     INTEGER NOT NULL
);

DROP SEQUENCE IF EXISTS category_id_seq  CASCADE;
CREATE SEQUENCE category_id_seq START 1;

CREATE TABLE category (
    category_id             INT PRIMARY KEY DEFAULT nextval('category_id_seq'),
    category_description    VARCHAR(150) NOT NULL,
    goal                    VARCHAR(20) NOT NULL
);

DROP SEQUENCE IF EXISTS transaction_id_seq  CASCADE;
CREATE SEQUENCE transaction_id_seq START 1;

CREATE TABLE transaction (
    transaction_id          INT PRIMARY KEY DEFAULT nextval('transaction_id_seq'),
    transaction_description VARCHAR(200) NOT NULL,
    amount                  NUMERIC(12,2) NOT NULL,
    transaction_type        VARCHAR(20) NOT NULL,

    category_id             INT REFERENCES category(category_id) NOT NULL,
    person_id               INT REFERENCES person(person_id) NOT NULL
);

INSERT INTO person (person_name, age) VALUES 
('João Silva', 28),
('Maria Santos', 35),
('Carlos Oliveira', 42),
('Ana Pereira', 31),
('Pedro Costa', 26);

INSERT INTO category (category_description, goal) VALUES 
('Alimentação', 'despesa'),
('Transporte', 'despesa'),
('Lazer', 'despesa'),
('Educação', 'ambas'),
('Saúde', 'despesa'),
('Salário', 'receita'),
('Investimentos', 'receita'),
('Freelance', 'receita'),
('Presentes', 'ambas');

INSERT INTO transaction 
(transaction_description, amount, transaction_type, category_id, person_id) 
VALUES 
    ('Supermercado', 350.75, 'despesa', 1, 1),
    ('Uber para trabalho', 45.50, 'despesa', 2, 1),
    ('Cinema', 80.00, 'despesa', 3, 1),
    ('Curso online', 299.90, 'despesa', 4, 2),
    ('Academia', 120.00, 'despesa', 5, 2),
    ('Salário mensal', 4500.00, 'receita', 6, 1),
    ('Dividendos de ações', 1000.00, 'receita', 7, 5),
    ('Salário', 5200.00, 'receita', 6, 3),
    ('Projeto freelancer', 1500.00, 'receita', 8, 2),
    ('Bônus anual', 800.00, 'receita', 6, 4);

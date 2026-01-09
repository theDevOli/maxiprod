DROP SEQUENCE IF EXISTS people_id_seq  CASCADE;
CREATE SEQUENCE people_id_seq START 1;

DROP TABLE IF EXISTS people;
CREATE TABLE people (
    people_id               INT PRIMARY KEY DEFAULT nextval('people_id_seq'),
    person_name             VARCHAR(150) NOT NULL,
    age                     INTEGER NOT NULL
);

DROP SEQUENCE IF EXISTS category_id_seq  CASCADE;
CREATE SEQUENCE category_id_seq START 1;

DROP TABLE IF EXISTS category;
CREATE TABLE category (
    category_id             INT PRIMARY KEY DEFAULT nextval('category_id_seq'),
    category_description    VARCHAR(150) NOT NULL,
    goal                    VARCHAR(20) NOT NULL
);

DROP SEQUENCE IF EXISTS transaction_id_seq  CASCADE;
CREATE SEQUENCE transaction_id_seq START 1;

DROP TABLE IF EXISTS transaction;
CREATE TABLE transaction (
    transaction_id          INT PRIMARY KEY DEFAULT nextval('transaction_id_seq'),
    transaction_description VARCHAR(200) NOT NULL,
    amount                  NUMERIC(12,2) NOT NULL,
    transaction_type        VARCHAR(20) NOT NULL,

    category_id             INT REFERENCES category(category_id) NOT NULL,
    people_id               INT REFERENCES people(people_id) NOT NULL
);

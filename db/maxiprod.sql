DROP SEQUENCE IF EXISTS person_id_seq  CASCADE;
CREATE SEQUENCE person_id_seq START 1;

DROP TABLE IF EXISTS person;
CREATE TABLE person (
    person_id               INT PRIMARY KEY DEFAULT nextval('person_id_seq'),
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
    person_id               INT REFERENCES person(person_id) NOT NULL
);

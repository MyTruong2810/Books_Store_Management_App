-- Table: public.book

-- DROP TABLE IF EXISTS public.book;

CREATE TABLE IF NOT EXISTS public.book
(
    imageurl character varying(255) COLLATE pg_catalog."default",
    title character varying(255) COLLATE pg_catalog."default",
    publisher character varying(255) COLLATE pg_catalog."default",
    author character varying(255) COLLATE pg_catalog."default",
    isbn character varying(13) COLLATE pg_catalog."default",
    year integer,
    price numeric(10,2),
    genre character varying(50) COLLATE pg_catalog."default",
    quantity integer,
    index integer NOT NULL,
    CONSTRAINT book_pkey PRIMARY KEY (index)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.book
    OWNER to postgres;
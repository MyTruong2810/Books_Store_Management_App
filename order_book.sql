-- Table: public.order_book

-- DROP TABLE IF EXISTS public.order_book;

CREATE TABLE IF NOT EXISTS public.order_book
(
    id character varying(100) COLLATE pg_catalog."default" NOT NULL,
    customer character varying(100) COLLATE pg_catalog."default" NOT NULL,
    date timestamp without time zone NOT NULL,
    discount numeric(5,2) NOT NULL,
    amount numeric(10,2) NOT NULL,
    price numeric(10,2) NOT NULL,
    orderindex integer NOT NULL
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.order_book
    OWNER to postgres;
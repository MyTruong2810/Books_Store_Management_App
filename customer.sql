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

INSERT INTO public.order_book (id,customer,"date",discount,amount,price,orderindex) VALUES
	 ('#001','Alice Johnson','2024-10-01 12:00:00',5.00,2.00,15.50,0),
	 ('#002','Bob Smith','2024-10-02 13:30:00',2.50,1.00,22.00,1),
	 ('#003','Charlie Brown','2024-10-03 14:45:00',0.00,5.00,10.00,2),
	 ('#004','Diana Prince','2024-10-04 09:15:00',1.00,3.00,18.75,3),
	 ('#005','Edward Elric','2024-10-05 16:20:00',0.00,4.00,12.50,4),
	 ('#006','Fiona Gallagher','2024-10-06 08:10:00',3.00,2.00,25.00,5),
	 ('#007','George Washington','2024-10-07 10:00:00',4.50,1.00,30.00,6),
	 ('#008','Hannah Montana','2024-10-08 11:30:00',0.00,7.00,8.99,7),
	 ('#009','Ian Malcolm','2024-10-09 15:00:00',1.50,2.00,20.00,8),
	 ('#010','Julia Child','2024-10-10 17:30:00',2.00,3.00,14.99,9);

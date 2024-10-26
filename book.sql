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

INSERT INTO public.book (imageurl,title,publisher,author,isbn,"year",price,genre,quantity,"index") VALUES
	 ('ms-appx:///Assets/image4.jpg','1984','George Orwell','George Orwell','9780451534852',1949,10.99,'Dystopian',12,4),
	 ('ms-appx:///Assets/image9.jpg','Animal Farm','George Orwell','George Orwell','9780451536050',1945,9.99,'Allegory',11,9),
	 ('ms-appx:///Assets/image1.jpg','The Lord
','J.R.R. Tolkien','J.R.R. Tolkien','9780007567499',1954,29.99,'Fantasy',10,1),
	 ('ms-appx:///Assets/image2.jpg','Pride
','Jane Austen','Jane Austen','9780140283204',1813,15.99,'Classic',5,2),
	 ('ms-appx:///Assets/image3.jpg','Killer','Harper Lee','Harper Lee','9780060735097',1960,12.99,'Fiction',8,3),
	 ('ms-appx:///Assets/image5.jpg','The Catcher','J.D. Salinger','J.D. Salinger','9780316769487',1951,14.99,'Coming-of-age',7,5),
	 ('ms-appx:///Assets/image6.jpg','Harry Potter','J.K. Rowling','J.K. Rowling','9780590353258',1997,19.99,'Fantasy',15,6),
	 ('ms-appx:///Assets/image7.jpg','Hitchhiker''s','Douglas Adams','Douglas Adams','9780345390827',1979,13.99,'Science Fiction',9,7),
	 ('ms-appx:///Assets/image8.jpg','The Great','F. Scott Fitzgerald','F. Scott Fitzgerald','9780743273563',1925,16.99,'Classic',6,8),
	 ('ms-appx:///Assets/image10.jpg','Little Prince','Antoine de Saint-Exupéry','Antoine de Saint-Exupéry','9780446315981',1943,11.99,'Children''s',13,10);

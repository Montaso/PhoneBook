--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2025-03-04 21:53:05

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16638)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: pbuser
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO pbuser;

--
-- TOC entry 218 (class 1259 OID 16643)
-- Name: categories; Type: TABLE; Schema: public; Owner: pbuser
--

CREATE TABLE public.categories (
    nazwa text NOT NULL
);


ALTER TABLE public.categories OWNER TO pbuser;

--
-- TOC entry 220 (class 1259 OID 16662)
-- Name: contacts; Type: TABLE; Schema: public; Owner: pbuser
--

CREATE TABLE public.contacts (
    id uuid NOT NULL,
    imie text NOT NULL,
    nazwisko text NOT NULL,
    email text NOT NULL,
    haslo text NOT NULL,
    telefon text NOT NULL,
    data_urodzenia date NOT NULL,
    podkategoria text NOT NULL
);


ALTER TABLE public.contacts OWNER TO pbuser;

--
-- TOC entry 219 (class 1259 OID 16650)
-- Name: subcategories; Type: TABLE; Schema: public; Owner: pbuser
--

CREATE TABLE public.subcategories (
    nazwa text NOT NULL,
    kategoria text NOT NULL
);


ALTER TABLE public.subcategories OWNER TO pbuser;

--
-- TOC entry 221 (class 1259 OID 16677)
-- Name: users; Type: TABLE; Schema: public; Owner: pbuser
--

CREATE TABLE public.users (
    login text NOT NULL,
    haslo text NOT NULL
);


ALTER TABLE public.users OWNER TO pbuser;

--
-- TOC entry 4870 (class 0 OID 16638)
-- Dependencies: 217
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: pbuser
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250302191845_InitialCreate	9.0.2
20250304143357_CategoryChangeNameType	9.0.2
20250304192524_UserHandlingAdd	9.0.2
\.


--
-- TOC entry 4871 (class 0 OID 16643)
-- Dependencies: 218
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: pbuser
--

COPY public.categories (nazwa) FROM stdin;
Prywatny
Służbowy
Inny
\.


--
-- TOC entry 4873 (class 0 OID 16662)
-- Dependencies: 220
-- Data for Name: contacts; Type: TABLE DATA; Schema: public; Owner: pbuser
--

COPY public.contacts (id, imie, nazwisko, email, haslo, telefon, data_urodzenia, podkategoria) FROM stdin;
55eed55e-c2f1-4982-8b89-ddcc4ac2e355	Janusz	Nowak	janusz@nowak.com	$2a$12$K9TKybCyAZYWn8upvxcG7.Qm4NyyCeTY9/DeCV4XCWJ3nm9DHTBbC	222222222	1988-01-13	klient
dc55a9ca-a2c4-4747-a819-0e2ebc9b637a	Paweł	Bóbr	pawel@bobr.com	$2a$12$GahqSfOIk1bdWvXFPYTSeu15oROk85uJVBEbkLFjNtF7YtMtkVjQO	444444444	2000-04-28	prywatny
441f48b4-15fc-4bab-8a06-63d5f7cc83e4	Jan	Kowalski	jan@kowalski.com	$2a$12$H5Qsvt6sSjJOXMNmQydXJO3VY4N6zYuqOyso0qM8j0vrw3aGWFM.W	111111111	1999-12-11	dostawca sushi
421df76b-efde-4954-8b5c-0ec5e6224cb3	Marcin	Siemaszko	marcin@siemasz.com	$2a$12$/gQzJjPDSroK5TNHs3HRhu/xEoE98U6DGrSqebitJSAeW/8c7obmC	997123321	1999-11-11	dostawca kebaba
\.


--
-- TOC entry 4872 (class 0 OID 16650)
-- Dependencies: 219
-- Data for Name: subcategories; Type: TABLE DATA; Schema: public; Owner: pbuser
--

COPY public.subcategories (nazwa, kategoria) FROM stdin;
szef	Służbowy
klient	Służbowy
prywatny	Prywatny
dostawca pizzy	Inny
dostawca sushi	Inny
dostawca kebaba	Inny
\.


--
-- TOC entry 4874 (class 0 OID 16677)
-- Dependencies: 221
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: pbuser
--

COPY public.users (login, haslo) FROM stdin;
admin	$2a$12$bN1r4Cs0JRuLhnuJbBEIj.5Xoa/WvR1DSLrfgmKHdljQISyYXzTVa
\.


--
-- TOC entry 4711 (class 2606 OID 16642)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4713 (class 2606 OID 16649)
-- Name: categories PK_categories; Type: CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT "PK_categories" PRIMARY KEY (nazwa);


--
-- TOC entry 4720 (class 2606 OID 16668)
-- Name: contacts PK_contacts; Type: CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.contacts
    ADD CONSTRAINT "PK_contacts" PRIMARY KEY (id);


--
-- TOC entry 4716 (class 2606 OID 16656)
-- Name: subcategories PK_subcategories; Type: CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.subcategories
    ADD CONSTRAINT "PK_subcategories" PRIMARY KEY (nazwa);


--
-- TOC entry 4722 (class 2606 OID 16683)
-- Name: users PK_users; Type: CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT "PK_users" PRIMARY KEY (login);


--
-- TOC entry 4717 (class 1259 OID 16674)
-- Name: IX_contacts_email; Type: INDEX; Schema: public; Owner: pbuser
--

CREATE UNIQUE INDEX "IX_contacts_email" ON public.contacts USING btree (email);


--
-- TOC entry 4718 (class 1259 OID 16675)
-- Name: IX_contacts_podkategoria; Type: INDEX; Schema: public; Owner: pbuser
--

CREATE INDEX "IX_contacts_podkategoria" ON public.contacts USING btree (podkategoria);


--
-- TOC entry 4714 (class 1259 OID 16676)
-- Name: IX_subcategories_kategoria; Type: INDEX; Schema: public; Owner: pbuser
--

CREATE INDEX "IX_subcategories_kategoria" ON public.subcategories USING btree (kategoria);


--
-- TOC entry 4724 (class 2606 OID 16669)
-- Name: contacts FK_contacts_subcategories_podkategoria; Type: FK CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.contacts
    ADD CONSTRAINT "FK_contacts_subcategories_podkategoria" FOREIGN KEY (podkategoria) REFERENCES public.subcategories(nazwa) ON DELETE CASCADE;


--
-- TOC entry 4723 (class 2606 OID 16657)
-- Name: subcategories FK_subcategories_categories_kategoria; Type: FK CONSTRAINT; Schema: public; Owner: pbuser
--

ALTER TABLE ONLY public.subcategories
    ADD CONSTRAINT "FK_subcategories_categories_kategoria" FOREIGN KEY (kategoria) REFERENCES public.categories(nazwa) ON DELETE CASCADE;


-- Completed on 2025-03-04 21:53:05

--
-- PostgreSQL database dump complete
--


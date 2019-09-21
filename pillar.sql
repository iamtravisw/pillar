--
-- PostgreSQL database dump
--

-- Dumped from database version 11.5
-- Dumped by pg_dump version 11.5

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: comments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.comments (
    commentid bigint NOT NULL,
    ticketid bigint,
    message text,
    posteddate time without time zone,
    userid bigint,
    added timestamp without time zone,
    updated timestamp without time zone
);


ALTER TABLE public.comments OWNER TO postgres;

--
-- Name: comments_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.comments_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comments_id_seq OWNER TO postgres;

--
-- Name: comments_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.comments_id_seq OWNED BY public.comments.commentid;


--
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    customerid bigint NOT NULL,
    requester text,
    title text,
    primarycontact "char",
    userid bigint,
    added timestamp without time zone,
    updated timestamp without time zone,
    organization text
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customers_id_seq OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.customerid;


--
-- Name: ticket_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ticket_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.ticket_id_seq OWNER TO postgres;

--
-- Name: tickets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tickets (
    ticketid bigint DEFAULT nextval('public.ticket_id_seq'::regclass) NOT NULL,
    description text,
    status text,
    agent text,
    priority text,
    type text,
    subject text,
    opendate timestamp(4) without time zone,
    firstreply timestamp(4) without time zone,
    solvedate timestamp(4) without time zone,
    reopendate timestamp(4) without time zone,
    closeddate timestamp(4) without time zone,
    duedate timestamp(4) without time zone,
    userid bigint,
    added timestamp without time zone,
    updated timestamp without time zone
);


ALTER TABLE public.tickets OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    userid bigint NOT NULL,
    username text,
    hashedpass text,
    admin "char",
    added timestamp without time zone,
    updated timestamp without time zone
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.userid;


--
-- Name: comments commentid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comments ALTER COLUMN commentid SET DEFAULT nextval('public.comments_id_seq'::regclass);


--
-- Name: customers customerid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers ALTER COLUMN customerid SET DEFAULT nextval('public.customers_id_seq'::regclass);


--
-- Name: users userid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN userid SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: comments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.comments (commentid, ticketid, message, posteddate, userid, added, updated) FROM stdin;
216	106	I am looking into this.	00:31:30.686	67	2019-09-14 00:31:30.686	2019-09-14 00:31:30.686
217	105	This is complete. Thank you.	00:31:38.83	67	2019-09-14 00:31:38.83	2019-09-14 00:31:38.83
218	103	I updated your bios. Can you confirm it is fixed? 	00:32:01.926	67	2019-09-14 00:32:01.926	2019-09-14 00:32:01.926
219	104	This is complete. Thanks!	00:32:38.287	67	2019-09-14 00:32:38.287	2019-09-14 00:32:38.287
220	103	Looks good!	03:29:58.868	68	2019-09-14 03:29:58.868	2019-09-14 03:29:58.868
221	109	With what, sir?	19:00:21.592	67	2019-09-14 19:00:21.592	2019-09-14 19:00:21.592
\.


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.customers (customerid, requester, title, primarycontact, userid, added, updated, organization) FROM stdin;
36	Admin	Admin	N	67	2019-09-10 21:44:19.483776	2019-09-10 21:44:19.483779	Pillar
37	Travis Woodward	Dad	N	68	2019-09-10 22:18:44.949689	2019-09-10 22:18:44.949692	Woodward Family
41	Pepper Marie	Boss	N	72	2019-09-13 18:28:45.735698	2019-09-13 18:28:45.735701	Pepper Corp.
42	Dru Hilleary	Machinist 	N	73	2019-09-13 18:29:50.151341	2019-09-13 18:29:50.151344	Machine LLC.
\.


--
-- Data for Name: tickets; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tickets (ticketid, description, status, agent, priority, type, subject, opendate, firstreply, solvedate, reopendate, closeddate, duedate, userid, added, updated) FROM stdin;
107	I have too many files... can I delete ALL of them?	New		Urgent	Question	Is there a way to delete all my files?	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566	73	2019-09-14 00:31:07.566	2019-09-14 00:31:07.566
106	Can someone help me troubleshoot the blue screen? happens at random. Thanks!	Open		Normal	Task	My screen keeps going blue, idk why?	2019-09-14 00:30:35	2019-09-14 00:30:35	2019-09-14 00:30:35	2019-09-14 00:30:35	2019-09-14 00:30:35	2019-09-14 00:30:35	73	2019-09-14 00:30:35	2019-09-14 00:31:30.687
105	I need someone to reset my password please! I have no idea how to do this.	Solved		Normal	Incident	Reset my password	2019-09-14 00:29:23	2019-09-14 00:29:23	2019-09-14 00:29:23	2019-09-14 00:29:23	2019-09-14 00:29:23	2019-09-14 00:29:23	72	2019-09-14 00:29:23	2019-09-14 00:31:38.831
104	I need someone to reset my password please. 	Solved		Normal	Incident	Reset my password	2019-09-14 00:28:18	2019-09-14 00:28:18	2019-09-14 00:28:18	2019-09-14 00:28:18	2019-09-14 00:28:18	2019-09-14 00:28:18	68	2019-09-14 00:28:18	2019-09-14 00:32:38.288
108	Please help! Contact back ASAP!	New		High	Task	Help!!	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152	68	2019-09-14 02:47:07.152	2019-09-14 02:47:07.152
103	My computer will not turn on. Can you please assist? 	Pending		Urgent	Problem	Computer not functional	2019-09-14 00:27:57	2019-09-14 00:27:57	2019-09-14 00:27:57	2019-09-14 00:27:57	2019-09-14 00:27:57	2019-09-14 00:27:57	68	2019-09-14 00:27:57	2019-09-14 03:29:58.869
109	Help!	Open		High	Task	Test ticket	2019-09-14 19:00:03	2019-09-14 19:00:03	2019-09-14 19:00:03	2019-09-14 19:00:03	2019-09-14 19:00:03	2019-09-14 19:00:03	68	2019-09-14 19:00:03	2019-09-14 19:00:21.594
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (userid, username, hashedpass, admin, added, updated) FROM stdin;
68	travis	$p!ll@r$V1$639$hKVxstsn1eb1lbi8Zbq6HPx/zLZ8Kt8lNITl53Xqj0KZp2q6	N	2019-09-10 22:18:44.949689	2019-09-10 22:18:44.949692
67	admin	$p!ll@r$V1$639$xPOTRuMuEk94ga0KMPM5uVB6iVT2d6a3w7fK/rBb/4P88wth	Y	2019-09-10 21:44:19.483776	2019-09-10 21:44:19.483779
72	pepper	$p!ll@r$V1$639$WO9VsMclij0feRRbzI/7ovnpb05GXGA9gMXdXlPy4XGutOxZ	N	2019-09-13 18:28:45.735698	2019-09-13 18:28:45.735701
73	dru	$p!ll@r$V1$639$vAJuUP2MArqa7LiHCZ5HVLm03V5hduRu73ZFh2An3600EOqb	N	2019-09-13 18:29:50.151341	2019-09-13 18:29:50.151344
\.


--
-- Name: comments_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.comments_id_seq', 228, true);


--
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customers_id_seq', 42, true);


--
-- Name: ticket_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ticket_id_seq', 109, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 73, true);


--
-- Name: comments Comments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.comments
    ADD CONSTRAINT "Comments_pkey" PRIMARY KEY (commentid);


--
-- Name: users user; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT "user" UNIQUE (username);


--
-- PostgreSQL database dump complete
--


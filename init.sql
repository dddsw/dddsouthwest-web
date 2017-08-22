CREATE USER dddsouthwest_user WITH PASSWORD 'letmein';
CREATE DATABASE dddsouthwest;
GRANT ALL PRIVILEGES ON DATABASE dddsouthwest TO dddsouthwest_user;

/* Run below against your new Postgres SQL database */

CREATE TABLE public.Events
(
    Id SERIAL PRIMARY KEY,
    EventName VARCHAR(200) NOT NULL,
    EventFilename VARCHAR(200),
    EventDate TIMESTAMP NULL
);

CREATE UNIQUE INDEX Events_Filename_uindex ON public.Events (EventFilename);


CREATE TABLE public.Talks
(
    Id SERIAL PRIMARY KEY,
    TalkTitle VARCHAR(255) NOT NULL,
    TalkFilename VARCHAR(255) NOT NULL,
    TalkSummary VARCHAR NOT NULL,
    TalkBody VARCHAR NOT NULL,
    SubmissionDate TIMESTAMP NOT NULL
);

CREATE TABLE public.Users
(
    Id SERIAL PRIMARY KEY,
    EmailAddress VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Salt VARCHAR(255) NOT NULL,
    Confirmed BOOLEAN DEFAULT FALSE
);

CREATE TABLE public.News
(
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Filename VARCHAR(255) NOT NULL,
    DatePosted TIMESTAMP,
    Body VARCHAR,
    IsLive BOOLEAN DEFAULT FALSE
);
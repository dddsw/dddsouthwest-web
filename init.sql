CREATE USER dddsouthwest_user WITH PASSWORD 'letmein';
CREATE DATABASE dddsouthwest;
GRANT ALL PRIVILEGES ON DATABASE dddsouthwest TO dddsouthwest_user;

CREATE TABLE public.Events
(
    Id SERIAL PRIMARY KEY,
    EventName VARCHAR(200) NOT NULL,
    EventFilename VARCHAR(200)
);

CREATE UNIQUE INDEX events_EventFilename_uindex ON public.events (EventFilename);
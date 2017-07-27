CREATE USER dddsouthwest_user WITH PASSWORD 'letmein';
CREATE DATABASE dddsouthwest;
GRANT ALL PRIVILEGES ON DATABASE dddsouthwest TO dddsouthwest_user;

CREATE TABLE public.Events
(
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    Filename VARCHAR(200)
);

CREATE UNIQUE INDEX Events_Filename_uindex ON public.Events (Filename);
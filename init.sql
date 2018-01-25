-- CREATE USER dddsouthwest_user WITH PASSWORD 'letmein';
-- CREATE DATABASE dddsouthwest;
-- GRANT ALL PRIVILEGES ON DATABASE dddsouthwest TO dddsouthwest_user;

-- Run below against your new Postgres SQL database

CREATE TABLE Events
(
    Id SERIAL PRIMARY KEY,
    EventName VARCHAR(200) NOT NULL,
    EventFilename VARCHAR(200),
    EventDate TIMESTAMP NULL
);

CREATE UNIQUE INDEX Events_Filename_uindex ON Events (EventFilename);


CREATE TABLE Talks
(
    Id SERIAL NOT NULL CONSTRAINT talks_pkey PRIMARY KEY,
    TalkTitle VARCHAR(255) NOT NULL,
    TalkSummary VARCHAR NOT NULL,
    TalkBodyHtml VARCHAR NOT NULL,
    TalkBodyMarkdown VARCHAR NOT NULL,
    DateAdded TIMESTAMP NOT NULL,
    LastModified TIMESTAMP NOT NULL,
    SubmissionDate TIMESTAMP,
    UserId INTEGER NOT NULL,
    IsApproved BOOLEAN DEFAULT FALSE NOT NULL,
    IsSubmitted BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT talks_users_id_fk REFERENCES Users
);


CREATE TABLE Users
(
    Id SERIAL PRIMARY KEY,
    GivenName VARCHAR(150) NULL,
    FamilyName VARCHAR(150) NULL,
    EmailAddress VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Salt VARCHAR(255) NOT NULL,
    IsBlocked BOOLEAN DEFAULT FALSE NOT NULL,
    IsActivated BOOLEAN DEFAULT FALSE NOT NULL,
    Roles JSON NULL,
    ReceiveNewsletter BOOLEAN DEFAULT FALSE NOT NULL,
    DateRegistered TIMESTAMP NOT NULL,
);

CREATE TABLE News
(
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Filename VARCHAR(255) NOT NULL,
    DatePosted TIMESTAMP,
    BodyHtml VARCHAR,
    BodyMarkdown VARCHAR,
    IsLive BOOLEAN DEFAULT FALSE NOT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE NOT NULL
);

CREATE TABLE Profiles
(
    id SERIAL NOT NULL
    CONSTRAINT profile_pkey
    PRIMARY KEY,
    Twitter VARCHAR(150),
    Website VARCHAR(255),
    Linkedin VARCHAR(255),
    BioMarkdown VARCHAR,
    BioHtml VARCHAR,
    Lastmodified TIMESTAMP NOT NULL,
    Userid INTEGER
    CONSTRAINT profile_users_id_fk
    REFERENCES users
);

CREATE TABLE Pages
(
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Filename VARCHAR(255) NOT NULL,
    Body VARCHAR,
    BodyMarkdown VARCHAR,
    BodyHtml VARCHAR,
    IsLive BOOLEAN DEFAULT FALSE NOT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE NOT NULL,
    PageOrder INT NOT NULL,
    DateCreated TIMESTAMP,
    LastModified TIMESTAMP
);
CREATE UNIQUE INDEX Pages_Id_uindex ON Pages (Id);
CREATE UNIQUE INDEX Pages_Filename_uindex ON Pages (Filename);
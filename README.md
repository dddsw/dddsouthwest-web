# DDD South West Website

[![Build status](https://ci.appveyor.com/api/projects/status/bq2h8brn3j1omihq?svg=true)](https://ci.appveyor.com/project/DDDSW/dddsouthwest-web)

New and improved website for the DDD South West conference.

## Running DDD South West locally

### Run manually

- Clone contents of repo to your local disk
- Run `dotnet restore` in solution root
- `dotnet run` within the `src/DDDSouthWest.IdentityServer/` directory
- `dotnet run` within the `src/DDDSouthWest.Website/` directory
- Navigate to the website on `http://0.0.0.0:5002`

**Populate Database**  

As this point you'll see the website fail to connect to the database. As the DDD South West website uses PostgreSQL as its data persistance engine you'll need to have an instance running (either installed locally, remotely or via Docker).

The easiest way is to use the PostgresSQL Docker image via the below command:

`docker run -d -p 5432:5432 -e POSTGRES_USER=dddsouthwest_user -e POSTGRES_PASSWORD=letmein -e POSTGRES_DB=dddsouthwest postgres:9.4`

Once PostgreSQL is running, you can use the `init.sql` file to seed the database schema.

### Run from Docker

- Publish artifacts and create images by running `$ sh publish_docker.sh`
- Run `docker-compose up` to launch
- Add `website` and `identityserver` to your host file, mapping them both to `localhost`. 
- Navigate to the website on `http://website:5002`
- Run Populate Database step outlined above

# Design

A preview of the front page:

![preview](./preview.jpg)

# DDD South West Website

[![Build status](https://ci.appveyor.com/api/projects/status/bq2h8brn3j1omihq?svg=true)](https://ci.appveyor.com/project/DDDSW/dddsouthwest-web)

New and improved website for the DDD South West conference.

## Running DDD South West locally

### Prerequisites

1. Ensure you have dotnet SDK v1 or above installed. You can do this using the `dotnet --info` command.

2. Ensure your `ASPNETCORE_ENVIRONMENT` environment variable is set to either `Development` or `development`. You can do this by running one of the following commands:

   **Windows:**  
   Command Prompt: `setx ASPNETCORE_ENVIRONMENT "Development"`  
   Powershell: `$env:ASPNETCORE_ENVIRONMENT = "Development"`

   **OS X:**  
   Run `export ASPNETCORE_ENVIRONMENT=Development` in your console.

3. The DDD South West website uses PostgreSQL as its data persistance engine so you'll need to have an instance running (either installed locally, remotely or via Docker) with the following development database credentials set:

   Database Name: `dddsouthwest`  
   Database Username: `dddsouthwest_user`  
   Database Password: `letmein`  

   The easiest way is to use the PostgresSQL Docker image via the below command:

   `docker run -d -p 5432:5432 -e POSTGRES_USER=dddsouthwest_user -e POSTGRES_PASSWORD=letmein -e POSTGRES_DB=dddsouthwest postgres:9.4`

   Once PostgreSQL is running, you can use the `init.sql` file to seed the database schema.

### Running manually

- Clone contents of repo to your local disk
- Run `dotnet restore` in solution root
- `dotnet run` within the `src/DDDSouthWest.IdentityServer/` directory
- `dotnet run` within the `src/DDDSouthWest.Website/` directory
- Navigate to the website on `http://0.0.0.0:5002`

### Run from Docker

- Publish artifacts and create images by running `$ sh publish_docker.sh`
- Run `docker-compose up` to launch
- Add `website` and `identityserver` to your host file, mapping them both to `localhost`. 
- Navigate to the website on `http://website:5002`
- Run Populate Database step outlined above

# Design

A preview of the front page:

![preview](./preview.jpg)

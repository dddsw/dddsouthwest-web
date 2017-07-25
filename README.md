# DDD South West Website

[![Build status](https://ci.appveyor.com/api/projects/status/bq2h8brn3j1omihq?svg=true)](https://ci.appveyor.com/project/DDDSW/dddsouthwest-web)

New and improved website for the DDD South West conference.

## Running DDD South West locally

### Run manually

- Clone contents of repo to your local disk
- Run `dotnet restore` in solution root
- `dotnet run` within the `DDDSouthWest.IdentityServer/` directory
- `dotnet run` within the `DDDSouthWest.Website/` directory
- Navigate to the website on `http://0.0.0.0:5002`

### Run from Docker

- Publish artifacts by running `$ sh publish.sh`
- Run `docker-compose build` to create images
- Run `docker-compose up` to launch
- Add `website` and `identityserver` to your host file, mapping them both to `localhost`. 
- Navigate to the website on `http://website:5002`

# Design

A preview of the front page:

![preview](./preview.jpg)
#!/usr/bin/env bash

# This script builds all the necessary assets for production

rm -rf ./dist/

dotnet restore src/DDDSouthwest.Website/
dotnet restore src/DDDSouthwest.IdentityServer/

dotnet publish src/DDDSouthwest.Website/ -c Debug -o ../../dist/website
dotnet publish src/DDDSouthwest.IdentityServer/ -c Debug -o ../../dist/identityserver
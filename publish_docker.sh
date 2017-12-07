#!/usr/bin/env bash

# This script builds all the necessary assets for production

rm -rf ./dist/

dotnet restore src/DDDSouthwest.Website/
dotnet restore src/DDDSouthwest.IdentityServer/

dotnet publish src/DDDSouthwest.Website/ -c Release -o ../../dist/website
dotnet publish src/DDDSouthwest.IdentityServer/ -c Release -o ../../dist/identityserver

cp ./website.dockerfile ./dist/website/website.dockerfile
cp ./identityserver.dockerfile ./dist/identityserver/identityserver.dockerfile

docker build -f ./dist/website/website.dockerfile -t ddd_southwest/website ./dist/website
docker build -f ./dist/identityserver/identityserver.dockerfile -t ddd_southwest/identityserver ./dist/identityserver
docker build -f postgres.dockerfile -t ddd_southwest/database .
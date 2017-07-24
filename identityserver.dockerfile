FROM microsoft/dotnet:1.1-runtime

MAINTAINER DDDSouthWest

COPY ./dist/identityserver /identityserver

WORKDIR /identityserver

EXPOSE 5000

ENTRYPOINT ["dotnet", "DDDSouthWest.IdentityServer.dll"]
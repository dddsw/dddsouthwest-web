FROM microsoft/dotnet:1.1-runtime

MAINTAINER DDDSouthWest

COPY . /identityserver

WORKDIR ./identityserver/

EXPOSE 5000

ENTRYPOINT ["dotnet", "DDDSouthWest.IdentityServer.dll"]
FROM mcr.microsoft.com/dotnet/core/runtime:1.1

COPY . /identityserver

WORKDIR /identityserver/

EXPOSE 5000

ENTRYPOINT ["dotnet", "DDDSouthWest.IdentityServer.dll"]
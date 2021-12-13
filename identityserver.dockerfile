FROM mcr.microsoft.com/dotnet/aspnet:2.1

COPY . /identityserver

WORKDIR ./identityserver/

EXPOSE 5000

ENTRYPOINT ["dotnet", "DDDSouthWest.IdentityServer.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:2.1

COPY . /website

WORKDIR ./website/

EXPOSE 5002

ENTRYPOINT ["dotnet", "DDDSouthWest.Website.dll"]
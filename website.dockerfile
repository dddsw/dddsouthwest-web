FROM mcr.microsoft.com/dotnet/runtime:6.0

COPY . /website

WORKDIR /website/

EXPOSE 5002

ENTRYPOINT ["dotnet", "DDDSouthWest.Website.dll"]
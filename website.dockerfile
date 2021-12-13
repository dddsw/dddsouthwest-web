FROM mcr.microsoft.com/dotnet/core/runtime:1.1

COPY . /website

WORKDIR ./website/

EXPOSE 5002

ENTRYPOINT ["dotnet", "DDDSouthWest.Website.dll"]
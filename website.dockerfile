FROM microsoft/dotnet:1.1-runtime

MAINTAINER DDDSouthWest

COPY . /website

WORKDIR ./website/

EXPOSE 5002

ENTRYPOINT ["dotnet", "DDDSouthWest.Website.dll"]
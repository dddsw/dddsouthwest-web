FROM microsoft/dotnet:1.1-runtime

MAINTAINER DDDSouthWest

COPY ./dist/website /website

WORKDIR /website

EXPOSE 5002

ENTRYPOINT ["dotnet", "DDDSouthWest.Website.dll"]
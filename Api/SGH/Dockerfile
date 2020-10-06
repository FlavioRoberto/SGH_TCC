FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine

LABEL version="2.0" maintainer="Flávio Roberto Teixeira"

WORKDIR /app

COPY ./SGH/bin/Debug/netcoreapp2.2 .

ENTRYPOINT  ["dotnet", "SGH.Api.dll"]

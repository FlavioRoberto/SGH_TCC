#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV CONNECTION_STRING=""

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SGH/SGH.Api.csproj", "SGH/"]

RUN dotnet restore "SGH/SGH.Api.csproj"
COPY . .
WORKDIR "/src/SGH"
RUN dotnet build "SGH.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGH.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGH.Api.dll"]
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["SGH.Api.csproj", "./"]
RUN dotnet restore "./SGH.Api.csproj"
COPY . .
RUN dotnet build "SGH.Api.csproj" -c Release -o /app
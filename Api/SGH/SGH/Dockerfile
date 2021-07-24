FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["SGH.Api.csproj", "./"]
COPY ["../SGH.Dominio.Core/SGH.Dominio.Core.csproj", "./"]
COPY ["../SGH.Dominio.Services/SGH.Dominio.Services.csproj", "./"]
COPY ["../SGH.Dominio.Email/SGH.Email.csproj", "./"]

RUN dotnet restore "./SGH.Api.csproj"
COPY . .
RUN dotnet build "SGH.Api.csproj" -c Release -o /app
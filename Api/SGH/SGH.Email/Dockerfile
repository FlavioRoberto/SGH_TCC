#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SGH.Email/SGH.Email.csproj", "SGH.Email/"]
RUN dotnet restore "SGH.Email/SGH.Email.csproj"
COPY . .
WORKDIR "/src/SGH.Email"
RUN dotnet build "SGH.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGH.Email.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGH.Email.dll"]
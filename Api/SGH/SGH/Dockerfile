FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY [".", "./"]

RUN dotnet restore "./SGH/SGH.Api.csproj"
COPY . .
RUN dotnet build "SGH/SGH.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SGH/SGH.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN apt-get install -y libc6-dev 
RUN ln -s /usr/lib/libgdiplus.so/usr/lib/gdiplus.dll

WORKDIR /app
EXPOSE 5000

FROM base AS final

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SGH.Api.dll"]
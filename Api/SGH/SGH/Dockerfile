#FROM microsoft/aspnetcore-build:2.2 AS build
#WORKDIR /src
#COPY ["./SGH.Api.csproj", "./"]
#
#RUN dotnet restore "SGH.Api.csproj"
#COPY . .
#RUN dotnet build "SGH/SGH.Api.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "SGH.Api.csproj" -c Release -o /app
#
#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 5000
#EXPOSE 443
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "SGH.Api.dll"]


# Step 1: create a base container
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
# Step 2: create a build container with dotnet sdk
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY SGH/SGH.Api.csproj SGH/
RUN dotnet restore SGH/SGH.Api.csproj
COPY . .
WORKDIR /src/SGH
RUN dotnet build SGH.Api.csproj -c Release -o /app
# Step 3: from the build container publish the website
FROM build AS publish
RUN dotnet publish SGH.Api.csproj -c Release -o /app
# Step 4: copy the output from publish container into base container
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SGH.Api.dll"]
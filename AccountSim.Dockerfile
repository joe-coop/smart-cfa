#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Staging
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Smart.FA.Catalog.AccountSim/Smart.FA.Catalog.AccountSimulation.csproj", "src/Smart.FA.Catalog.AccountSim/"]

RUN dotnet restore "src/Smart.FA.Catalog.AccountSim/Smart.FA.Catalog.AccountSimulation.csproj"
COPY . .
WORKDIR "/src/src/Smart.FA.Catalog.AccountSim"
RUN dotnet build "Smart.FA.Catalog.AccountSimulation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Smart.FA.Catalog.AccountSimulation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Smart.FA.Catalog.AccountSimulation.dll"]

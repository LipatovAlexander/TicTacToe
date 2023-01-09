FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/DBMigrator/DBMigrator.csproj", "src/DBMigrator/"]
RUN dotnet restore "src/DBMigrator/DBMigrator.csproj"
COPY . .
WORKDIR "/src/src/DBMigrator"
RUN dotnet build "DBMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DBMigrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DBMigrator.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ConsumerWebHost/ConsumerWebHost.csproj", "src/ConsumerWebHost/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/ConsumerWebHost/ConsumerWebHost.csproj"
COPY . .
WORKDIR "/src/src/ConsumerWebHost"
RUN dotnet build "ConsumerWebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsumerWebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsumerWebHost.dll"]

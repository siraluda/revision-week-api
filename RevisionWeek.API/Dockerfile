FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RevisionWeek.API/RevisionWeek.API.csproj", "RevisionWeek.API/"]
RUN dotnet restore "RevisionWeek.API/RevisionWeek.API.csproj"
COPY . .
WORKDIR "/src/RevisionWeek.API"
RUN dotnet build "./RevisionWeek.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RevisionWeek.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RevisionWeek.API.dll"]

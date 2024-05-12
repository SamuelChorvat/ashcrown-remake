FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ashcrown.Remake.Api/Ashcrown.Remake.Api.csproj", "src/Ashcrown.Remake.Api/"]
COPY ["src/Ashcrown.Remake.Core/Ashcrown.Remake.Core.csproj", "src/Ashcrown.Remake.Core/"]
RUN dotnet restore "src/Ashcrown.Remake.Api/Ashcrown.Remake.Api.csproj"
COPY . .
WORKDIR "/src/src/Ashcrown.Remake.Api"
RUN dotnet build "Ashcrown.Remake.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ashcrown.Remake.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ashcrown.Remake.Api.dll"]

# Consultez https://aka.ms/customizecontainer pour savoir comment personnaliser votre conteneur de débogage et comment Visual Studio utilise ce Dockerfile pour générer vos images afin d’accélérer le débogage.

# Cet index est utilisé lors de l’exécution à partir de VS en mode rapide (par défaut pour la configuration de débogage)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# (Optional) If you have private feed credentials
# If using a private feed that needs credentials:
ARG FEED_URL="https://pkgs.dev.azure.com/hafifi0336/AlexsysFoundations/_packaging/NuGet-NetCore/nuget/v3/index.json"
ARG FEED_USERNAME="your-username"
ARG FEED_PASSWORD="hlbqvxdpzkyml4bpoitebvwiwguphrwb4iro2osnl4pv73gdvrza" 

WORKDIR /src
COPY ["src/Directory.Build.props", "src/"]
COPY ["Directory.Build.props", "."]
COPY ["src/Api/ProjectAPI.Api.csproj", "src/Api/"]
COPY ["src/Domain/ProjectAPI.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/ProjectAPI.Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet nuget add source \
        "$FEED_URL" \
        --name "MyPrivateFeed" \
        --username "$FEED_USERNAME" \
        --password "$FEED_PASSWORD" \
        --store-password-in-clear-text

RUN dotnet restore "./src/Api/ProjectAPI.Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "./ProjectAPI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectAPI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Cette phase est utilisée en production ou lors de l’exécution à partir de VS en mode normal (par défaut quand la configuration de débogage n’est pas utilisée)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectAPI.Api.dll"]
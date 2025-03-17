ARG runtime=mcr.microsoft.com/dotnet/aspnet:8.0
ARG builder=mcr.microsoft.com/dotnet/sdk:8.0

FROM $builder AS publish
RUN groupadd -r dotnet && useradd -r -g dotnet dotnet
WORKDIR /src
COPY --chown=dotnet:dotnet . .
RUN dotnet publish src/Api/AuthenticationAPI.Api.csproj -c Release -o /app

FROM $runtime AS final

LABEL maintainer="my.team.name" \
      com.alexsys.description="My sample API description" \
      com.alexsys.repository="url repository" \
      com.alexsys.imageName="docker-registry/my.team/authenticationapi.api"

WORKDIR /app
COPY --from=publish /app .

COPY src/Api/Dockerfile /root/

ENTRYPOINT ["dotnet", "AuthenticationAPI.Api.dll"]

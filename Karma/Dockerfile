#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Karma/Karma.API.csproj", "Karma/"]
COPY ["Karma.Application/Karma.Application.csproj", "Karma.Application/"]
COPY ["Karma.Domain/Karma.Core.csproj", "Karma.Domain/"]
COPY ["Karma.Infrastructure/Karma.Infrastructure.csproj", "Karma.Infrastructure/"]
RUN dotnet restore "./Karma/Karma.API.csproj"
COPY . .
WORKDIR "/src/Karma"
RUN dotnet build "./Karma.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Karma.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Karma.API.dll"]
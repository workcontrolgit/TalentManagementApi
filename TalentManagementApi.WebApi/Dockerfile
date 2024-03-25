#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TalentManagementApi.WebApi/TalentManagementApi.WebApi.csproj", "TalentManagementApi.WebApi/"]
COPY ["TalentManagementApi.Infrastructure.Elastic/TalentManagementApi.Infrastructure.Search.csproj", "TalentManagementApi.Infrastructure.Elastic/"]
COPY ["TalentManagementApi.Application/TalentManagementApi.Application.csproj", "TalentManagementApi.Application/"]
COPY ["TalentManagementApi.Domain/TalentManagementApi.Domain.csproj", "TalentManagementApi.Domain/"]
COPY ["TalentManagementApi.Infrastructure.Persistence/TalentManagementApi.Infrastructure.Persistence.csproj", "TalentManagementApi.Infrastructure.Persistence/"]
COPY ["TalentManagementApi.Infrastructure.Shared/TalentManagementApi.Infrastructure.Shared.csproj", "TalentManagementApi.Infrastructure.Shared/"]
RUN dotnet restore "./TalentManagementApi.WebApi/./TalentManagementApi.WebApi.csproj"
COPY . .
WORKDIR "/src/TalentManagementApi.WebApi"
RUN dotnet build "./TalentManagementApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TalentManagementApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentManagementApi.WebApi.dll"]
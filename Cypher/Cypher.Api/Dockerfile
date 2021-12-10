#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cypher.Api/Cypher.Api.csproj", "Cypher.Api/"]
COPY ["Cypher.Application/Cypher.Application.csproj", "Cypher.Application/"]
COPY ["Cypher.Domain/Cypher.Domain.csproj", "Cypher.Domain/"]
COPY ["Cypher.Infrastructure/Cypher.Infrastructure.csproj", "Cypher.Infrastructure/"]
COPY ["Cypher.Infrastructure.Shared/Cypher.Infrastructure.Shared.csproj", "Cypher.Infrastructure.Shared/"]
RUN dotnet restore "Cypher.Api/Cypher.Api.csproj"
COPY . .
WORKDIR "/src/Cypher.Api"
RUN dotnet build "Cypher.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cypher.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY "Cypher.Api/appsettings.json" "/app/AppSettings.json"
#COPY "Cypher.Api/appsettings.Development.json" "/app/AppSettings.Development.json"
ENTRYPOINT ["dotnet", "Cypher.Api.dll"]
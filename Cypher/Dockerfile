#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .

# COPY ["Cypher.Api/Cypher.Api.csproj", "Cypher.Api/"]
# COPY ["Cypher.Application/Cypher.Application.csproj", "Cypher.Application/"]
# COPY ["Cypher.Domain/Cypher.Domain.csproj", "Cypher.Domain/"]
# COPY ["Cypher.Infrastructure/Cypher.Infrastructure.csproj", "Cypher.Infrastructure/"]
# COPY ["Cypher.Infrastructure.Shared/Cypher.Infrastructure.Shared.csproj", "Cypher.Infrastructure.Shared/"]

RUN dotnet restore "Cypher.Api/Cypher.Api.csproj"
WORKDIR "/src/."
COPY . .
RUN dotnet build "Cypher.Api/Cypher.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cypher.Api/Cypher.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cypher.Api.dll"]
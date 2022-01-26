#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cypher.Web/Cypher.Web.csproj", "Cypher.Web/"]
COPY ["Cypher.Infrastructure/Cypher.Infrastructure.csproj", "Cypher.Infrastructure/"]
COPY ["Cypher.Domain/Cypher.Domain.csproj", "Cypher.Domain/"]
COPY ["Cypher.Application/Cypher.Application.csproj", "Cypher.Application/"]
COPY ["Cypher.Infrastructure.Shared/Cypher.Infrastructure.Shared.csproj", "Cypher.Infrastructure.Shared/"]
RUN dotnet restore "Cypher.Web/Cypher.Web.csproj"
COPY . .
WORKDIR "/src/Cypher.Web"
RUN dotnet build "Cypher.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cypher.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cypher.Web.dll"]
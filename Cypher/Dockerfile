#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:15.4 as build 
WORKDIR /app-mobile
COPY ../cypher-mobile-app/package*.json .
RUN npm install
COPY . .
RUN npm run prod

FROM nginx:1.19


COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/www/ /usr/share/nginx/html



 
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY . .

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
ENTRYPOINT ["dotnet", "Cypher.Api.dll"]
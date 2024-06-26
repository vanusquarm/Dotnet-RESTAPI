#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Partner/Partner.API/Partner.API.csproj", "Services/Partner/Partner.API/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Partner/Partner.API/Partner.API.csproj"
COPY . .
WORKDIR "/src/Services/Partner/Partner.API"
RUN dotnet build "Partner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Partner.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Partner.API.dll"]
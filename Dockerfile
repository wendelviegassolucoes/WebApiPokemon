FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY . ./

RUN dotnet build "WebApiPokemon/WebApiPokemon.csproj" -c Release -o /app/build
RUN dotnet publish "WebApiPokemon/WebApiPokemon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app/publish .
ARG MONGO_USER
ARG MONGO_PASSWORD
ENV MONGO_USER=${MONGO_USER}
ENV MONGO_PASSWORD=${MONGO_PASSWORD}

ENTRYPOINT ["dotnet", "WebApiPokemon.dll"]
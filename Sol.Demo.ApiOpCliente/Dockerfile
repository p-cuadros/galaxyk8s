#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as base
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8095;https://+:8096"
ENV IdentityServer__UrlServer="DEMO"
EXPOSE 8095
EXPOSE 8096

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sol.Demo.ApiOpCliente/Sol.Demo.ApiOpCliente.csproj", "Sol.Demo.ApiOpCliente/"]
COPY ["Sol.Demo.Comunes/Sol.Demo.Comunes.csproj", "Sol.Demo.Comunes/"]
RUN dotnet restore "Sol.Demo.ApiOpCliente/Sol.Demo.ApiOpCliente.csproj"
COPY . .
WORKDIR "/src/Sol.Demo.ApiOpCliente"
RUN dotnet build "Sol.Demo.ApiOpCliente.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sol.Demo.ApiOpCliente.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sol.Demo.ApiOpCliente.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ENV UrlApiCliente="UrlApiCliente"
ENV IdentityServer__UrlServer="http://seguridad:9090"
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sol.Demo.ApiUxCustomer/Sol.Demo.ApiUxCustomer.csproj", "Sol.Demo.ApiUxCustomer/"]
COPY ["Sol.Demo.Comunes/Sol.Demo.Comunes.csproj", "Sol.Demo.Comunes/"]
RUN dotnet restore "Sol.Demo.ApiUxCustomer/Sol.Demo.ApiUxCustomer.csproj"
COPY . .
WORKDIR "/src/Sol.Demo.ApiUxCustomer"
RUN dotnet build "Sol.Demo.ApiUxCustomer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sol.Demo.ApiUxCustomer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sol.Demo.ApiUxCustomer.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PDFA3/PDFA3.csproj", "PDFA3/"]
RUN dotnet restore "PDFA3/PDFA3.csproj"
COPY . .
WORKDIR "/src/PDFA3"
RUN dotnet build "PDFA3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PDFA3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PDFA3.dll"]
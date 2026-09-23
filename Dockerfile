# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Api.csproj ./
RUN dotnet restore Api.csproj

COPY . ./
RUN dotnet publish Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

# Render injects PORT at runtime; default to 10000 locally
EXPOSE 10000
ENTRYPOINT ["sh", "-c", "dotnet Api.dll --urls http://0.0.0.0:${PORT:-10000}"]

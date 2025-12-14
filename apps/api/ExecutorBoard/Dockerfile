# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
LABEL authors="gollumeo"
WORKDIR /src

COPY ExecutorBoard.sln .
COPY ExecutorBoard.Api/ExecutorBoard.Api.csproj ExecutorBoard.Api/
COPY ExecutorBoard.Application/ExecutorBoard.Application.csproj ExecutorBoard.Application/
COPY ExecutorBoard.Domain/ExecutorBoard.Domain.csproj ExecutorBoard.Domain/
COPY ExecutorBoard.Infrastructure/ExecutorBoard.Infrastructure.csproj ExecutorBoard.Infrastructure/
COPY ExecutorBoard.Persistence/ExecutorBoard.Persistence.csproj ExecutorBoard.Persistence/
COPY ExecutorBoard.Presentation/ExecutorBoard.Presentation.csproj ExecutorBoard.Presentation/

# 🔑 Restore ciblé
RUN dotnet restore ExecutorBoard.Api/ExecutorBoard.Api.csproj

COPY . .

RUN dotnet publish ExecutorBoard.Api/ExecutorBoard.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExecutorBoard.Api.dll"]
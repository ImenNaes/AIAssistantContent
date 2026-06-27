# ============================
# 1. Restore stage (cache)
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

COPY AIAssistantContent.csproj .
RUN dotnet restore

# ============================
# 2. Build stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet publish -c Release -o /app/publish

# ============================
# 3. Runtime stage
# ============================	
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AIAssistantContent.dll"]

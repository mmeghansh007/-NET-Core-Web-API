# See https://aka.ms/customizecontainer to learn how to customize your debug container
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NET Core Web API HiringTest.csproj", "."]
RUN dotnet restore "./NET Core Web API HiringTest.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./NET Core Web API HiringTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NET Core Web API HiringTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NET Core Web API HiringTest.dll"]
# Add this line in your final stage
RUN mkdir -p /app/keys && chmod 777 /app/keys


#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "./BreakfastAPI/./BreakfastAPI.csproj"
RUN dotnet restore "./BreakfastAPI.Test/./BreakfastAPI.UnitTest.csproj"
RUN dotnet build "./BreakfastAPI/BreakfastAPI.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreakfastAPI/BreakfastAPI.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
CMD ./docker_scripts/copy_artifacts.sh -t "publish"

FROM build AS unittest
ARG BUILD_CONFIGURATION=Release
RUN dotnet test "./BreakfastAPI.Test/BreakfastAPI.UnitTest.csproj" --no-restore --collect:"XPlat Code Coverage" --logger "trx;LogFileName=UnitTestResult.trx"
CMD ./docker_scripts/copy_artifacts.sh -t "unitTest"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreakfastAPI.dll"]
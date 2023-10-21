FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/StammPhoenix.Api/StammPhoenix.Api.csproj", "src/StammPhoenix.Api/"]
RUN dotnet restore "src/StammPhoenix.Api/StammPhoenix.Api.csproj"
COPY . .
WORKDIR "/src/src/StammPhoenix.Api"
RUN dotnet build "StammPhoenix.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StammPhoenix.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StammPhoenix.Api.dll"]

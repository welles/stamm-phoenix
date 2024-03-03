FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:latest AS restore
COPY . /app
WORKDIR /app
RUN dotnet restore StammPhoenix.Api.csproj

FROM restore AS publish
ARG VERSION="0.0.0-dev.0"
ARG ASSEMBLY_VERSION="0.0.0.0"
ARG FILE_VERSION="0.0.0.0"
ARG INFORMATIONAL_VERSION="0.0.0-dev.0"
RUN dotnet publish StammPhoenix.Api.csproj -c Release -o /app/publish  /property:Version=$VERSION /property:AssemblyVersion=$ASSEMBLY_VERSION /property:FileVersion=$FILE_VERSION /property:InformationalVersion=$INFORMATIONAL_VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StammPhoenix.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:latest as publish
WORKDIR /src
COPY /src .
ARG VERSION="0.0.0-dev.0"
ARG ASSEMBLY_VERSION="0.0.0.0"
ARG FILE_VERSION="0.0.0.0"
ARG INFORMATIONAL_VERSION="0.0.0-dev.0"
RUN dotnet publish /src/StammPhoenix.Api/StammPhoenix.Api.csproj -c Release -o /publish  /property:Version=$VERSION /property:AssemblyVersion=$ASSEMBLY_VERSION /property:FileVersion=$FILE_VERSION /property:InformationalVersion=$INFORMATIONAL_VERSION

FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=publish /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "StammPhoenix.Api.dll"]

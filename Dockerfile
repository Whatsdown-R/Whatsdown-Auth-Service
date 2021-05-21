
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
ENV PATH_WITH_SPACE="Whatsdown-Auth-Service"
copy . ./
# COPY *.sln .

COPY "*.csproj" ""
RUN dotnet restore
# copy everything else and build app
COPY .. ""
WORKDIR "/source/"
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Whatsdown-Auth-Service.dll"]
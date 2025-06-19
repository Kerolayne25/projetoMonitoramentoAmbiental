# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY ./src/Main/*.csproj ./Main/
COPY ./src/Tests/*.csproj ./Tests/
RUN dotnet restore ./Main/Fase4Cap7WebserviceASPNET.csproj

# Copia o restante do código e compila
COPY . .
RUN dotnet publish ./src/Main/Fase4Cap7WebserviceASPNET.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta padrão
EXPOSE 80

# Ponto de entrada
ENTRYPOINT ["dotnet", "Fase4Cap7WebserviceASPNET.dll"]

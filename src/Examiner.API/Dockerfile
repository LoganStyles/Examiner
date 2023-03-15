FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5257

ENV ASPNETCORE_URLS=http://+:5257
# enable below variables for local docker execution
# ENV DB_HOST=localhost
# ENV DB_NAME=examiner
# ENV DB_PASSWORD=
# ENV DB_USER=root
# ENV DB_PORT=3306


# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Examiner.API/Examiner.API.csproj", "src/Examiner.API/"]
RUN dotnet restore "src/Examiner.API/Examiner.API.csproj"
COPY . .
WORKDIR "/src/src/Examiner.API"
RUN dotnet build "Examiner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Examiner.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Examiner.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0.3 AS base-env
WORKDIR /app
EXPOSE 80




# BUILD APPLICATION
FROM mcr.microsoft.com/dotnet/sdk:6.0.201 AS build-env

WORKDIR /src

COPY ./src/*.csproj ./src/

# ========== RESTORE NUGET PACKAGES TO CACHE THE LAYERS ==========
RUN dotnet restore ./src/*.csproj




# ========== BUILD APPLICATION AND PUBLISH ==========
COPY ./src ./src


FROM build-env AS publish-env
RUN dotnet publish ./src/*.csproj -c Release --no-restore -o /app/publish



# ========== RUN APPLICATION ==========

FROM base-env AS final-env

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production

COPY --from=publish-env /app/publish .

ENTRYPOINT ["dotnet", "Demo.WebAPI.dll"]
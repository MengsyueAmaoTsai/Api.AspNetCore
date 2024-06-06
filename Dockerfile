ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /app

# Restore DotNet Tools
COPY .config/dotnet-tools.json .config/
RUN dotnet tool restore

# Restore NuGet Packages
ARG APP_NAME=RichillCapital.Api

COPY ./build.cake ./${APP_NAME}.sln ./${APP_NAME}.csproj ./

COPY ./Libs/RichillCapital.Contracts/RichillCapital.Contracts.csproj ./Libs/RichillCapital.Contracts/
COPY ./Libs/RichillCapital.Domain/RichillCapital.Domain.csproj ./Libs/RichillCapital.Domain/
COPY ./Libs/RichillCapital.UseCases/RichillCapital.UseCases.csproj ./Libs/RichillCapital.UseCases/
COPY ./Libs/RichillCapital.Persistence/RichillCapital.Persistence.csproj ./Libs/RichillCapital.Persistence/
COPY ./Libs/RichillCapital.Logging/RichillCapital.Logging.csproj ./Libs/RichillCapital.Logging/
COPY ./Libs/RichillCapital.Identity/RichillCapital.Identity.csproj ./Libs/RichillCapital.Identity/

COPY ./Tests/RichillCapital.Api.AcceptanceTests/RichillCapital.Api.AcceptanceTests.csproj ./Tests/RichillCapital.Api.AcceptanceTests/
RUN dotnet cake --target restore 

# Build and Publish Source Code
COPY . ./
RUN dotnet cake --target build && dotnet cake --target publish


FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

COPY --from=build ./app/publish ./

EXPOSE 8080
ENTRYPOINT [ "dotnet", "RichillCapital.Api.dll" ]
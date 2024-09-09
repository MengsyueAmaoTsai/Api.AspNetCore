ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /app

# Restore DotNet Tools
COPY .config/dotnet-tools.json .config/
RUN dotnet tool restore

# Restore NuGet Packages
ARG APP_NAME=RichillCapital.Api

COPY ./build.cake ./${APP_NAME}.sln ./${APP_NAME}.csproj ./

COPY ./Libs/RichillCapital.Contracts/*.csproj ./Libs/RichillCapital.Contracts/
COPY ./Libs/RichillCapital.UseCases/*.csproj ./Libs/RichillCapital.UseCases/
COPY ./Libs/RichillCapital.Persistence/*.csproj ./Libs/RichillCapital.Persistence/
COPY ./Libs/RichillCapital.Logging/*.csproj ./Libs/RichillCapital.Logging/
COPY ./Libs/RichillCapital.Identity/*.csproj ./Libs/RichillCapital.Identity/
COPY ./Libs/RichillCapital.Storage/*.csproj ./Libs/RichillCapital.Storage/
COPY ./Libs/RichillCapital.Notifications/*.csproj ./Libs/RichillCapital.Notifications/
COPY ./Libs/RichillCapital.Monitoring/*.csproj ./Libs/RichillCapital.Monitoring/
COPY ./Libs/RichillCapital.BackgroundJobs/*.csproj ./Libs/RichillCapital.BackgroundJobs/
COPY ./Libs/RichillCapital.Messaging/*.csproj ./Libs/RichillCapital.Messaging/
COPY ./Libs/RichillCapital.Domain/*.csproj ./Libs/RichillCapital.Domain/
COPY ./Libs/RichillCapital.Brokerages/*.csproj ./Libs/RichillCapital.Brokerages/
COPY ./Libs/RichillCapital.DataFeeds/*.csproj ./Libs/RichillCapital.DataFeeds/
COPY ./Libs/RichillCapital.Max/*.csproj ./Libs/RichillCapital.Max/
COPY ./Libs/RichillCapital.Payments/*.csproj ./Libs/RichillCapital.Payments/

COPY ./Tests/RichillCapital.Api.AcceptanceTests/*.csproj ./Tests/RichillCapital.Api.AcceptanceTests/
COPY ./Tests/RichillCapital.Api.ArchitectureTests/*.csproj ./Tests/RichillCapital.Api.ArchitectureTests/
RUN dotnet cake --target restore 

# Build and Publish Source Code
COPY . ./
RUN dotnet cake --target build && dotnet cake --target publish


FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

COPY --from=build ./app/publish ./

EXPOSE 8080
ENTRYPOINT [ "dotnet", "RichillCapital.Api.dll" ]
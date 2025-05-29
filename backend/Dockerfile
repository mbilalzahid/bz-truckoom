# Use official .NET 6 SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use official ASP.NET runtime image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy build output from previous stage
COPY --from=build /app/out ./

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 5000

ENTRYPOINT ["dotnet", "backend.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dotnet
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-dotnet
WORKDIR /App
COPY --from=build-dotnet /App/out .
ENTRYPOINT ["dotnet", "EMPIRIAN-Net.dll"]

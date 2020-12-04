FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
RUN mkdir /app
WORKDIR /app

COPY SortableAuction/AuctionConsoleApp/AuctionConsoleApp.csproj .
RUN dotnet restore AuctionConsoleApp.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "AuctionConsoleApp.dll"]
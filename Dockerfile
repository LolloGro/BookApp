# ---------- Build Angular ----------
FROM node:20 AS angular-build
WORKDIR /app
COPY Frontend/BookFrontend/package*.json ./
RUN npm install
COPY Frontend/ .
RUN npm run build --configuration production

# ---------- Build .NET backend ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build
WORKDIR /src
COPY BookApp/ ./BookApp
COPY --from=angular-build /app/dist/browser ./BookApp/wwwroot
WORKDIR /src/BookApp
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=dotnet-build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "BookApp.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:18-alpine as angular-build
WORKDIR /app/build
COPY "Presentation/Corp.ERP.Presentation.Web/Frontend" .
RUN npm install
RUN npm run build -- --output-path='/app/publish'


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Corp.ERP.Presentation.Web/Corp.ERP.Presentation.Web.csproj", "Presentation/Corp.ERP.Presentation.Web/"]
RUN dotnet restore "Presentation/Corp.ERP.Presentation.Web/Corp.ERP.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/Corp.ERP.Presentation.Web"
RUN dotnet build "Corp.ERP.Presentation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Corp.ERP.Presentation.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=angular-build /app/publish ./wwwroot
ENTRYPOINT ["dotnet", "Corp.ERP.Presentation.Web.dll"]

#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Corp.ERP.Presentation.Web/Corp.ERP.Presentation.Web.csproj", "Presentation/Corp.ERP.Presentation.Web/"]
RUN dotnet restore "Presentation/Corp.ERP.Presentation.Web/Corp.ERP.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/Corp.ERP.Presentation.Web"
RUN dotnet build "Corp.ERP.Presentation.Web.csproj" -c Release -o /app/build
COPY ["dist", "./app/wwwroot"]
RUN echo "test"

FROM build AS publish
RUN dotnet publish "Corp.ERP.Presentation.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/publish/dist ./wwwroot
ENTRYPOINT ["dotnet", "Corp.ERP.Presentation.Web.dll"]

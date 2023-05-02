#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM nginx:latest AS base
EXPOSE 80
EXPOSE 443

FROM node:18-alpine as build
WORKDIR /app/build
COPY "Presentation/Corp.ERP.Presentation.Web" .
RUN cp -f /app/build/src/environments/environment.prod.ts /app/build/src/environments/environment.ts
RUN rm -rf /app/build/src/environments/environment.prod.ts
RUN npm install
RUN npm run build -- --output-path='/app/publish'

FROM base AS final
COPY --from=build /app/publish /usr/share/nginx/html

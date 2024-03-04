FROM node:lts AS build
WORKDIR /app
COPY . .
RUN npm i
RUN npm run astro -- build # TODO NW: USE CORRECT BUILD COMMAND

FROM httpd:2.4 AS runtime
COPY --from=build /app/dist /usr/local/apache2/htdocs/
EXPOSE 80

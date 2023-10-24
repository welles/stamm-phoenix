FROM node:lts as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run publish

FROM nginx:stable as serve
COPY --from=build /app/dist/stamm-phoenix-web /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

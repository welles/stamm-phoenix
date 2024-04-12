# TODO: Clean up

FROM node:lts-slim AS base
# install bun
RUN npm i -g bun
# Set working directory
WORKDIR /usr/src/app
COPY . .

# install dependencies into temp directory
# this will cache them and speed up future builds

#FROM base AS install_dev
#RUN mkdir -p /temp/dev
#COPY package.json /temp/dev/
#COPY bun.lockb /temp/dev/
#WORKDIR /temp/dev
#RUN bun install

# install with --production (exclude devDependencies)
FROM base AS install_prod
RUN mkdir -p /temp/prod
COPY package.json /temp/prod/
# COPY bun.lockb /temp/prod/
WORKDIR /temp/prod
RUN bun install --production

# copy node_modules from temp directory
# then copy all (non-ignored) project files into the image
FROM base AS prerelease
COPY --from=install_prod /temp/dev/node_modules node_modules

FROM prerelease AS build
# [optional] tests & build
ENV NODE_ENV=production
# RUN bun test
RUN bun run build

# copy production dependencies and source code into final image
FROM build AS release
COPY --from=install_prod /temp/prod/node_modules node_modules
COPY --from=build /usr/src/app/dist dist
COPY --from=build /usr/src/app/package.json .

FROM release AS final
# run app
ENV HOST=0.0.0.0
ENV PORT=80
EXPOSE 80
ENTRYPOINT ["bun", "run", "./dist/server/entry.mjs"]

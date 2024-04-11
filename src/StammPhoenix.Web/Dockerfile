FROM node:lts-slim AS base
# install bun
RUN npm i -g bun

# COPY package.json and lock
COPY package.json /usr/src/app/
COPY bun.lockb /usr/src/app/

# Set working directory
WORKDIR /usr/src/app

# install dependencies into temp directory
# this will cache them and speed up future builds

FROM base AS install_dev
RUN mkdir -p /temp/dev
COPY package.json /temp/dev/
COPY bun.lockb /temp/dev/
RUN cd /temp/dev && bun install

# install with --production (exclude devDependencies)
FROM base AS install_prod
RUN mkdir -p /temp/prod
COPY package.json /temp/prod/
COPY bun.lockb /temp/prod/
RUN cd /temp/prod && bun install

# copy node_modules from temp directory
# then copy all (non-ignored) project files into the image
FROM base AS prerelease
COPY --from=install_dev /temp/dev/node_modules node_modules
COPY . .

# [optional] tests & build
ENV NODE_ENV=production
# RUN bun test
RUN bun run build

# copy production dependencies and source code into final image
FROM base AS release
COPY --from=install_prod /temp/prod/node_modules node_modules
COPY --from=prerelease /usr/src/app/dist dist
COPY --from=prerelease /usr/src/app/package.json .

# run app
ENV HOST=0.0.0.0
ENV PORT=80
EXPOSE 80
ENTRYPOINT ["bun", "run", "./dist/server/entry.mjs"]

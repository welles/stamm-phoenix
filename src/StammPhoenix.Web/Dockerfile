FROM oven/bun:alpine AS base

# Set working directory
WORKDIR /app

COPY src/StammPhoenix.Web/package.json src/StammPhoenix.Web/bun.lockb ./

FROM base AS prod-deps
RUN bun install --production

FROM base AS build-deps
RUN bun install --production=false

FROM build-deps AS build
COPY . .
RUN bun run build

FROM base AS runtime
COPY --from=prod-deps /app/node_modules ./node_modules
COPY --from=build /app/dist ./dist

ENV HOST=0.0.0.0
ENV PORT=80
EXPOSE 80
CMD ["bun", "./dist/server/entry.mjs"]

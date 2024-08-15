FROM oven/bun:alpine AS base
# FROM node:lts AS base

# Set working directory
WORKDIR /usr/src/app

FROM base AS install

# Copy dependency files
COPY package.json .

# Install dependencies
RUN ["bun", "install"]

FROM base AS build

COPY --from=install /usr/src/app/node_modules ./node_modules
COPY . .

# Build the project
RUN ["bun", "run", "build"]

FROM base AS production

# Copy all files needed for production
COPY --from=build /usr/src/app/. .

# Set ENV variables
ENV PORT=80
ENV HOST=0.0.0.0

# Expose port
EXPOSE 80

# Start the application
CMD ["bun", "run", ".output/server/index.mjs"]

FROM oven/bun:alpine AS base

# Set working directory
WORKDIR /usr/src/app

# install node and npm
RUN ["apk", "add", "--no-cache", "nodejs", "npm"]

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Expose port
EXPOSE 80

# Install dependencies
RUN ["bun", "install", "--production"]

# Build the project
RUN ["bun", "run", "build"]

# Set ENV variables 
ENV PORT=80
ENV HOST=0.0.0.0

# expose port
EXPOSE 80

# Start the application
CMD ["bun", "run", "./dist/server/entry.mjs"]
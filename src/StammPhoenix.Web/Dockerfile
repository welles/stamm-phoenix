FROM oven/bun:alpine AS base

# Set working directory
WORKDIR /usr/src/app

# install node and npm
RUN ["apk", "add", "--no-cache", "nodejs", "npm"]

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Expose port
EXPOSE 80

# Verify bun installation
RUN ["bun", "--version"]

# Install dependencies
RUN ["bun", "install"]

# Build the project
RUN ["bun", "run", "build"]

# Start the application
CMD ["bun", "run", "produce"]

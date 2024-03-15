FROM oven/bun:alpine AS base

# Set working directory
WORKDIR /usr/src/app

# install node and npm
RUN ["apk", "add", "--update", "nodejs", "npm"]

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Expose port
EXPOSE 80

# Verify bun installation
RUN ["bun", "--version"]

# Install dependencies and build the project
RUN ["bun", "install"]
RUN ["bun", "run", "build"]

# Start the application
CMD ["bun", "run", "gen-i18n"]
CMD ["bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]

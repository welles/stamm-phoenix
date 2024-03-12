# Build stage
FROM oven/bun:latest AS build

WORKDIR /app

# Copy only the necessary files for installation
COPY package.json package.json
COPY bun.lockb bun.lockb

# Install dependencies
RUN bun install --production

# Copy the entire application code
COPY . .

# Build the Astro application
RUN bun run build

# Runtime stage
FROM oven/bun:latest AS runtime

WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/dist ./dist

# Expose the port
EXPOSE 80

# Set the entry point to the Bun runtime
ENTRYPOINT ["bun", "run"]

# Set the command to run the built application
CMD ["dev --host --port 80"]

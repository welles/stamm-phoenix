# Use the official Bun image (replace with desired version if needed)
FROM oven/bun:latest AS base

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY . .

# Install dependencies
RUN bun install

# Build the Astro project
RUN bun run build

# Expose port and run the Astro app
EXPOSE 80

ENTRYPOINT ["bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]

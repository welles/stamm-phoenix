# Use the official Bun image (replace with desired version if needed)
FROM oven/bun:latest AS base

# Set working directory
WORKDIR /usr/src/app

# Install dependencies
RUN bun install

# [Optional] Tests & Build (modify commands as needed)
ENV NODE_ENV=production
RUN bun run build

# Copy project files
COPY . .

# Expose port and run the app
EXPOSE 80/tcp
USER bun
ENTRYPOINT [ "bun", "run", "start", "--port", "80", "--host" ]

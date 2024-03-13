FROM node:lts AS base

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Install dependencies
RUN npm install --global bun
RUN bun install

# Build the Astro project
RUN bun run build

# Expose port and run the Astro app
EXPOSE 80

ENTRYPOINT ["/bin/bash"]

CMD ["bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]

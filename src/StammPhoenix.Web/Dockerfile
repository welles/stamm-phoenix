FROM node:latest AS base

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Install dependencies
RUN npm install -g bun
RUN bun install

# Build the Astro project
RUN bun run build

# Expose port and run the Astro app
EXPOSE 80

RUN ["chmod", "+x", "/usr/local/bin/bun"]

ENTRYPOINT ["/bin/bash", "-l", "-c"]

#CMD ["bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]

CMD ["bun", "--version"]

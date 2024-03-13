FROM node:current-alpine AS base

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Install dependencies
RUN npm install bun
RUN npm run bun install

# Build the Astro project
RUN npm run bun run build

# Expose port and run the Astro app
EXPOSE 80

ENTRYPOINT ["/bin/bash"]

CMD ["npm", "run", "bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]

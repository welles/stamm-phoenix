FROM node:latest AS base

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Expose port and run the Astro app
EXPOSE 80

RUN chmod +x /usr/src/app/start.sh

# Set entrypoint
ENTRYPOINT /bin/bash -c

# Run start script
CMD /usr/src/app/start.sh

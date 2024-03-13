FROM bash:devel

# Set working directory
WORKDIR /usr/src/app

# Copy project files
COPY /src/StammPhoenix.Web/. .

# Expose port
EXPOSE 80

# Install curl
RUN ["apt-get", "update"]
RUN ["apt-get", "install", "-y", "curl"]

# Install bun
RUN ["curl", "-fsSL", "https://bun.sh/install", "|", "bash"]

# Verify bun installation
RUN ["bun", "--version"]

# Install dependencies and build the project
RUN ["bun", "install"]
RUN ["bun", "run", "build"]

# Start the application
CMD ["bun", "run", "dev", "--host", "0.0.0.0", "--port", "80"]
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

# Install NodeJS
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - \
	&& apt update \
	&& apt install -y nodejs

# Copy app folders from host to container
COPY pillar.api /app/pillar.api
COPY pillar.ui /app/pillar.ui
COPY pillar.test /app/pillar.test

# Build and install backend dependencies
WORKDIR /app/pillar.api
RUN dotnet build

# Install frontend dependencies
WORKDIR /app/pillar.ui
RUN npm install

# Install Angular CLI globally
RUN npm install -g @angular/cli@8.3.2

WORKDIR /app/
# =========================
# Build stage
# =========================
FROM node:20-alpine AS build
LABEL authors="gollumeo"
WORKDIR /app

# Install deps
COPY package.json pnpm-lock.yaml ./
RUN npm install -g pnpm && pnpm install

# Build
COPY . .
RUN pnpm build

# =========================
# Runtime stage
# =========================
FROM nginx:alpine

# Copy built assets
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

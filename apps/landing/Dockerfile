# =========================
# Build stage
# =========================
FROM node:20-alpine AS build
LABEL authors="gollumeo"
WORKDIR /app

# Enable corepack (pnpm sans npm install)
RUN corepack enable

# Copy deps files only
COPY package.json pnpm-lock.yaml ./

# Install deps (cache friendly)
RUN pnpm install --frozen-lockfile

# Copy rest & build
COPY . .
RUN pnpm build

# =========================
# Runtime stage
# =========================
FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]


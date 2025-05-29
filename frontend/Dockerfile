# Step 1: Build the Angular app
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Step 2: Serve the app with Nginx
FROM nginx:alpine

# Remove the default Nginx site config to avoid conflicts
RUN rm /etc/nginx/conf.d/default.conf

# Copy built Angular app
COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html

# Copy custom nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

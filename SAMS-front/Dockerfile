# BASE IMAGE with an alias #
FROM node:18.10-alpine as build
WORKDIR /app
# Install Angular CLI to run Build #
RUN npm install -g @angular/cli@13.0

COPY package.json ./
RUN npm install
COPY . .
RUN ng build

EXPOSE 80

# BASE IMAGE with an alias #
FROM nginx as runtime
# Copy contents from the other container with alias "build" #
# onto the specified path in the current container#
COPY --from=build /app/dist/project-front /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/
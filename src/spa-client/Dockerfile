FROM node:18-alpine as build-deps
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . ./
ENV NODE_ENV=production

# RUN export $(cat .env | xargs) && npm run build
RUN npm run build

# Stage 2 - the production environment
FROM nginx:1.23.3-alpine
COPY --from=build-deps /usr/src/app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
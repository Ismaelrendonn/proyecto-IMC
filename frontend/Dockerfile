# Usar Nginx como servidor web
FROM nginx:alpine

# Copiar archivos del frontend a Nginx
COPY index.html /usr/share/nginx/html/
COPY app.js /usr/share/nginx/html/

# Copiar configuración personalizada de Nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Exponer puerto 80
EXPOSE 80

# Comando para iniciar Nginx
CMD ["nginx", "-g", "daemon off;"]

# 🎨 Frontend Calculadora IMC - Interfaz Moderna

Este es el frontend moderno y profesional de la Calculadora IMC, diseñado con las últimas tecnologías web.

## ✨ Características

- **Diseño Moderno**: Interfaz atractiva con gradientes y animaciones suaves
- **Responsive**: Se adapta perfectamente a móviles, tablets y escritorio
- **Animaciones**: Transiciones y efectos visuales elegantes
- **Historial Local**: Guarda tus últimos 10 cálculos en localStorage
- **Validaciones en Tiempo Real**: Feedback instantáneo al usuario
- **Recomendaciones Personalizadas**: Consejos de salud según tu IMC
- **Iconografía**: Font Awesome para iconos profesionales
- **Categorización Visual**: Colores específicos para cada categoría de IMC

## 🎨 Tecnologías Utilizadas

- HTML5
- CSS3 (Con animaciones y gradientes)
- JavaScript (Vanilla JS - Sin frameworks)
- Bootstrap 5.3.2
- Font Awesome 6.4.2
- Google Fonts (Poppins)

## 🐳 Ejecución con Docker

### Opción 1: Solo Frontend (Para desarrollo)
```bash
cd frontend
docker build -t imc-frontend .
docker run -p 3000:80 imc-frontend
```

Luego abre: http://localhost:3000

### Opción 2: Todo el Sistema (Recomendado)
Desde la raíz del proyecto:
```bash
docker-compose up --build
```

Servicios disponibles:
- **Frontend**: http://localhost:3000
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger
- **SQL Server**: localhost:1433

## 📱 Uso de la Aplicación

1. **Completa el Formulario**
   - Ingresa tu peso en kilogramos
   - Ingresa tu altura en metros
   - Ingresa tu edad
   - Selecciona tu género

2. **Calcula tu IMC**
   - Haz clic en "Calcular Mi IMC"
   - Espera los resultados

3. **Visualiza tu Resultado**
   - Tu IMC se mostrará en grande
   - Verás tu categoría con color específico
   - Recibirás una recomendación personalizada

4. **Revisa tu Historial**
   - Todos tus cálculos se guardan automáticamente
   - Puedes ver hasta 10 registros anteriores
   - Puedes eliminar registros individuales

## 🎯 Categorías de IMC

| Rango IMC | Categoría | Color |
|-----------|-----------|-------|
| < 18.5 | Bajo peso | Azul |
| 18.5 - 24.9 | Peso normal | Verde |
| 25.0 - 29.9 | Sobrepeso | Amarillo |
| 30.0 - 34.9 | Obesidad | Naranja |
| ≥ 35.0 | Obesidad severa | Rojo |

## 🔧 Configuración

### Conectar con la API Real

En el archivo `app.js`, encontrarás dos opciones:

**Opción 1: Cálculo Local (Predeterminado)**
```javascript
// El código actual hace cálculos en el navegador
// No requiere la API
```

**Opción 2: Usar la API Backend**
```javascript
// Descomenta el bloque de código que dice:
// "Si quieres usar la API real, descomenta esto"

// Y ajusta la URL de la API si es necesario:
const API_URL = 'http://localhost:5000/api';
```

## 📂 Estructura del Proyecto

```
frontend/
├── index.html          # Página principal con toda la UI
├── app.js              # Lógica de la aplicación
├── nginx.conf          # Configuración de Nginx
├── Dockerfile          # Imagen Docker del frontend
└── README.md           # Este archivo
```

## 🌟 Funcionalidades Destacadas

### 1. Validaciones Inteligentes
- Límites automáticos en los campos numéricos
- Mensajes de error claros y visibles
- Validación antes de calcular

### 2. Interfaz Intuitiva
- Iconos descriptivos en cada campo
- Placeholders con ejemplos
- Botones grandes y fáciles de usar

### 3. Historial Persistente
- Los datos se guardan en el navegador
- Sobrevive a recargas de página
- Fácil de limpiar registro por registro

### 4. Diseño Profesional
- Paleta de colores moderna
- Gradientes suaves
- Sombras y profundidad
- Animaciones no intrusivas

## 🔒 Seguridad

- No se envían datos a servidores externos
- Los cálculos se hacen localmente (por defecto)
- Headers de seguridad configurados en Nginx
- Validación tanto en cliente como servidor

## 📊 Optimizaciones

- Caché de archivos estáticos
- Compresión Gzip habilitada
- Imágenes y fuentes optimizadas
- Carga rápida y eficiente

## 🤝 Integración con la API

Si deseas usar la API backend completa:

1. Asegúrate de que la API esté corriendo
2. Descomenta el código de API en `app.js`
3. Verifica la URL de conexión
4. Los datos se guardarán en SQL Server

## 💡 Consejos de Uso

- **Móvil**: La aplicación funciona perfectamente en celulares
- **Historial**: Puedes eliminar registros tocando el icono de basura
- **Privacidad**: Todo se guarda en tu navegador, nada se envía a internet
- **Actualización**: Recarga la página para ver cambios en el código

## 🐛 Solución de Problemas

### El frontend no carga
```bash
# Verifica que el contenedor esté corriendo
docker ps

# Revisa los logs
docker logs imc-frontend
```

### No se conecta a la API
```bash
# Verifica que la API esté corriendo
docker ps | grep imc-api

# Revisa la configuración de CORS en Program.cs
```

### El historial no se guarda
- Verifica que tu navegador permita localStorage
- Revisa la consola del navegador (F12)

## 📞 Soporte

Si tienes problemas o sugerencias:
1. Revisa la consola del navegador (F12)
2. Verifica los logs de Docker
3. Asegúrate de que todos los servicios estén corriendo

---

**Creado con ❤️ para tu salud**

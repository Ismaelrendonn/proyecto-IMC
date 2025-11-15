# 🚀 Guía Rápida de Inicio

## Para Empezar en 3 Pasos

### 1️⃣ Construir y Ejecutar Todo el Sistema

Desde la carpeta raíz del proyecto (`proyecto-IMC`):

```powershell
docker-compose up --build
```

### 2️⃣ Acceder a la Aplicación

Abre tu navegador en:
- **Frontend (Lo nuevo y bonito)**: http://localhost:3000
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger

### 3️⃣ ¡Úsalo!

1. Ingresa tu peso, altura, edad y género
2. Haz clic en "Calcular Mi IMC"
3. ¡Disfruta de los resultados con diseño hermoso!

---

## 🎨 ¿Qué Hay de Nuevo?

### ✨ Frontend Moderno
- Diseño profesional con gradientes morados
- Animaciones suaves y elegantes
- Totalmente responsive (funciona en celular)
- Iconos modernos de Font Awesome
- Historial de cálculos guardado localmente

### 📊 Características Destacadas
- **Resultados Visuales**: Colores según categoría de IMC
- **Recomendaciones**: Consejos personalizados de salud
- **Historial**: Guarda tus últimos 10 cálculos
- **Validación**: Te avisa si ingresas datos incorrectos
- **Offline**: Funciona sin necesidad de la API

---

## 🔧 Comandos Útiles

### Iniciar Solo el Frontend
```powershell
cd frontend
docker build -t imc-frontend .
docker run -p 3000:80 imc-frontend
```

### Detener Todo
```powershell
docker-compose down
```

### Ver Logs
```powershell
# Ver logs del frontend
docker logs imc-frontend

# Ver logs de la API
docker logs imc-api

# Ver logs en tiempo real
docker-compose logs -f
```

### Reiniciar un Servicio
```powershell
docker-compose restart frontend
docker-compose restart api
```

---

## 📱 Probando en el Navegador

### En tu Computadora
Abre: http://localhost:3000

### En tu Celular (Misma Red WiFi)
1. Encuentra tu IP local:
   ```powershell
   ipconfig
   ```
2. Busca tu dirección IPv4 (ejemplo: 192.168.1.10)
3. En tu celular, abre: http://TU-IP:3000

---

## 🎯 Arquitectura del Sistema

```
┌─────────────────┐
│   Navegador     │  ← Usuario ingresa aquí
│  (localhost:3000)│
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│   Frontend      │  ← HTML + CSS + JavaScript bonito
│   (Nginx)       │     Cálculos locales o llama a API
└────────┬────────┘
         │ (Opcional)
         ▼
┌─────────────────┐
│   API Backend   │  ← .NET 8 Web API
│ (localhost:5000)│     Procesa y guarda en BD
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│   SQL Server    │  ← Base de datos
│ (localhost:1433)│     Almacena historial
└─────────────────┘
```

---

## 🎨 Colores de Categorías IMC

| IMC | Categoría | Color |
|-----|-----------|-------|
| < 18.5 | Bajo peso | 🔵 Azul |
| 18.5 - 24.9 | Normal | 🟢 Verde |
| 25.0 - 29.9 | Sobrepeso | 🟡 Amarillo |
| 30.0 - 34.9 | Obesidad | 🟠 Naranja |
| ≥ 35.0 | Obesidad severa | 🔴 Rojo |

---

## 💡 Consejos Pro

### Para Desarrollo
- El frontend funciona de forma **independiente** (no necesita API)
- Los cálculos se hacen en JavaScript por defecto
- Puedes habilitar la API editando `app.js`

### Para Producción
- Cambiar contraseñas en `docker-compose.yml`
- Ajustar CORS en `Program.cs`
- Usar HTTPS en producción
- Limitar origins en CORS

---

## 🐛 Solución Rápida de Problemas

### ❌ Puerto 3000 ocupado
```powershell
# Cambiar puerto en docker-compose.yml
ports:
  - "8080:80"  # Usar 8080 en lugar de 3000
```

### ❌ Error de CORS
- Verifica que la API tenga configurado CORS correctamente
- Chequea la URL en `app.js` (línea 2)

### ❌ Contenedor no inicia
```powershell
# Ver qué pasó
docker-compose logs frontend

# Reconstruir desde cero
docker-compose down
docker-compose up --build
```

---

## 📚 Archivos Importantes

- **frontend/index.html**: Toda la interfaz visual
- **frontend/app.js**: Lógica de cálculos y animaciones
- **frontend/nginx.conf**: Configuración del servidor web
- **docker-compose.yml**: Orquestación de contenedores
- **IMCAPI/IMCAPI/Program.cs**: Configuración de la API

---

## 🎉 ¡Listo para Usar!

Ahora tienes un sistema completo con:
✅ Frontend hermoso y moderno
✅ API robusta con .NET 8
✅ Base de datos SQL Server
✅ Todo en contenedores Docker
✅ Fácil de desplegar y mantener

**¡Disfruta tu nueva Calculadora IMC!** 💪

---

**Nota**: Este frontend es completamente **independiente** del anterior.
Es un "proyecto aparte" como pediste, más bonito, fácil de usar y llamativo.

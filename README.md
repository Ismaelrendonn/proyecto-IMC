# 🏥 Calculadora IMC - Sistema Completo

Sistema profesional para calcular el Índice de Masa Corporal (IMC) con frontend moderno y API robusta.

## 🎨 Vista Previa

### Frontend Moderno (¡NUEVO!)
- ✨ Diseño hermoso con gradientes y animaciones
- 📱 Totalmente responsive (móvil, tablet, desktop)
- 🎯 Interfaz intuitiva y fácil de usar
- 📊 Visualización clara de resultados con colores
- 📝 Historial de cálculos guardado localmente
- 💡 Recomendaciones personalizadas de salud

## 🚀 Inicio Rápido

```powershell
# Clonar el repositorio
git clone https://github.com/Ismaelrendonn/proyecto-IMC.git
cd proyecto-IMC

# Ejecutar todo el sistema con Docker
docker-compose up --build
```

**Accede a:**
- 🎨 **Frontend Moderno**: http://localhost:3000
- 🔧 **API Backend**: http://localhost:5000
- 📚 **Swagger UI**: http://localhost:5000/swagger

## 📦 Estructura del Proyecto

```
proyecto-IMC/
├── frontend/                    # 🎨 Aplicación web moderna (NUEVA)
│   ├── index.html              # Interfaz visual hermosa
│   ├── app.js                  # Lógica interactiva
│   ├── nginx.conf              # Configuración del servidor
│   ├── Dockerfile              # Contenedor del frontend
│   └── README.md               # Documentación del frontend
│
├── IMCAPI/                     # 🔧 API Backend (.NET 8)
│   ├── IMCAPI/
│   │   ├── Controllers/        # Endpoints de la API
│   │   ├── Services/           # Lógica de negocio
│   │   ├── Models/             # Modelos de datos
│   │   ├── Data/               # DbContext y migraciones
│   │   ├── Program.cs          # Configuración de la API
│   │   └── Dockerfile          # Contenedor de la API
│   └── TestIMCAPI/             # Pruebas unitarias
│
├── docker-compose.yml          # Orquestación de contenedores
├── INICIO-RAPIDO.md            # Guía rápida de inicio
└── README.md                   # Este archivo
```

## 🛠️ Tecnologías Utilizadas

### Frontend
- **HTML5 + CSS3**: Interfaz moderna con animaciones
- **JavaScript (Vanilla)**: Sin dependencias pesadas
- **Bootstrap 5.3**: Framework CSS responsive
- **Font Awesome 6.4**: Iconografía profesional
- **Nginx**: Servidor web ligero y rápido

### Backend
- **.NET 8**: Framework moderno y potente
- **Entity Framework Core**: ORM para base de datos
- **SQL Server**: Base de datos robusta
- **Swagger/OpenAPI**: Documentación automática

### DevOps
- **Docker**: Contenedores para todo
- **Docker Compose**: Orquestación simple

## 🎯 Características Principales

### ✨ Frontend Moderno
- Diseño atractivo con gradientes morados
- Animaciones suaves y profesionales
- Responsive para todos los dispositivos
- Historial local de cálculos
- Validaciones en tiempo real
- Recomendaciones de salud personalizadas

### 🔧 API Robusta
- RESTful API con .NET 8
- Validaciones completas
- Pruebas unitarias
- Documentación Swagger
- Base de datos SQL Server

### 🐳 Fácil Despliegue
- Todo en contenedores Docker
- Un comando para iniciar todo
- Fácil de escalar y mantener

## 📖 Documentación Detallada

- **[INICIO-RAPIDO.md](./INICIO-RAPIDO.md)**: Guía rápida para empezar
- **[frontend/README.md](./frontend/README.md)**: Documentación del frontend
- **Swagger UI**: http://localhost:5000/swagger (cuando está corriendo)

## 🎨 Categorías de IMC

| Rango IMC | Categoría | Color | Icono |
|-----------|-----------|-------|-------|
| < 18.5 | Bajo peso | 🔵 Azul | ⬇️ |
| 18.5 - 24.9 | Peso normal | 🟢 Verde | ✅ |
| 25.0 - 29.9 | Sobrepeso | 🟡 Amarillo | ⚠️ |
| 30.0 - 34.9 | Obesidad | 🟠 Naranja | 🔶 |
| ≥ 35.0 | Obesidad severa | 🔴 Rojo | 🚨 |

## 🔧 Comandos Útiles

```powershell
# Iniciar todo el sistema
docker-compose up --build

# Iniciar en segundo plano
docker-compose up -d

# Ver logs en tiempo real
docker-compose logs -f

# Ver logs de un servicio específico
docker-compose logs frontend
docker-compose logs api

# Detener todo
docker-compose down

# Detener y eliminar volúmenes (base de datos)
docker-compose down -v

# Reiniciar un servicio
docker-compose restart frontend
```

## 🧪 Ejecutar Pruebas

```powershell
cd IMCAPI
dotnet test
```

## 📱 Uso del Frontend

1. **Ingresa tus datos**:
   - Peso en kilogramos
   - Altura en metros
   - Edad en años
   - Género

2. **Haz clic en "Calcular Mi IMC"**

3. **Visualiza tus resultados**:
   - Tu IMC con tamaño grande
   - Categoría con color específico
   - Recomendación personalizada
   - Se guarda automáticamente en el historial

## 🌐 Conectar desde Móvil (Misma Red)

1. Encuentra tu IP local:
   ```powershell
   ipconfig
   ```

2. Busca tu IPv4 (ej: 192.168.1.10)

3. En tu móvil, abre:
   ```
   http://TU-IP:3000
   ```

## 🔒 Seguridad

- Validaciones en cliente y servidor
- Headers de seguridad configurados
- CORS configurado apropiadamente
- Contraseñas deben cambiarse en producción

## 🤝 Contribuir

1. Fork el proyecto
2. Crea tu rama: `git checkout -b feature/AmazingFeature`
3. Commit tus cambios: `git commit -m 'Add: Amazing Feature'`
4. Push a la rama: `git push origin feature/AmazingFeature`
5. Abre un Pull Request

## 📝 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👨‍💻 Autor

**Ismael Rendón**
- GitHub: [@Ismaelrendonn](https://github.com/Ismaelrendonn)

## 🎉 Agradecimientos

- Bootstrap por el framework CSS
- Font Awesome por los iconos
- La comunidad de .NET por las herramientas

---

**¡Disfruta tu nueva Calculadora IMC con interfaz hermosa!** 💪❤️

*Hecho con ❤️ para promover la salud y el bienestar*
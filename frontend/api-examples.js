// =============================================================
// GUÍA DE INTEGRACIÓN CON LA API
// =============================================================
// Este archivo contiene ejemplos de cómo usar la API backend
// desde el frontend. Por defecto, el frontend funciona sin API,
// pero puedes habilitar la integración siguiendo estos ejemplos.
// =============================================================

// -------------------------------------------------------------
// CONFIGURACIÓN BASE
// -------------------------------------------------------------

// URLs de la API según el entorno
const API_CONFIG = {
    // Desarrollo local
    local: 'http://localhost:5000/api',
    
    // Docker (contenedores)
    docker: 'http://imc-api/api',
    
    // Producción (cambiar por tu dominio)
    production: 'https://tu-dominio.com/api'
};

// Detectar entorno automáticamente
function getApiUrl() {
    const hostname = window.location.hostname;
    
    if (hostname === 'localhost' || hostname === '127.0.0.1') {
        return API_CONFIG.local;
    } else if (hostname.includes('docker')) {
        return API_CONFIG.docker;
    } else {
        return API_CONFIG.production;
    }
}

// -------------------------------------------------------------
// EJEMPLO 1: CALCULAR IMC CON LA API
// -------------------------------------------------------------

async function calcularIMCConAPI(peso, altura, edad, genero) {
    const apiUrl = getApiUrl();
    
    try {
        const response = await fetch(`${apiUrl}/CalculoIMCs`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                peso: peso,
                altura: altura,
                edad: edad,
                genero: genero
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        
        // Respuesta esperada:
        // {
        //   "id": 1,
        //   "peso": 70,
        //   "altura": 1.75,
        //   "edad": 25,
        //   "genero": "Masculino",
        //   "imc": 22.86,
        //   "categoria": "Peso normal",
        //   "recomendacion": "¡Excelente! Mantén tu peso...",
        //   "fechaCalculo": "2025-11-04T15:30:00"
        // }
        
        return data;
        
    } catch (error) {
        console.error('Error al calcular IMC:', error);
        throw error;
    }
}

// Uso:
// const resultado = await calcularIMCConAPI(70, 1.75, 25, 'Masculino');
// console.log(resultado);

// -------------------------------------------------------------
// EJEMPLO 2: OBTENER HISTORIAL DE LA API
// -------------------------------------------------------------

async function obtenerHistorialAPI() {
    const apiUrl = getApiUrl();
    
    try {
        const response = await fetch(`${apiUrl}/CalculoIMCs`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const historial = await response.json();
        
        // Respuesta esperada: Array de cálculos
        // [
        //   { id: 1, peso: 70, altura: 1.75, ... },
        //   { id: 2, peso: 68, altura: 1.75, ... }
        // ]
        
        return historial;
        
    } catch (error) {
        console.error('Error al obtener historial:', error);
        throw error;
    }
}

// Uso:
// const historial = await obtenerHistorialAPI();
// historial.forEach(calculo => console.log(calculo));

// -------------------------------------------------------------
// EJEMPLO 3: OBTENER UN CÁLCULO ESPECÍFICO
// -------------------------------------------------------------

async function obtenerCalculoPorId(id) {
    const apiUrl = getApiUrl();
    
    try {
        const response = await fetch(`${apiUrl}/CalculoIMCs/${id}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
            }
        });

        if (!response.ok) {
            if (response.status === 404) {
                throw new Error('Cálculo no encontrado');
            }
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const calculo = await response.json();
        return calculo;
        
    } catch (error) {
        console.error('Error al obtener cálculo:', error);
        throw error;
    }
}

// Uso:
// const calculo = await obtenerCalculoPorId(1);
// console.log(calculo);

// -------------------------------------------------------------
// EJEMPLO 4: MANEJO DE ERRORES COMPLETO
// -------------------------------------------------------------

async function calcularIMCConManejoDeErrores(peso, altura, edad, genero) {
    const apiUrl = getApiUrl();
    
    try {
        // Validaciones antes de enviar
        if (peso <= 0 || peso > 500) {
            throw new Error('Peso inválido');
        }
        
        if (altura <= 0 || altura > 3) {
            throw new Error('Altura inválida');
        }
        
        // Hacer la petición
        const response = await fetch(`${apiUrl}/CalculoIMCs`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ peso, altura, edad, genero })
        });

        // Manejar diferentes códigos de estado
        if (response.status === 400) {
            const errorData = await response.json();
            throw new Error(`Datos inválidos: ${JSON.stringify(errorData)}`);
        }
        
        if (response.status === 500) {
            throw new Error('Error del servidor. Intenta más tarde.');
        }
        
        if (!response.ok) {
            throw new Error(`Error HTTP: ${response.status}`);
        }

        const data = await response.json();
        return { success: true, data };
        
    } catch (error) {
        console.error('Error:', error.message);
        return { success: false, error: error.message };
    }
}

// Uso:
// const resultado = await calcularIMCConManejoDeErrores(70, 1.75, 25, 'Masculino');
// if (resultado.success) {
//     console.log('IMC:', resultado.data.imc);
// } else {
//     console.error('Error:', resultado.error);
// }

// -------------------------------------------------------------
// EJEMPLO 5: INTEGRACIÓN COMPLETA EN EL FORMULARIO
// -------------------------------------------------------------

// Esta función reemplazaría el manejo actual del formulario
// para usar la API en lugar de cálculos locales

async function integrarAPIEnFormulario() {
    const form = document.getElementById('imcForm');
    
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        // Obtener valores
        const peso = parseFloat(document.getElementById('peso').value);
        const altura = parseFloat(document.getElementById('altura').value);
        const edad = parseInt(document.getElementById('edad').value);
        const genero = document.getElementById('genero').value;
        
        // Mostrar loading
        showLoading(true);
        
        try {
            // Usar la API
            const resultado = await calcularIMCConAPI(peso, altura, edad, genero);
            
            // Mostrar resultados
            displayResults(
                resultado.imc,
                resultado.categoria,
                resultado.recomendacion
            );
            
            // Guardar en historial local también
            saveToHistory({
                fecha: new Date(resultado.fechaCalculo).toLocaleString('es-ES'),
                peso: resultado.peso,
                altura: resultado.altura,
                edad: resultado.edad,
                genero: resultado.genero,
                imc: resultado.imc.toFixed(2),
                categoria: resultado.categoria
            });
            
            showSuccess('¡Cálculo realizado exitosamente!');
            
        } catch (error) {
            // En caso de error, usar cálculo local como fallback
            console.warn('API no disponible, usando cálculo local:', error);
            
            const imc = peso / (altura * altura);
            const categoria = getCategoria(imc);
            const recomendacion = getRecomendacion(categoria);
            
            displayResults(imc, categoria, recomendacion);
            
            showWarning('Cálculo realizado localmente (API no disponible)');
            
        } finally {
            showLoading(false);
        }
    });
}

// -------------------------------------------------------------
// EJEMPLO 6: SINCRONIZACIÓN DE HISTORIAL
// -------------------------------------------------------------

async function sincronizarHistorial() {
    try {
        // Obtener historial de la API
        const historialAPI = await obtenerHistorialAPI();
        
        // Obtener historial local
        const historialLocal = JSON.parse(localStorage.getItem('imcHistory')) || [];
        
        // Combinar ambos historiales (evitando duplicados)
        const historialCombinado = [...historialAPI];
        
        historialLocal.forEach(itemLocal => {
            const existe = historialAPI.some(itemAPI => 
                itemAPI.peso === itemLocal.peso &&
                itemAPI.altura === itemLocal.altura &&
                new Date(itemAPI.fechaCalculo).getTime() === new Date(itemLocal.fecha).getTime()
            );
            
            if (!existe) {
                historialCombinado.push(itemLocal);
            }
        });
        
        // Ordenar por fecha (más reciente primero)
        historialCombinado.sort((a, b) => {
            const fechaA = new Date(a.fechaCalculo || a.fecha);
            const fechaB = new Date(b.fechaCalculo || b.fecha);
            return fechaB - fechaA;
        });
        
        // Guardar en localStorage
        localStorage.setItem('imcHistory', JSON.stringify(historialCombinado.slice(0, 10)));
        
        // Renderizar
        renderHistory();
        
        return historialCombinado;
        
    } catch (error) {
        console.error('Error al sincronizar historial:', error);
        // Continuar con historial local si falla la API
        renderHistory();
    }
}

// -------------------------------------------------------------
// EJEMPLO 7: VERIFICAR ESTADO DE LA API
// -------------------------------------------------------------

async function verificarEstadoAPI() {
    const apiUrl = getApiUrl();
    
    try {
        const response = await fetch(`${apiUrl}/health`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
            }
        });
        
        if (response.ok) {
            console.log('✅ API disponible');
            return true;
        } else {
            console.warn('⚠️ API respondió con error:', response.status);
            return false;
        }
        
    } catch (error) {
        console.warn('❌ API no disponible:', error.message);
        return false;
    }
}

// Verificar al cargar la página
window.addEventListener('DOMContentLoaded', async () => {
    const apiDisponible = await verificarEstadoAPI();
    
    if (apiDisponible) {
        console.log('Modo: API Backend');
        // Sincronizar historial con la API
        await sincronizarHistorial();
    } else {
        console.log('Modo: Cálculo Local (Offline)');
        // Usar solo historial local
        renderHistory();
    }
});

// -------------------------------------------------------------
// NOTAS DE USO
// -------------------------------------------------------------

/*
PARA HABILITAR LA API EN TU APLICACIÓN:

1. Abre app.js en tu editor

2. Encuentra la sección "Si quieres usar la API real, descomenta esto"

3. Descomenta ese bloque de código

4. Comenta o elimina el bloque de cálculo local (setTimeout)

5. Asegúrate de que la API esté corriendo:
   - docker-compose up (si usas Docker)
   - dotnet run (si estás en desarrollo local)

6. Verifica que CORS esté configurado en Program.cs:
   - Debe incluir el origen de tu frontend
   - http://localhost:3000 para Docker
   - http://localhost:80 para desarrollo

7. Prueba la conexión abriendo la consola del navegador (F12)
   y buscando mensajes de éxito o error

VENTAJAS DE USAR LA API:
- Los datos se persisten en base de datos
- Puedes acceder al historial desde cualquier dispositivo
- Mayor control y seguridad
- Escalabilidad

VENTAJAS DEL MODO LOCAL:
- Funciona sin conexión (offline)
- Respuesta instantánea
- No requiere servidor
- Más privacidad (datos solo en el navegador)

MODO HÍBRIDO (RECOMENDADO):
- Intenta usar la API primero
- Si falla, usa cálculo local (fallback)
- Mejor experiencia de usuario
- Funciona siempre
*/

// -------------------------------------------------------------
// FIN DE LA GUÍA
// -------------------------------------------------------------

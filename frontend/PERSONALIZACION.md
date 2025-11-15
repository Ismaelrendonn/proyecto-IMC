# 🎨 Guía de Personalización del Frontend

¿Quieres cambiar los colores, textos o funcionalidad? ¡Esta guía te ayudará!

## 🎨 Cambiar Colores

### Cambiar el Color Principal (Gradiente Morado)

En `index.html`, busca en la sección `<style>`:

```css
/* ANTES: */
background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);

/* DESPUÉS (ejemplo con azul): */
background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);

/* DESPUÉS (ejemplo con verde): */
background: linear-gradient(135deg, #56ab2f 0%, #a8e063 100%);

/* DESPUÉS (ejemplo con rojo): */
background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);
```

### Cambiar Colores de Categorías IMC

En `index.html`, busca las clases `.category-*`:

```css
/* Bajo peso */
.category-bajo-peso { 
    background: linear-gradient(135deg, #56ccf2 0%, #2f80ed 100%); 
}

/* Peso normal */
.category-peso-normal { 
    background: linear-gradient(135deg, #56ab2f 0%, #a8e063 100%); 
}

/* Sobrepeso */
.category-sobrepeso { 
    background: linear-gradient(135deg, #f7971e 0%, #ffd200 100%); 
}

/* Obesidad */
.category-obesidad { 
    background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%); 
}

/* Obesidad severa */
.category-obesidad-severa { 
    background: linear-gradient(135deg, #870000 0%, #190a05 100%); 
}
```

### Generador de Gradientes

Visita: https://cssgradient.io/ para crear tus propios gradientes personalizados.

---

## 📝 Cambiar Textos

### Título Principal

En `index.html`:

```html
<!-- ANTES: -->
<h1><i class="fas fa-heartbeat"></i> Calculadora IMC</h1>
<p>Conoce tu estado de salud y recibe recomendaciones personalizadas</p>

<!-- DESPUÉS: -->
<h1><i class="fas fa-heart"></i> Mi Salud App</h1>
<p>Tu asistente personal de bienestar</p>
```

### Botón de Cálculo

```html
<!-- ANTES: -->
<button type="submit" class="btn btn-calculate">
    <i class="fas fa-calculator"></i> Calcular Mi IMC
</button>

<!-- DESPUÉS: -->
<button type="submit" class="btn btn-calculate">
    <i class="fas fa-check-circle"></i> ¡Calcular Ahora!
</button>
```

### Recomendaciones Personalizadas

En `app.js`, busca la función `getRecomendacion()`:

```javascript
const recomendaciones = {
    'Bajo peso': 'Tu mensaje personalizado aquí...',
    'Peso normal': 'Tu mensaje personalizado aquí...',
    // ... etc
};
```

---

## 🔤 Cambiar Fuente

### Usar Otra Fuente de Google Fonts

1. Ve a: https://fonts.google.com/
2. Selecciona tu fuente favorita (ejemplo: Montserrat, Roboto, Open Sans)
3. En `index.html`, reemplaza:

```html
<!-- ANTES: -->
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">

<!-- DESPUÉS (ejemplo con Montserrat): -->
<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700&display=swap" rel="stylesheet">
```

4. En el CSS, cambia:

```css
/* ANTES: */
font-family: 'Poppins', sans-serif;

/* DESPUÉS: */
font-family: 'Montserrat', sans-serif;
```

---

## 🖼️ Cambiar Iconos

### Usar Diferentes Iconos de Font Awesome

Busca iconos en: https://fontawesome.com/icons

```html
<!-- Peso -->
<i class="fas fa-weight"></i>        <!-- Actual -->
<i class="fas fa-balance-scale"></i> <!-- Alternativa -->

<!-- Altura -->
<i class="fas fa-ruler-vertical"></i> <!-- Actual -->
<i class="fas fa-arrows-alt-v"></i>   <!-- Alternativa -->

<!-- Edad -->
<i class="fas fa-birthday-cake"></i>  <!-- Actual -->
<i class="fas fa-calendar-alt"></i>   <!-- Alternativa -->

<!-- Género -->
<i class="fas fa-venus-mars"></i>     <!-- Actual -->
<i class="fas fa-user"></i>           <!-- Alternativa -->
```

---

## 🎭 Modificar Animaciones

### Cambiar Velocidad de Animaciones

En `index.html`, en la sección CSS:

```css
/* ANTES (0.8 segundos): */
animation: fadeInDown 0.8s ease;

/* DESPUÉS (más rápido): */
animation: fadeInDown 0.4s ease;

/* DESPUÉS (más lento): */
animation: fadeInDown 1.5s ease;
```

### Desactivar Animaciones

Para desactivar todas las animaciones:

```css
* {
    animation: none !important;
    transition: none !important;
}
```

### Agregar Nuevas Animaciones

```css
/* Definir animación de rebote */
@keyframes bounce {
    0%, 100% { transform: translateY(0); }
    50% { transform: translateY(-10px); }
}

/* Aplicar a un elemento */
.btn-calculate {
    animation: bounce 2s infinite;
}
```

---

## 📐 Ajustar Tamaños

### Tamaño del Valor IMC

En `index.html`, busca `.imc-value`:

```css
/* ANTES: */
.imc-value {
    font-size: 4rem;  /* 64px */
}

/* DESPUÉS (más grande): */
.imc-value {
    font-size: 6rem;  /* 96px */
}

/* DESPUÉS (más pequeño): */
.imc-value {
    font-size: 3rem;  /* 48px */
}
```

### Ancho Máximo de la Aplicación

```css
/* ANTES: */
.container-main {
    max-width: 1200px;
}

/* DESPUÉS (más ancho): */
.container-main {
    max-width: 1400px;
}

/* DESPUÉS (más angosto): */
.container-main {
    max-width: 900px;
}
```

---

## 🔢 Modificar Límites de Valores

En `app.js`:

```javascript
// Límites de Peso
<input type="number" min="1" max="500">  // Actual
<input type="number" min="10" max="300"> // Personalizado

// Límites de Altura
<input type="number" min="0.5" max="3">  // Actual
<input type="number" min="1" max="2.5">  // Personalizado

// Límites de Edad
<input type="number" min="1" max="120">  // Actual
<input type="number" min="18" max="100"> // Solo adultos
```

---

## 🌐 Cambiar Idioma

### Traducir a Inglés

En `index.html`:

```html
<!-- Español -->
<label>Peso</label>
<span class="input-group-text">kg</span>

<!-- Inglés -->
<label>Weight</label>
<span class="input-group-text">kg</span>
```

En `app.js`:

```javascript
// Español
const recomendaciones = {
    'Bajo peso': 'Se recomienda...',
};

// Inglés
const recommendations = {
    'Underweight': 'It is recommended...',
};
```

---

## 🎨 Temas Predefinidos

### Tema Oscuro

Agrega en el `<style>`:

```css
body {
    background: linear-gradient(135deg, #1a1a2e 0%, #16213e 100%);
}

.main-card {
    background: #0f3460;
    color: white;
}

.form-control, .form-select {
    background: #16213e;
    color: white;
    border-color: #e94560;
}
```

### Tema Minimalista

```css
body {
    background: #f5f5f5;
}

.main-card {
    background: white;
    box-shadow: 0 2px 10px rgba(0,0,0,0.1);
}

.btn-calculate {
    background: black;
    border-radius: 4px;
}
```

### Tema Colorido

```css
body {
    background: linear-gradient(135deg, #ff6b6b 0%, #4ecdc4 50%, #45b7d1 100%);
}

.btn-calculate {
    background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
}
```

---

## 🔧 Agregar Funcionalidades

### Agregar Unidad Imperial (Libras/Pies)

En `index.html`, agrega un selector:

```html
<select id="sistema">
    <option value="metrico">Métrico (kg/m)</option>
    <option value="imperial">Imperial (lb/ft)</option>
</select>
```

En `app.js`, agrega conversión:

```javascript
const sistema = document.getElementById('sistema').value;

if (sistema === 'imperial') {
    // Convertir libras a kg: lb * 0.453592
    peso = peso * 0.453592;
    
    // Convertir pies a metros: ft * 0.3048
    altura = altura * 0.3048;
}
```

### Agregar Exportación de Datos

```javascript
function exportarHistorial() {
    const historial = JSON.parse(localStorage.getItem('imcHistory')) || [];
    const csv = convertirACSV(historial);
    descargarArchivo(csv, 'historial-imc.csv');
}

function convertirACSV(data) {
    const headers = 'Fecha,Peso,Altura,IMC,Categoria\n';
    const rows = data.map(item => 
        `${item.fecha},${item.peso},${item.altura},${item.imc},${item.categoria}`
    ).join('\n');
    return headers + rows;
}

function descargarArchivo(contenido, nombreArchivo) {
    const blob = new Blob([contenido], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = nombreArchivo;
    a.click();
}
```

### Agregar Gráfico de Progreso

1. Incluir Chart.js:

```html
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
```

2. Crear un canvas:

```html
<canvas id="imcChart"></canvas>
```

3. Generar el gráfico:

```javascript
function crearGrafico() {
    const ctx = document.getElementById('imcChart').getContext('2d');
    const historial = JSON.parse(localStorage.getItem('imcHistory')) || [];
    
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: historial.map(h => h.fecha),
            datasets: [{
                label: 'IMC',
                data: historial.map(h => h.imc),
                borderColor: '#667eea',
                tension: 0.4
            }]
        }
    });
}
```

---

## 📱 Optimizaciones Móviles

### Aumentar Tamaño de Botones para Móvil

```css
@media (max-width: 768px) {
    .btn-calculate {
        padding: 20px 40px;
        font-size: 1.3rem;
    }
}
```

### Mejorar Inputs en Móvil

```html
<!-- Usar teclado numérico en móvil -->
<input type="number" inputmode="decimal" pattern="[0-9]*">
```

---

## 🔒 Personalización Avanzada

### Cambiar Puerto del Contenedor

En `docker-compose.yml`:

```yaml
frontend:
  ports:
    - "8080:80"  # Cambiar 3000 por 8080
```

### Agregar Variables de Entorno

En `docker-compose.yml`:

```yaml
frontend:
  environment:
    - API_URL=http://mi-api-personalizada.com
```

---

## 💡 Consejos de Personalización

1. **Haz una copia de seguridad** antes de hacer cambios grandes
2. **Prueba en varios navegadores** (Chrome, Firefox, Safari)
3. **Verifica en móvil** usando las DevTools (F12 → Toggle device)
4. **Usa colores con buen contraste** para accesibilidad
5. **Mantén la simplicidad** - menos es más

---

## 🆘 Problemas Comunes

### Los cambios no se ven
- Limpia caché: Ctrl + Shift + R (Windows) o Cmd + Shift + R (Mac)
- Cierra y abre el navegador
- Verifica que editaste el archivo correcto

### Se rompió el diseño
- Verifica que no falten llaves `{ }` en el CSS
- Revisa la consola del navegador (F12)
- Compara con el archivo original

### Los colores no funcionan
- Asegúrate de usar formato correcto: `#hexadecimal` o `rgb(r,g,b)`
- Verifica que no haya punto y coma (`;`) faltante

---

**¡Diviértete personalizando tu aplicación! 🎨✨**

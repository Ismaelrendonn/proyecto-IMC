// Configuración de la API
// Detectar si estamos en Docker o desarrollo local
const isDocker = window.location.hostname !== 'localhost';
const API_URL = isDocker 
    ? 'http://imc-api/api'  // Nombre del contenedor en Docker
    : 'http://localhost:5000/api';  // Desarrollo local

// Elementos del DOM
const form = document.getElementById('imcForm');
const resultSection = document.getElementById('resultSection');
const imcValue = document.getElementById('imcValue');
const imcCategory = document.getElementById('imcCategory');
const recommendationText = document.getElementById('recommendationText');
const historyContainer = document.getElementById('historyContainer');
const loadingSpinner = document.getElementById('loadingSpinner');

// Historial en LocalStorage
let history = JSON.parse(localStorage.getItem('imcHistory')) || [];

// Cargar historial al iniciar
window.addEventListener('DOMContentLoaded', () => {
    renderHistory();
});

// Manejar envío del formulario
form.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    // Obtener valores del formulario
    const peso = parseFloat(document.getElementById('peso').value);
    const altura = parseFloat(document.getElementById('altura').value);
    const edad = parseInt(document.getElementById('edad').value);
    const genero = document.getElementById('genero').value;
    
    // Validaciones
    if (!validateInputs(peso, altura, edad, genero)) {
        return;
    }
    
    // Mostrar loading
    showLoading(true);
    
    // Calcular IMC
    const imc = peso / (altura * altura);
    const categoria = getCategoria(imc);
    const recomendacion = getRecomendacion(categoria);
    
    // Simular llamada a API (puedes habilitar la API real si lo deseas)
    setTimeout(() => {
        // Mostrar resultados
        displayResults(imc, categoria, recomendacion);
        
        // Guardar en historial
        saveToHistory({
            fecha: new Date().toLocaleString('es-ES'),
            peso,
            altura,
            edad,
            genero,
            imc: imc.toFixed(2),
            categoria
        });
        
        // Ocultar loading
        showLoading(false);
        
        // Scroll suave a resultados
        resultSection.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
    }, 800);
    
    // Si quieres usar la API real, descomenta esto:
    /*
    try {
        const response = await fetch(`${API_URL}/CalculoIMCs`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                peso,
                altura,
                edad,
                genero
            })
        });
        
        if (!response.ok) {
            throw new Error('Error al calcular IMC');
        }
        
        const data = await response.json();
        displayResults(data.imc, data.categoria, data.recomendacion);
        saveToHistory({
            fecha: new Date().toLocaleString('es-ES'),
            peso,
            altura,
            edad,
            genero,
            imc: data.imc,
            categoria: data.categoria
        });
        
    } catch (error) {
        showError('Error al calcular el IMC. Por favor, intenta de nuevo.');
        console.error(error);
    } finally {
        showLoading(false);
    }
    */
});

// Validar entradas
function validateInputs(peso, altura, edad, genero) {
    if (peso <= 0 || peso > 500) {
        showError('El peso debe estar entre 1 y 500 kg');
        return false;
    }
    
    if (altura <= 0 || altura > 3) {
        showError('La altura debe estar entre 0.5 y 3 metros');
        return false;
    }
    
    if (edad <= 0 || edad > 120) {
        showError('La edad debe estar entre 1 y 120 años');
        return false;
    }
    
    if (!genero) {
        showError('Por favor, selecciona un género');
        return false;
    }
    
    return true;
}

// Obtener categoría según IMC
function getCategoria(imc) {
    if (imc < 18.5) return 'Bajo peso';
    if (imc < 25) return 'Peso normal';
    if (imc < 30) return 'Sobrepeso';
    if (imc < 35) return 'Obesidad';
    return 'Obesidad severa';
}

// Obtener recomendación según categoría
function getRecomendacion(categoria) {
    const recomendaciones = {
        'Bajo peso': 'Se recomienda aumentar la ingesta calórica con una dieta balanceada rica en nutrientes. Consulta con un nutricionista para un plan personalizado.',
        'Peso normal': '¡Excelente! Mantén tu peso actual con una dieta equilibrada y ejercicio regular. Continúa con tus hábitos saludables.',
        'Sobrepeso': 'Se recomienda reducir la ingesta calórica y aumentar la actividad física. Una pérdida gradual de peso (0.5-1 kg por semana) es lo ideal.',
        'Obesidad': 'Es importante iniciar un plan de pérdida de peso bajo supervisión médica. Incluye ejercicio regular y una dieta balanceada baja en calorías.',
        'Obesidad severa': 'Se recomienda consultar con un médico especialista de inmediato. Es necesario un plan integral que puede incluir dieta, ejercicio y posiblemente intervención médica.'
    };
    
    return recomendaciones[categoria] || 'Consulta con un profesional de la salud.';
}

// Obtener clase CSS según categoría
function getCategoryClass(categoria) {
    const classes = {
        'Bajo peso': 'category-bajo-peso',
        'Peso normal': 'category-peso-normal',
        'Sobrepeso': 'category-sobrepeso',
        'Obesidad': 'category-obesidad',
        'Obesidad severa': 'category-obesidad-severa'
    };
    
    return classes[categoria] || '';
}

// Obtener color del badge según categoría
function getBadgeColor(categoria) {
    const colors = {
        'Bajo peso': 'background: linear-gradient(135deg, #56ccf2 0%, #2f80ed 100%);',
        'Peso normal': 'background: linear-gradient(135deg, #56ab2f 0%, #a8e063 100%);',
        'Sobrepeso': 'background: linear-gradient(135deg, #f7971e 0%, #ffd200 100%);',
        'Obesidad': 'background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);',
        'Obesidad severa': 'background: linear-gradient(135deg, #870000 0%, #190a05 100%);'
    };
    
    return colors[categoria] || '';
}

// Mostrar resultados
function displayResults(imc, categoria, recomendacion) {
    imcValue.textContent = imc.toFixed(2);
    imcCategory.textContent = categoria;
    imcCategory.className = 'imc-category ' + getCategoryClass(categoria);
    recommendationText.textContent = recomendacion;
    
    resultSection.classList.add('show');
}

// Guardar en historial
function saveToHistory(data) {
    history.unshift(data);
    
    // Limitar a 10 registros
    if (history.length > 10) {
        history = history.slice(0, 10);
    }
    
    localStorage.setItem('imcHistory', JSON.stringify(history));
    renderHistory();
}

// Renderizar historial
function renderHistory() {
    if (history.length === 0) {
        historyContainer.innerHTML = `
            <div class="history-empty">
                <i class="fas fa-folder-open" style="font-size: 3rem; color: #ddd; margin-bottom: 10px;"></i>
                <p>Aún no hay cálculos registrados. ¡Haz tu primer cálculo!</p>
            </div>
        `;
        return;
    }
    
    historyContainer.innerHTML = history.map((item, index) => `
        <div class="history-item">
            <div>
                <strong>${item.fecha}</strong><br>
                <small>Peso: ${item.peso}kg | Altura: ${item.altura}m | Edad: ${item.edad} años | ${item.genero}</small>
            </div>
            <div class="d-flex align-items-center gap-2">
                <span class="badge-imc" style="${getBadgeColor(item.categoria)} color: white;">
                    IMC: ${item.imc} - ${item.categoria}
                </span>
                <button class="btn btn-sm btn-danger" onclick="deleteHistoryItem(${index})" title="Eliminar">
                    <i class="fas fa-trash"></i>
                </button>
            </div>
        </div>
    `).join('');
}

// Eliminar item del historial
function deleteHistoryItem(index) {
    if (confirm('¿Estás seguro de que deseas eliminar este registro?')) {
        history.splice(index, 1);
        localStorage.setItem('imcHistory', JSON.stringify(history));
        renderHistory();
    }
}

// Mostrar/ocultar loading
function showLoading(show) {
    if (show) {
        loadingSpinner.style.display = 'block';
        form.style.display = 'none';
    } else {
        loadingSpinner.style.display = 'none';
        form.style.display = 'block';
    }
}

// Mostrar error
function showError(message) {
    // Crear alerta
    const alertDiv = document.createElement('div');
    alertDiv.className = 'alert alert-danger alert-custom';
    alertDiv.innerHTML = `
        <i class="fas fa-exclamation-triangle"></i> ${message}
    `;
    
    // Insertar antes del formulario
    form.parentNode.insertBefore(alertDiv, form);
    
    // Eliminar después de 5 segundos
    setTimeout(() => {
        alertDiv.remove();
    }, 5000);
}

// Agregar animación al input cuando se escribe
document.querySelectorAll('.form-control, .form-select').forEach(input => {
    input.addEventListener('focus', function() {
        this.parentElement.style.transform = 'scale(1.02)';
        this.parentElement.style.transition = 'transform 0.2s ease';
    });
    
    input.addEventListener('blur', function() {
        this.parentElement.style.transform = 'scale(1)';
    });
});

// Validación en tiempo real
document.getElementById('peso').addEventListener('input', function() {
    if (this.value < 0) this.value = 0;
    if (this.value > 500) this.value = 500;
});

document.getElementById('altura').addEventListener('input', function() {
    if (this.value < 0) this.value = 0;
    if (this.value > 3) this.value = 3;
});

document.getElementById('edad').addEventListener('input', function() {
    if (this.value < 0) this.value = 0;
    if (this.value > 120) this.value = 120;
});

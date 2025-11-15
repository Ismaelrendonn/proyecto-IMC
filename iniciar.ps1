# Script de inicio rápido para Windows
Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  Calculadora IMC - Inicio Rápido" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Verificar si Docker está instalado
Write-Host "[1/4] Verificando Docker..." -ForegroundColor Yellow
try {
    docker --version | Out-Null
    Write-Host "✓ Docker encontrado" -ForegroundColor Green
} catch {
    Write-Host "✗ Error: Docker no está instalado o no está en el PATH" -ForegroundColor Red
    Write-Host "Por favor, instala Docker Desktop desde: https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "[2/4] Verificando Docker Compose..." -ForegroundColor Yellow
try {
    docker-compose --version | Out-Null
    Write-Host "✓ Docker Compose encontrado" -ForegroundColor Green
} catch {
    Write-Host "✗ Error: Docker Compose no está disponible" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "[3/4] Construyendo contenedores..." -ForegroundColor Yellow
Write-Host "Esto puede tardar unos minutos la primera vez..." -ForegroundColor Cyan
docker-compose up --build -d

Write-Host ""
Write-Host "[4/4] Verificando servicios..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

$services = docker-compose ps --format json | ConvertFrom-Json

Write-Host ""
Write-Host "==================================" -ForegroundColor Green
Write-Host "  ¡Sistema Iniciado Exitosamente!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Green
Write-Host ""
Write-Host "Accede a los servicios en:" -ForegroundColor Cyan
Write-Host "  🎨 Frontend:  http://localhost:3000" -ForegroundColor White
Write-Host "  🔧 API:       http://localhost:5000" -ForegroundColor White
Write-Host "  📚 Swagger:   http://localhost:5000/swagger" -ForegroundColor White
Write-Host ""
Write-Host "Para ver los logs:" -ForegroundColor Yellow
Write-Host "  docker-compose logs -f" -ForegroundColor White
Write-Host ""
Write-Host "Para detener el sistema:" -ForegroundColor Yellow
Write-Host "  docker-compose down" -ForegroundColor White
Write-Host ""
Write-Host "Abriendo el navegador..." -ForegroundColor Cyan
Start-Sleep -Seconds 3
Start-Process "http://localhost:3000"

Write-Host ""
Write-Host "¡Disfruta tu Calculadora IMC! 💪❤️" -ForegroundColor Magenta

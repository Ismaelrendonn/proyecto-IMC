# Script para abrir el frontend directamente (sin Docker)
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Calculadora IMC - Inicio Instantáneo" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "✨ Abriendo frontend directamente..." -ForegroundColor Yellow
Write-Host ""

$htmlPath = "$PSScriptRoot\frontend\index.html"

if (Test-Path $htmlPath) {
    Write-Host "✅ Archivo encontrado: $htmlPath" -ForegroundColor Green
    Write-Host ""
    Write-Host "🌐 Abriendo en tu navegador predeterminado..." -ForegroundColor Cyan
    Write-Host ""
    
    Start-Process $htmlPath
    
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "  ¡Frontend Abierto Exitosamente!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "📝 CARACTERÍSTICAS:" -ForegroundColor Yellow
    Write-Host "  ✅ Diseño hermoso con gradientes" -ForegroundColor White
    Write-Host "  ✅ Cálculos instantáneos" -ForegroundColor White
    Write-Host "  ✅ Animaciones suaves" -ForegroundColor White
    Write-Host "  ✅ Historial local" -ForegroundColor White
    Write-Host "  ✅ Totalmente funcional" -ForegroundColor White
    Write-Host ""
    Write-Host "💡 VENTAJAS de abrir directo:" -ForegroundColor Cyan
    Write-Host "  ⚡ Instantáneo (no espera Docker)" -ForegroundColor White
    Write-Host "  💯 Funciona al 100%" -ForegroundColor White
    Write-Host "  🎨 Todo el diseño visible" -ForegroundColor White
    Write-Host "  📱 Responsive completo" -ForegroundColor White
    Write-Host ""
    Write-Host "🔧 ¿Quieres usar con Docker?" -ForegroundColor Yellow
    Write-Host "  Ejecuta: .\iniciar.ps1" -ForegroundColor White
    Write-Host ""
    Write-Host "¡Disfruta tu Calculadora IMC! 💪❤️" -ForegroundColor Magenta
    
} else {
    Write-Host "❌ Error: No se encontró el archivo index.html" -ForegroundColor Red
    Write-Host "   Ruta buscada: $htmlPath" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Solución:" -ForegroundColor Cyan
    Write-Host "  Verifica que estés en la carpeta correcta del proyecto" -ForegroundColor White
}

# Infraestructura: Terraform + Ansible + GitHub Actions

## Resumen rápido
- **Terraform**: configura S3 (bucket) y EC2 (instancia Ubuntu + security group).
- **Ansible**: playbook que configura la instancia (instala Docker, clona repo y levanta docker-compose).
- **Workflows GitHub**: `terraform.yml` (plan/apply) y `deploy.yml` (obtiene IP y ejecuta Ansible).

## Archivos creados
- `infra/` : `main.tf`, `variables.tf`, `outputs.tf`, `terraform.tfvars.example`
- `ansible/` : `playbook.yml`, `inventory.ini.example`
- `.github/workflows/` : `terraform.yml`, `deploy.yml`

## Secrets GitHub Actions necesarios

Debes añadir estos secrets en **Settings > Secrets and variables > Actions**:

| Secret | Descripción |
|--------|-------------|
| `AWS_ACCESS_KEY_ID` | Credenciales de IAM con permisos para crear EC2 y S3 |
| `AWS_SECRET_ACCESS_KEY` | Secreto correspondiente a AWS_ACCESS_KEY_ID |
| `AWS_REGION` | Región de AWS (ej. `us-east-1`) |
| `SSH_PRIVATE_KEY` | Contenido de la clave privada PEM (la que corresponde a `key_name` en Terraform) |

## Pasos de uso local

### 1. Crear archivo `terraform.tfvars`
Copia `infra/terraform.tfvars.example` a `infra/terraform.tfvars` y edita:
```hcl
aws_region = "us-east-1"
environment = "dev"
instance_type = "t3.micro"
key_name = "mi-clave-aws"  # Debe existir en tu AWS
s3_bucket_name = "project-imc-bucket"
ssh_cidr = "0.0.0.0/0"  # Restricción SSH (en prod: tu IP)
```

### 2. Inicializar y aplicar Terraform
```powershell
cd infra
terraform init
terraform plan
terraform apply
```

### 3. Obtener IP de la instancia
```powershell
terraform output instance_public_ip
```

### 4. Probar conexión SSH
```powershell
ssh -i C:\ruta\a\mi-clave.pem ubuntu@<IP_PUBLICA>
```

## Usando GitHub Actions

1. **Añade los secrets** en el repositorio GitHub (Settings > Secrets).
2. **Push a main** ejecutará automáticamente:
   - `terraform.yml` → plan + apply
   - `deploy.yml` → ejecuta Ansible en la EC2
3. O ejecuta manualmente desde la pestaña **Actions**.

## Variables Terraform

| Variable | Tipo | Por defecto | Descripción |
|----------|------|------------|-------------|
| `aws_region` | string | `us-east-1` | Región AWS |
| `environment` | string | `dev` | Tag de entorno |
| `instance_type` | string | `t3.micro` | Tipo de instancia EC2 |
| `key_name` | string | *requerido* | Nombre de Key Pair en AWS |
| `s3_bucket_name` | string | `project-imc-bucket` | Nombre base del bucket S3 |
| `ssh_cidr` | string | `0.0.0.0/0` | Rango CIDR permitido para SSH |

## Outputs Terraform

Después de `terraform apply`:
```powershell
terraform output bucket_name     # Nombre del S3 bucket
terraform output instance_public_ip  # IP pública de la EC2
```

## Seguridad en producción

- **ssh_cidr**: Cambia de `0.0.0.0/0` a tu IP específica (ej. `1.2.3.4/32`)
- **instance_type**: Usa `t3.small` o superior en producción
- **S3**: Habilita versioning y logging:
  ```bash
  aws s3api put-bucket-versioning --bucket <nombre> --versioning-configuration Status=Enabled
  ```
- **IAM**: Crea un usuario IAM con solo permisos necesarios (EC2, S3, VPC)

## Troubleshooting

**Error: "Key pair not found"**
- Asegúrate de que `key_name` existe en tu AWS account
- Verifica la región correcta

**Error: "Permission denied" en SSH**
- Comprueba permisos del archivo PEM: `chmod 600 mi-clave.pem`
- Verifica que usas el usuario correcto (`ubuntu` para AMI Ubuntu)

**Ansible no conecta**
- Espera unos segundos después de `terraform apply` a que la instancia esté lista
- Verifica el security group permite puerto 22

## Limpiar recursos

```powershell
cd infra
terraform destroy
```

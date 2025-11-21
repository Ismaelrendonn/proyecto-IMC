variable "aws_region" {
  description = "AWS region to deploy into"
  type        = string
  default     = "us-east-1"
}

variable "environment" {
  description = "Environment tag"
  type        = string
  default     = "dev"
}

variable "instance_type" {
  description = "EC2 instance type"
  type        = string
  default     = "t3.micro"
}

variable "s3_bucket_name" {
  description = "Base name for S3 bucket (a random suffix will be appended)"
  type        = string
  default     = "project-imc-bucket"
}

variable "ssh_cidr" {
  description = "CIDR range allowed to SSH into the instance"
  type        = string
  default     = "0.0.0.0/0"
}
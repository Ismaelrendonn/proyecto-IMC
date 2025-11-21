# S3 Bucket
resource "random_id" "bucket_suffix" {
  byte_length = 4
}

resource "aws_s3_bucket" "project_imc_bucket" {
  bucket = "${var.s3_bucket_name}-${random_id.bucket_suffix.hex}"

  tags = {
    Name = "project-imc-bucket"
    Env  = var.environment
  }
}

resource "aws_s3_bucket_acl" "project_imc_bucket_acl" {
  bucket = aws_s3_bucket.project_imc_bucket.id
  acl    = "private"
}

# Security Group
resource "aws_security_group" "imc_sg" {
  name        = "imc-sg"
  description = "Allow SSH and HTTP"

  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = [var.ssh_cidr]
  }

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "imc-sg"
  }
}

# EC2 Instance
data "aws_ami" "ubuntu" {
  most_recent = true
  owners      = ["099720109477"] # Canonical

  filter {
    name   = "name"
    values = ["ubuntu/images/hvm-ssd/ubuntu-focal-20.04-amd64-server-*"]
  }
}

resource "aws_instance" "imc_ec2" {
  ami                    = data.aws_ami.ubuntu.id
  instance_type          = var.instance_type
  vpc_security_group_ids = [aws_security_group.imc_sg.id]

  tags = {
    Name = "imc-ec2"
    Env  = var.environment
  }
}
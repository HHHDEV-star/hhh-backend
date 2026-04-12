#!/bin/bash
set -eux

IMAGE_NAME="hhh-admin-api"
CONTAINER_NAME="hhh_admin_api"
PORT=6000
EC2_HOST="18.177.57.10"
SSH_KEY="/var/jenkins_home/.ssh/ec2-key.pem"
SSH_USER="ubuntu"

echo "🔨 Building Docker image..."
docker build -t ${IMAGE_NAME}:latest .

echo "📦 Saving image..."
docker save ${IMAGE_NAME}:latest | gzip > /tmp/${IMAGE_NAME}.tar.gz

echo "🚚 Transferring image to EC2..."
scp -i "$SSH_KEY" -o StrictHostKeyChecking=no \
  /tmp/${IMAGE_NAME}.tar.gz \
  "${SSH_USER}@${EC2_HOST}:/tmp/${IMAGE_NAME}.tar.gz"

echo "🚀 Deploying on EC2..."
ssh -i "$SSH_KEY" -o StrictHostKeyChecking=no "${SSH_USER}@${EC2_HOST}" bash << 'EOF'
  set -eux
  IMAGE_NAME="hhh-admin-api"
  CONTAINER_NAME="hhh_admin_api"
  PORT=6000

  echo "📥 Loading image..."
  docker load < /tmp/${IMAGE_NAME}.tar.gz
  rm -f /tmp/${IMAGE_NAME}.tar.gz

  echo "🔄 Restarting container..."
  docker stop ${CONTAINER_NAME} 2>/dev/null || true
  docker rm   ${CONTAINER_NAME} 2>/dev/null || true

  docker run -d \
    --name ${CONTAINER_NAME} \
    --restart unless-stopped \
    -p ${PORT}:${PORT} \
    ${IMAGE_NAME}:latest

  echo "✅ Container status:"
  docker ps | grep ${CONTAINER_NAME}
EOF

rm -f /tmp/${IMAGE_NAME}.tar.gz
echo "🎉 Deploy complete!"
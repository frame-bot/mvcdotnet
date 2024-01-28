# Build the Image and Run the Container
## Build your image use
### docker build -t aspnetapp:latest .

## Run a container with previous image. 
### docker run --name dotnetappcontainer -p 8081:80 -d aspnetapp:latest

## Check your container. docker ps. You would see like this.

## set tag image
### docker tag aspnetapp:latest framebot/dotnet-mvc-web:v1

## push to docker repository (public)
### docker push framebot/dotnet-mvc-web:v1

# k8s
## url
### https://prosbeginner.medium.com/%E0%B8%A8%E0%B8%B6%E0%B8%81%E0%B8%A9%E0%B8%B2-kubernetes-%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%95%E0%B8%99%E0%B9%80%E0%B8%AD%E0%B8%87-part-2-overview-and-practice-b7da5d7de0c1
### https://phayao.medium.com/%E0%B8%A1%E0%B8%B2%E0%B8%A5%E0%B8%AD%E0%B8%87-kubernetes-%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2-minikube-%E0%B8%81%E0%B8%B1%E0%B8%99%E0%B9%80%E0%B8%96%E0%B8%AD%E0%B8%B0-b8a7bd1a6b59
### https://blog.openlandscape.cloud/what-is-kubernetes
### https://minikube.sigs.k8s.io/docs/handbook/accessing/#using-minikube-tunnel

## command
### minikube --help
### minikube status
### minikube start
### kubectl cluster-info
### kubectl version
### kubectl cluster-info
### kubectl get nodes
### minikube dashboard
### kubectl get deployments
### kubectl create deployment k8s-bootcamp --image=gcr.io/google-samples/kubernetes-bootcamp:v1
### kubectl get pods
### kubectl proxy
### -- kubectl apply -f  my-dotnet-web.yaml
### -- kubectl delete -f  my-dotnet-web.yaml
### kubectl describe pod $pod
### kubectl expose deployment/my-dotnet-web --type="NodePort" --port 8082
### kubectl expose deployment/my-dotnet-web --type=LoadBalancer --port 8082
### kubectl scale deployments/my-dotnet-web --replicas=4 deployment.extensions/my-dotnet-web scaled
### minikube tunnel
### -- kubectl delete pod $pod
### kubectl get services
### minikube service $service
### kubectl delete service $service
### kubectl describe service $service
### kubectl get node -o wide
### kubectl get node
### minikube addons list
### 
### 
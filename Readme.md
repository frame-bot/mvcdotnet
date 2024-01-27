# Build the Image and Run the Container
## Build your image use
### docker build -t aspnetapp:latest .

## Run a container with previous image. 
### docker run --name dotnetappcontainer -p 8081:80 -d aspnetapp:latest

## Check your container. docker ps. You would see like this.
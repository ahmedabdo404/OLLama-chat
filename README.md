# OLLama chat
##  to run the ollama_chat locally you need to run these docker commands
1: docker run --gpus all -d -v ollama_data:/root/.ollama -p 11434:11434 --name ollama ollama/ollama:latest


2: docker exec -it ollama ollama pull llama3
or
docker exec -it ollama ollama pull phi3:mini
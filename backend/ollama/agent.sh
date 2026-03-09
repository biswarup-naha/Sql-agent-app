#!/bin/sh

echo "Starting Ollama server..."
ollama serve &

echo "Waiting for Ollama to become ready..."

until curl -s http://localhost:11434 > /dev/null; 
do sleep 1
done

echo "Ollama started"

echo "Pulling LLaVA model..."
ollama pull llava:7b

echo "Model ready"

wait
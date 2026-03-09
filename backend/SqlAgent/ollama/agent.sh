#!/bin/sh

echo "Starting Ollama..."
ollama serve &

echo "Waiting for Ollama..."
sleep 1

echo "Pulling model..."
ollama pull llama3

wait
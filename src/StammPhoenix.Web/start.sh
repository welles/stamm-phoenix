#!/bin/bash

npm install -g bun

bun --version

bun install

bun run build

bun run dev --host 0.0.0.0 --port 80

#!/usr/bin/env bash
set -e
DOCKER_BUILDKIT=0 docker build -t aoc .
docker run aoc
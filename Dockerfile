FROM mcr.microsoft.com/dotnet/sdk

WORKDIR /app

RUN pwsh --version

COPY . .

RUN pwd && \
    ls -la

ENTRYPOINT ["/app/runall.ps1"]

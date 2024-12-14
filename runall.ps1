#!/usr/bin/env pwsh

$ErrorActionPreference = "Stop"

$problems = dir -dir ??? | Sort-Object Name

foreach ($problem in $problems) {
  [string] $name = $problem.Name
  pushd $name

  Write-Host "*** $name ***" -f Green

  if (!(Test-Path "example.txt")) {
    Write-Host "No example.txt file." -f Yellow
    continue
  }

  sed -i 's/input.txt/example.txt/g' Program.cs
  if ($name -eq '141') {
    sed -i 's/101/11/g' Program.cs
    sed -i 's/103/7/g' Program.cs
  }
  dotnet run

  popd
}

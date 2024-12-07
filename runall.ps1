#!/usr/bin/env pwsh

$ErrorActionPreference = "Stop"

$problems = dir -dir ??? | Sort-Object Name

foreach ($problem in $problems) {
  [string] $name = $problem.Name
  pushd $name

  Write-Host "*** $name ***" -f Green
  sed -i 's/input.txt/example.txt/g' Program.cs
  dotnet run

  popd
}

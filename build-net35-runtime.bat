@echo off
PATH=%PATH%;%SystemRoot%\Microsoft.NET\Framework\v3.5
msbuild SportsTacticsBoard\SportsTacticsBoard.csproj /p:Configuration=Release %1 %2 %3

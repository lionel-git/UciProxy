@echo off
echo Start network1
start "NETWORK1" cmd /c "UciProxy.exe --ConfigFile=Configs\Network1.json"
echo Start network2
start "NETWORK2" cmd /c "UciProxy.exe --ConfigFile=Configs\Network2.json"
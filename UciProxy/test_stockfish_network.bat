@echo off
echo Start stockfish network
start "STOCKFISH_NETWORK" cmd /c "UciProxy.exe --ConfigFile=Configs\StockfishNetwork.json"
echo Start network2
start "CONSOLE_NETWORK" cmd /c "UciProxy.exe --ConfigFile=Configs\ConsoleNetwork.json"
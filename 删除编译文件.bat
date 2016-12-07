@echo off  
setlocal enabledelayedexpansion    

for /r . %%i in (obj,bin) do (
  IF EXIST %%i RD /s /q %%i
  )
  
pause  
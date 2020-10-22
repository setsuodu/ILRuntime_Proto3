@echo off
cd src
for %%i in (*.proto) do (
    protoc --csharp_out=../../HotFix/Message/ %%i
    rem 从这里往下都是注释，可忽略
    echo From %%i To %%~ni.cs Successfully!  
)
pause
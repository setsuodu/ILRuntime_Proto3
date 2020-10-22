@echo off
rem cd src rem 从Editor中执行，默认指到了Unity根目录
cd ../Protoc/src
rem echo %cd%
rem C:\Users\Junjing-13\Desktop\Github\ILRuntimeQ/convert_proto.bat

for %%i in (*.proto) do (
    protoc --csharp_out=../../HotFix/Message/ %%i
    rem 从这里往下都是注释，可忽略
    echo From %%i To %%~ni.cs Successfully!  
)
pause
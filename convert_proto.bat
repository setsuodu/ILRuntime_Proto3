@echo off
rem cd src rem 从Editor中执行，默认指到了Unity根目录


echo current at: %cd%
rem set output=%cd%/HotFix/Message
set output1=%cd%/HotFix/Message
echo output1: %output1%
set output2=%cd%/NetCoreServer/NetCoreApp/Message
echo output2: %output2%
rem pause


cd Protoc
rem pause

rem *.proto的目录
cd src
echo current at: %cd%

for %%i in (*.proto) do (
	rem 导出到热更工程
    protoc --csharp_out=%output1%/ %%i
	
	rem 导出到服务器工程
    protoc --csharp_out=%output2%/ %%i
	
    echo From %%i To %%~ni.cs Successfully!
)
pause
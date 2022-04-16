@echo off
echo start compile...

rem 打印当前目录
rem C:\Users\Junjing-13\Desktop\Github\ILRuntimeQ\Unity
rem echo %cd% rem /*当前目录*/

rem cd..
set result="%cd%\HotFix\HotFix_Project.csproj"
rem echo %result%

rem TODO:将devenv设置为环境变量
rem cd C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE
cd C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE
devenv %result% /rebuild
rem devenv %result% /build

echo compile ok.
pause
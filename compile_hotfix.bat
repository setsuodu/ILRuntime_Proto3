@echo off
echo start compile...

rem 打印当前目录
rem C:\Users\Junjing-13\Desktop\Github\ILRuntimeQ\Unity
rem echo %cd%

cd..
set result="%cd%\HotFix\HotFix.csproj"
rem echo %result%

rem TODO:将devenv设置为环境变量
cd C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE
devenv %result% /Rebuild

echo compile ok.
pause
@Echo Off

cls

set %ErrorLevel% = 0

echo .
echo ########################################
echo Building Solution
echo ########################################
echo .

MSBuild build.proj /property:Configuration=Release

if %ERRORLEVEL% equ 0 (     
    type win.txt
)

if %ERRORLEVEL% neq 0 (
    type fail.txt
    GOTO EOF
)

:EOF
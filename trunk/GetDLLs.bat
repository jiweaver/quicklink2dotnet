@ECHO OFF

REM File: GetDLLs.bat
REM Author: Justin Weaver (Jul 2010)
REM Description: This batch file will locate the QuickGlance installation
REM directory via the windows registry, then copy (if newer) the DLL files 
REM required by QuickLinkAPI into the QuickLinkAPI4NET source directory.

REM The registry key that holds the QuickGlance installation path
SET regKey=HKEY_LOCAL_MACHINE\SOFTWARE\EyeTech Digital Systems

REM Check that QuickGlance is installed
REG QUERY "%regKey%" > nul
IF ERRORLEVEL 1 GOTO QUICKGLANCE_NOT_FOUND

REM Get the path of the QuickGlance installation
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "%regKey%" /v Path') DO SET QuickGlancePath=%%B
IF ERRORLEVEL 1 GOTO QUICKGLANCE_NOT_FOUND

REM Find the destination directory
SET destdir=%~dp0\QuickLinkAPI4NET\QuickLinkAPI4NET

ECHO Updating DLLs from "%QuickGlancePath%" to "%destdir%"

REM *** Copy the libs we need 
XCOPY /Y /D "%QuickGlancePath%\bin\QuickLinkAPI.dll" "%destdir%"
IF ERRORLEVEL 1 GOTO COPY_FAILED
XCOPY /Y /D "%QuickGlancePath%\bin\PGRFlyCapture.dll" "%destdir%"
IF ERRORLEVEL 1 GOTO COPY_FAILED

ECHO Your 32-bit DLLs are now up to date.
PAUSE
EXIT /B 0

:QUICKGLANCE_NOT_FOUND

ECHO Please install QuickGlance.
PAUSE
EXIT /B 2

:COPY_FAILED

ECHO Failed to copy files from "%QuickGlancePath%\bin" to %dest%
PAUSE
EXIT /B 3

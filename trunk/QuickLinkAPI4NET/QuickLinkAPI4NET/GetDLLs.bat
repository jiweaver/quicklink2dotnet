@ECHO OFF

REM File: GetDLLs.bat
REM Author: Justin Weaver (Jul 2010)
REM Description: This batch file will locate the QuickGlance installation
REM directory via the windows registry, then copy (if newer) the DLL files 
REM required by QuickLinkAPI into the specified destination directory.  This
REM script is used automatically by the QuickLinkAPI4NET project's pre-build 
REM event to copy the DLLs to the build output directory.

REM Read destination directory from the command line.
IF [%1] EQU [] GOTO USAGE
IF [%2] NEQ [] GOTO USAGE
SET dest=%1

REM The registry key that holds the QuickGlance installation path
SET regKey=HKEY_LOCAL_MACHINE\SOFTWARE\EyeTech Digital Systems

REM Check that QuickGlance is installed
REG QUERY "%regKey%" > nul
IF ERRORLEVEL 1 GOTO QUICKGLANCE_NOT_FOUND

REM Get the path of the QuickGlance installation
FOR /F "tokens=2* delims=	 " %%A IN ('REG QUERY "%regKey%" /v Path') DO SET QuickGlancePath=%%B
IF ERRORLEVEL 1 GOTO QUICKGLANCE_NOT_FOUND

REM *** Copy the libs we need into the specified directory
XCOPY /Y /D "%QuickGlancePath%\bin\QuickLinkAPI.dll" %dest%
IF ERRORLEVEL 1 GOTO COPY_FAILED
XCOPY /Y /D "%QuickGlancePath%\bin\PGRFlyCapture.dll" %dest%
IF ERRORLEVEL 1 GOTO COPY_FAILED

EXIT /B 0

:USAGE

echo Usage: %0 ^<DESTINATION^>

EXIT /B 1

:QUICKGLANCE_NOT_FOUND

ECHO Please install QuickGlance.

EXIT /B 2

:COPY_FAILED

ECHO Failed to copy files from "%QuickGlancePath%\bin" to %dest%

EXIT /B 3

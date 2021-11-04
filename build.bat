::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::
:: build.bat
:: ---------
::
:: Builds Sports Tactics Board using NAnt.
::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::
:: Sports Tactics Board
::
:: http://sportstacticsbd.sourceforge.net/
:: http://sourceforge.net/projects/sportstacticsbd/
:: 
:: Sports Tactics Board is a utility that allows coaches, trainers and 
:: officials to describe sports tactics, strategies and positioning using 
:: a magnetic or chalk-board style approach.
:: 
:: Copyright (C) 2007 Robert Turner
:: 
:: This program is free software; you can redistribute it and/or modify
:: it under the terms of the GNU General Public License as published by
:: the Free Software Foundation; either version 2 of the License, or
:: (at your option) any later version.
:: 
:: This program is distributed in the hope that it will be useful,
:: but WITHOUT ANY WARRANTY; without even the implied warranty of
:: MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
:: GNU General Public License for more details.
:: 
:: You should have received a copy of the GNU General Public License along
:: with this program; if not, write to the Free Software Foundation, Inc.,
:: 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::
:: In order to build this program you need the following:
::
::   - NAnt version 0.85 or higher
::   - .NET Framework 2.0 (SDK is optional)
::   - WiX 3.0.
::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

@echo off
setlocal

:: Cache some environment variables.
set ROOT=%~dp0

:: Set the default arguments
set FLAVOR=debug
set ACTION=
set VERBOSE=

:ParseArgs
:: Parse the incoming arguments
if /i "%1"==""        goto Build
if /i "%1"=="-?"      goto Syntax
if /i "%1"=="-h"      goto Syntax
if /i "%1"=="-help"   goto Syntax
if /i "%1"=="-v"      (set VERBOSE=-v)   & shift & goto ParseArgs
if /i "%1"=="debug"   (set FLAVOR=debug) & shift & goto ParseArgs
if /i "%1"=="ship"    (set FLAVOR=ship)  & shift & goto ParseArgs
if /i "%1"=="rebuild" (set ACTION=rebuild) & shift & goto ParseArgs
if /i "%1"=="clean"   (set ACTION=clean) & shift & goto ParseArgs
if /i "%1"=="build"   (set ACTION=)      & shift & goto ParseArgs
goto Error

:Build
pushd %ROOT%

nant.exe -buildfile:"%ROOT%SportsTacticsBoard.build" %ACTION% -D:flavor=%FLAVOR% %VERBOSE%

popd
goto End

:Error
echo Invalid command line parameter: %1
echo.

:Syntax
echo %~nx0 [-?] [-v] [debug or ship] [clean or rebuild or build]
echo.
echo   -?      : this help
echo   -v      : verbose messages
echo   debug   : builds a debug version of Sports Tactics Board (default)
echo   ship    : builds a release (ship) version of Sports Tactics Board
echo   clean   : cleans the build
echo   rebuild : does a full rebuild
echo   build   : does an incremental rebuild (default)
echo.
echo.

:End
endlocal
# Sports Tactics Board
Sports Tactics Board is a utility that allows coaches, trainers and officials to describe sports tactics, strategies and positioning using a magnetic or chalk-board style approach. Supports soccer, hockey, volleyball and floorball.

Features
- Supports more than one sport (currently Soccer/Football, Hockey, Volleyball, Floorball, Futsal and American Football).
- Records sequences of positions for documenting tactical strategies or plays.
- Save and loads tactical sequences - allows building a library of plays or drills.
- Supports pre-defined layouts that position players, ball/puck and officials.
- Supports exporting images and image sequences to files or clipboard.
- English and German translations of the UI. (Not working that greate now)

This project has been [forked](https://sourceforge.net/projects/sportstacticsbd/) from, Some parts taken [from](https://github.com/manio143/SportsTacticsBoard), [old home page](http://sportstacticsbd.sourceforge.net/)

[Screenshots](https://github.com/ModeenF/SportsTacticsBoard/blob/main/Screenshots.md)

## New
- .Net 6.0
- More .net Core changes and updates
- Support for soccer (Swedish rules)
  - 5 vs 5
  - 7 vs 7
  - 9 vs 9

## Table of Contents
1. Copyrights
2. License
3. Supported Platforms
   3.1. Requirements
4. Installation
   4.1. Windows Installer (MSI file)
   4.2. Binary installation
   4.3. Source installation
5. Compiling From Source Code
   5.1. Requirements
   5.2. Procedure
   5.2.1. Visual Studio (any version supporting C#)
   5.2.2. No Development Environment on Windows with .NET 6.0 Runtime Installed   
   5.2.3. .NET Framework SDK or Windows SDK

## 1. Copyrights
Copyright (C) 2006 - 2010 Robert Turner</br>
Copyright (C) 2016 Marian Dziubiak</br>
Copyright (C) 2021 - Fredrik Modéen</br>

## 2. License
This program is free software; you can redistribute it and/or modify</br>
it under the terms of the GNU General Public License as published by</br>
the Free Software Foundation; either version 2 of the License, or</br>
(at your option) any later version.</br>

This program is distributed in the hope that it will be useful,</br>
but WITHOUT ANY WARRANTY; without even the implied warranty of</br>
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the</br>
GNU General Public License for more details.</br>

You should have received a copy of the GNU General Public License along</br>
with this program; if not, write to the Free Software Foundation, Inc.,</br>
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.</br>

## 3. Supported Platforms
- Windows 10
- Windows 11
Can work on older if they can use .net 6.0

### 3.1 Requirements
.net 6.0

## 4. Installation
There are three different methods of installation. The preferred and the easiest is
using the Windows Installer (MSI). Most uses shoudl use this method.

## 4.1 Windows Installer (MSI file)
1. Ensure that the Microsoft .net 6.0 runtime is installed on your computer.
2. Download the MSI file from the downloads area.
   [old Link](http://sportstacticsbd.sourceforge.net/downloads.php), new link will follow
3. Save the download to your computer.
4. Open the MSI file (e.g. by double-clicking on it in Windows Explorer) to install
5. Follow the instructions in the installer.

## 4.2 Binary installation
1. Ensure that the Microsoft .net 6.0 runtime is installed on your computer.
2. Download the ZIP file containing the binaries from the downloads area.
   [old Link](http://sportstacticsbd.sourceforge.net/downloads.php), new link will follow
3. Extract the files into a folder, e.g.: C:\Program Files\Sports Tactics Board
4. Run the program by double-clicking on SportsTacticsBoard.exe

## 4.3 Source installation
1. Ensure that the Microsoft .net 6.0 Framework runtime is installed on your computer.
2. Download the ZIP file containing the source code from the downloads area.
   [old Link](http://sportstacticsbd.sourceforge.net/downloads.php), new link will follow
3. Extract the source code to a folder, e.g.: C:\Source\SportsTacticsBoard
4. Follow the instructions in section 5. Compiling From Source Code

## 5. Compiling From Source Code
Source code can be obtained from SourceForge.net. See the following URLs and
follow standard SourceForge process for obtaining source code:
[https://github.com/ModeenF/SportsTacticsBoard](https://github.com/ModeenF/SportsTacticsBoard)

### 5.1 Requirements
There is no need for any special development environment to compile or
build this program from source code. However you will need other tools
to build the installers or other outputs.

If you have any supported Windows platform with the .net 6.0 runtime
installed, you can compile this program from source code.

The following development tools can also be used to modify and compile
the program:
- Visual Studio 2022
  Commerical product available from Microsoft and resellers.
  For more information see:
  http://msdn.microsoft.com/vstudio/default.aspx

- Microsoft .net 6.0 SDK
  No-cost SDK for Windows and .NET development
  Downloadable from:
  https://dotnet.microsoft.com/download/dotnet/6.0

In order to build the installer and other targets, you will need the following:

(will change, for now this don't work)
- Windows Installer XML Toolset (WiX) v3.0.5419.0
  The Windows Installer XML (WiX) is a toolset that builds Windows installation
  packages from XML source code.
  Downloadable from:
  http://wix.sourceforge.net/
  Installation Notes:
  - Default installation path of C:\Program Files\Windows Installer XML v3
    is the easiest, otherwise you may have to modify files.
  - Builds automatically from NAnt build script.

## 5.2 Procedure
See the appropriate section below that corresponds to the environment you have.

non of these are working for now..
## 5.2.1 Visual Studio 2022 (any version supporting C#)
1. Open the solution file (.SLN file) from the root folder of the
   repository.
2. Build the project using normal build procedures (either Build
   Solution or Batch Build from the Build menu).

## 5.2.2 No Development Environment on Windows with .NET 6.0 Runtime Installed
1. Open a command shell:
   Start | Run...
   Type: "cmd", hit OK
2. Change working directories to the folder with the complete
   source tree extracted to.
   e.g.: CD /D C:\SportsTacticsBoard-src
3. Run build-net35-runtime.bat by typing:
   build-net35-runtime
   in the command shell and hitting enter.

## 5.2.3 .NET Framework SDK or Windows SDK
1. Open an "SDK Command Prompt" or "CMD Shell" window from
   Start | Programs | Microsoft .NET 6.0 SDK
   OR
   Start | Programs | Microsoft Windows SDK
2. Change working directories to the folder with the complete
   source tree extracted to.
   e.g.: CD /D C:\SportsTacticsBoard-src
3. Run MSBuild for the targets you wish to compile.
   e.g.: msbuild /p:Configuration=Release

Sports Tactics Board
--------------------

http://sportstacticsbd.sourceforge.net/
http://sourceforge.net/projects/sportstacticsbd/

Sports Tactics Board is a utility that allows coaches, trainers and 
officials to describe sports tactics, strategies and positioning using 
a magnetic or chalk-board style approach.


Table of Contents
-----------------
1. Copyrights
2. License
3. Supported Platforms
 3.2 Requirements
4. Installation
 4.1 Windows Installer (MSI file)
 4.2 Binary installation
 4.3 Source installation
5. Compiling From Source Code
 5.1 Requirements
 5.2 Procedure
  5.2.1 No Development Environment on Windows with .NET 2.0 Runtime Installed
  5.2.2 Visual Studio (any version supporting C#)
  5.2.3 .NET Framework SDK or Windows SDK
  5.2.4 NAnt


1. Copyrights
-------------

Copyright (C) 2006-2010 Robert Turner


2. License
----------

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.


3. Supported Platforms
----------------------

- Windows XP SP2, SP3
- Windows Server 2003 SP1
- Windows Vista
- Windows Server 2008
- Windows 7
- Windows Server 2008 R2


3.2 Requirements
----------------

Microsoft .NET Framework 3.5 SP1 must be installed. It can be downloaded using
Windows Update or Microsoft Update, or directly from Microsoft from:

http://www.microsoft.com/downloads/details.aspx?FamilyID=AB99342F-5D1A-413D-8319-81DA479AB0D7&displaylang=en


4. Installation
---------------

There are three different methods of installation. The preferred and the easiest is
using the Windows Installer (MSI). Most uses shoudl use this method.


4.1 Windows Installer (MSI file)
--------------------------------

1. Ensure that the Microsoft .NET 3.5 SP1 Framework runtime is installed on your computer.
2. Download the MSI file from the downloads area.
     http://sportstacticsbd.sourceforge.net/downloads.php
3. Save the download to your computer.
4. Open the MSI file (e.g. by double-clicking on it in Windows Explorer) to install
5. Follow the instructions in the installer.


4.2 Binary installation
-----------------------

1. Ensure that the Microsoft .NET 3.5 SP1 Framework runtime is installed on your computer.
2. Download the ZIP file containing the binaries from the downloads area.
     http://sportstacticsbd.sourceforge.net/downloads.php
3. Extract the files into a folder, e.g.: C:\Program Files\Sports Tactics Board 
4. Run the program by double-clicking on SportsTacticsBoard.exe


4.3 Source installation
-----------------------

1. Ensure that the Microsoft .NET 3.5 SP1 Framework runtime is installed on your computer.
2. Download the ZIP file containing the source code from the downloads area.
     http://sportstacticsbd.sourceforge.net/downloads.php
3. Extract the source code to a folder, e.g.: C:\Source\SportsTacticsBoard
4. Follow the instructions in section 5. Compiling From Source Code


5. Compiling From Source Code
-----------------------------

Source code can be obtained from SourceForge.net. See the following URLs and 
follow standard SourceForge process for obtaining source code:

  http://sportstacticsbd.sourceforge.net/
  http://sourceforge.net/projects/sportstacticsbd/


5.1 Requirements
----------------

There is no need for any special development environment to compile or
build this program from source code. However you will need other tools
to build the installers or other outputs.

If you have any supported Windows platform with the .NET 3.5 SP1 runtime 
installed, you can compile this program from source code.

NAnt build script is the most complete, and supports building
installers and release files for web site upload. The NAnt build script
is considered the primary technique for building this tool.

The following development tools can also be used to modify and compile
the program:

  - Visual Studio 2008 Professional 
      Commerical product available from Microsoft and resellers.
      For more information see: 
         http://msdn.microsoft.com/vstudio/default.aspx

  - Visual C# 2008 Express Edition
      No-cost product available from Microsoft.
      For more information see:
         http://msdn.microsoft.com/vstudio/express/visualcsharp/

  - Microsoft Windows SDK 6.1
      No-cost SDK for Windows and .NET development
      Downloadable from:
         http://www.microsoft.com/downloads/details.aspx?FamilyId=F26B1AA4-741A-433A-9BE5-FA919850BDBF&displaylang=en

In order to build the installer and other targets, you will need the following:

  - NAnt 0.86-beta1
      NAnt is a free .NET build tool. In theory it is kind of like make without 
      make's wrinkles.
      Downloadable from:
         http://nant.sourceforge.net/
      Installation Notes:
       - Must be in your executable path (modify PATH environment variable)

  - NAnt Contrib 0.86 -- Not yet released - Use a recent nightly build
      NAnt Contrib is a free add-on for NAnt.
      Downloadable from:
         http://nantcontrib.sourceforge.net/
      Installation Notes:
       - Must be installed into NAnt install folder

  - Windows Installer XML Toolset (WiX) v3.0.5419.0
      The Windows Installer XML (WiX) is a toolset that builds Windows installation 
      packages from XML source code.
      Downloadable from:
         http://wix.sourceforge.net/
      Installation Notes:
       - Default installation path of C:\Program Files\Windows Installer XML v3
         is the easiest, otherwise you may have to modify files.
       - Builds automatically from NAnt build script.

Optionally, you can install the following:

  - FxCop 1.36
      A code analysis tool provided by Microsoft for .NET assembly code analysis.
      Downloadable from:
         http://www.microsoft.com/downloads/details.aspx?FamilyID=9aeaa970-f281-4fb0-aba1-d59d7ed09772&DisplayLang=en
      Installation Notes:
       - Default installation path of C:\Program Files\Microsoft FxCop 1.36
         is the easiest, otherwise you may have to modify files.
       - Runs automatically from NAnt build script.
         


5.2 Procedure
-------------

See the appropriate section below that corresponds to the environment you have.

5.2.1 No Development Environment on Windows with .NET 3.5 SP1 Runtime Installed
-------------------------------------------------------------------------------

1. Open a command shell:
     Start | Run...
   Type: "cmd", hit OK
2. Change working directories to the folder with the complete 
   source tree extracted to.
    e.g.:   CD /D C:\SportsTacticsBoard-src
3. Run build-net35-runtime.bat by typing:
     build-net35-runtime
   in the command shell and hitting enter.

5.2.2 Visual Studio 2008 (any version supporting C#)
----------------------------------------------------

1. Open the solution file (.SLN file) from the root folder of the 
   repository.
2. Build the project using normal build procedures (either Build 
   Solution or Batch Build from the Build menu).


5.2.3 .NET Framework SDK or Windows SDK
---------------------------------------

1. Open an "SDK Command Prompt" or "CMD Shell" window from 
     Start | Programs | Microsoft .NET Framework SDK v3.5
   OR
     Start | Programs | Microsoft Windows SDK
2. Change working directories to the folder with the complete 
   source tree extracted to.
    e.g.:   CD /D C:\SportsTacticsBoard-src
3. Run MSBuild for the targets you wish to compile.
    e.g.:   msbuild /p:Configuration=Release


5.2.4 NAnt
----------

Note: Ensure NAnt is in your PATH before you start, otherwise you will need to 
      specify the full path to the program to run it.
  e.g.:  PATH %PATH%;C:\NAnt-0.86-beta1\bin

1. Open a "Visual Studio 2008 Command Prompt"
     Start | Programs | Microsoft Visual Studio 2008 | Visual Studio Tools | Visual Studio 2008 Command Prompt
2. Change working directories to the folder with the complete 
   source tree extracted to.
    e.g.:   CD /D C:\SportsTacticsBoard-src
3. You have two choices:
   a) Run build.bat, optionally specifying "build" or "ship", 
      optionally specifying a build target:
    e.g.:   build
    e.g.:   build debug build
    e.g.:   build ship rebuild
   b) Run NAnt, specifying the targets and the "flavor":
    e.g.:   nant 
    e.g.:   nant -D:flavor=debug build
    e.g.:   nant -D:flavor=ship rebuild

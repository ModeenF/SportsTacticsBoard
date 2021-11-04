@echo off
setlocal
set WINSCP="%ProgramFiles%\WinSCP\WinSCP.com"

xcopy /D /Y /R changelog.txt WebSiteFiles
xcopy /D /Y /R readme.txt WebSiteFiles

%WINSCP% /script=UploadWebFiles.txt

:: ---------------- 获取换行符 0A ---------------------
@echo off
set "n=&echo;"
echo 欢迎使用FilesCleanJob服务安装程序%n%%n%安装程序分三个步骤：%n%步骤1.切换到.net目录下，打开windows安装工具%n%步骤2.卸载服务%n%步骤3.安装服务%n%按下回车键进行第一步%n%
pause


cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
C: 
echo 已打开windows服务安装工具，点击回车进行FilesCleanJob服务卸载和安装
pause


installUtil -u %~dp0\..\CleanJobService.exe
echo 卸载FilesCleanJob服务完成，按下回车键进行安装
pause 


installUtil %~dp0\..\CleanJobService.exe
echo 服务安装完成，点击回车退出安装程序
pause 
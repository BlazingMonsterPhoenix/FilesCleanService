:: ---------------- ��ȡ���з� 0A ---------------------
@echo off
set "n=&echo;"
echo ��ӭʹ��FilesCleanJob����װ����%n%%n%��װ������������裺%n%����1.�л���.netĿ¼�£���windows��װ����%n%����2.ж�ط���%n%����3.��װ����%n%���»س������е�һ��%n%
pause


cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
C: 
echo �Ѵ�windows����װ���ߣ�����س�����FilesCleanJob����ж�غͰ�װ
pause


installUtil -u %~dp0\..\CleanJobService.exe
echo ж��FilesCleanJob������ɣ����»س������а�װ
pause 


installUtil %~dp0\..\CleanJobService.exe
echo ����װ��ɣ�����س��˳���װ����
pause 
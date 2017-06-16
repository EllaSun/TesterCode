cd C:\Ella\mycode\performancereport
echo %cd%



:: yesterdays date

set day=-1
echo >"%temp%\%~n0.vbs" s=DateAdd("d",%day%,now) : d=weekday(s)
echo>>"%temp%\%~n0.vbs" WScript.Echo year(s)^& right(100+month(s),2)^& right(100+day(s),2)
for /f %%a in ('cscript /nologo "%temp%\%~n0.vbs"') do set "result=%%a"
del "%temp%\%~n0.vbs"
set "YYYY=%result:~0,4%"
set "MM=%result:~4,2%"
set "DD=%result:~6,2%"
set "data="%dd%/%mm%/%yyyy%"
::set data="28/04/2017"


del /f  input\* 
C:\Python27\python.exe 1dowload_time_sheet.py ella.sun@plexure.com "pa10086&11"  %data%
::set result=%ERRORLEVEL%
::if %result% equ 0 ( echo ‘’step 1  download success“”) 
::else (
::echo "step1" %result
::pause
::)
dir input
C:\Python27\python.exe 2merge_time_sheet.py ella.sun@plexure.com "password" " jasmine.zhang@plexure.com, sowmya.velicherla@plexure.com, leon.liu@plexure.com, tingting.du@plexure.com, tracis.lum@plexure.com, deepthi.gaddam@plexure.com, ella.sun@plexure.com,wilson.joe@plexure.com" %data%
set result=%ERRORLEVEL%
::if %result% equ 0 ( echo ‘’step 2 merge success“”) else ( exit)


dir img
del  /f img\*
dir img
C:\Python27\python.exe 3pycha_test.py
set result=%ERRORLEVEL%
::if %result% equ 0 ( echo ‘’step 3 draw pic success“”) else ( exit)
dir img
C:\Python27\python.exe 4email_pic.py ella.sun@plexure.com "password" "ella.sun@plexure.com, jasmine.zhang@plexure.com" %data%
::set result=%ERRORLEVEL%
::if %result% equ 0 ( echo ‘’step 4 email report success“”) else ( exit)

::C:\Python27\python.exe 5.report_data.py ella.sun@plexure.com "password" " jasmine.zhang@plexure.com, sowmya.velicherla@plexure.com, leon.liu@plexure.com, tingting.du@plexure.com, tracis.lum@plexure.com, deepthi.gaddam@plexure.com, ella.sun@plexure.com,wilson.joe@plexure.com" %data%






pause
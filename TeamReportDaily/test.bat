:: yesterdays date

set day=-1
echo >"%temp%\%~n0.vbs" s=DateAdd("d",%day%,now) : d=weekday(s)
echo>>"%temp%\%~n0.vbs" WScript.Echo year(s)^& right(100+month(s),2)^& right(100+day(s),2)
for /f %%a in ('cscript /nologo "%temp%\%~n0.vbs"') do set "result=%%a"
del "%temp%\%~n0.vbs"
set "YYYY=%result:~0,4%"
set "MM=%result:~4,2%"
set "DD=%result:~6,2%"
set data="%dd%/%mm%/%yyyy%"
set data_1="%dd%-%mm%-%yyyy%"

set data="28/04/2017"
::set location="C:\Ella\mycode\performancereport"
::cd %location%
::echo %location%


::copy  %location%"\TeamTimeSheet.xlsx" TeamTimeSheet_%data_1%.xlsx
::move %location"\"%TeamTimeSheet_%data_1%.xlsx  history
::C:\Python27\python.exe 2merge_time_sheet.py "ella.sun@plexure.com" "pa10086&11" "ella.sun@plexure.com" %data%


C:\Python27\python.exe 1dowload_time_sheet.py ella.sun@plexure.com "pa10086&11"  %data%

pause
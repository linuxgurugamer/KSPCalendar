

set H=R:\KSP_1.2.2_dev
echo %H%
cd


copy bin\Debug\KSPCalendar.dll ..\GameData\KSPCalendar\Plugins
xcopy /E /Y ..\GameData\KSPCalendar %H%\GameData\KSPCalendar

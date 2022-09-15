 net stop PowerBIReportServer
 Write-Host "Copying Logon.aspx page `n" -ForegroundColor Green
Copy-Item -Path C:\PBISSO\pbiserver\Logon.aspx -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\"

Write-Host "Copying PowerBI.SSO.dll `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"

Write-Host "Copying PowerBI.SSO.dll.config `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"

Write-Host "Copying PowerBI.SSO.pdb `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\pbiserver\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"


Write-Host "Newtonsoft.Json.dll `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"
#
Write-Host "Newtonsoft.Json.xml `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\pbiserver\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"


net start PowerBIReportServer
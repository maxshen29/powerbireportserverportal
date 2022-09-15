#Write-Host "Creating the User Store Database"
#& sqlcmd -S "." -i "Setup\CreateUserStore.Sql"
 net stop PowerBIReportServer

Write-Host "Copying Logon.aspx page `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\Logon.aspx -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\"
#Copy-Item -Path myreports.aspx -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\"

Write-Host "Newtonsoft.Json.dll `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"
#
Write-Host "Newtonsoft.Json.xml `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\PBISERVER\Newtonsoft.Json.xml -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"
 
 
 

Write-Host "Copying PowerBI.SSO.dll `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"

Write-Host "Copying PowerBI.SSO.dll.config `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.dll.config -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"

Write-Host "Copying PowerBI.SSO.pdb `n" -ForegroundColor Green
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\Bin\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\Portal\"
Copy-Item -Path  C:\PBISSO\PBISERVER\PowerBI.SSO.pdb -Destination "C:\Program Files\Microsoft Power BI Report Server\PBIRS\PowerBi"

Write-Host "Updating rsreportserver.config `n" -ForegroundColor Green
$rsConfigFilePath = "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\rsreportserver.config"
[xml]$rsConfigFile = (Get-Content $rsConfigFilePath)
Write-Host "Copy of the original config file in $rsConfigFilePath.backup"
$rsConfigFile.Save("$rsConfigFilePath.backup")
$rsConfigFile.Configuration.Authentication.AuthenticationTypes.InnerXml = "<Custom />"

$extension = $rsConfigFile.CreateElement("Extension")
$extension.SetAttribute("Name","Forms")
$extension.SetAttribute("Type","PowerBI.SSO.Authorization, PowerBI.SSO")
$configuration =$rsConfigFile.CreateElement("Configuration")
$configuration.InnerXml="<AdminConfiguration>`n<UserName>pbiadmin</UserName>`n</AdminConfiguration>"
$extension.AppendChild($configuration)
$rsConfigFile.Configuration.Extensions.Security.AppendChild($extension)
$rsConfigFile.Configuration.Extensions.Authentication.Extension.Name ="Forms"
$rsConfigFile.Configuration.Extensions.Authentication.Extension.Type ="PowerBI.SSO.AuthenticationExtension,PowerBI.SSO"

$rsConfigFile.Save($rsConfigFilePath)

Write-Host "Updating RSSrvPolicy.config `n" -ForegroundColor Green
$rsPolicyFilePath = "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\rssrvpolicy.config"
[xml]$rsPolicy = (Get-Content $rsPolicyFilePath)
Write-Host "Copy of the original config file in $rsPolicyFilePath.backup"
$rsPolicy.Save("$rsPolicyFilePath.backup")

$codeGroup = $rsPolicy.CreateElement("CodeGroup")
$codeGroup.SetAttribute("class","UnionCodeGroup")
$codeGroup.SetAttribute("version","1")
$codeGroup.SetAttribute("Name","SecurityExtensionCodeGroup")
$codeGroup.SetAttribute("Description","Code group for the sample security extension")
$codeGroup.SetAttribute("PermissionSetName","FullTrust")
$codeGroup.InnerXml ="<IMembershipCondition class=""UrlMembershipCondition"" version=""1"" Url=""C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\bin\PowerBI.SSO.dll""/>"
$rsPolicy.Configuration.mscorlib.security.policy.policylevel.CodeGroup.CodeGroup.AppendChild($codeGroup)
$rsPolicy.Save($rsPolicyFilePath)


Write-Host "Updating web.config `n" -ForegroundColor Green
$webConfigFilePath = "C:\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\web.config"
[xml]$webConfig = (Get-Content $webConfigFilePath)
Write-Host "Copy of the original config file in $webConfigFilePath.backup"
$webConfig.Save("$webConfigFilePath.backup")
$webConfig.configuration.'system.web'.identity.impersonate="false"
$webConfig.configuration.'system.web'.authentication.mode="Forms"
$webConfig.configuration.'system.web'.authentication.InnerXml="<forms loginUrl=""logon.aspx"" name=""sqlAuthCookie"" timeout=""60"" path=""/""></forms>"
$authorization = $webConfig.CreateElement("authorization")
$authorization.InnerXml="<deny users=""?"" />"
$webConfig.configuration.'system.web'.AppendChild($authorization)
$webConfig.Save($webConfigFilePath)


Write-Host "Adding Machine Keys to $rsConfigFilePath `n" -ForegroundColor Green
[xml]$rsConfigFile = (Get-Content $rsConfigFilePath)
$machineKey = $rsConfigFile.CreateElement("MachineKey")
$machineKey.SetAttribute("ValidationKey","A3341B44EB01FD160AB24B139462F2B280B9342A901FBAEF64B1A36C6EDE99540D01336CAD70CB19261E0842C5ACEE512225B3CBF6FCE8460BB2E2F497493A91")
$machineKey.SetAttribute("DecryptionKey","8F8CD8E4EBF3D096EC6E71D7F663CA42489D317C0821867C")
$machineKey.SetAttribute("Validation","AES")
$machineKey.SetAttribute("Decryption","AES")
$rsConfigFile.Configuration.AppendChild($machineKey)
$rsConfigFile.Save($rsConfigFilePath)


Write-Host "Configuring Passthrough cookies `n" -ForegroundColor Green
[xml]$rsConfigFile = (Get-Content $rsConfigFilePath)
$customUI = $rsConfigFile.CreateElement("CustomAuthenticationUI")
$customUI.InnerXml ="<PassThroughCookies><PassThroughCookie>sqlAuthCookie</PassThroughCookie></PassThroughCookies>"
$rsConfigFile.Configuration.UI.AppendChild($customUI)
$rsConfigFile.Save($rsConfigFilePath)


net start PowerBIReportServer







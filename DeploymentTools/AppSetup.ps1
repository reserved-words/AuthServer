param($DomainName, $AppName, $AppUserPassword, $CertificateThumbprint)

Import-Module $PSScriptRoot\SetupTools.psm1
Import-Module $PSScriptRoot\AddUserToCertificate.psm1

$AppUserName = 'auth-app'

Run-Setup -DomainName $DomainName
Setup-WebApp -AppName $AppName
New-WebUser -UserName $AppUserName -Password $AppUserPassword -AppFolderName $AppName
Add-UserToCertificate -UserName $AppUserName -Permission read -CertStoreLocation \LocalMachine\My -CertThumbprint $CertificateThumbprint
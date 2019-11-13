param($CertificateThumbprint)

Import-Module $PSScriptRoot\AddUserToCertificate.psm1

$AppUserName = 'IIS APPPOOL\Auth'

Add-UserToCertificate -UserName $AppUserName -Permission read -CertStoreLocation \LocalMachine\My -CertThumbprint $CertificateThumbprint
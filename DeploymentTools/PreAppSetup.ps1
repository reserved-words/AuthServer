param($DomainName, $AppName)

Import-Module $PSScriptRoot\SetupTools.psm1

Run-Setup -DomainName $DomainName
Setup-WebApp -AppName $AppName
# based off of the scrit at http://psget.net/GetPsGet.ps1
# based2 off of the script at https://github.com/ligershark/psbuild/blob/master/src/GetPSBuild.ps1
function Install-TfsManager {
    $ModulePaths = @($Env:PSModulePath -split ';')
    
    $ExpectedUserModulePath = Join-Path -Path ([Environment]::GetFolderPath('MyDocuments')) -ChildPath WindowsPowerShell\Modules
    $Destination = $ModulePaths | Where-Object { $_ -eq $ExpectedUserModulePath}
    if (-not $Destination) {
        $Destination = $ModulePaths | Select-Object -Index 0
    }

    $downloadUrl = 'https://raw.github.com/mitulsuthar/TfsManager/master/lib/TfsManager.Cmdlets.dll'
    New-Item ($Destination + "\TfsManager.Cmdlets\") -ItemType Directory -Force | out-null
    'Downloading PsGet from {0}' -f $downloadUrl | Write-Host
    $client = (New-Object Net.WebClient)
    $client.Proxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials
    $client.DownloadFile($downloadUrl, $Destination + "\TfsManager.Cmdlets\TfsManager.Cmdlets.dll")

    $executionPolicy  = (Get-ExecutionPolicy)
    $executionRestricted = ($executionPolicy -eq "Restricted")
    if ($executionRestricted){
        Write-Warning @"
Your execution policy is $executionPolicy, this means you will not be able import or use any scripts including modules.
To fix this change your execution policy to something like RemoteSigned.

        PS> Set-ExecutionPolicy RemoteSigned

For more information execute:
        
        PS> Get-Help about_execution_policies

"@
    }

    if (!$executionRestricted){
        # ensure PsGet is imported from the location it was just installed to
        Import-Module -Name $Destination\TfsManager.Cmdlets
    }    
    Write-Host "TfsManager cmdlets are installed and ready to use" -Foreground Green    
}

Install-TfsManager
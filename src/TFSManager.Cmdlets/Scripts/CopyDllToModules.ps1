param(
	[string]$source
)
$modulesPath = ($env:PSModulePath).Split(";")[0]
$dest = $modulesPath + "\TfsManager.Cmdlets"
Copy-Item $source $dest
TfsManager
==========

TfsManager is a set of powershell cmdlets to work with Team Foundation Server.

**Getting Started**

Copy the following command and paste into PowerShell prompt by doing a right click on your mouse. And then press enter.
```PowerShell
(new-object Net.WebClient).DownloadString("https://raw.github.com/mitulsuthar/TfsManager/master/tools/InstallTfsManager.ps1") | iex
```
Please note that if script execution is disabled on your machine then first you will have to change it.

```PowerShell
Set-ExecutionPolicy RemoteSigned
```
or
```PowerShell
Set-ExecutionPolicy Unrestricted
```

Get all the available commands

```PowerShell
Get-Command -Module TfsManager.Cmdlets
```

Get all the available Team Foundation Server instances
```PowerShell
Get-TfsServer
```

Get all the Team Project Collection for a given Tfs Server
If only one Tfs Server is found then you can just pass the server like this and get all the Tfs Project Collections.
```PowerShell
Get-TfsProjectCollection -ServerUri (Get-TfsServer)
```

And important thing to note here is that it returns a .Net object

If you navigate to a mapped folder on your local machine then you can get the workspace associated with it.
```PowerShell
cd C:\sourcecode\project1
PS C:\sourcecode\project1> Get-TfsWorkspace
```

In that same folder if you can also get the Build Defintions for a Tfs Project
```PowerShell
PS C:\sourcecode\project1>Get-TfsBuildDefinition
```

Since it returns .Net object there is too much information associated with it. With PowerShell you can select only the important information by using the Select Command.
```PowerShell
PS C:\sourcecode\project1>Get-TfsBuildDefinition | Select Name
```

Get all the builds for a particular project. 
If you have queued builds in the past then for a particular project you can get all the builds assoicated with all the build definitions.
```PowerShell
PS C:\sourcecode\project1>Get-TfsBuild | Select BuildNumber, Status, BuildQuality, StartTime, FinishTime | FT
```


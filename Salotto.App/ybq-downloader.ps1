#region global variables
$execPath = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$projectWwwRootPath = "$execPath\wwwroot"
#endregion

#region Create directories
try {
    Write-Host 'Creating directories...'
    if (Test-Path -Path "$execPath\ybqdownloader") {
        #nothing
    } else {
        New-Item "$execPath\ybqdownloader" -itemType Directory
    }
    if (Test-Path -Path $projectWwwRootPath) {
        #nothing
    } else {
        New-Item $projectWwwRootPath -itemType Directory
    }
    Write-Host 'Directories has been created'
}
catch {
    Write-Host 'An error has occurred'
    Write-Error $_.Exception.ToString()
    Read-Host -Prompt "The above error occurred. Press Enter to exit."
}
#endregion

#region Downloading repos
try {
    Write-Host 'Downloading from repos...'
    Invoke-WebRequest 'https://github.com/despos/ybq/archive/refs/heads/main.zip' -OutFile "$execPath\ybqdownloader\repos.zip"
    Write-Host 'Files from repos has been downloaded'
}
catch {
    Write-Host 'An error has occurred'
    Write-Error $_.Exception.ToString()
    Read-Host -Prompt "The above error occurred. Press Enter to exit."
}
#endregion

#region Unzipping folder
try {
    Write-Host 'Unzipping folder...'
    Expand-Archive -Path "$execPath\ybqdownloader\repos.zip" -DestinationPath "$execPath\ybqdownloader\repos" -Force
    Write-Host 'Unzip finished'
}
catch {
    Write-Host 'An error has occurred'
    Write-Error $_.Exception.ToString()
    Read-Host -Prompt "The above error occurred. Press Enter to exit."
}
#endregion

#region Copy files in destination folder
try {
    Write-Host 'Copying file in destination folder...'
    if (Test-Path -Path "$projectWwwRootPath\js") {
    } else {
        New-Item "$projectWwwRootPath\js" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\js\lib") {
    } else {
        New-Item "$projectWwwRootPath\js\lib" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\js\lib\Ybq") {
    } else {
        New-Item "$projectWwwRootPath\js\lib\Ybq" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\css") {
    } else {
        New-Item "$projectWwwRootPath\css" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\css\lib") {
    } else {
        New-Item "$projectWwwRootPath\css\lib" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\css\lib\Ybq") {
    } else {
        New-Item "$projectWwwRootPath\css\lib\Ybq" -itemType Directory
    }
    if (Test-Path -Path "$projectWwwRootPath\js\lib\Ybq") {
        Copy-Item -Path "$execPath\ybqdownloader\repos\ybq-main\1.0.0\js\lib\Ybq\*" -Destination "$projectWwwRootPath\js\lib\Ybq" -Recurse -Force
    } else {
        New-Item "$projectWwwRootPath\js\lib\Ybq" -itemType Directory
        Copy-Item -Path "$execPath\ybqdownloader\repos\ybq-main\1.0.0\js\lib\Ybq\*" -Destination "$projectWwwRootPath\js\lib\Ybq" -Recurse -Force
    }
    if (Test-Path -Path "$projectWwwRootPath\css\lib\Ybq") {
        Copy-Item -Path "$execPath\ybqdownloader\repos\ybq-main\1.0.0\css\lib\Ybq\*" -Destination "$projectWwwRootPath\css\lib\Ybq" -Recurse -Force
    } else {
        New-Item "$projectWwwRootPath\css\lib\Ybq" -itemType Directory
        Copy-Item -Path "$execPath\ybqdownloader\repos\ybq-main\1.0.0\css\lib\Ybq\*" -Destination "$projectWwwRootPath\css\lib\Ybq" -Recurse -Force
    }
    Write-Host 'Files has been copyied in destination folder...'
}
catch {
    Write-Host 'An error has occurred'
    Write-Error $_.Exception.ToString()
    Read-Host -Prompt "The above error occurred. Press Enter to exit."
}
#endregion

#region Finally operations
try {
    Write-Host 'Cleaning and deleting temporary folder...'
    Get-ChildItem "$execPath" -Recurse -Filter "ybqdownloader" | Remove-Item -Force -Recurse
    #Get-ChildItem "$execPath\ybqdownloader" -Recurse -Filter "repos" | Remove-Item -Force -Recurse
    #Get-ChildItem "$execPath\ybqdownloader" -Recurse -Filter "repos.zip" | Remove-Item -Force -Recurse
    #Get-ChildItem C -Recurse -Filter "ybqdownloader" | Remove-Item -Force -Recurse
    Write-Host 'Operation has been terminated'
}
catch {
    Write-Host 'An error has occurred'
    Write-Error $_.Exception.ToString()
    Read-Host -Prompt "The above error occurred. Press Enter to exit."
}
#endregion

Read-Host -Prompt "Updating file has been finished, press key to exit"
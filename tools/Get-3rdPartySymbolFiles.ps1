<#
.SYNOPSIS
    Downloads a file from the web if it is not already present.
.PARAMETER Uri
    The URI to download.
.PARAMETER OutFile
    The path to write the downloaded file to.
#>
Function Get-FileFromWeb([Uri]$Uri, $OutFile) {
    $OutDir = Split-Path $OutFile
    if (!(Test-Path $OutFile)) {
        Write-Verbose "Downloading $Uri..."
        if (!(Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir | Out-Null }
        try {
            (New-Object System.Net.WebClient).DownloadFile($Uri, $OutFile)
        }
        catch {
            throw "Failed to download '$Uri' to '$OutFile'. $($_.Exception.Message)"
        }
    }
}

<#
.SYNOPSIS
    Extracts a zip archive into a directory, overwriting existing files.
.PARAMETER Path
    The archive to extract.
.PARAMETER OutDir
    The directory to extract files into.
#>
Function Unzip($Path, $OutDir) {
    $OutDir = (New-Item -ItemType Directory -Path $OutDir -Force).FullName
    Add-Type -AssemblyName System.IO.Compression.FileSystem

    # Start by extracting to a temporary directory so that there are no file conflicts.
    Remove-Item -LiteralPath "$OutDir.out" -Recurse -Force -ErrorAction Ignore
    [System.IO.Compression.ZipFile]::ExtractToDirectory($Path, "$OutDir.out")

    # Now move all files from the temp directory to $OutDir, overwriting any files.
    Get-ChildItem -Path "$OutDir.out" -Recurse -File | ForEach-Object {
        $destinationPath = Join-Path -Path $OutDir -ChildPath $_.FullName.Substring("$OutDir.out".Length).TrimStart([io.path]::DirectorySeparatorChar, [io.path]::AltDirectorySeparatorChar)
        if (!(Test-Path -Path (Split-Path -Path $destinationPath -Parent))) {
            New-Item -ItemType Directory -Path (Split-Path -Path $destinationPath -Parent) | Out-Null
        }
        Move-Item -Path $_.FullName -Destination $destinationPath -Force
    }
    Remove-Item -LiteralPath "$OutDir.out" -Recurse -Force
}

<#
.SYNOPSIS
    Downloads a NuGet package and its symbols package, then emits matching binaries and PDBs.
.PARAMETER id
    The NuGet package ID.
.PARAMETER version
    The NuGet package version.
.OUTPUTS
    System.String. Paths to matching binaries and PDB files.
#>
Function Get-SymbolsFromPackage($id, $version) {
    $symbolPackagesPath = "$PSScriptRoot/../obj/SymbolsPackages"
    New-Item -ItemType Directory -Path $symbolPackagesPath -Force | Out-Null
    $nupkgPath = Join-Path $symbolPackagesPath "$id.$version.nupkg"
    $snupkgPath = Join-Path $symbolPackagesPath "$id.$version.snupkg"
    $unzippedPkgPath = Join-Path $symbolPackagesPath "$id.$version"
    Get-FileFromWeb -Uri "https://www.nuget.org/api/v2/package/$id/$version" -OutFile $nupkgPath
    Get-FileFromWeb -Uri "https://www.nuget.org/api/v2/symbolpackage/$id/$version" -OutFile $snupkgPath

    Unzip -Path $nupkgPath -OutDir $unzippedPkgPath
    Unzip -Path $snupkgPath -OutDir $unzippedPkgPath

    Get-ChildItem -Recurse -LiteralPath $unzippedPkgPath -Filter *.pdb | % {
        # Collect the DLLs/EXEs as well.
        $rootName = Join-Path $_.Directory $_.BaseName
        if ($rootName.EndsWith('.ni')) {
            $rootName = $rootName.Substring(0, $rootName.Length - 3)
        }

        $dllPath = "$rootName.dll"
        $exePath = "$rootName.exe"
        if (Test-Path $dllPath) {
            $BinaryImagePath = $dllPath
        }
        elseif (Test-Path $exePath) {
            $BinaryImagePath = $exePath
        }
        else {
            Write-Warning "`"$_`" found with no matching binary file."
            $BinaryImagePath = $null
        }

        if ($BinaryImagePath) {
            Write-Output $BinaryImagePath
            Write-Output $_.FullName
        }
    }
}

$versionProps = [xml](Get-Content -LiteralPath $PSScriptRoot\..\Directory.Packages.props)

<#
.SYNOPSIS
    Gets the centrally managed version for a package.
.PARAMETER id
    The NuGet package ID.
.OUTPUTS
    System.String. The package version.
#>
Function Get-PackageVersion($id) {
    $version = $versionProps.Project.ItemGroup.PackageVersion | ? { $_.Include -eq $id } | % { $_.Version }
    if (!$version) {
        Write-Error "No package version found in Directory.Packages.props for the package '$id'"
    }

    $version
}

# All 3rd party packages for which symbols packages are expected should be listed here.
# These must all be sourced from nuget.org, as it is the only feed that supports symbol packages.
$3rdPartyPackageIds = @()

$3rdPartyPackageIds | % {
    $version = Get-PackageVersion $_
    if ($version) {
        Get-SymbolsFromPackage -id $_ -version $version
    }
}

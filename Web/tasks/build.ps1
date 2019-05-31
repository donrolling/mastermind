$path = Get-Location
Write-Output $path 
if(!$path.ToString().Contains('Web')){
    Set-Location -Path "$path\Web"
}
$location = Get-Location
Write-Output $location 
tsc
browserify js/app/main.js > js/app/bundle.js

# Note: Required Powershell 6!!!

#$random=$(-join ((48..57) | Get-Random -Count 32 | % {[char]$_}))
$rgName="intpoc"
$location="eastus2"
$swagger=Get-Content "..\api\swagger.json" | ConvertFrom-Json -AsHashTable
$serviceurl="https://dchreverse.azurewebsites.net/api"


$additionalParameters = @{
    Swagger=$swagger;
    ServiceUrl=$serviceurl;
}

.\deploy.ps1 -ResourceGroupLocation $location -ResourceGroupName $rgName `
    -UploadArtifacts `
    -TemplateFile .\connectorStringManip.json `
    -TemplateParametersFile .\connectorStringManip.parameters.json `
    -ArtifactStagingDirectory ./artifacts `
    -AdditionalParameters $additionalParameters


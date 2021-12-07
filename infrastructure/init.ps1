$env:AZURE_STORAGE_ACCOUNT="mtpshrpulstg01"
$env:AZURE_STORAGE_KEY=az storage account keys list -n mtpshrpulstg01 --query "[0].value" -o=tsv
$env:AZURE_KEYVAULT_AUTH_VIA_CLI="true"
pulumi login --cloud-url azblob://mtpshrpulctn01

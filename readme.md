# Init

## Prerequisites
- Have a service principal authorized as the owner of the subscription
- Have a Azure AD Group for administrators with the SPN inside, this group will have access to all keyvaults of the subscriptions

## Create state storage resources for pulumi
- az group create --location westeurope --name mtpshrpulrsg01
- az storage account create --name mtpshrpulstg01 --resource-group mtpshrpulrsg01 --location westeurope --sku Standard_LRS
- az storage container create --name mtpshrpulctn01 --account-name mtpshrpulstg01
- az keyvault create --location westeurope --name mtpshrpulkvt01 --resource-group mtpshrpulrsg01 --enable-rbac-authorization true
- az role assignment create --role 00482a5a-887f-4fb3-b363-3b7fe8e74483 --assignee <service principal> --scope /subscriptions/<subscription id>
- az keyvault key create --kty RSA --size 2048 --name pulumiStateEncryptionKey --vault-name mtpshrpulkvt01

## How to retrieve the storage account key
- az storage account keys list -n mtpshrpulstg01 --query "[0].value" -o=tsv

## Connect for local deployment
- $env:AZURE_STORAGE_ACCOUNT="mtpshrpulstg01"
- $env:AZURE_STORAGE_KEY=az storage account keys list -n mtpshrpulstg01 --query "[0].value" -o=tsv
- $env:AZURE_KEYVAULT_AUTH_VIA_CLI="true"
- pulumi login --cloud-url azblob://mtpshrpulctn01 

## Creating the stack (if needed)
- pulumi stack init --secrets-provider="azurekeyvault://mtpshrpulkvt01.vault.azure.net/keys/pulumiStateEncryptionKey" 

## Debug
- $env:PULUMI_DEBUG = $false
- pulumi up
Then attach the debugger
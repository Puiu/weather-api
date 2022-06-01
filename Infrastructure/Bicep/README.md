# Prerequisites
* [Bicep and tools](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/install)

# Deploy the template
Use these commands to deploy the template file. Replace **myResourceGroup** and **path-to-template** with the appropiate values. 
```
az group create --name myResourceGroup --location "westeurope" && 
az deployment group create --resource-group myResourceGroup --template-file <path-to-template>
```





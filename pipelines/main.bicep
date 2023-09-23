targetScope = 'subscription'

param resourceGroupName string
param location string
param webAppName string
param appServicePlanName string
param runtimeStack string
param sku string

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}

module webApp './web-app.bicep' = {
  name: 'storageDeployment'
  scope: rg
  params: {
    webAppName: webAppName
    appServicePlanName: appServicePlanName
    runtimeStack: runtimeStack
    location: location
    sku: sku
  }
}

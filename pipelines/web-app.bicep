@minLength(2)
param webAppName string = 'AzureLinuxApp'
@minLength(2)
param appServicePlanName string
param sku string = 'S1'
param runtimeStack string = 'dotnet|7.0'
param location string = resourceGroup().location

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webAppPortal 'Microsoft.Web/sites@2022-03-01' = {
  name: webAppName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: runtimeStack
      ftpsState: 'FtpsOnly'
    }
    httpsOnly: true
  }
  identity: {
    type: 'SystemAssigned'
  }
}

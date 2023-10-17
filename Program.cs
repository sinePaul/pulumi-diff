using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage.Inputs;
using Pulumi.AzureNative.Storage;
using System.Collections.Generic;


return await Pulumi.Deployment.RunAsync(() =>
{
    var resourceGroup = new ResourceGroup("RG-DIFF");

    var storageAccount = new StorageAccount("stdiff", new StorageAccountArgs
    {
        ResourceGroupName = resourceGroup.Name,
        Sku = new SkuArgs
        {
            Name = SkuName.Standard_LRS,
        },
        Kind = Kind.StorageV2,
    });

    // Create a Blob Container in the storage account.
    var blobContainer = new BlobContainer("container", new BlobContainerArgs
    {
        AccountName = storageAccount.Name,
        ResourceGroupName = resourceGroup.Name,
        PublicAccess = PublicAccess.None,
    });

    // Upload a Blob to the Blob Container.
    var blob = new Blob("blob", new BlobArgs
    {
        AccountName = storageAccount.Name,
        ContainerName = blobContainer.Name,
        ResourceGroupName = resourceGroup.Name,
        Type = BlobType.Block,
    });

    // Export outputs here
    return new Dictionary<string, object?>
    {
        ["outputKey"] = "outputValue"
    };
});

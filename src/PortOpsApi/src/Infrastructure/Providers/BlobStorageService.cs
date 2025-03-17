using Als.Foundation.Data.BlobStorage;
using Azure.Storage.Blobs;
using Sonasid.DisUp.PortOps.Domain.Common.Interfaces;

namespace Sonasid.DisUp.PortOps.Infrastructure.Providers;

public class BlobStorageService : BaseBlobStorage, IBlobStorageService
{
    public BlobStorageService(BlobContainerClient containerClient) : base(containerClient)
    {
    }

    public async Task<bool> DeleteBlobAsync(string blobName, CancellationToken cancellationToken = default)
    {
        var blobClient = _containerClient.GetBlobClient(blobName);
        var response = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        return response.Value; // Returns true if the blob is deleted, false otherwise
    }

}

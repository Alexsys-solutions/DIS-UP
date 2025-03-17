using Als.Foundation.Data.Abstractions.BlobStorage;

namespace Sonasid.DisUp.PortOps.Domain.Common.Interfaces;
public interface IBlobStorageService : IBaseBlobStorage
{
    Task<bool> DeleteBlobAsync(string fileName, CancellationToken cancellationToken);
}

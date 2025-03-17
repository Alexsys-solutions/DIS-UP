using Sonasid.DisUp.PortOps.Domain.Common.Interfaces;

namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.DeleteFromBlob;

/// <summary>
/// Handler for deleting a file from blob storage.
/// </summary>
public class DeleteFileHandler : IRequestHandler<DeleteFileCommand, DeleteFileResponse>
{
    private readonly IBlobStorageService _blobStorageService;

    public DeleteFileHandler(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    public async Task<DeleteFileResponse> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _blobStorageService.DeleteBlobAsync(request.FileName, cancellationToken);

        return new DeleteFileResponse
        {
            IsSuccess = isDeleted,
            Message = isDeleted ? "File deleted successfully." : "File not found or couldn't be deleted."
        };
    }
}

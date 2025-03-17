using Sonasid.DisUp.PortOps.Domain.Common.Interfaces;

namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UpdateBlob;

/// <summary>
/// Handler for updating a file in blob storage.
/// </summary>
public class UpdateFileHandler : IRequestHandler<UpdateFileCommand, UpdateFileResponse>
{
    private readonly IBlobStorageService _blobStorageService;

    public UpdateFileHandler(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    public async Task<UpdateFileResponse> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _blobStorageService.DeleteBlobAsync(request.FileName, cancellationToken);

        if (!isDeleted)
        {
            return new UpdateFileResponse
            {
                IsSuccess = false,
                Message = "File not found or couldn't be deleted."
            };
        }

        var newFileName = $"{Guid.NewGuid()}_{Path.GetFileName(request.File.FileName)}";

        using var stream = request.File.OpenReadStream();
        await _blobStorageService.UploadBlobAsync(newFileName, stream, cancellationToken);

        var fileLink = $"https://gpiablob.blob.core.windows.net/images/{newFileName}";

        return new UpdateFileResponse
        {
            IsSuccess = true,
            FileLink = fileLink,
            Message = "File updated successfully."
        };
    }
}

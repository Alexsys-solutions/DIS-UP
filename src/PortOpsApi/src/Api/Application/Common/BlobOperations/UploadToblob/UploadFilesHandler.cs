using Sonasid.DisUp.PortOps.Domain.Common.Interfaces;

namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UploadToblob;


/// <summary>
/// Handler for uploading files to blob storage.
/// </summary>
public class UploadFilesHandler : IRequestHandler<UploadFilesCommand, UploadFilesResponse>
{
    private readonly IBlobStorageService _blobStorageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadFilesHandler"/> class.
    /// </summary>
    /// <param name="blobStorageService">The blob storage service.</param>
    public UploadFilesHandler(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    /// <summary>
    /// Handles the file upload command.
    /// </summary>
    /// <param name="request">The upload file command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the list of file links.</returns>
    public async Task<UploadFilesResponse> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
    {
        var fileLinks = new List<string>();

        foreach (var file in request.Files)
        {
            if (file == null || file.Length == 0)
                continue;

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            // Upload file to blob storage
            using var stream = file.OpenReadStream();
            await _blobStorageService.UploadBlobAsync(fileName, stream, cancellationToken);

            // Assuming the blob storage link format
            var fileLink = $"https://gpiablob.blob.core.windows.net/images/{fileName}";
            fileLinks.Add(fileLink);
        }

        return new UploadFilesResponse { FileLinks = fileLinks };
    }
}
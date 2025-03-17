namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UploadToblob;

/// <summary>
/// Command for uploading a list of files to blob storage.
/// </summary>
public class UploadFilesCommand : IRequest<UploadFilesResponse>
{
    /// <summary>
    /// Gets or sets the list of files to be uploaded.
    /// </summary>
    public List<IFormFile> Files { get; set; }
}
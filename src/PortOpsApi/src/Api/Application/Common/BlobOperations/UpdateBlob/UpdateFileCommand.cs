namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UpdateBlob;

/// <summary>
/// Command for updating a file.
/// </summary>
public class UpdateFileCommand : IRequest<UpdateFileResponse>
{
    public string FileName { get; set; }
    public IFormFile File { get; set; }
}

namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UpdateBlob;

/// <summary>
/// Response for updating a file.
/// </summary>
public class UpdateFileResponse
{
    public bool IsSuccess { get; set; }
    public string FileLink { get; set; }
    public string Message { get; set; }
}
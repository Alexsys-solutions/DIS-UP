namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.DeleteFromBlob;

/// <summary>
/// Response for deleting a file.
/// </summary>
public class DeleteFileResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

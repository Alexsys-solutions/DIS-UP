namespace Sonasid.DisUp.PortOps.Api.Application.Common.BlobOperations.UploadToblob;

/// <summary>
/// Response for the uploaded files containing their blob storage links.
/// </summary>
public class UploadFilesResponse
{
    /// <summary>
    /// Gets or sets the list of links to the uploaded files.
    /// </summary>
    public List<string> FileLinks { get; set; }
}
namespace Sonasid.DisUp.PortOps.Domain.Arrivages.Entities;
public class ArrivageDocument
{
    public Guid Id { get; set; }
    public Guid ArrivageId { get; set; }
    public Arrivage? Arrivage { get; set; }

    // Some reference to the type (Proforma, Contrat, etc.)
    public string DocTypeCode { get; set; } = default!;
    // The actual file path, storage key, or external link:
    public string FilePath { get; set; } = default!;
    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
}

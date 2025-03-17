namespace Sonasid.DisUp.PortOps.Domain.Arrivages.Entities;
public class ArrivageDocument
{
    public Guid Id { get; set; }
    public Guid ArrivageId { get; set; }
    public Arrivage? Arrivage { get; set; }

    // e.g. "Contrat", "DemandeLC", "LicenceImport", "CertNonRadio", "CertNonExplosif", ...
    public string DocumentTypeCode { get; set; } = default!;

    // The actual path or file reference
    public string FilePath { get; set; } = default!;

    // Whether it's optional or mandatory
    public bool IsMandatory { get; set; }

    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    public string UploadedBy { get; set; } = default!;
}
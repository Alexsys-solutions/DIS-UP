namespace Sonasid.DisUp.PortOps.Domain.Arrivages.Entities;

public class ArrivageLine
{
    public Guid Id { get; set; }

    // Link to parent Arrivage
    public Guid ArrivageId { get; set; }
    public Arrivage? Arrivage { get; set; }

    // The purchase order from SAP. If you store multiple lines per order,
    // you might want a "PurchaseOrderLineId" or "ItemNumber" as well.
    public string PurchaseOrderNo { get; set; } = default!;

    // Data from SAP for each PO line:
    public string? FactureProformaNo { get; set; }
    public string Qualite { get; set; } = default!; // e.g. “S235”
    public decimal Tonnage { get; set; }
    public decimal PrixUnitaireFinal { get; set; }
    public decimal? TauxDeChange { get; set; }
    public string? Incoterm { get; set; }
    public int? DelaiPaiement { get; set; }

    // Possibly also store a "FournisseurRefId" if each line can differ
    // or store it in the parent if it's consistent across lines.
}


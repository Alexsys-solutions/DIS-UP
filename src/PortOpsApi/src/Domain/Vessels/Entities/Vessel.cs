using Sonasid.DisUp.PortOps.Domain.Arrivages.Entities;

namespace Sonasid.DisUp.PortOps.Domain.Vessels.Entities;
public class Vessel
{
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the vessel
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Port de chargement (Valeur Par défaut "Port de chargement" ou port spécifique)
    /// This might be stored as a reference ID to your referential microservice,
    /// e.g. "PortRefId".
    /// </summary>
    public string? PortChargementRefId { get; set; }

    /// <summary>
    /// Taux de dechargement (Obligatoire).
    /// </summary>
    public decimal TauxDechargement { get; set; }

    /// <summary>
    /// Conditions of half dispatch? 
    /// Possibly store as a bool or a small string describing them.
    /// </summary>
    public bool HalfDispatchEligible { get; set; }

    /// <summary>
    /// Demurrage rate or conditions.
    /// If it's a numeric rate, store decimal.
    /// If you have more text describing it, store a string.
    /// </summary>
    public decimal? DemurrageRate { get; set; }

    /// <summary>
    /// Date limite de chargement => We can tie it to LaycanEnd or
    /// store a separate field if that differs from Laycan.
    /// </summary>
    public DateTime? DateLimiteChargement { get; set; }

    // Additional fields from your earlier Vessel approach:
    public DateTime? LaycanStart { get; set; }
    public DateTime? LaycanEnd { get; set; }

    // Real arrival times, startDischarge, endDischarge...
    public DateTime? RealDeparture { get; set; }
    public DateTime? RealArrival { get; set; }
    public DateTime? StartDischarge { get; set; }
    public DateTime? EndDischarge { get; set; }

    // One vessel can be linked to multiple Arrivages
    public List<Arrivage> Arrivages { get; set; } = new();
}
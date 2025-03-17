using Sonasid.DisUp.PortOps.Domain.Vessels.Entities;

namespace Sonasid.DisUp.PortOps.Domain.Arrivages.Entities;
public class Arrivage
{
    public Guid Id { get; set; }

    // Payment method, etc.
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Unknown;

    /// <summary>
    /// If PaymentMethod == LC, we store the Swift opening date here
    /// (3.1 in your scenario).
    /// </summary>
    public DateTime? SwiftOpeningDate { get; set; }

    // "RG3" scenario: Arrivage can have multiple lines if you do that approach
    public List<ArrivageLine> Lines { get; set; } = new();

    // Core domain fields
    public ArrivageState State { get; set; } = ArrivageState.Draft;

    // Possibly a link to the Vessel if that step is done
    public Guid? VesselId { get; set; }
    public Vessel? Vessel { get; set; }

    /// <summary>
    /// Poids & qualite contractualisé [Depart, Arrivee, Moyenne] (Obligatoire).
    /// If it’s a single set for the entire contract, store them here.
    /// If they vary by line, store them in ArrivageLine.
    /// </summary>
    public decimal ContractualWeightDepart { get; set; }
    public decimal ContractualWeightArrivee { get; set; }
    public decimal ContractualWeightMoyenne { get; set; }

    /// <summary>
    /// Date signature contrat (Obligatoire)
    /// </summary>
    public DateTime DateSignatureContrat { get; set; }

    /// <summary>
    /// Date demande licence import (Obligatoire)
    /// </summary>
    public DateTime DateDemandeLicenceImport { get; set; }

    /// <summary>
    /// Date obtention licence import (Obligatoire)
    /// </summary>
    public DateTime DateObtentionLicenceImport { get; set; }

    /// <summary>
    /// Taxes
    /// </summary>
    public decimal? Taxes { get; set; }

    /// <summary>
    /// Conditions d'achat
    /// </summary>
    public string? ConditionsAchat { get; set; }

    /// <summary>
    /// Informations contractuelles
    /// </summary>
    public string? InformationsContractuelles { get; set; }

    // Documents or references to them
    public List<ArrivageDocument> Documents { get; set; } = new();

    // Auditing
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = default!;

    public void Close()
    {
        State = ArrivageState.Closed;
    }
}
public enum ArrivageState
{
    Draft = 0,
    Active = 1,
    Closed = 2
}

public enum PaymentMethod
{
    Unknown = 0,
    LC = 1,
    Transfer = 2
}
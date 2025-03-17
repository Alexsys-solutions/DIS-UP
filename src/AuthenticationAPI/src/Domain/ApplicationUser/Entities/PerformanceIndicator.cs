namespace AuthenticationAPI.Domain.ApplicationUser.Entities;

/// <summary>
/// Represents a performance indicator for an agent.
/// </summary>
public class PerformanceIndicator
{
    public Guid Id { get; set; }
    public string AgentId { get; set; } // Agent's User ID
    public int LeadsGenerated { get; set; } = 0; // Initialized to 0
    public int AppointmentsScheduled { get; set; } = 0; // Initialized to 0
    public int SuccessfulSales { get; set; } = 0; // Initialized to 0
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow; // Default to current time
}

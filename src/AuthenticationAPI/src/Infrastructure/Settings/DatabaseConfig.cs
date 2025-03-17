namespace AuthenticationAPI.Infrastructure.Settings;

/// <summary>
/// Represents the configuration for the database.
/// </summary>
public sealed class DatabaseConfig
{
    /// <summary>
    /// Gets or sets the ID of the database.
    /// </summary>
    public string DatabaseId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the containerId within the database.
    /// </summary>
    public string ContainerId { get; set; } = default!;
}

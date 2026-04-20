namespace HolyCRMApi.Dtos;

/// <summary>
/// Represents a postal address.
/// </summary>
public class AddressDto
{
    /// <summary>
    /// Primary street address line.
    /// </summary>
    public string Address1 { get; set; } = string.Empty;

    /// <summary>
    /// Secondary street address line (e.g. apartment, suite, unit).
    /// </summary>
    public string? Address2 { get; set; }

    /// <summary>
    /// Postal or ZIP code.
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// City or locality name.
    /// </summary>
    public string City { get; set; }  = string.Empty;

    /// <summary>
    /// Country name.
    /// </summary>
    public string? Country { get; set; }
}
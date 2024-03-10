using StammPhoenix.Domain.Core;

namespace StammPhoenix.Domain.Models;

/// <summary>
/// A person that has some kind of leadership role in the tribe, who needs to be able to access planning data and/or
/// have their public information displayed on the website.
/// </summary>
public sealed class Leader : DomainEntity
{
    private List<Group> groups = new();

    /// <summary>
    /// The groups this leader is a member of.
    /// </summary>
    public IReadOnlyCollection<Group> Groups => this.groups.AsReadOnly();

    /// <summary>
    /// The family name of the leader.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// The first name of the leader.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// The e-mail adress that the leader uses to login to the website and receive notifications.
    /// </summary>
    public required string LoginEmail { get; set; }

    /// <summary>
    /// The hash and salt of the password that the leader uses to log in to the website.
    /// </summary>
    public required string PasswordHash { get; set; }

    /// <summary>
    /// The full address of the leader. Can be set if the address needs to be displayed publicly.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The public phone number of the leader. Can be set if the number needs to be displayed publicly.
    /// </summary>
    public string? PhoneNumber { get; set; }
}

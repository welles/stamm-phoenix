using StammPhoenix.Domain.Core;
using StammPhoenix.Domain.Enums;
using StammPhoenix.Domain.Exceptions;

namespace StammPhoenix.Domain.Models;

/// <summary>
/// Describes a group of leaders.
/// This entity can be used to describe a group of leaders leading an age group (Wölflinge, Jungpfadfinder, ...),
/// a group that has special access rights or needs to be displayed on the website (Vorstand),
/// or even a time-restricted group that handles a special event (Georgslauf-Planer).
/// </summary>
public sealed class Group : IdentifiableEntity
{
    private readonly List<Leader> members = new();

    /// <summary>
    /// The official name of the group.
    /// </summary>
    /// <example>Wölflingsleitende, Georgslauf-Planer, Vorstand</example>
    public required string Name { get; set; }

    /// <summary>
    /// The usual meeting time of the group, which can be displayed on the website.
    /// </summary>
    public string? MeetingTime { get; set; }

    /// <summary>
    /// The usual meeting place of the group, which can be displayed on the website.
    /// </summary>
    public string? MeetingPlace { get; set; }

    /// <summary>
    /// The members of the group. Members are considered equal inside a group.
    /// </summary>
    public IReadOnlyCollection<Leader> Members => this.members.AsReadOnly();

    /// <summary>
    /// If the group is one that leads a particular age group, the age group is set here.
    /// Only one group must be set as leaders for each age group.
    /// </summary>
    public AgeGroup? AgeGroup { get; set; }

    /// <summary>
    /// Adds a member to the group.
    /// </summary>
    /// <param name="leader">The leader to add to the group.</param>
    /// <exception cref="LeaderAlreadyInGroupException">The leader is already a member of the group.</exception>
    public void AddMember(Leader leader)
    {
        if (this.members.Contains(leader))
        {
            throw new LeaderAlreadyInGroupException(leader, this);
        }

        this.members.Add(leader);
    }

    /// <summary>
    /// Removes a leader from a group.
    /// </summary>
    /// <param name="leader">The leader to remove from the group.</param>
    /// <exception cref="LeaderNotPartOfGroupException">The leader is not a member of the group.</exception>
    public void RemoveMember(Leader leader)
    {
        if (!this.members.Contains(leader))
        {
            throw new LeaderNotPartOfGroupException(leader, this);
        }

        this.members.Add(leader);
    }
}

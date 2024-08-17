using StammPhoenix.Domain.Core;

namespace StammPhoenix.Domain.Models;

/// <summary>
/// Describes a single event for all or a subsection of members.
/// Central data point for all planning and informational data surrounding a single, time-restricted activity like a camp, day trip or cabin weekend.
/// </summary>
public sealed class Event : DomainEntity
{
    /// <summary>
    /// Title of an event. Not required to be unique, but must be unique when including the year.
    /// <example>
    /// Hüttenwochenende
    /// </example>
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The start date of the event.
    /// For one-day events, only the <see cref="StartDate"/> can be configured and <see cref="EndDate"/> can be left empty.
    /// </summary>
    public required DateOnly StartDate { get; set; }

    /// <summary>
    /// The end date of the event.
    /// For one-day events, only the <see cref="StartDate"/> can be configured and <see cref="EndDate"/> can be left empty.
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Free text for describing the event in a few words.
    /// Must not be used for internal notes for the event, but for public-facing advertisement of what the event is about.
    /// </summary>
    /// <example>
    /// Am diesjährgen Hüttenwochenende fahren wir auf die Berghütte in Bergenhausen.
    /// </example>
    public string? Description { get; set; }

    /// <summary>
    /// A simple and short unique name for the event that can be used in URLs.
    /// </summary>
    public required string Link { get; set; }

    /// <summary>
    /// Whether the event should be displayed on the public facing event calendar.
    /// </summary>
    public required bool Public { get; set; }
}

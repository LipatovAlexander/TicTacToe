using Application.Common.Interfaces;

namespace Application.Events.UserRegistered;

public sealed class UserRegisteredEvent : IEvent
{
    public required int UserId { get; set; }
    public required string UserName { get; set; }
}
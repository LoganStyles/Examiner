using Examiner.Domain.Dtos;

namespace Examiner.Application.Notifications.Interfaces;

/// <summary>
/// Describes contract for sending messages to channels
/// </summary>
public interface IMessageService
{
    Task<GenericResponse> SendMessage(string channel, string message);
}
using NotificationSchedule.Contracts.DTO;
using System;
using System.Threading.Tasks;

namespace NotificationSchedule.Services.Interfaces
{
    public interface INotificationService
    {
        NotificationResponseDTO CreateNotification(NotificationDTO notificationDTO);
        Task<NotificationResponseDTO> GetNotification(Guid id);
    }
}

using Microsoft.Extensions.Logging;
using Notification.DataAccess.GenericRepository.Interface;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;
using NotificationSystem.infrastructure.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;


namespace NotificationSchedule.Services
{
    public class NotificationService : INotificationService
    {
        private const string SUCCCESS_MESSAGE = "Data Saved Successfully";
        private const string Notification_Message = "Notification Saved Sucessfully";
        private readonly IGenericRepository<CompanyNotification> genericRepository;
        private readonly ILogger logger;

        public NotificationService(IGenericRepository<CompanyNotification> genericRepository, ILogger<NotificationService> logger)
        {
            this.genericRepository = genericRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Create Notifcation Based On users inputs
        /// </summary>
        /// <param name="notificationDTO"></param>
        /// <returns>Return response string</returns>
        /// <exception cref="Exception"></exception>
        public string CreateNotification(NotificationDTO notificationDTO)
        {
            using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    switch (notificationDTO.Market)
                    {
                        case "Denmark":
                            string response = NotificationCreate(notificationDTO, 5);
                            break;
                        case "Norway":
                            NorwayNotificationCreate(notificationDTO, 4);
                            break;
                        case "Sweden":
                            SwedenNotificationCreate(notificationDTO, 4);
                            break;
                        case "Finland":
                            NotificationCreate(notificationDTO, 5);
                            break;
                    }
                    scope.Complete();
                    return Notification_Message;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    logger.LogError("Create Notification Service unsuccessful Request", ex.Message);
                    throw new Exception(ex.Message, ex);
                }
            }

        }

        /// <summary>
        /// GetNotification
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return notification response objects</returns>
        public async Task<NotificationResponseDTO> GetNotification(Guid id)
        {
            try
            {
                var notifications = await genericRepository.GetAsync(x => x.CompanyId == id);
                NotificationResponseDTO notificationResponseDTO = null;
                if (notifications != null)
                {
                    string[] notiDate = notifications.Select(s => s.SendDate.ToString("dd/MM/yyyy")).ToArray();
                    notificationResponseDTO = new()
                    {
                        CompanyId = id,
                        Notifications = notiDate
                    };
                }
                return notificationResponseDTO;
            }
            catch (Exception ex)
            {
                logger.LogError("Get Notification Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        #region Private Methods
        private string NotificationCreate(NotificationDTO notificationDTO, int dateRange)
        {
            if (notificationDTO.Market.ToLower() == "denmark" || (notificationDTO.Market.ToLower() == "finland" && notificationDTO.CompanyType.ToLower() == "large"))
            {
                for (int i = 0; i < dateRange; i++)
                {
                    int dateAdd = i == 0 ? 1 : i == 1 ? 5 : i == 2 ? 10 : i == 3 ? 15 : i == 4 ? 20 : 0;
                    NotificationInsert(notificationDTO, dateAdd, true);
                }
            }
            else if (notificationDTO.Market.ToLower() == "finland" && notificationDTO.CompanyType.ToLower() != "large")
            {
                NotificationInsert(notificationDTO, 0, false);
            }
            return SUCCCESS_MESSAGE;
        }

        private string NorwayNotificationCreate(NotificationDTO notificationDTO, int dateRange)
        {

            for (int i = 0; i < dateRange; i++)
            {
                int dateAdd = i == 0 ? 1 : i == 1 ? 5 : i == 2 ? 10 : i == 3 ? 20 : 0;
                NotificationInsert(notificationDTO, dateAdd, true);
            }
            return SUCCCESS_MESSAGE;
        }

        private string SwedenNotificationCreate(NotificationDTO notificationDTO, int dateRange)
        {
            if (notificationDTO.CompanyType.ToLower() == "large")
            {
                for (int i = 0; i < dateRange; i++)
                {
                    int dateAdd = i == 0 ? 1 : i == 1 ? 7 : i == 2 ? 14 : i == 3 ? 28 : 0;
                    NotificationInsert(notificationDTO, dateAdd, true);
                }
            }
            else
            {
                NotificationInsert(notificationDTO, 0, false);
            }

            return SUCCCESS_MESSAGE;
        }

        private void NotificationInsert(NotificationDTO notificationDTO, int dateAdd, bool IsSent)
        {
            CompanyNotification companyNotification = new()
            {
                CompanyId = notificationDTO.CompanyId,
                SendDate = notificationDTO.CallDate.AddDays(dateAdd),
                IsSent = IsSent
            };
            genericRepository.Insert(companyNotification);
            genericRepository.Save();
        }
        #endregion

    }
}

﻿using System;
using System.Runtime.Serialization;

namespace NotificationSchedule.Contracts.Models.Exceptions
{
    [Serializable]
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException()
        {
        }

        public ServiceUnavailableException(string message) : base(message)
        {
        }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using TalentManagementApi.Application.Interfaces;
using System;

namespace TalentManagementApi.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
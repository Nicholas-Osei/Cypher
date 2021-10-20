using Cypher.Application.Interfaces.Shared;
using System;

namespace Cypher.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDefaults.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid EventId { get; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
            EventType = GetType().AssemblyQualifiedName!;
        }


    }
}

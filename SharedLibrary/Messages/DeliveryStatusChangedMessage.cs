using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Messages
{
    public class DeliveryStatusChangedMessage : MessageBase
    {
        public DeliveryStatuses Status { get; set; }
    }
}

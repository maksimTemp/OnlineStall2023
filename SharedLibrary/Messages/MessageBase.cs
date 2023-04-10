using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Messages
{
    public abstract class MessageBase
    {
        public OperationTypeMessage OperationType { get; set; }
        public Guid EntityId { get; set; }
    }
}

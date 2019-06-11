using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCMemoryException : MCProtocolException
    {
        public MCMemoryException()
        {
        }

        public MCMemoryException(string message) : base(message)
        {
        }

        public MCMemoryException(string message, MCErrorCode errorCode)
            : base(message, errorCode)
        {

        }

        public MCMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected MCMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

}

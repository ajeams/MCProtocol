using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCProtocolException : Exception
    {
        public MCErrorCode ErrorCode { get; }

        public MCProtocolException()
        {
        }

        public MCProtocolException(string message) : base(message)
        {
        }

        public MCProtocolException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected MCProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public MCProtocolException(string message, MCErrorCode errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

}

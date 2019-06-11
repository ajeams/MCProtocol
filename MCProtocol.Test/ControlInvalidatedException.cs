using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class ControlInvalidatedException : Exception
    {
        public LabeledControl Control { get; }

        public ControlInvalidatedException()
        {

        }

        public ControlInvalidatedException(LabeledControl control)
        {
            this.Control = control;
        }

        public ControlInvalidatedException(string message) : base(message)
        {
        }

        public ControlInvalidatedException(LabeledControl control, string message) : base(message)
        {
            this.Control = control;
        }

        public ControlInvalidatedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ControlInvalidatedException(LabeledControl control, string message, Exception innerException) : base(message, innerException)
        {
            this.Control = control;
        }

        [SecuritySafeCritical]
        protected ControlInvalidatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

}

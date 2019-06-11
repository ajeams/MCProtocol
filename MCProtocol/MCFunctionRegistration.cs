using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCFunctionRegistration
    {
        public ushort Id { get; }
        public Type RequestType { get; }
        public Type ResponseType { get; }

        public MCFunctionRegistration(ushort id, Type requestType, Type responseType)
        {
            this.Id = id;
            this.RequestType = requestType;
            this.ResponseType = responseType;
        }

        public abstract MCFunction Generate(PacketDirection direction, ushort subCommandCode, MCFunction request);
    }

    public class MCFunctionRegistration<REQ, RES> : MCFunctionRegistration
        where REQ : MCFunctionRequest
        where RES : MCFunctionResponse
    {
        public Func<ushort, REQ> RequestGenerator { get; }
        public Func<ushort, REQ, RES> ResponseGenerator { get; }

        public MCFunctionRegistration(ushort id, Func<ushort, REQ> requestGenerator, Func<ushort, REQ, RES> responseGenerator)
            : base(id, typeof(REQ), typeof(RES))
        {
            this.RequestGenerator = requestGenerator;
            this.ResponseGenerator = responseGenerator;
        }

        public override MCFunction Generate(PacketDirection direction, ushort subCommandCode, MCFunction request)
        {
            if (direction == PacketDirection.Request)
            {
                return this.RequestGenerator(subCommandCode);
            }
            else if (direction == PacketDirection.Response)
            {
                return this.ResponseGenerator(subCommandCode, (REQ)request);
            }

            return null;
        }

    }

}

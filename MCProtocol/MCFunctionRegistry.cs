using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public static class MCFunctionRegistry
    {
        private static Dictionary<ushort, MCFunctionRegistration> IdRegistrationCaches { get; } = new Dictionary<ushort, MCFunctionRegistration>();
        private static Dictionary<Type, MCFunctionRegistration> TypeIdCaches { get; } = new Dictionary<Type, MCFunctionRegistration>();

        static MCFunctionRegistry()
        {
            Register(new MCFunctionRegistration<MCFunctionBatchReadRequest, MCFunctionBatchReadResponse>(0x0401, i => new MCFunctionBatchReadRequest(), (i, r) => new MCFunctionBatchReadResponse(r)));
            Register(new MCFunctionRegistration<MCFunctionBatchWriteRequest, MCFunctionBatchWriteResponse>(0x1401, i => new MCFunctionBatchWriteRequest(), (i, r) => new MCFunctionBatchWriteResponse(r)));
        }

        public static MCFunctionRegistration Find(ushort id)
        {
            return IdRegistrationCaches.TryGetValue(id, out var registration) ? registration : null;
        }

        public static MCFunctionRegistration Find(Type type)
        {
            return TypeIdCaches.TryGetValue(type, out var registration) ? registration : null;
        }

        private static void Register(MCFunctionRegistration registration)
        {
            IdRegistrationCaches[registration.Id] = registration;
            TypeIdCaches[registration.RequestType] = registration;
            TypeIdCaches[registration.ResponseType] = registration;
        }

    }

}

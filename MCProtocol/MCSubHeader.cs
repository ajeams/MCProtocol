using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCSubHeader
    {
        private static Dictionary<Type, MCSubHeaderRegistration> TypeRegistrations { get; } = new Dictionary<Type, MCSubHeaderRegistration>();
        private static Dictionary<MCFrame, MCSubHeaderRegistration> FrameRegistrations { get; } = new Dictionary<MCFrame, MCSubHeaderRegistration>();

        static MCSubHeader()
        {
            Register(new MCSubHeaderRegistration<MCSubHeader3E>(MCFrame.MC3E, () => new MCSubHeader3E()));
            Register(new MCSubHeaderRegistration<MCSubHeader4E>(MCFrame.MC4E, () => new MCSubHeader4E()));
        }

        private static void Register(MCSubHeaderRegistration registration)
        {
            TypeRegistrations[registration.InstanceType] = registration;
            FrameRegistrations[registration.Frame] = registration;
        }

        public static MCSubHeaderRegistration Find(Type type)
        {
            TypeRegistrations.TryGetValue(type, out var registration);
            return registration;
        }

        public static MCSubHeaderRegistration Find(MCFrame frame)
        {
            FrameRegistrations.TryGetValue(frame, out var registration);
            return registration;
        }

        public MCSubHeader()
        {

        }

        public virtual void Write(DataProcessor target)
        {

        }

        public virtual void Read(DataProcessor source)
        {

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCSubHeaderRegistration
    {
        public MCFrame Frame { get; }
        public Type InstanceType { get; }

        public MCSubHeaderRegistration(MCFrame frame, Type instanceType)
        {
            this.Frame = frame;
            this.InstanceType = instanceType;
        }

        public abstract MCSubHeader CreateInstance();
    }

    public class MCSubHeaderRegistration<T> : MCSubHeaderRegistration where T : MCSubHeader
    {
        public Func<MCSubHeader> Generator { get; }

        public MCSubHeaderRegistration(MCFrame frame, Func<T> generator)
            : base(frame, typeof(T))
        {
            this.Generator = generator;
        }

        public override MCSubHeader CreateInstance()
        {
            return this.Generator();
        }

    }

}

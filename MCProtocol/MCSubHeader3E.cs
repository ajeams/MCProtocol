using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCSubHeader3E : MCSubHeader
    {
        public MCSubHeader3E()
        {

        }

        public override void Write(DataProcessor target)
        {
            base.Write(target);
        }

        public override void Read(DataProcessor source)
        {
            base.Read(source);
        }

    }

}

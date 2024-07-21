using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s2ProtocolFurry.Helpers
{
    public class SVarUint32
    {
        public uint Value { get; set; }

        public SVarUint32(uint value)
        {
            Value = value;
        }
    }

    public static class VarUint32Helper
    {       
        public static uint GetValue(SVarUint32 instance)
        {            
            if (instance != null)
            {
                return instance.Value;
            }
            
            return 0;
        }
    }
}

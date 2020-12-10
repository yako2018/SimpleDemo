using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FactoryConsole.Ab
{
    abstract public class AbstractMain
    {
        internal abstract Task DoAb();
        internal virtual async Task DoVirtual()
        {

        }
    }
}

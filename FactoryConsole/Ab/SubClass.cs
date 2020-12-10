using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FactoryConsole.Ab
{
    public class SubClass : AbstractMain, ISubClass
    {
        public void Do() 
        {
        
        }

        internal override async Task DoAb()
        {
            await Task.CompletedTask;
        }

        internal override async Task DoVirtual()
        {
            await Task.CompletedTask;
        }
    }
}

using LogSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
{
    public interface ILogHelper
    {
        //string SayHi();

        Task DoLog(LogModel model);

    }
}

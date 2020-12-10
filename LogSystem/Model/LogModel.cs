using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogSystem.Model
{
    public class LogModel
    {
        public string Name { get; set; }

        public string KeyWord { get; set; }

        public string Message { get; set; }

        public string ExceptionMessage { get; set; }
        public string ExceptionDetail { get; set; }

        public LogLevel LogLevel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCoreIocLearn
{
    public class Logger : ILogger
    {
        public void LogInfo(string info)
        {
            Console.WriteLine(info);
        }
    }

    public interface ILogger
    {
        void LogInfo(string info);
    }
}

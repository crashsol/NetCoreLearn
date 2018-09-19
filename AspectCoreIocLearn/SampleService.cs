using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCoreIocLearn
{
    public class SampleService:ISampleService
    {
        private readonly ILogger _logger;

        //构造器注入
        public SampleService(ILogger logger)
        {
            _logger = logger;
          
        }

        public void Test()
        {
            _logger.LogInfo("构造器注入成功");
        }
    }

    public interface ISampleService
    {
        void Test();
    }
}

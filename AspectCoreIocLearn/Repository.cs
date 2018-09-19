using AspectCore.Injector;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCoreIocLearn
{
   public  class Repository:IRepository
    {

        //属性注入
        [FromContainer]
        private ILogger logger { get; set; }

        public void Test()
        {
            logger.LogInfo("属性注入成功");
        }
    }

    public interface IRepository
    {

        void Test();

    }
}

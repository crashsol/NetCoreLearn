using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCoreReflectionLearn
{
    [Customer("2")]
    [Customer("1")]
    public class Test
    {
        [Customer("1")]
        [Customer("1")]
        [Customer("1")]
        [Customer("1")]
        [Customer("1")]
        [Customer("1")]
        [Customer("1")]
        public string Name { get; set; }
    }
}

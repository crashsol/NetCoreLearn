using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCoreReflectionLearn
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true,Inherited =false)]
    public class CustomerAttribute : Attribute
    {

     
        public  string Name { get; set; }
        public  CustomerAttribute(string name)
        {
            Name = name;
        }
    }
}

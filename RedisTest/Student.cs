using System;
using System.Collections.Generic;
using System.Text;

namespace RedisTest
{
    [Serializable]
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}

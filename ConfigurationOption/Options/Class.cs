﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationOption.Options
{
    public class Class
    {

        public string ClassNo { get; set; }

        public string ClassDesc { get; set; }

        public List<Student> Students { get; set; }
    }


    public  class Student
    {
        public string Name { get; set; }

        public string Age { get; set; }
    }
}

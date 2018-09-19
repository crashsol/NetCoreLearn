using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeCycleLearn.Services
{
    public class TestService : ITestService
    {
      public TestService()
       {
            Id = Guid.NewGuid();
       }
        public Guid Id { get; set; }

        public List<string> GetList(string a)
        {
            return new List<string>() { "sol", "crash", "crashsol" };
        }
    }

    public class TestService2 : ITestService2
    {
        public TestService2()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public List<string> GetList(string a)
        {
            return new List<string>() { "sol", "crash", "crashsol" };
        }
    }

    public class TestService3 : ITestService3
    {
       public TestService3()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public List<string> GetList(string a)
        {
            return new List<string>() { "sol", "crash", "crashsol" };
        }
    }
}

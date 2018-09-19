using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeCycleLearn.Services
{
    public interface ITestService
    {
        Guid Id { get; }
        List<string> GetList(string a);
    }
}

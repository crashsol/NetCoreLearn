using Microsoft.AspNetCore.Authorization;

namespace AuthorizeLearn.Service
{

    /// <summary>
    /// 最小年龄显示
    /// </summary>
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; private set; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
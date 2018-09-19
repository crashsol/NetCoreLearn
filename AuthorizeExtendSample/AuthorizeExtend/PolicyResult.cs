using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace AuthorizeExtendSample.AuthorizeExtend
{

    /// <summary>
    /// 策略结果
    /// </summary>
    public class PolicyResult
    {
        /// <summary>
        /// 所有角色
        /// </summary>
        public IEnumerable<string> Roles { get; set; }


        /// <summary>
        /// 所有权限
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }

    }
}

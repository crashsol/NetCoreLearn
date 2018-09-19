using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Models.ConsentViewModels
{
    /// <summary>
    /// 同意授权 用户输入Model   
    /// </summary>
    public class ConsentInputModel
    {
        /// <summary>
        /// 授权 / 拒绝
        /// </summary>
        public string Button { get; set; }


        /// <summary>
        /// 确认授权的内容
        /// </summary>
        public IEnumerable<string> ScopesConsented { get; set; }


        /// <summary>
        /// 记住我,下次就不用再显示授权
        /// </summary>
        public bool RememberConsent { get; set; }


        /// <summary>
        /// 回调地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}

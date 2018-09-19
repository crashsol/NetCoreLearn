using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Models
{
    public class ConsentViewModel:ConsentInputModel
    {

        public string ClientName { get; set; }           //客户端名称

        public string ClientUrl { get; set; }            //客户端网站 URL（在同意屏幕上使用）

        public string ClientLogo { get; set; }           //客户端图标连接

        public bool RequireConsent { get; set; }         //指定用户是否可以选择存储同意决定。默认为true。        

        public bool AllowRememberConsent { get; set; }   //用户是否可以进行 记住我 选择。默认为true。       


        /// <summary>
        /// 认证的资源
        /// </summary>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }     

        /// <summary>
        /// Api资源
        /// </summary>
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }

    }

    public class ScopeViewModel
    {
        public string Name { get; set; }              //唯一   认证资源名称

        public string DisplayName { get; set; }       //资源名称（前台显示）

        public string Description { get; set; }      //资源描述

        //供 Consent页面回显使用，显示资源是否被选择   默认值为:false
        //Specifies whether the user can de-select the scope on the consent screen (if the consent screen wants to implement such a feature). 
        //Defaults to false.
        public bool Required { get; set; }

        //强调
        //Specifies whether the consent screen will emphasize this scope (if the consent screen wants to implement such a feature). 
        //Use this setting for sensitive or important scopes. Defaults to false.
        public bool Emphasize { get; set; }        

        public bool Checked { get; set; }         //由用户选择是否勾选 

    }

    /// <summary>
    /// Consent 用户选择页面
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
        /// 记住我
        /// </summary>
        public bool RememberConsent { get; set; }


        /// <summary>
        /// 回调地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// 授权 处理结果
    /// </summary>
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }

        public bool ShowView => ViewModel != null;
        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;
        public string ValidationError { get; set; }
    }

    public class ConsentOptions {

        public static bool EnableOfflineAccess = true;
        public static string OfflineAccessDisplayName = "Offline Access";
        public static string OfflineAccessDescription = "当你离线时，允许访问你的应用和资源";

        public static readonly string MustChooseOneErrorMessage = "至少选择一样授权";
        public static readonly string InvalidSelectionErrorMessage = "无效的选择";

    }
}

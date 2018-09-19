using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Models.ConsentViewModels
{
    /// <summary>
    /// 同意授权 页面Model
    /// </summary>
    public class ConsentViewModel:ConsentInputModel
    {
        /// <summary>
        /// 第三方名称
        /// </summary>
        public string ClientName { get; set; }           //客户端名称

        /// <summary>
        /// 第三方网址
        /// </summary>
        public string ClientUrl { get; set; }            //客户端网站 URL（在同意屏幕上使用）

        /// <summary>
        /// 第三方Logo图标
        /// </summary>
        public string ClientLogo { get; set; }           //客户端图标连接


        /// <summary>
        /// 允许记住我
        /// </summary>
        public bool AllowRememberConsent { get; set; }   

        /// <summary>
        /// 认证的资源
        /// </summary>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        /// <summary>
        /// Api资源
        /// </summary>
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }

    }

    /// <summary>
    ///ApiResource ,IdentityResource  Scpoe选项 
    /// </summary>
    public class ScopeViewModel
    {
        /// <summary>
        /// Scope名称
        /// </summary>
        public string Name { get; set; }              //唯一   认证资源名称

        /// <summary>
        /// Scope 显示名
        /// </summary>
        public string DisplayName { get; set; }       //资源名称（前台显示）

        /// <summary>
        /// Scope 描述
        /// </summary>
        public string Description { get; set; }      //资源描述

        /// <summary>
        /// 是否为必选
        /// </summary>
        public bool Required { get; set; }          //该资源是否为必选

        /// <summary>
        /// 强调
        /// </summary>
        public bool Emphasize { get; set; }         
    
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool Checked { get; set; }         //由用户选择是否勾选 

    }

   
}

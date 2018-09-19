using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspIdAndEF.Models.ConsentViewModels;

namespace IdentityServerWithAspIdAndEF.Services
{
    /// <summary>
    /// 同意授权服务
    /// </summary>
    public class ConsentService
    {
        /// <summary>
        /// 与认证服务器通讯服务类
        /// </summary>
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        /// <summary>
        /// 客户端 仓储
        /// </summary>
        private readonly IClientStore _clientStore;
        /// <summary>
        /// 资源 仓储
        /// </summary>
        private readonly IResourceStore _resourceStore;
        private readonly ILogger<ConsentService> _logger;

        public ConsentService(
            IIdentityServerInteractionService identityServerInteractionService,
            IClientStore clientStore,
            IResourceStore resourceStore,
            ILogger<ConsentService> logger)
        {

            _identityServerInteractionService = identityServerInteractionService;
            _resourceStore = resourceStore;
            _clientStore = clientStore;
            _logger = logger;

        }



        #region 创建Consent 页面Model


        /// <summary>
        /// 创建同意页面视图模型
        /// </summary>
        /// <param name="client">客户端信息</param>
        /// <param name="resource">请求的资源</param>      
        /// <param name="model">用户Consent input</param>
        private ConsentViewModel CreateConsentViewModel(
            Client client,
            Resources resource,
            ConsentInputModel model,
            string returnUrl)
        {
            var vm = new ConsentViewModel();
            //获取用户选择信息
            vm.RememberConsent = model?.RememberConsent ?? client.AllowRememberConsent;     //如果用户没有输入 设为默认值
            vm.ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>();      //主要用户页显示用户勾选内容，如果用户没有输入 默认选中内容为空 ,

            vm.ClientName = client.ClientName;
            vm.ClientUrl = client.ClientUri;
            vm.ClientLogo = client.LogoUri;
            vm.ReturnUrl = returnUrl;
            vm.AllowRememberConsent = client.AllowRememberConsent;

            //添加Identity Resource ScopeViewModel
            vm.IdentityScopes = resource.IdentityResources.Select(x => CreateScopeViewModel(x, (vm.ScopesConsented.Contains(x.Name) || model == null))).ToArray();
            //添加ApiResource Scope 
            vm.ResourceScopes = resource.ApiResources.SelectMany(b => b.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();

            //是否允许离线访问
            if (resource.OfflineAccess == true)
            {
                //添加离线访问Scope
                vm.ResourceScopes = vm.ResourceScopes.Union(new ScopeViewModel[] {
                    //根据用户输入 确定是否勾选
                    GetOffLineAccess(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)

                });
            }
            return vm;

        }

        /// <summary>
        /// 添加 Identity Scope
        /// </summary>
        /// <param name="identityResource">身份认证资源</param>
        /// <param name="check">是否被选中</param>
        /// <returns></returns>
        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource, bool check)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Emphasize = identityResource.Emphasize,
                Required = identityResource.Required,
                Checked = check || identityResource.Required
            };
        }
        /// <summary>
        /// 添加 ApiResource scope
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        private ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Emphasize = scope.Emphasize,
                Required = scope.Required,
                Checked = check || scope.Required

            };
        }

        /// <summary>
        /// 添加允许离线访问
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        private ScopeViewModel GetOffLineAccess(bool check)
        {
            return new ScopeViewModel
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = "离线访问",
                Description = "允许用户离线访问",
                Emphasize = true,
                Checked = check
            };
        }
        #endregion



        /// <summary>
        /// 创建 授权页面model
        /// </summary>
        /// <param name="returnUrl">授权returnUrl</param>
        /// <param name="inputModel">用户授权选择Model</param>
        /// <returns></returns>
        public async Task<ConsentViewModel> BuildConsentViewModelAsync(string returnUrl, ConsentInputModel inputModel)
        {
            var vm = new ConsentViewModel();
            //根据Url获取授权的Request
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                //获取请求授权的 客户端信息
                var client = await _clientStore.FindClientByIdAsync(request.ClientId);
                if (client != null)
                {
                    //根据客户端请求scopes    获取所有可用资源
                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
                    if (resources != null && (resources.ApiResources.Any() || resources.IdentityResources.Any()))
                    {
                        //根据 客户端信息、所有可用资源 、用户输入信息，returnUrl，创建同意页面视图模型
                        return CreateConsentViewModel(client, resources, inputModel, returnUrl);
                    }
                    else
                    {
                        _logger.LogError("没有匹配的可用资源:{0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }

                }
                else
                {
                    _logger.LogError("无效的客户端:{0}", request.ClientId);
                }
            }
            else
            {
                _logger.LogError("没找到与授权匹配的请求：{0}", returnUrl);
            }

            return null;
        }



        /// <summary>
        /// 处理授权
        /// </summary>
        /// <param name="model">用户授权输入</param>
        /// <returns>ProcessConsentResult</returns>
        public async Task<ProcessConsentResult> ProcessConsentAsync(ConsentInputModel model)
        {
            var result = new ProcessConsentResult();
            //授权返回结果
            ConsentResponse grantedConsent = null;
            //拒绝授权
            if(model.Button =="no")
            {
                grantedConsent = ConsentResponse.Denied;
            }
            else if(model.Button =="yes"&& model !=null)
            {
                //如果授权内容不为空,添加授权结果
                if(model.ScopesConsented !=null && model.ScopesConsented.Any() )
                {
                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = model.ScopesConsented
                    };
                }
                else
                {   //未选择授权内容
                    result.ValidationError = "至少需要一个授权内容";
                }

            }
            else
            {
                result.ValidationError = "无效的选择";
            }

           
            if(grantedConsent !=null)
            {
                //验证授权请求是否后效
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);
                if (result == null) return result;

                //将授权结果返回给 IdentityServer
                await _identityServerInteractionService.GrantConsentAsync(request, grantedConsent);

                //返回授权后的 回调地址
                result.RedirectUri = model.ReturnUrl;           

               
            }
            else
            {
                //出现错误，将返回 授权页面
                result.ViewModel = await BuildConsentViewModelAsync(model.ReturnUrl, model);
            
            }
            return result;
        }
        
    }
}

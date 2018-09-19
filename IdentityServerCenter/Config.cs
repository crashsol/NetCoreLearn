using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource{
                    Name ="api",
                    DisplayName="values Api 资源",                  
                    Scopes ={
                        new   Scope{
                            Name ="api",
                            DisplayName="Values Api",
                            Description ="可以访问Values方法",
                            Emphasize= true,
                            UserClaims ={ "permission", "role" }     //与用户相关的ClaimsType,并会被附加到  该Api UserClaims 
                        }
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //ClientCredentials 设置客户端认证模式：客户端认证模式   返回 Access_token
                new Client{
                    ClientId="client",                                    //客户端ID
                    AllowedGrantTypes = GrantTypes.ClientCredentials,     //设置客户端认证模式：客户端认证模式
                    ClientSecrets = { new Secret("secret".Sha256()) },    //设置加密

                    AllowedScopes = {"api"}                               //定义可以访问的 Resource api，就是上面定义的resource name

                    
                },
                //ResourceOwnerPassword 添加一个 第三方消费客户，使用用户名，密码登录获取授权  返回 Access_token
                new Client{
                    ClientId="pwdClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,    //设置客户端认证模式：用户名密码模式

                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes ={ "api" },
                    RequireClientSecret = false                              //设置不需要 Https


                },

                //OpenIDConnet    implicit 模式 返回一个 Id Token 包含一组关于身份认证会话的声明，只返回认证信息
                // Authorization Code的简化版本，
                //其中省略掉了颁发授权码（Authorization Code）给客户端的过程，只做了认证，只返回了Identity Resource 并未返回 ApiResource
                // OpenID Connect implicit flow client (MVC)
                new Client
                {

                    ClientId="ImplicitMVC",
                    ClientName = "MVC Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,

                     // where to redirect to after login   用户登录成功跳转地址  localhost:5002 为客户端MVC地址
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // where to redirect to after logout  用户退出登录后跳转地址
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    //不启用 https连接
                    RequireClientSecret =false,
                    //授权页面
                    RequireConsent = true,
                    //允许访问的 资源
                    AllowedScopes = new List<string>
                    {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile,
                       "api"                                         //并不会返回给客户端
                    }

                },
                //HybridAndClientCredentials 混合模式 返回 id_token  code 包括认证和授权信息
                new Client{
                    ClientId="MVC",
                    ClientName = "MVC Client",
                    LogoUri ="https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1516257337186&di=db41e5611bcdb4eede8979af104b2eeb&imgtype=0&src=http%3A%2F%2Fimg4.99114.com%2Fgroup1%2FM00%2FA3%2F7F%2FwKgGMFVk__WAeRumAAAY72dGo-o180.png",
                    
                    //客户端地址
                    ClientUri = "http://localhost:5002",

                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,                    

                     // where to redirect to after login   用户登录成功跳转地址  localhost:5002 为客户端MVC地址
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // where to redirect to after logout  用户退出登录后跳转地址
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    //不启用 https连接
                    RequireClientSecret =false,
                    //回显授权页面
                    RequireConsent = true,
                                       //允许访问的 资源
                    AllowedScopes = new List<string>
                    {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile,
                       "api"
                    },
                    AllowOfflineAccess = true    // long lived API access 允许离线使用，返回 refresh token
                }
            };
        }
        /// <summary>
        /// 添加测试 用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                ///password 登录用户测试
                new TestUser
                {
                    SubjectId="pwdClient",
                    Password = "123",
                    Username ="123"
                },


                // MVC 登录测试用户
                new TestUser
                {
                    SubjectId="10000",
                    Password = "admin",
                    Username ="admin",                   
                    Claims = new Claim[]{
                        new Claim("permission","home.read"),                //无法返回
                        new Claim("permission","home.write"),                //无法返回
                        new Claim("permission","home.delete"),                //无法返回
                        new Claim("role","Admin"),                  //无法返回
                        new Claim("website", "https://bob.com")
                    }
                }


            };
        }

        /// <summary>
        /// 添加 Identity Resource
        /// </summary>
        /// <returns></returns>
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile()              
            };

        }
    }
}

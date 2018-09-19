using System.Threading.Tasks;
using AuthorizeExtendSample.AuthorizeExtend.PolicyServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace AuthorizeExtendSample.AuthorizeExtend.AspNetCore
{

    /// <summary>
    /// 继承AspNetCore 默认实现的 DefaultAuthorizePolicyProvider
    /// </summary>
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 重写获得 PolicyName的方法
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            //获得默认的 policy 
            var policy =await  base.GetPolicyAsync(policyName);
            if(policy ==null)
            {
                //创建一个自定义的 Policy
                policy = new AuthorizationPolicyBuilder()
                     .AddRequirements(new PermissionRequirement(policyName))
                     .Build();
            }
            return policy;
        }
    }

    /// <summary>
    /// 自定义 授权验证资源
    /// </summary>
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string Name { get; private set; }

        public PermissionRequirement(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// 自定义 授权验证实现
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPolicyServerClient _policyServerClient;

        public PermissionHandler(IPolicyServerClient policyServerClient)
        {
            _policyServerClient = policyServerClient;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
           if(await _policyServerClient.HasPermissionAsync(context.User,requirement.Name))
            {
                context.Succeed(requirement);
            }


        }
    }
}

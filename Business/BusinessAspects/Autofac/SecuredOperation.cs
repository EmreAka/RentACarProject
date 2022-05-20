using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Castle.DynamicProxy;
using Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Extensions;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        public SecuredOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_roles == null)
            {
                if (_httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
                {
                    return;
                }
                throw new AuthorizationDeniedException("Authorization Denied");
            }
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            //throw new Exception("Authorization Denied");
            throw new AuthorizationDeniedException("Authorization Denied");
        }
    }
}

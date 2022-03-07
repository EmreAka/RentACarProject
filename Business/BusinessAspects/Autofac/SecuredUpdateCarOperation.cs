using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Entity.Concrete;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredUpdateCarOperation : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredUpdateCarOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimsIdentities = _httpContextAccessor.HttpContext.User.Identities;
            var arguments = invocation.Arguments.Cast<Car>().ToList();
            foreach (var id in claimsIdentities)
            {
                foreach (var userClaim in id.Claims.ToList())
                {
                    if (arguments[0].UserId.ToString().Equals(userClaim.Value))
                    {
                        return;
                    }
                }
            }
            throw new AuthorizationDeniedException("Authorization Denied");
        }
    }
}
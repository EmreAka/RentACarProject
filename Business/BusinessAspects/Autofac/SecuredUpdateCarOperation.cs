using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
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
        private ICarService _carService;

        public SecuredUpdateCarOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _carService = ServiceTool.ServiceProvider.GetService<ICarService>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimsIdentities = _httpContextAccessor.HttpContext.User.Identities;
            var arguments = invocation.Arguments.Cast<Car>().ToList();
            //Why I did this is I don't trust the data We receive from client. 
            //They could send UserId in Car object different for example.
            var car = _carService.GetById(arguments[0].Id);
            foreach (var claimIdentity in claimsIdentities)
            {
                foreach (var claim in claimIdentity.Claims.ToList())
                {
                    if (car.Data.UserId.ToString().Equals(claim.Value))
                    {
                        return;
                    }
                }
            }
            throw new AuthorizationDeniedException("Authorization Denied");
        }
    }
}
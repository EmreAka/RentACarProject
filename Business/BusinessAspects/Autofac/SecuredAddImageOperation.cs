using System.Linq;
using Business.Abstract;
using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredAddImageOperation : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICarService _carService;
        
        public SecuredAddImageOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _carService = ServiceTool.ServiceProvider.GetService<ICarService>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimsIdentities = _httpContextAccessor.HttpContext.User.Identities;
            var argument = invocation.Arguments.
                Where(a => a.GetType() == typeof(CarImage)).Cast<CarImage>().ToList()[0];
            var car = _carService.GetById(argument.CarId);

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
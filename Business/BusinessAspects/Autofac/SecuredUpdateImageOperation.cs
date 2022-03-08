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
    public class SecuredUpdateImageOperation : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICarImageService _carImageService;
        private ICarService _carService;
        
        public SecuredUpdateImageOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _carImageService = ServiceTool.ServiceProvider.GetService<ICarImageService>();
            _carService = ServiceTool.ServiceProvider.GetService<ICarService>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimsIdentities = _httpContextAccessor.HttpContext.User.Identities;
            var arguments = invocation.Arguments.Cast<CarImage>().ToList();
            //Why I did this is I don't trust the data We receive from client. 
            //They could send CarImage with only Its id included or with wrong carId...
            //for example, a car id that belongs to that user.
            var carImage = _carImageService.GetById(arguments[0].Id);
            var car = _carService.GetById(carImage.Data.CarId);
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
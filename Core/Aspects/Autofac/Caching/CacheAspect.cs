using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private readonly int _duration;
        private readonly string _type;
        private readonly ICacheManager _cacheManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<HttpContextAccessor>();
        }

        public CacheAspect(string type, int duration = 60)
        {
            _duration = duration;
            _type = type;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            if (_type != null)
            {
                arguments.Add(_httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value);
            }
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}

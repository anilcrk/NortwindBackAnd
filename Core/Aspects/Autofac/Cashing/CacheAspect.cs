using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Cashing;
using Core.Utilities.Interceptors;
using Core.Utilities.Interceptors.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Cashing
{
  public  class CacheAspect:MethodInterception // aspect olduğu için.
    {
        private int _duration;
        private ICasheManager _cacheManager;
        public CacheAspect(int duration=60) //değer girmezse 60 dk default
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICasheManager>();  
        }
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //key değeri --> Method Adı
            var argument = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",argument.Select(x=>x?.ToString()??"<Null>"))})"; //cashe için dinamik key oluşturduk.
            if(_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}

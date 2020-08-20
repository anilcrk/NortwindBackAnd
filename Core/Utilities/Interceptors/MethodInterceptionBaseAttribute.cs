
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)] // bu attribute classın en tepesinde kullanılabillir.
    public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor // Autofac tarafından geliyor
    {
        public int Priority { get; set; }

        internal object ToList()
        {
            throw new NotImplementedException();
        }

        public virtual void Intercept(IInvocation invocation) //sonradan değiştirilme imkanı sağlamak için virtual keyboardı eklendi.
        {
           
        }
    }
}

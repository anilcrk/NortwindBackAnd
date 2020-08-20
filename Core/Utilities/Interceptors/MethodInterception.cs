
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //methodu nasıl yorunmlayacağını anlattığımız yer.
   public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)//method çalışmadan önce  invocation = çalışacak methodtur.
        {
        }
        protected virtual void OnAfter(IInvocation invocation)//method çalıştıktan sonra  invocation = çalışacak methodtur.
        {
        }
        protected virtual void OnException(IInvocation invocation)//method hata verdiğinde  invocation = çalışacak methodtur.
        {
        }
        protected virtual void OnSuccess(IInvocation invocation)//method başarılıysa  invocation = çalışacak methodtur.
        {
        }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation); // onbefore da bişey varsa invocationu çalıştır.
            try
            {
                invocation.Proceed(); //operasyonu çalıştır.
            }
            catch
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if(isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation); //her halükarda çalıştır.
        }
    }
}

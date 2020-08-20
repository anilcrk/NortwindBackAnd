using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
  public  class TransactionScopeAspepct:MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope=new TransactionScope())//disponsible pattern uygulandığı için using kullanıldı
            {
                try
                {
                    invocation.Proceed();               //Methodu çalıştırmaya çalış eğer başarılıysa devam et
                    transactionScope.Complete();
                }
                catch //Başarılı olmadıysa yapılan işllemleri geri al
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}

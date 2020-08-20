using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception //yazdığımız methodinterceptionu inheritance ettik
    {
        Type _validatorType;
        public ValidationAspect(Type validatorType) //gelen tipe göre
        {

            if(!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatortype IValidor türünde değilse hata fırlat
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
            _validatorType = validatorType; //hata yoksa dependeny injection yapıyoruz.
        }
        protected override void OnBefore(IInvocation invocation) // inheritance ettiğimiz classtaki virtual methodları override edip içini dolduruyoruz.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //reflection yöntemiyle instance üretiyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];   //gönderile n generic argumanı alıyor. ör: product
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //Git metodun argumanlarına bak validate et
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
                
        }
    }
}

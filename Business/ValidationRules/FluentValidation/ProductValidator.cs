using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            /*
              ---- GENEL NOT ----
              Aşağıdaki magic string yazılan hata mesajları yerine bir hata sınıfı üretip mesajları oradan çekebiliriz.
             
             */
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz !"); // ürünün adı boş olamaz kuralı ve mesajı. eğer withMessage eklemezsek default mesaj dönderir.
            RuleFor(p => p.ProductName).Length(2, 30).WithMessage("Ürünün adı en az 2 en fazla 30 karakter olmalıdır."); //ürünün adı en az 2 en fazla 30 karakter olmalı.
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Ürün Fiyatı boş olamaz !");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1); // ürünlerin ubitprice  den büyük veya eşit olamalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1); // category id si 1 olan ürünün minimum fiyatı 10 dur.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Productname 'A' ile başlamalıdır !"); // custom kural
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Lütfen Kategori Belirleyin !");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A"); // ürün adı A ile başlamalı
        }
    }
}

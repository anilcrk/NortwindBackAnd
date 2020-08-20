using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Cashing
{
    public interface ICasheManager
    {
        T Get<T>(string key);  //cashe getir


        object Get(string key);  //cashe getir.

        void Add(string key, object data, int duration); // cashe ekleme

        bool IsAdd(string key);
        
        void Remove(string key);  //Cashe kaldırma

        void RemoveByPattern(string pattern);  //belirli kurallara uyan casheleri siler ör: Get ile başlayanlar.
    }
}

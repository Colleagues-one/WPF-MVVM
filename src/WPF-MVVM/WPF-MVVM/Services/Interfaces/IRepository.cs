using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models.Interfaces;

namespace WPF_MVVM.Services.Interfaces
{
    internal interface IRepository<T> where T : IEntity
    {
        public T GetById(int id) => GetAll().FirstOrDefault(x => x.Id == id);
        public IEnumerable<T> GetAll();
        void Add(T item);
        bool Remove(T item);
        void Update(int id,T item);
    }
}

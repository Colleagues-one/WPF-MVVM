using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models.Interfaces;
using WPF_MVVM.Services.Interfaces;

namespace WPF_MVVM.Services.BaseServices
{
    internal abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
    {
        private List<T> _items = new List<T>();

        private int _lastId = 0;

        protected RepositoryInMemory(){}

        protected RepositoryInMemory(IEnumerable<T> items)
        {
            foreach (var item in _items) _items.Add(item);
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (_items.Contains(item)) return;
            item.Id = ++_lastId;
            _items.Add(item);
        }

        public IEnumerable<T> GetAll() => _items;

        public bool Remove(T item) => _items.Remove(item);

        public void Update(int id, T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if(id<=0) throw new ArgumentOutOfRangeException(nameof(id), id, "Индекс не может быть меньше 1");
            if (_items.Contains(item)) return;

            var dbItem = ((IRepository<T>)this).GetById(id);
            if (dbItem is null) throw new InvalidOperationException("Редактируемый элемент не найден в репозитории");
            Update(item, dbItem);
        }

        protected abstract void Update(T source, T destination);
    }
}

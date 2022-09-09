using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OrderRegistrar.Domain.Concepts;

namespace OrderRegistrar.Infrastructure.Repository
{
    public class InMemRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly List<T> _items = new List<T>();

        public IQueryable<T> ItemsWhere(Expression<Func<T, bool>> predicate)
        {
            return _items
                .AsQueryable()
                .Where(predicate);
        }

        public T ItemWhere(Expression<Func<T, bool>> predicate)
        {
            return _items
                .AsQueryable()
                .SingleOrDefault(predicate);
        }

        public void Create(T item)
        {
            // Generate Id
            item.Id = _items.Any() 
                ? _items.Max(p => p.Id) + 1 
                : 1;
            _items.Add(item);
        }

        public void Update(T item)
        {
            // If same instance then update is already 'done', remember we're faking a persistent repository here.
            if (_items.Contains(item))
                return;

            // Otherwise 'Overwrite' 
            var existingItem = ItemWhere(p => p.Id == item.Id);

            if (existingItem != null)
                Delete(existingItem);

            _items.Add(item);
        }

        public void Delete(T item)
        {
            _items.Remove(_items.Single(p => p.Id == item.Id));
        }
    }
}
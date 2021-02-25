using System;
using System.Linq;
using System.Linq.Expressions;

namespace OrderRegistrar.Domain.Concepts
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        IQueryable<T> ItemsWhere(Expression<Func<T, bool>> predicate);

        T ItemWhere(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}

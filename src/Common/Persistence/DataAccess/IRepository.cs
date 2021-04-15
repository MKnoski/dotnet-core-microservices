using System;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<T> All<U>() where U : BaseEntity;

        T Find<U>(Expression<Func<U, bool>> predicate) where U : BaseEntity;

        bool Contains<U>(Expression<Func<U, bool>> predicate) where U : BaseEntity;
    }
}
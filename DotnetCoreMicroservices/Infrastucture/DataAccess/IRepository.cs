using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastucture.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<T> All<T>() where T : BaseEntity;

        T Find<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;
    }
}
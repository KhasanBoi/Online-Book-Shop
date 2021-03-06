using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.IRepository
{
    public interface IRepository<T> where T : class // T can be any class in this way
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}

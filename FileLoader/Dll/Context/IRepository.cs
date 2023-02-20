using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Context
{
    public interface IRepository<T> where T : class
    {
        void Create(T obj);
        void Delete(int id);
        void Update(int id,T obj);
        T GetValue(int id);
        IEnumerable<T> GetFromCondition(Expression<Func<T, bool>> condition);

    }
}

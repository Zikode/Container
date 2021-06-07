using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public interface IGenericRepository<T> where T : class
    {
        int Add(T entity);
        IQueryable<T> GetAll();
        T Get(int Id);
        int Update(T entity);
        int Delete(int Id);
    }
}

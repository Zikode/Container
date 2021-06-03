using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
   public interface IDapperRepository
    {
        IQueryable<T> Query<T>(string sqlCommand);
        IQueryable<T> QueryWithParameter<T>(string sqlCommand, object parameters);
        int ExecuteWithParameters(string sqlCommand, object parameters);
        int Excute(string sqlCommand);
    }
}

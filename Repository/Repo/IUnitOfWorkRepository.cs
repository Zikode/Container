using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        IContainerRepository _container { get; }
        int Complete();
    }
}

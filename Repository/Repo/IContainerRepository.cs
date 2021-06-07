using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public interface IContainerRepository : IGenericRepository<Containerobj>
    {
        Containerobj GetContainerByContainerNumber(int containerNumber);
    }
}

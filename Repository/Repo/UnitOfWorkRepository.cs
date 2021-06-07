using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
  public  class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly AppDbContext _context;

        public IContainerRepository _container { get; }

        public UnitOfWorkRepository(AppDbContext context, IContainerRepository containerRepository)
        {
            this._context = context;
            _container = containerRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

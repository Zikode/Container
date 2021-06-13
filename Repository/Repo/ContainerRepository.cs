using DAL.Context;
using DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
  public  class ContainerRepository : IContainerRepository
    {
        private readonly AppDbContext _context;
        private readonly IDapperRepository _dapperRepository;

        public ContainerRepository(AppDbContext context, IDapperRepository dapperRepository)
        {
            _context = context;
            _dapperRepository = dapperRepository;
        }
        public int Add(Containerobj entity)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerNumber", entity.ContainerNumber, DbType.Int32);
            dbparams.Add("Code", entity.Code, DbType.String);
            dbparams.Add("Color", entity.Color, DbType.String);
            dbparams.Add("ContainerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dapperRepository.ExecuteWithParameters("[dbo].[CreateContainer]", dbparams);
            return dbparams.Get<int>("ContainerId");
        }
        public int Delete(int Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerId", Id, DbType.Int32);
            dbparams.Add("RowCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dapperRepository.ExecuteWithParameters("[dbo].[DeleteContainerByContainerId]", dbparams);
            return dbparams.Get<int>("RowCount");
        }
        public Containerobj Get(int Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerId", Id, DbType.Int32);
            return  _dapperRepository.QueryWithParameter<Containerobj>("[dbo].[GetContainerById]", dbparams);
        }
        public IQueryable<Containerobj> GetAll()
        {
            return  _dapperRepository.Query<Containerobj>("GetListOfContainers");
        }

        public int Update(Containerobj entity)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerId", entity.ContainerID, DbType.Int32);
            dbparams.Add("Code", entity.Code, DbType.String);
            dbparams.Add("Color", entity.Color, DbType.String);
            dbparams.Add("RowCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dapperRepository.ExecuteWithParameters("[dbo].[UpdateContainerByContainerId]", dbparams);
            return dbparams.Get<int>("RowCount");
        }
        public Containerobj GetContainerByContainerNumber(int containerNumber)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerNumber", containerNumber, DbType.Int32);
            return _dapperRepository.QueryWithParameter<Containerobj>("[dbo].[GetContainerByContainerNumber]", dbparams);
        }

    }
}

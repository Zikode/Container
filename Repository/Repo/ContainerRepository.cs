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
            var response =  _dapperRepository.ExecuteWithParameters("[dbo].[CreateContainer]", dbparams);
            return response;
        }
        public int Delete(int Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerId", Id, DbType.Int32);
            var response =  _dapperRepository.ExecuteWithParameters("[dbo].[DeleteContainerByContainerNumber]", dbparams);
            return response;
        }
        public IQueryable<Containerobj> Get(int Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerId", Id, DbType.Int32);
            return  _dapperRepository.QueryWithParameter<Containerobj>("[dbo].[GetContainerById]", dbparams);
        }
        public IQueryable<Containerobj> GetAll()
        {
            return  _dapperRepository.Query<Containerobj>("[dbo].[GetListOfContainers]");
        }

        public int Update(Containerobj entity)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerNumber", entity.ContainerNumber, DbType.Int32);
            dbparams.Add("Code", entity.Code, DbType.String);
            dbparams.Add("Color", entity.Color, DbType.String);
            return  _dapperRepository.ExecuteWithParameters("[dbo].[UpdateContainerByContainerNumber]", dbparams);
        }
        public IQueryable<Containerobj> GetContainerByContainerNumber(int containerNumber)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ContainerNumber", containerNumber, DbType.Int32);
            return  _dapperRepository.QueryWithParameter<Containerobj>("[dbo].[GetContainerByContainerNumber]", dbparams);
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;


namespace Repository.Repo
{
   public class DapperRepository: IDapperRepository
    {
        private readonly IConfiguration _configuration;

        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public  int ExecuteWithParameters(string sqlCommand, object parameters)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("ContainerDatabase"));
            using (connection)
                connection.Open();
            var result =  connection.Execute(sqlCommand, parameters,commandType: CommandType.StoredProcedure);
            return result;
        }

        public IQueryable<T> QueryWithParameter<T>(string sqlCommand, object parameters)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("ContainerDatabase"));
            using (connection)
                connection.Open();
            var results = connection.QueryAsync<T>(sqlCommand, parameters, commandType: CommandType.StoredProcedure).Result.AsQueryable();
            return results;
        }

        public IQueryable<T> Query<T>(string sqlCommand)
        {
            //var connection = new SqlConnection(_configuration.GetConnectionString("ContainerDatabase"));
            //using (connection)
            //    connection.Open();
            //var results = await connection.QueryAsync<T>(sqlCommand).
            //return results;
            throw new NotImplementedException();
        }
        public int Excute(string sqlCommand)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("ContainerDatabase"));
            using (connection)
                connection.Open();
            var result =  connection.Execute(sqlCommand);
            return result;
        }
    }
}

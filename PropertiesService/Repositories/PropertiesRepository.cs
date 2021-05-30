using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using PropertyService.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace PropertyService.Services
{
    public class PropertiesRepository : IPropertiesRepository
    {
        private const int TimeOut = 1600;
        private readonly ICommandHelper _sqlHelper;
        private readonly ILogger<PropertiesRepository> _logger;

        public PropertiesRepository(ILogger<PropertiesRepository> logger, ICommandHelper sqlHelper)
        {
            _logger = logger;
            _sqlHelper = sqlHelper;
        }

        public IDataReader ExecuteQuery(string sql)
        {
            using (IDbCommand comm = GetCommand(sql))
            {
                comm.CommandTimeout = TimeOut;
                return _sqlHelper.ExecuteQuery(comm);
            }
        }

        public IDataReader ExecuteSpAddProduct(string sql, PropertyDto property)
        {
            using (IDbCommand comm = GetCommand(sql))
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@address", property.address.country));
                comm.Parameters.Add(new SqlParameter("@yearBuilt", property.physical.yearBuilt));
                comm.Parameters.Add(new SqlParameter("@listPrice", property.financial.listPrice));
                comm.Parameters.Add(new SqlParameter("@monthlyRent", property.financial.monthlyRent));
                comm.Parameters.Add(new SqlParameter("@grossYield", string.Empty));                
                return _sqlHelper.ExecuteQuery(comm);
            }
        }


        public IDataReader ExecuteSpGetProduct(string sql)
        {
            using (IDbCommand comm = GetCommand(sql))
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                return _sqlHelper.ExecuteQuery(comm);
            }
        }


        public async Task<Result<Property, ErrorResult>> AddProperty(PropertyDto property)
        {
            string sql = string.Empty;
            try
            {
                // select top 100 will be removed, this is for short term test only.
                sql = "[dbo].[AddProperty]";          
                var reader = ExecuteSpAddProduct(sql, property);          

                await Task.Yield();
                return Result.Ok<Property, ErrorResult>(new Property());
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Error occured on GetLineItemValues with idn: {templateName}.TimeOut in Seconds: {TimeOut}. Query was: {sql}, \n Error: {Message}",
                    property,
                    TimeOut,
                    sql,
                    e.Message);
                return ResultGenerator.RepositoryError<Property>();
            }
        }

   

        public async Task<Result<Properties, ErrorResult>> propertiesRepository()
        {
            string sql = string.Empty;
            try
            {
                // select procuct name, this is for short term test only.
                sql = "GetProperty";

                var properties = new Properties();
                var propertyList = new List<Property>();

                var reader = ExecuteSpGetProduct(sql);

                while (reader.Read())
                {
                    // IF this product IDN already exists.                        
                    // Add new product.

                        Property property = new Property() 
                        {
                            address = new Address(),
                            physical = new Physical(),
                            financial = new Financial()
                        };

                        property.address.address1 = GetPropValue(reader, "address");
                        property.physical.yearBuilt = Convert.ToInt32(GetPropValue(reader, "yearBuilt"));
                        property.financial.listPrice = Convert.ToDouble(GetPropValue(reader, "listPrice"));
                        property.financial.monthlyRent = Convert.ToDouble(GetPropValue(reader, "monthlyRent"));
                        propertyList.Add(property);
                }

                properties.properties = propertyList;
                await Task.Yield();
                return Result.Ok<Properties, ErrorResult>(properties);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Error occured on GetProduct.TimeOut in Seconds: {TimeOut}. Query was: {sql}, \n Error: {Message}", TimeOut, sql, e.Message);
                return ResultGenerator.RepositoryError<Properties>();
            }
        }

        internal virtual string GetPropValue(IDataReader obj, string propName)
        {
            string[] nameParts = propName.Split('.');
            if (nameParts.Length == 1)
            {
                return obj[propName].ToString();
            }

            if (obj == null)
            {
                return null;
            }

            return obj[nameParts[nameParts.Length - 1]].ToString();
        }

        private IDbCommand GetCommand(string sql)
        {
            var comm = new SqlCommand { CommandText = sql, CommandType = CommandType.Text };
            return comm;
        }
    }
}

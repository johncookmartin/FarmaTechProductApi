using System.Data;

namespace FarmaTechData.Data;
public interface ISqlDataAccess
{
    Task ExecuteDataAsync<T>(string queryString, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringName = "Default");
    Task<IEnumerable<T>> QueryDataAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringName = "Default");
}
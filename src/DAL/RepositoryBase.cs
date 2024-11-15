using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL;

public abstract class RepositoryBase
{
    public static IDbConnection OpenConnection()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        var connection = new MySqlConnection("Server=maria-db;Database=dora_meter;User=dbadmin;Password=TogetherCenterExceptThusFew");
        connection.Open();
        return connection;
    }
}
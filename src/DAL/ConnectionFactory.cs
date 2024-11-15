using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL;

internal static class ConnectionFactory
{
    internal static IDbConnection CreateConnection()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        var connection = new MySqlConnection("Server=maria-db;Database=dora_meter;User=dbadmin;Password=TogetherCenterExceptThusFew");
        connection.Open();
        return connection;
    }
}
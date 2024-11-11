using System.Data.SqlClient;

namespace BLL;

public abstract class GitHubPayloadHandler
{
    protected SqlConnection Connection;

    public GitHubPayloadHandler(SqlConnection connection)
    {
        Connection = connection;
    }
    
    public abstract void Handle(Dictionary<string, object> payload);
}
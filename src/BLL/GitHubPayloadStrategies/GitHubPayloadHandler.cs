using System.Data;
using System.Data.SqlClient;

namespace BLL;

public abstract class GitHubPayloadHandler
{
    protected IDbConnection Connection;

    public GitHubPayloadHandler(IDbConnection connection)
    {
        Connection = connection;
    }
    
    public abstract void Handle(dynamic payload);
}
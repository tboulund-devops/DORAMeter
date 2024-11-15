using System.Data;
using System.Data.SqlClient;

namespace BLL;

public abstract class GitHubPayloadHandler
{
    public abstract void Handle(dynamic payload);
}
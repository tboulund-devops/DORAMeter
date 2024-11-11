using System.Data.SqlClient;
using Dapper;

namespace BLL.GitHubPayloadStrategies;

public class RegisterRepositoryHandler : GitHubPayloadHandler
{
    public RegisterRepositoryHandler(SqlConnection connection) : base(connection)
    {
    }

    public override void Handle(Dictionary<string, object> payload)
    {
        Dictionary<string, object>? repository = payload["repository"] as Dictionary<string, object>;
        if (repository != null)
        {
            var id = (int)repository["id"];
            var name = repository["full_name"];
            var exists = Connection.ExecuteScalar<bool>("SELECT 1 FROM repositories WHERE id = @Id", new { Id = id });
            if (!exists)
            {
                Connection.Execute("INSERT INTO repositories (id, name) VALUES (@Id, @Name)", new { Id = id, Name = name });
            }
            else
            {
                Console.WriteLine("Repository already exists");
            }
        }
        else
        {
            Console.WriteLine("Repository not understood as a dictionary");
        }
    }
}
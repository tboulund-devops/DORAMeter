using System.Data.SqlClient;
using Dapper;

namespace BLL.GitHubPayloadStrategies;

public class RegisterRepositoryHandler : GitHubPayloadHandler
{
    public RegisterRepositoryHandler(SqlConnection connection) : base(connection)
    {
    }

    public override void Handle(dynamic payload)
    {
        int id = payload.repository.id;
        string name = payload.repository.name;
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
}
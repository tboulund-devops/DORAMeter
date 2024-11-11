using System.Data.SqlClient;
using Dapper;

namespace BLL.GitHubPayloadStrategies;

public class BranchCreatedHandler : GitHubPayloadHandler
{
    public override void Handle(Dictionary<string, object> payload)
    {
        var branchName = payload["ref"].ToString().Split("refs/heads/")[1];
        var firstCommit = ((Dictionary<string, object>)payload["commits"]).First();
        var commitTimestamp = DateTime.Parse(firstCommit.Value?.ToString() ?? DateTime.Now.ToString());

        var exists = Connection.ExecuteScalar<bool>("SELECT 1 FROM branches WHERE name = @Name", new { Name = branchName });
        if (!exists)
        {
            Connection.Execute(
                "INSERT INTO branches (name, first_commit) VALUES (@Name, @CreatedAt)",
                new { Name = branchName, CreatedAt = commitTimestamp }
            );
        }
    }

    public BranchCreatedHandler(SqlConnection connection) : base(connection)
    {
    }
}
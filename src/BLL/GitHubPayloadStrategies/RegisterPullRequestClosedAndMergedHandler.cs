using System.Data;
using BLL;
using Dapper;

namespace DefaultNamespace;

public class RegisterPullRequestClosedAndMergedHandler : GitHubPayloadHandler
{
    public RegisterPullRequestClosedAndMergedHandler(IDbConnection connection) : base(connection)
    {
    }

    public override void Handle(dynamic payload)
    {
        string branchName = payload.pull_request.head.@ref;
        int repositoryId = payload.repository.id;
        DateTime closedDate = DateTime.Parse(payload.pull_request.closed_at.ToString());
        
        Connection.Execute("UPDATE branches SET closed_date = @ClosedDate WHERE name = @BranchName AND repository_id = @RepositoryId", new { ClosedDate = closedDate, BranchName = branchName, RepositoryId = repositoryId });
    }
}
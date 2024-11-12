using System.Data;
using Dapper;

namespace BLL.GitHubPayloadStrategies;

public class RegisterBranchHandler : GitHubPayloadHandler
{
    public enum BranchTypes
    {
        Unknown = 1,
        Feature = 2,
        Hotfix = 3
    }
    
    public RegisterBranchHandler(IDbConnection connection) : base(connection)
    {
    }

    public override void Handle(dynamic payload)
    {
        var branchName = ((string)payload.@ref).Substring(11);
        var branchType = BranchTypes.Unknown;
        var repositoryId = payload.repository.id;
        if (branchName.ToLower().StartsWith("feature"))
        {
            branchType = BranchTypes.Feature;
        }
        else if (branchName.ToLower().StartsWith("hotfix"))
        {
            branchType = BranchTypes.Hotfix;
        }

        var firstCommit = DateTime.Parse(payload.commits[0].timestamp.ToString());

        var exists = Connection.ExecuteScalar<bool>("SELECT 1 FROM branches WHERE name = @Name AND repository_id = @RepositoryId", new { Name = branchName, RepositoryId = repositoryId });
        if(!exists) {
            Connection.Execute("INSERT INTO branches (name, branch_type_id, repository_id, first_commit) VALUES (@Name, @Type, @RepositoryId, @FirstCommit)", new { Name = branchName, Type = branchType, RepositoryId = repositoryId, FirstCommit = firstCommit });
            if (branchType == BranchTypes.Hotfix)
            {
                // Update latest deployed feature branch and mark it as failure
                Connection.Execute("UPDATE branches SET is_failure = 1 WHERE repository_id = @RepositoryId AND branch_type_id = @BranchType AND deployed_date IS NOT NULL ORDER BY deployed_date DESC LIMIT 1", new { RepositoryId = repositoryId, BranchType = (int)BranchTypes.Feature });
            }
        }
    }
}
using Dapper;
using Models;

namespace DAL;

public class BranchRepository : RepositoryBase
{
    public bool DoesBranchExists(string branchName, int repositoryId)
    {
        using var connection = OpenConnection();
        return connection.ExecuteScalar<bool>("SELECT 1 FROM branches WHERE name = @Name AND repository_id = @RepositoryId", new { Name = branchName, RepositoryId = repositoryId });
    }
    
    public void MarkPreviousFeatureBranchAsFailure(int repositoryId)
    {
        using var connection = OpenConnection();
        connection.Execute("UPDATE branches SET is_failure = 1 WHERE repository_id = @RepositoryId AND branch_type_id = @BranchType AND deployed_date IS NOT NULL ORDER BY deployed_date DESC LIMIT 1", new { RepositoryId = repositoryId, BranchType = BranchType.Feature });
    }

    public void MarkBranchAsClosed(string branchName, int repositoryId, DateTime closedDate)
    {
        using var connection = OpenConnection();
        connection.Execute("UPDATE branches SET closed_date = @ClosedDate WHERE name = @BranchName AND repository_id = @RepositoryId", new { ClosedDate = closedDate, BranchName = branchName, RepositoryId = repositoryId });
    }

    public void MarkClosedBranchesAsDeployed(int repositoryId, DateTime deployedDate)
    {
        using var connection = OpenConnection();
        connection.Execute("UPDATE branches SET deployed_date = @DeployedDate WHERE closed_date IS NOT NULL AND repository_id = @RepositoryId", new { DeployedDate = deployedDate, RepositoryId = repositoryId });
    }
    
    public Branch SaveBranch(string branchName, BranchType branchType, int repositoryId, DateTime firstCommit)
    {
        using var connection = OpenConnection();
        var branch = new Branch(branchName, repositoryId, branchType, firstCommit);
        branch.Id = connection.ExecuteScalar<int>("INSERT INTO branches (name, branch_type_id, repository_id, first_commit) VALUES (@Name, @Type, @RepositoryId, @FirstCommit); SELECT LAST_INSERT_ID();", new { Name = branch.Name, Type = branch.Type, RepositoryId = branch.RepositoryId, FirstCommit = branch.FirstCommit });
        return branch;
    }
}
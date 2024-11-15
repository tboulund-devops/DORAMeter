namespace Models;

public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RepositoryId { get; set; }
    public BranchType Type { get; set; }
    public DateTime FirstCommit { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? DeployedDate { get; set; }
    public bool IsFailure { get; set; }

    public Branch(string name, int repositoryId, BranchType branchTypeId, DateTime firstCommit)
    {
        Name = name;
        RepositoryId = repositoryId;
        Type = branchTypeId;
        FirstCommit = firstCommit;
    }

}
using DAL;

namespace BLL.GitHubPayloadStrategies;

public class RegisterPullRequestClosedAndMergedHandler : GitHubPayloadHandler
{
    private readonly BranchRepository _branchRepository = new BranchRepository();
    
    public override void Handle(dynamic payload)
    {
        string branchName = payload.pull_request.head.@ref;
        int repositoryId = payload.repository.id;
        DateTime closedDate = DateTime.Parse(payload.pull_request.closed_at.ToString());
        
        _branchRepository.MarkBranchAsClosed(branchName, repositoryId, closedDate);
    }
}
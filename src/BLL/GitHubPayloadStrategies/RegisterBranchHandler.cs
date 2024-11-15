using DAL;
using Models;

namespace BLL.GitHubPayloadStrategies;

public class RegisterBranchHandler : GitHubPayloadHandler
{
    private readonly BranchRepository _repository = new();
    
    public override void Handle(dynamic payload)
    {
        var branchName = ((string)payload.@ref).Substring(11);
        var branchType = BranchType.Unknown;
        int repositoryId = payload.repository.id;
        if (branchName.ToLower().StartsWith("feature"))
        {
            branchType = BranchType.Feature;
        }
        else if (branchName.ToLower().StartsWith("hotfix"))
        {
            branchType = BranchType.Hotfix;
        }

        DateTime firstCommit = DateTime.Parse(payload.commits[0].timestamp.ToString());
        
        if(!_repository.DoesBranchExists(branchName, repositoryId))
        {
            _repository.SaveBranch(branchName, branchType, repositoryId, firstCommit);
            if (branchType == BranchType.Hotfix)
            {
                _repository.MarkPreviousFeatureBranchAsFailure(repositoryId);
            }
        }
    }
}
using DAL;

namespace BLL.GitHubPayloadStrategies;

public class RegisterSuccessfulDeploymentHandler : GitHubPayloadHandler
{
    private readonly BranchRepository _branchRepository = new BranchRepository();
    private readonly DeploymentRepository _deploymentRepository = new DeploymentRepository();
    
    public override void Handle(dynamic payload)
    {
        int repositoryId = payload.repository.id;
        DateTime startDate = DateTime.Parse(payload.workflow_run.created_at.ToString());
        DateTime endDate = DateTime.Parse(payload.workflow_run.updated_at.ToString());

        _deploymentRepository.RegisterDeployment(startDate, endDate);
        _branchRepository.MarkClosedBranchesAsDeployed(repositoryId, endDate);
    }
}
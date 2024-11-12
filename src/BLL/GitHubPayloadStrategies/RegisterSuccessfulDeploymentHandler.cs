using System.Data;
using BLL;
using Dapper;

namespace DefaultNamespace;

public class RegisterSuccessfulDeploymentHandler : GitHubPayloadHandler
{
    public RegisterSuccessfulDeploymentHandler(IDbConnection connection) : base(connection)
    {
    }

    public override void Handle(dynamic payload)
    {
        int repositoryId = payload.repository.id;
        DateTime startDate = DateTime.Parse(payload.workflow_run.created_at.ToString());
        DateTime endDate = DateTime.Parse(payload.workflow_run.updated_at.ToString());
        
        Connection.Execute("INSERT INTO deployments (start_date, end_date) VALUES (@StartDate, @EndDate)", new { StartDate = startDate, EndDate = endDate });
        Connection.Execute("UPDATE branches SET deployed_date = @EndDate WHERE closed_date IS NOT NULL AND repository_id = @RepositoryId", new { EndDate = endDate, RepositoryId = repositoryId });
    }
}
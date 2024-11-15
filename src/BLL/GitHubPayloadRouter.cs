using System.Data;
using System.Transactions;
using BLL.GitHubPayloadStrategies;
using MySql.Data.MySqlClient;

namespace DefaultNamespace;

public class GitHubPayloadRouter
{
    private static GitHubPayloadRouter? _instance;

    private GitHubPayloadRouter()
    { }
    
    public static GitHubPayloadRouter Instance => _instance ??= new GitHubPayloadRouter();

    public void Process(params dynamic[] payloads)
    {
        using var transaction = new TransactionScope();

        foreach(var payload in payloads)
        {
            new RegisterRepositoryHandler().Handle(payload);
            if (payload.@ref != null)
            {
                new RegisterBranchHandler().Handle(payload);
            }

            if (payload.pull_request != null && payload.pull_request.merged == true && payload.action == "closed")
            {
                new RegisterPullRequestClosedAndMergedHandler().Handle(payload);
            }

            if (payload.deployment_status?.state == "success")
            {
                new RegisterSuccessfulDeploymentHandler().Handle(payload);
            }

            transaction.Complete();
        }
    }
}
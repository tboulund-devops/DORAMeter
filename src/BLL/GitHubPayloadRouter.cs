using System.Data;
using BLL.GitHubPayloadStrategies;
using MySql.Data.MySqlClient;

namespace DefaultNamespace;

public class GitHubPayloadRouter
{
    private static IDbConnection _connection = new MySqlConnection("Server=localhost;Database=dora_meter;User=dbadmin;Password=TogetherCenterExceptThusFew");
    private static GitHubPayloadRouter? _instance;
    
    private GitHubPayloadRouter()
    { }
    
    public static GitHubPayloadRouter Instance => _instance ??= new GitHubPayloadRouter();

    public void Process(dynamic payload)
    {
        _connection.Open();
        using var transaction = _connection.BeginTransaction();
        
        try
        {
            new RegisterRepositoryHandler(_connection).Handle(payload);
            if(payload.@ref != null)
            {
                new RegisterBranchHandler(_connection).Handle(payload);
            }

            if (payload.pull_request != null && payload.pull_request.merged == true && payload.action == "closed")
            {
                new RegisterPullRequestClosedAndMergedHandler(_connection).Handle(payload);
            }
            
            if(payload.deployment_status?.state == "success")
            {
                new RegisterSuccessfulDeploymentHandler(_connection).Handle(payload);
            }
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
        }
    }
}
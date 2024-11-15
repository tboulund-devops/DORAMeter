using Dapper;
using Models;

namespace DAL;

public class DeploymentRepository : RepositoryBase
{
    public Deployment RegisterDeployment(DateTime startDate, DateTime endDate)
    {
        using var connection = OpenConnection();
        var deployment = new Deployment(startDate, endDate)
        {
            Id = connection.ExecuteScalar<int>("INSERT INTO deployments (start_date, end_date) VALUES (@StartDate, @EndDate); SELECT LAST_INSERT_ID()", new { StartDate = startDate, EndDate = endDate })
        };
        return deployment;
    }
    
    public ICollection<Deployment> GetAll()
    {
        using var connection = OpenConnection();
        return connection.Query<Deployment>("SELECT * FROM deployments").ToList();
    }
}
using System.Data;
using System.Data.SqlClient;
using DAL;
using Dapper;

namespace BLL.GitHubPayloadStrategies;

public class RegisterRepositoryHandler : GitHubPayloadHandler
{
    private readonly RepositoryRepository _repository = new();
    
    public override void Handle(dynamic payload)
    {
        int id = payload.repository.id;
        string name = payload.repository.full_name;

        if (!_repository.DoesRepositoryExists(id))
        {
            _repository.SaveRepository(id, name);
        }
        else
        {
            Console.WriteLine("Repository already exists");
        }
    }
}
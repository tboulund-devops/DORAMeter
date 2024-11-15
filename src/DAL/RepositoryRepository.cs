using Dapper;
using Models;

namespace DAL;

public class RepositoryRepository : RepositoryBase
{
    public bool DoesRepositoryExists(int repositoryId)
    {
        using var connection = OpenConnection();
        return connection.ExecuteScalar<bool>("SELECT 1 FROM repositories WHERE id = @Id", new { Id = repositoryId });
    }

    public Repository SaveRepository(int id, string name)
    {
        using var connection = OpenConnection();
        var repository = new Repository(id, name);
        connection.Execute("INSERT INTO repositories (id, name) VALUES (@Id, @Name)", new { Id = repository.Id, Name = repository.Name });
        return repository;
    }

}
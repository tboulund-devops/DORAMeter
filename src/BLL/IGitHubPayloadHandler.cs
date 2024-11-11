namespace BLL;

public interface IGitHubPayloadHandler
{
    void Handle(Dictionary<string, object> payload);
}
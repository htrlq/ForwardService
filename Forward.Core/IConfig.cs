namespace Forward.Core
{
    public interface IConfig
    {
        bool TryAdd<IService>(string expression) where IService : IForwardService;
        bool TryContains(string value, out IForwardService forwardService);
    }
}

using System.Threading.Tasks;

namespace Forward.Core
{
    public interface IForwardService
    {
        Task<object> ExecuteAsync(object param);
    }
}

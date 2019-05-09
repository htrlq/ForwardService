using System.IO;
using System.Threading.Tasks;

namespace Forward.Core
{
    public interface IForwardFactory
    {
        Task<object> ForwardAsync(string url, string json);
    }
}

using System.Diagnostics;
using System.Threading.Tasks;

namespace Forward.Core
{
    public abstract class AbstractPublishService : IForwardService
    {
        private DiagnosticSource source;

        public AbstractPublishService()
        {
            var key = GetType().FullName;

            source = new DiagnosticListener(key);
        }

        public virtual async Task<object> PublishAsync(object param)
        {
            var response = await ExecuteAsync(param);

            source.Write("Publish", response);

            return response;
        }

        public abstract Task<object> ExecuteAsync(object param);
    }
}

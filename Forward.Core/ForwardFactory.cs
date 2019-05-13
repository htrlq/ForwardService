using Forward.Core.Core.Excpetion;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Core
{
    internal class ForwardFactory : IForwardFactory
    {
        private IConfig Config { get; }

        public ForwardFactory(IConfig config)
        {
            Config = config;
        }

        public async Task<object> ForwardAsync(string url, string json)
        {
            if (Config.TryContains(url, out IForwardService service))
            {
                var type = service.GetType();
                var methodInfo = type.GetMethod("ExecuteAsync");
                var paramInfo = (ParamTypeAttribute)methodInfo.GetCustomAttributes(typeof(ParamTypeAttribute), false).FirstOrDefault();
                var paramType = paramInfo == null ? typeof(object) : paramInfo.ParamType;

                var entry = JsonConvert.DeserializeObject(json, paramType);

                if (service is AbstractPublishService publishService)
                {
                    return await publishService.PublishAsync(entry);
                }
                else
                {
                    return await service.ExecuteAsync(entry);
                }
            }

            throw new ServiceNotRegister("Service Not Register");
        }
    }
}

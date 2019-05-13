using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace Forward.Core
{
    public class Config : IConfig
    {
        private ConcurrentDictionary<string, Type> Dictionary { get; } = new ConcurrentDictionary<string, Type>();
        public bool TryAdd<IService>(string expression) where IService: IForwardService
        {
            return Dictionary.TryAdd(expression, typeof(IService));
        }

        public bool TryContains(string value, out IForwardService forwardService)
        {
            var array = Dictionary.ToArray();
            
            if (array.Any(_keyPair=> value.Equals(_keyPair.Key)))
            {
                var type = array.FirstOrDefault(_keyPair => Regex.IsMatch(value, _keyPair.Key)).Value;

                forwardService = (IForwardService)UnitWork.Instance.ServiceProvider.GetRequiredService(type);

                return true;
            }

            forwardService = null;
            return false;
        }
    }
}

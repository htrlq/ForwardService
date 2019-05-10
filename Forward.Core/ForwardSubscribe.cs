using System;

namespace Forward.Core
{
    public class ForwardSubscribe<TForwardService>
        where TForwardService: IForwardService
    {
        public Func<object, object> Func { get; }
        public Type ServiceType { get; }

        public ForwardSubscribe(Func<object,object> func)
        {
            Func = func;
            ServiceType = typeof(TForwardService);
        }
    }
}

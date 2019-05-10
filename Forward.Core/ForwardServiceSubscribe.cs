using System;
using System.Collections.Generic;

namespace Forward.Core
{
    internal class ForwardServiceSubscribe : IObserver<KeyValuePair<string, object>>
    {
        private IForwardService ForwardService { get; }
        private Func<object,object> Func { get; }

        public ForwardServiceSubscribe(IForwardService forwardService, Func<object, object> func)
        {
            ForwardService = forwardService;
            Func = func;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public async void OnNext(KeyValuePair<string, object> pair)
        {
            if (pair.Key.Equals("Publish"))
            {
                var convert = Func(pair.Value);

                if (ForwardService is AbstractPublishService publishService)
                {
                    await publishService.PublishAsync(convert);
                }
                else
                {
                    await ForwardService.ExecuteAsync(convert);
                }
            }
        }
    }
}

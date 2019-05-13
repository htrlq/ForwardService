using BeetleX.FastHttpApi;
using Forward.Core.Core.Excpetion;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Core.Filter
{
    public class RetryAttribute: FilterAttribute
    {
        private int _count;

        public RetryAttribute(int count)
        {
            _count = count;
        }

        public override void Executed(ActionContext context)
        {
            var requestTime = DateTime.Now;

            try
            {
                var policy = Policy
                .Handle<Exception>()
                .Retry(_count, (ex, count) =>
                {
                    Console.WriteLine($"Retry Index:{count}, Exception:{ex.Message}");
                });

                policy.Execute(() =>
                {
                    base.Executed(context);

                    if (context.Exception != null)
                        throw context.Exception;
                });
            }
            catch(Exception ex)
            {
                context.Result = new ResponseModel
                {
                    RequestTime = requestTime,
                    ResponseTime = DateTime.Now,
                    IsSuccessFul = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
                context.Exception = null;
            }
        }
    }
}

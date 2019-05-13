using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Data;
using Forward.Core.Filter;
using System;
using System.Threading.Tasks;

namespace Forward.Core
{
    [Controller(BaseUrl = "/Forward", SingleInstance = false)]
    public class Forward
    {
        private IForwardFactory ForwardFactory { get; }

        public Forward(IForwardFactory forwardFactory)
        {
            ForwardFactory = forwardFactory;
        }

        [Post(Route = "{url}")]
        [NoDataConvert]
        [Retry(5)]
        public async Task<ResponseModel> Service(string url,IHttpContext context)
        {
            var result = new ResponseModel()
            {
                RequestTime = DateTime.Now
            };

            var json = context.Request.Stream.ReadString(context.Request.Length);

            result.Data = await ForwardFactory.ForwardAsync(url, json);
            result.ResponseTime = DateTime.Now;

            result.IsSuccessFul = true;

            return result;
        }
    }
}

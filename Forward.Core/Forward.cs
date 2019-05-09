using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Data;
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
        public async Task<ResponseModel> Service(string url,IHttpContext context)
        {
            var result = new ResponseModel()
            {
                RequestTime = DateTime.Now
            };

            try
            {
                var json = context.Request.Stream.ReadString(context.Request.Length);

                result.Data = await ForwardFactory.ForwardAsync(url, json);
                result.ResponseTime = DateTime.Now;

                result.IsSuccessFul = true;
            }
            catch(Exception ex)
            {
                result.ResponseTime = DateTime.Now;
                result.Data = ex.ToString();
            }

            return result;
        }
    }
}

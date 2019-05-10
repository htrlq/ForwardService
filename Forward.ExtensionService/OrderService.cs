using Forward.Core;
using System;
using System.Threading.Tasks;

namespace Forward.ExtensionService
{

    public class OrderService : IForwardService
    {
        [ParamType(typeof(OrderModel))]
        public Task<object> ExecuteAsync(object param)
        {
            var orderParam = param as OrderModel;
            Console.WriteLine($"Title:{orderParam.Title} Money:{orderParam.PayMoney} CreateTime:{orderParam.CreateTime}");

            return Task.FromResult<object>(true);
        }
    }

    public class OrderModel
    {
        public string Title { get; set; }
        public decimal PayMoney { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

using System;

namespace Forward.Core
{

    public class ResponseModel
    {
        public bool IsSuccessFul { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}

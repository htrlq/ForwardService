using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Core.Core.Excpetion
{
    class ServiceNotRegister: Exception
    {
        public ServiceNotRegister(string message):base(message)
        {

        }
    }
}

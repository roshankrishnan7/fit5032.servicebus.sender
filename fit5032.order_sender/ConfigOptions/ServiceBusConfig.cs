using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit5032.order_sender.ConfigOptions
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
    }
}

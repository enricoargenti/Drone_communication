using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreClient.Packets
{
    internal class Status
    {
        public string Drone_ID { get; set; }
        public string Drone_Name { get; set; }
        public Object Data { get; set; }
    }
}

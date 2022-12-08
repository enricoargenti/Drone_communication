using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreClient.Packets;

public class Status
{
    public string Drone_ID { get; set; }
    public Object Data { get; set; }

    public Status(string drone_id, Object data)
    {
        this.Drone_ID = drone_id;
        this.Data = data;
    }
}


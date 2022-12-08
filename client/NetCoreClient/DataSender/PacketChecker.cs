using NetCoreClient.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataSender
{
    internal class PacketChecker
    {
        internal PacketChecker() { }

        // Accepts as parameters: a JSON string packet containing Data and the type we want to check
        // Deserializes JSON packet to understand the type of measurement contained and
        // returns true if ther type matches with what expected, false in the other case
        public bool Check(string packet, string type)
        {
            
            Status? deserializedStatus = System.Text.Json.JsonSerializer.Deserialize<Status>(packet);

            // Here an Anonymous Type of deserializer is used because
            // we don't know what type of object Data is
            var definition = new { Type = "" };
            var deserializedData = JsonConvert.DeserializeAnonymousType(
                deserializedStatus.Data.ToString(), definition);
            //Console.WriteLine("Type: " + deserializedData.Type);
            if (deserializedData.Type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PackageServer
{
    public class Packet
    {
        public DateTime Date { get; set; }

        public string IMEI { get; set; }

        public string Data { get; set; }

        public Packet(string rawData)
        {
            this.Data = rawData;
        }

        public static Packet Parse(string rawData)
        {
            return new Packet(rawData);
        }
 

    }
}

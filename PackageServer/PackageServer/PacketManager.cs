using PackageServer.Interfaces;
using System;
using System.IO;

namespace PackageServer
{
    public class PacketManager : IPacketManager
    {
        private string filePath;
        private static object lockObj = new object();

        public PacketManager(string filePath)        
        {
            this.filePath = filePath;
        }

        public string GetReply(Packet p)
        {
            return "#AP#simple_reply#\r\n";
        }

        public void SavePacket(Packet p)
        {
            lock (lockObj)
            {
                using (var file = new StreamWriter(filePath, true))
                {
                    file.WriteLine($"#{DateTime.Now}#{p.Data}");
                }
            }
        }
    }
}

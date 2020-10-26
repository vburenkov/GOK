using System;
using System.Collections.Generic;
using System.Text;

namespace PackageServer.Interfaces
{
    public interface IPacketManager
    {
        void SavePacket(Packet p);

        string GetReply(Packet p);
    }
}

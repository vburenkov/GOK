using PackageServer.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PackageServer
{
    public class TcpServer
    {       
        TcpListener server = null;
        IPacketManager packetManager = null;
        ILogger log;

        public TcpServer(string ip, 
                         int port, 
                         IPacketManager packetManager,
                         ILogger log)
        {
            this.packetManager = packetManager;
            this.log = log;
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();            
            StartListener();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    log.LogInfo("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    log.LogInfo($"Connected! {client.Client.RemoteEndPoint}");

                    var newTask = Task.Run(() => HandleDeivce(client));               
                }
            }
            catch (SocketException e)
            {
                log.LogError($"SocketException: {e.Message}");
                server.Stop();
            }
        }

        public void HandleDeivce(TcpClient client)
        {
            Byte[] bytes = new Byte[1024];
            var stream = client.GetStream(); 
            int i;           

            try
            {

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, i);
                    log.LogInfo($"Received: {data}");

                    // save packet data
                    var packet = Packet.Parse(data);
                    packetManager.SavePacket(packet);

                    // send reply
                    var replyStr = packetManager.GetReply(packet);
                    log.LogInfo($"Reply: {replyStr}");
                    Byte[] reply = Encoding.ASCII.GetBytes(replyStr);
                    stream.Write(reply, 0, reply.Length);
                }
            }
            catch (Exception e)
            {
                log.LogError($"{nameof(HandleDeivce)} exception: {e.Message}");
                client.Close();
            }
        }        
    }
}

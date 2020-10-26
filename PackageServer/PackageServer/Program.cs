using log4net;
using PackageServer.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PackageServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogConfigurator.Setup();
            var nlog = LogManager.GetLogger(typeof(Program));
            IPacketManager packetManager = new PacketManager("Logs\\Packets.txt");
            ILogger consoleLogger = new ConsoleLogger();

            // run server in separate thread
            var server = Task.Run(() =>
            {
                if (args.Length == 2)
                {
                    TcpServer server = new TcpServer(args[0], int.Parse(args[1]), packetManager, consoleLogger);                    
                }
                else
                {
                    TcpServer server = new TcpServer("192.168.56.1", 9999, packetManager, consoleLogger);
                }
            });

            server.Wait();

        }
    }
}

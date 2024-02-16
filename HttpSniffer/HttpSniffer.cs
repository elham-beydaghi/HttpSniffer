using System.Net.Sockets;
using System.Net;
using System.Text;

namespace HttpSniffer
{
    public class HttpSniffer
    {
        static void Main(string[] args)
        {
            //here addressfamily.internetwork means fthe socket use ipv4 
            //raw means the transport layer is not applied
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            //0 means os should assign some port number automatically
            socket.Bind(new IPEndPoint(IPAddress.Parse("192.168.100.5"), 0)); 
            //again specify we want to have messages in ip layer or network
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            //size of buffer is 4096
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while (true)
                {
                    bytesRead = socket.Receive(buffer);
                    ProcessPacket(buffer, bytesRead);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            finally
            {
                socket.Close();
            }
        }

        static void ProcessPacket(byte[] buffer, int length)
        { 
            string packetData = Encoding.ASCII.GetString(buffer, 0, length);
            Console.WriteLine(packetData);
        }
    }
}

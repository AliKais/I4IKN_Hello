using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp
{
  class UDP_Client
  {
    static void Main(string[] args)
    {
      const int PORT = 9000;
  		const int BUFSIZE = 1000;

      var client = new UdpClient();
      var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      var location = new IPEndPoint(IPAddress.Parse(args[0]), PORT);

      byte[] send = Encoding.ASCII.GetBytes(args[1]);
      socket.SendTo (send, location);

  ////////////////////////////////

      switch (//)
      {
        case "U" :
        case "u" :
          //
          break;

        case "L" :
        case "l" :
          //
          break;

        default:
          break;
      }
    }
  }

}

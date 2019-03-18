using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text

class UDP_Client
{
  static void Main(string[] args)
  {
    const int PORT = 9000;

    UdpClient client = new UdpClient();
    Socket socket = new Socket();

    string IPAddress = IPAddress.Parse(args[0]);

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

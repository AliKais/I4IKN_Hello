using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Opgave7
{
    class File_client
    {
        static void Main(string[] arg)
        {         
            const int PORT = 9000;

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //opretter forbindelse til serveren 
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(arg[0], PORT);
            //sender beskeden til serveren
            byte[] sendbyte = Encoding.ASCII.GetBytes(arg[1]);
            udpClient.Send(sendbyte, sendbyte.Length);
            //aflæser fra serveren 
            IPEndPoint IPE = new IPEndPoint(IPAddress.Parse(/*"10.0.0.1"*/arg[0]), PORT);
            //udskriver beskeden fra serverens respons 
            Byte[] rec_byte = udpClient.Receive(ref IPE);         
            string returndata = Encoding.ASCII.GetString(rec_byte);
			Console.WriteLine("Oplysninger fra serveren er " + rec_byte.ToString());         
        }
    }
}
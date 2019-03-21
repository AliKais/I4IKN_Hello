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
            udpClient.Connect(/*"10.0.0.1"*/ arg[0], PORT);
            //sender beskeden til serveren
            byte[] sendbyte = Encoding.ASCII.GetBytes(arg[1]);
            udpClient.Send(sendbyte, sendbyte.Length);
            //aflæser fra serveren 
            IPEndPoint IPE = new IPEndPoint(IPAddress.Parse(/*"10.0.0.1"*/arg[0]), PORT);
            //udskriver beskeden fra serverens respons 
            //Byte[] rec_byte = udpClient.Receive(ref IPE);
            //string returndata = Encoding.ASCII.GetString(rec_byte);
            //Console.Write("Oplysninger fra serveren er " +rec_byte.ToString());

            switch (arg[1])
            {
                case "U":
                case "u":
                    Byte[] rec_byte = udpClient.Receive(ref IPE);
                    string returndata = Encoding.ASCII.GetString(rec_byte);
                    Console.Write("du har sendt U Oplysninger fra serveren er " + rec_byte.ToString());
                    break;

                case "L":
                case "l":
                    Byte[] rec_byte_L = udpClient.Receive(ref IPE);
                    string returndata_L = Encoding.ASCII.GetString(rec_byte_L);
                    Console.Write(" du har sendt L oplysninger fra serveren er " + rec_byte_L.ToString());
                    break;

                default:
                    Console.WriteLine("du indtastede forkert");
                    break;
            }


        }

        //public static void Main (string[] args)
        //{

        //  Console.WriteLine ("Client starts...");
        //}
    }
}
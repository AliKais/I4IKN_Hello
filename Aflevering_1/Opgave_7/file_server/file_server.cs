using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp
{
    class file_server
    {
        /// <summary>
        /// The PORT
        /// </summary>
        const int PORT = 9000;

        private file_server()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, PORT);
            UdpClient serverSocket = new UdpClient(PORT);
            Console.WriteLine(">> UDP Server started");

            // Received message
            byte[] receivedDataBytearray;
            string receivedData;
            string file_path;

            while (true)
            {
                try
                {
                    receivedDataBytearray = serverSocket.Receive(ref ipPoint);
                    Console.WriteLine("Received from: {0}",ipPoint.ToString());
                    receivedData = Encoding.ASCII.GetString(receivedDataBytearray, 0, receivedDataBytearray.Length);
                    Console.WriteLine("Data received: {0}", receivedData);

                    Console.WriteLine("Controlling data");
                    /*
                    switch (receivedData.ToUpper())
                    {
                        case "U":
                            file_path = "/proc/uptime";
                            Console.WriteLine("File = Uptime");
                            break;

                        case "L":
                            file_path = "/proc/loadavg";
                            Console.WriteLine("File = Loadavg");
                            break;
                        
                        default:
                            Console.WriteLine("Input doesn't match U or L");
                            break;
                    }
                    */
                    if (receivedData.ToUpper() == "U")
                    {
                        file_path = "/proc/uptime";
                        Console.WriteLine("File = Uptime");
                        var file = File.ReadAllText(file_path);
                        byte[] fileBytes = Encoding.ASCII.GetBytes(file);
                        serverSocket.Send(fileBytes, fileBytes.Length, ipPoint);
                    }

                    else if (receivedData.ToUpper() == "L")
                    {
                        file_path = "/proc/loadavg";
                        Console.WriteLine("File = Loadavg");
                        var file = File.ReadAllText(file_path);
                        byte[] fileBytes = Encoding.ASCII.GetBytes(file);
                        serverSocket.Send(fileBytes, fileBytes.Length, ipPoint);
                    }

                    else
                    {
                        Console.WriteLine("Input doesn't match U or L");
                        file_path = "Input is not u or l, wrong request";
                        byte[] fileBytes = Encoding.ASCII.GetBytes(file_path);
                        serverSocket.Send(fileBytes, fileBytes.Length, ipPoint);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Server starts...");
            new file_server();
        }
    }
}

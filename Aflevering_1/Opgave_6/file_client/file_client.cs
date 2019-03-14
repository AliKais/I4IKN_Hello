using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class File_client
	{
		const int PORT = 9000;
		const int BUFSIZE = 1000;

		private File_client(string[] args)
		{
			Console.WriteLine("Connecting");

            TcpClient client = new TcpClient();
			client.Connect(args[0], PORT);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("File requested");

            LIB.writeTextTCP(stream, args[1]);
			ReceiveFile(args[1], stream);

            Console.WriteLine("File received");

            stream.Close();
            client.Close();
		}


		private void ReceiveFile(string fileName, NetworkStream io)
        {
            int file_Size = (int)LIB.getFileSizeTCP(io);

            if (file_Size == 0)
            {
                Console.WriteLine("File ikke fundet ");

            }
            else
            {
                long totalReceived = 0;

                byte[] file_bit = new byte[BUFSIZE];
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                while (totalReceived < file_Size)
                {

                    int Current_Read = io.Read(file_bit, 0, file_bit.Length);

                    fs.Write(file_bit, 0, Current_Read);
                    totalReceived += Current_Read;

                    Array.Clear(file_bit, 0, BUFSIZE);
                }

                fs.Close();
            }
        }


		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new File_client(args);
		}
	}
}

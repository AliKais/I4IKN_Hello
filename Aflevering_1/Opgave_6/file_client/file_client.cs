using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_client
	{
		const int PORT = 9000;
		const int BUFSIZE = 1000;

		private file_client (string[] args)
		{
			Console.WriteLine("Connecting");

            TcpClient client = new TcpClient();
			client.Connect(args[0], PORT);

            NetworkStream stream = client.GetStream();

            Console.WriteLine("File requested");

            LIB.writeTextTCP(stream, args[1]);
			receiveFile(args[1], stream);

            Console.WriteLine("File received");

            stream.Close();
            client.Close();
		}


		private void receiveFile (String fileName, NetworkStream io)
		{
			//Find fil størrelsen
			long Size_f = LIB.getFileSizeTCP(io);
			if(Size_f<1)
			{
				Console.WriteLine("Filen kunne ikke findes :( Programmet lukker");
				return;
			}
            
			FileStream fs = File.OpenWrite("root/downloads/"+LIB.extractFileName(fileName));
			byte[] file = new byte[BUFSIZE];
			int currentRec = 0;
			int totalRec = 0;
			while(totalRec<Size_f)
			{
				currentRec = io.Read(file, (int)Size_f-totalRec, BUFSIZE );
				fs.Write(file, 0, currentRec);
				totalRec = totalRec + currentRec;
			}
			fs.Close();
		}


		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}

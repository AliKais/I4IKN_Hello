using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_client
	{
		/// <summary>
		/// The PORT.
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments. First ip-adress of the server. Second the filename
		/// </param>
		private file_client (string[] args)
		{
			// TO DO Your own code
           
			Console.WriteLine(Connecting);
			var client = new Tcpclient();
			client.Connect(args[0], PORT);

			var stream = client.GetStream();

			LIB.writeTextTCP(stream, args[1]);
			receiveFile(args[1], stream);
			client.close();
			stream.close();

		}

		/// <summary>
		/// Receives the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='io'>
		/// Network stream for reading from the server
		/// </param>
		private void receiveFile (String fileName, NetworkStream io)
		{
			// TO DO Your own code
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

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}

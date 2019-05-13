using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_client
	{
		private const int BUFSIZE = 1000;
		private const string APP = "FILE_CLIENT";

		private file_client(String[] args)
		{
			var fileSize    = 0;
			var transport   = new Transport(BUFSIZE);
			var buf         = new byte[BUFSIZE];
			var filePath    = args[0];

			Console.WriteLine("Anmodning om fil fra server");

			transport.Send(Encoding.UTF8.GetBytes(filePath), filePath.Length);
			var size = transport.Recive(ref buf);

			if ((size != 0) && (fileSize > 0))
			{
				fileSize = int.Parse(Encoding.UTF8.GetString(buf, 0, size));
				receiveFile(filePath, transport);
			}
		}

		private void receiveFile (String fileName, Transport transport)
		{
			var read        = 0;
			var readSize    = 0;
			var filebuf     = new byte[BUFSIZE];
			var fileName    = LIB.extractFileName(path);
			var newFile     = new FileStream(fileName, FileMode.Create, FileAccess.Write);

			Console.WriteLine("Modtager fil");

			while ((read < fileSize) && ((readSize = transport.Receive(ref fileBuf)) > 0)) {
				newFile.Write(fileBuf, 0, readSize);
				read += readSize;
			}

			if (read == fileSize){
				Console.WriteLine("Den ønskede fil blev modtaget");
			}

			newFile.Close();
		}

		public static void Main (string[] args)
		{
			new file_client(args);
		}
	}
}

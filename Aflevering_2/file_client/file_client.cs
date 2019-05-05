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
            var transport = new Transport(BUFSIZE);
            var buf = new byte[BUFSIZE];
            var filePath = args[0];

            transport.Send(Encoding.UTF8.GetBytes(filePath), filePath.Length);
            var size = transport.Recive(ref buf);
            var fileSize = 0;

            if (size != 0)
            {
                fileSize = int.Parse(Encoding.UTF8.GetString(buf, 0, size));

                if (fileSize > 0)
                {
                    ReceiveFile(filePath, fileSize, transport);
                }
            }
	    }


		private void receiveFile (String fileName, Transport transport)
		{
			// TO DO Your own code
		}

		public static void Main (string[] args)
		{
			new file_client(args);
		}
	}
}
using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_server
	{
		private const int BUFSIZE = 1000;
		private const string APP = "FILE_SERVER";

		private file_server ()
		{
            var transport = new Transport(BUFSIZE);
            var buf = new byte[BUFSIZE];
            var size = 0;

            while (true){
                while ((size = transport.Receive(ref buf)) == 0)
                { };  

                if (size != 0){
                    var filePath = Encoding.UTF8.GetString(buf, 0, size);
                    var tmp = Path.GetFullPath("File_Server_Home/" + filePath);
                    sendFile(tmp, transport);
                }
                transport = new Transport(BUFSIZE);
            }
        }

		private void sendFile(String fileName, long fileSize, Transport transport)
		{
			// TO DO Your own code
		}

		public static void Main (string[] args)
		{
			new file_server();
		}
	}
}
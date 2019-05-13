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

        private file_server()
        {
            Transport transport = new Transport(BUFSIZE, APP);
            var buffer = new byte[BUFSIZE];
            var file_size = transport.Receive(ref buffer);

            while (true)
            {
                if (file_size == 0)
                {
                    Console.WriteLine("file not found");
                }
                else
                {
                    var file = Encoding.UTF8.GetString(buffer, 0, file_size);
                    var file_path = Path.GetFullPath("file_server_home/" + file);
                    sendFile(file_path, file_size ,transport);
                }
            }
            /*
            var transport = new Transport(BUFSIZE);
            var buf = new byte[BUFSIZE];
            var size = 0;

            while (true)
            {
                while ((size = transport.Receive(ref buf)) == 0)
                { };

                if (size != 0)
                {
                    var filePath = Encoding.UTF8.GetString(buf, 0, size);
                    var tmp = Path.GetFullPath("File_Server_Home/" + filePath);
                    sendFile(tmp, transport);
                }
                transport = new Transport(BUFSIZE);
            }
            */

        }

        private void sendFile(String fileName, long fileSize, Transport transport)
        {
            //starter med at checke hvorvidt filen overhovedet eksisterer 
            int file = (int)LIB.check_File_Exists(fileName);
            var file_size = Encoding.UTF8.GetBytes(fileSize.ToString());
            transport.send(file_size, file_size.Length);
            if (file == 0)
            {
                Console.WriteLine("File not found");
            }
            else
            {

                Console.WriteLine("sending File");
                var file_send = File.Open(@fileName, FileMode.Open);
                var current_send = 0;
                var buffer = new byte[BUFSIZE];
                while (current_send < buffer.Length)
                {
                    var reading = file_send.Read(buffer, current_send, buffer.Length);
                    if (reading == 0)
                    {
                        Console.WriteLine("File transfer failed");
                    }
                    current_send += reading;

                }
                transport.send(buffer, current_send);
                Console.WriteLine("file transfer complete");


            }
            // TO DO Your own code
        }

        public static void Main(string[] args)
        {
            new file_server();
        }
    }
}
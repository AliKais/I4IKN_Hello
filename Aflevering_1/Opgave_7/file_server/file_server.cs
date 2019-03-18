using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
    class file_server
    {
        /// <summary>
        /// The PORT
        /// </summary>
        const int PORT = 9000;
        /// <summary>
        /// The BUFSIZE
        /// </summary>
        const int BUFSIZE = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="file_server"/> class.
        /// Opretter en socket.
        /// Venter på en connect fra en klient.
        /// Modtager filnavn
        /// Finder filstørrelsen
        /// Kalder metoden sendFile
        /// Lukker socketen og programmet
        /// </summary>
        private file_server()
        {
            // TO DO Your own code
            IPAddress localAddress = IPAddress.Any;

            // Initiate an instance of server socket
            TcpListener serverSocket = new TcpListener(localAddress, PORT);

            // Initiate an instance of client socket
            TcpClient clientSocket = default(TcpClient);

            // Start tC:\Users\flole\Desktop\GIT_Skole\I4IKN_Hello\Aflevering_1\Opgave_6\LIB\lib.cshe socket for listening
            serverSocket.Start();



            // Received message
            String filepath = null;

            Console.WriteLine(">> Server started");

            while (true)
            {
                try
                {
                    // Accepts client socket
                    clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine(">> Accepted connection from client");

                    NetworkStream stream = clientSocket.GetStream();

                    filepath = LIB.readTextTCP(stream);


                    long filesize = LIB.check_File_Exists(filepath);

                    if (filesize > 0)
                        sendFile(filepath, filesize, stream);

                    stream.Close();
                    clientSocket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name='fileName'>
        /// The filename.
        /// </param>
        /// <param name='fileSize'>
        /// The filesize.
        /// </param>
        /// <param name='io'>
        /// Network stream for writing to the client.
        /// </param>
        private void sendFile(String fileName, long fileSize, NetworkStream io)
        {
            // TO DO Your own code
            LIB.writeTextTCP(io, fileSize.ToString());

            byte[] filebyte = File.ReadAllBytes(fileName);

            int total = 0;

            while (total < fileSize)
            {

                if ((fileSize - total) < BUFSIZE)
                {
                    var newSize = (int)fileSize;
                    io.Write(filebyte, total, newSize - total);
                    total += newSize - total;
                    Console.WriteLine(">> Program shutting down...");
                    System.Environment.Exit(1);
                }
                else
                {
                    io.Write(filebyte, total, BUFSIZE);
                    total += BUFSIZE;
                }
            }
        }

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name='args'>
        /// The command-line arguments.
        /// </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Server starts...");
            new file_server();
        }
    }
}

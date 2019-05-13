using System;
using System.IO.Ports;

/// <summary>
/// Link.
/// </summary>
namespace Linklaget
{
    /// <summary>
    /// Link.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// The DELIMITE for slip protocol.
        /// </summary>
        const byte DELIMITER = (byte)'A';
        /// <summary>
        /// The buffer for link.
        /// </summary>
        private byte[] buffer;
        /// <summary>
        /// The serial port.
        /// </summary>
        SerialPort serialPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="link"/> class.
        /// </summary>
        public Link(int BUFSIZE, string APP)
        {
            // Create a new SerialPort object with default settings.
#if DEBUG
            if (APP.Equals("FILE_SERVER"))
            {
                serialPort = new SerialPort("/dev/ttySn0", 115200, Parity.None, 8, StopBits.One);
            }
            else
            {
                serialPort = new SerialPort("/dev/ttySn1", 115200, Parity.None, 8, StopBits.One);
            }
#else
				serialPort = new SerialPort("/dev/ttyS1",115200,Parity.None,8,StopBits.One);
#endif
            if (!serialPort.IsOpen)
                serialPort.Open();

            buffer = new byte[(BUFSIZE * 2)];

            // Uncomment the next line to use timeout
            //serialPort.ReadTimeout = 500;

            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
        }

        /// <summary>
        /// Send the specified buf and size.
        /// </summary>
        /// <param name='buf'>
        /// Buffer.
        /// </param>
        /// <param name='size'>
        /// Size.
        /// </param>
        public void send(byte[] buf, int size)
        {
            int i;
            int current = 1;
            for (i = 0; i < size; i++)
            {
                //ser om vores besked er karakteren A samme værdi som delimeter eller om det er b ellers får den default værdi
                if (buf[i] == 'A')
                {
                    buffer[current] = (byte)'B';
                    buffer[current++] = (byte)'C';
                }
                else if (buf[i] == 'B')
                {
                    buffer[current] = (byte)'B';
                    buffer[current++] = (byte)'D';
                }
                else
                {
                    buffer[current] = buf[i];
                }
                buffer[i++] = (byte)'A';
            }


            //sender beskeden videre 
            serialPort.Write(buffer, 0, current);
            // TO DO Your own code
        }

        /// <summary>
        /// Receive the specified buf and size.
        /// </summary>
        /// <param name='buf'>
        /// Buffer.
        /// </param>
        /// <param name='size'>
        /// Size.
        /// </param>
        public int receive(ref byte[] buf)
        {
            //starter først med at afgøre størrelsen 
            var data_size = 0;
            while (data_size < buffer.Length)
            {
                var current_read = (byte)serialPort.ReadByte();
                buffer[data_size++] = current_read;
                if (current_read == (byte)'A')
                {
                    break;
                }
            }


            int buf_count = 0;
            //kigger på vores buffer nu 
            for (int i = 0; i < data_size; i++)
            {
                if (buffer[i] == (byte)'B')
                {
                    if (buffer[i + 1] == (byte)'C')
                    {
                        buf[buf_count++] = (byte)'B';
                    }
                    else
                    {
                        buf[buf_count++] = (byte)'C';
                    }
                }
                buf[buf_count] = buffer[i];
            }


            // TO DO Your own code
            return buf_count;
        }
    }
}


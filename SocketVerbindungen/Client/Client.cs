using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
                Console.WriteLine("Client");
                Console.WriteLine("\nText zum senden eingeben");
                string eingabe = Console.ReadLine();
                ClientStart("127.0.0.1", eingabe);

        }

        static void ClientStart(string server, string message)
        {

            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.
            Int32 port = 9000;
            TcpClient client = new TcpClient(server, port);

            // Get a client stream for reading and writing.
            NetworkStream stream = client.GetStream();

                   
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                string responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
                
                // Close everything.
                stream.Close();          
                client.Close();

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

    }
}

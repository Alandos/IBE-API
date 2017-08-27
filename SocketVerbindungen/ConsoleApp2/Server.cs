using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    class Server
    {
        public void ServerStart()
        {
            Console.WriteLine("Server");

            IPAddress ipadr = IPAddress.Parse("127.0.0.1");

            TcpListener tcpListener = new TcpListener(ipadr, 9000);
            tcpListener.Start();
            

            while(true)
            {
                Socket socket = tcpListener.AcceptSocket();

                if (socket.Connected)
                {
                    Console.WriteLine("Connection accepted" + socket.RemoteEndPoint);
                    NetworkStream stream = new NetworkStream(socket);
                    
                    if (stream.DataAvailable)
                    {
                        //Read the Stream and display the received String
                        Byte[] receiveData = new Byte[256];
                        string receiveMessage = String.Empty;

                        Int32 bytes = stream.Read(receiveData, 0, receiveData.Length);
                        receiveMessage = System.Text.Encoding.ASCII.GetString(receiveData, 0, bytes);
                        Console.WriteLine("Received: {0}", receiveMessage);

                        //Send back the received string + "Echo"
                        string responseMessage = "Echo" + receiveMessage;
                        Console.WriteLine("Response: {0}", responseMessage);
                        Byte[] responseData = System.Text.Encoding.ASCII.GetBytes(responseMessage);
                        stream.Write(responseData, 0, responseData.Length);
                       
                    } 
                    
                    stream.Close();
                    socket.Disconnect(false);
                }
            }
           

           
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();

        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            
                Server server1 = new Server();
                server1.ServerStart();
                       

        }
    }
}

using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using tcp;

namespace ConsoleApp2
{
    class Program
    {
        const int echo_port = 8080;
        public static int nClients = 0;
        static void Main(string[] args)
        {
            try
            {
                TcpListener clientListener = new TcpListener(echo_port);
                clientListener.Start();
                Console.WriteLine("Waiting for connections...");
                while (nClients < 3)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.clientSocket = client;
                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClients++;
                }
                clientListener.Stop();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp);
            }
        }
    }
}

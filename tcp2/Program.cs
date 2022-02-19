using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace tcp2
{
        class Program
        {
            const int echo_port = 8080;
            public static int nClinets = 0;
            static void Main(string[] args)
            {
                Console.WriteLine("Your name");
                string name = Console.ReadLine();
                Console.WriteLine("Logged in");
                try
                {
                    TcpClient eClient = new TcpClient("127.0.0.1", echo_port);
                    StreamReader readerStream = new StreamReader(eClient.GetStream());
                    NetworkStream writerStream = eClient.GetStream();
                    string dataToSend;
                    dataToSend = name;
                    dataToSend += "\r\n";
                    byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                    writerStream.Write(data, 0, data.Length);
                    while (true)
                    {
                        Console.Write(name + " : ");
                        dataToSend = Console.ReadLine();
                        dataToSend += "\r\n";
                        data = Encoding.ASCII.GetBytes(dataToSend);
                        writerStream.Write(data, 0, data.Length);

                        if (dataToSend.IndexOf("QUIT") > 1)
                            break;

                        string returnData;

                        returnData = readerStream.ReadLine();

                        Console.WriteLine("Server: " + returnData);
                    }
                    eClient.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
    }

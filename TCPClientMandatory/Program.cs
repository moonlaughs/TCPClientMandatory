using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientMandatory
{
    class Program
    {
        private static TcpClient _clientSocket;
        private static Stream _stream;
        private static StreamWriter _writer;
        private static StreamReader _reader;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Inseart port number: ");
            try
            {
                var portNumber = Console.ReadLine();
                using (_clientSocket = new TcpClient("127.0.0.1", Convert.ToInt32(portNumber)))
                {
                    using (_stream = _clientSocket.GetStream())
                    {
                        _writer = new StreamWriter(_stream)
                        {
                            AutoFlush = true
                        };
                        Console.WriteLine("Now you are conected to the server.");
                        Console.WriteLine();
                        Console.WriteLine("Weight Converter");
                        Console.WriteLine("Type one of options:\nTOGRAM [number]\nTOOUNCE [number]\nSTOP");
                        while (true)
                        {
                            Console.WriteLine("Type the message and press ENTER:");
                            string messageFromClient = Console.ReadLine();
                            _writer.WriteLine(messageFromClient);
                            _reader = new StreamReader(_stream);
                            string messageFromServer = _reader.ReadLine();
                            if (messageFromServer != null)
                            {
                                Console.WriteLine(messageFromServer);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("DISCONNECTED");
                                Console.WriteLine();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please insert correct number");
                Console.WriteLine();
            }
        }
    }
}

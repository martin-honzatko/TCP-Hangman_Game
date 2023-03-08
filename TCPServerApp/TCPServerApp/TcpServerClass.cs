using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServerApp
{
    public static class TcpServerClass
    {
        private static string[] gameWords = new string[3];
        public static void Main(Int32 port)
        {
            TcpListener server = null;
            gameWords[0] = "Apple";
            gameWords[1] = "Party";
            gameWords[2] = "World";
            Random rand = new Random();
            int randInt;
            string gameString = string.Empty;

            try
            {
                IPAddress localAddr = IPAddress.Loopback;
                server = new TcpListener(localAddr, port);
                server.Start();
                byte[] bytes = new byte[256];
                string data = null;
                char[] gameChars = new char[2];
                bool gameBegin = false;
                int attemps = 2;
                int successes = 0;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine($"Received: {data}");

                        if (data == "Hello from a client!")
                        {
                            string msgString = "Hello from a server!";
                            byte[] msg = Encoding.ASCII.GetBytes(msgString);
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine($"Sent: {msgString}");
                        }

                        if (data == "Begin game")
                        {
                            randInt = rand.Next(0, 2);
                            StringBuilder sb = new StringBuilder(gameWords[randInt]);
                            gameChars[0] = sb[1];
                            gameChars[1] = sb[3];
                            sb[1] = '_';
                            sb[3] = '_';
                            gameString = sb.ToString();

                            byte[] msg = Encoding.ASCII.GetBytes($"Your game word: {gameString}");
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine($"Sent: {gameString}");
                            gameBegin = true;
                        }

                        if (gameBegin && gameChars.Contains(data.ToCharArray(0, 1)[0]))
                        {
                            var secondR = true;
                            if (attemps != 0 && successes != 1)
                            {
                                successes = 1;
                                secondR = false;
                                var msgStr = "You got one, one is remaining";
                                byte[] msg = Encoding.ASCII.GetBytes(msgStr);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine($"Sent: {msgStr}");
                            }

                            if (attemps != 0 && successes == 1 && secondR)
                            {
                                successes = 2;
                                var msgStr = "You won!";
                                byte[] msg = Encoding.ASCII.GetBytes(msgStr);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine($"Sent: {msgStr}");
                            }
                        }

                        if (gameBegin && !gameChars.Contains(data.ToCharArray(0, 1)[0]) && data != "Begin game")
                        {
                            if (attemps != 0)
                            {
                                attemps -= 1;
                                var msgStr = $"You got {attemps} attemps remaining";
                                byte[] msg = Encoding.ASCII.GetBytes(msgStr);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine($"Sent: {msgStr}");
                            }

                            if (attemps == 0)
                            {
                                var msgStr = "Game Over!";
                                byte[] msg = Encoding.ASCII.GetBytes(msgStr);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine($"Sent: {msgStr}");
                            }
                        }
                    }

                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}

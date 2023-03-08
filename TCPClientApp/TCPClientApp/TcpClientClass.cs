using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientApp
{
    public static class TcpClientClass
    {
        private static NetworkStream stream;
        private static TcpClient client;
        public static bool gameOverOrWon = false;

        public static void Connect(String server, Int32 port)
        {
            string message = "Hello from a client!";
            try
            {
                client = new TcpClient(server, port);
                byte[] data = Encoding.ASCII.GetBytes(message);

                stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                data = new byte[256];
                string responseData = string.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, data.Length);
                Console.WriteLine($"Received: {responseData}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
        }

        public static void Send(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                data = new byte[256];
                string responseData = string.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, data.Length);
                if (responseData.ToString() == "Game Over!" || responseData.ToString() == "You won!")
                {
                    gameOverOrWon = true;
                    Console.WriteLine($"Received: {responseData}");
                    Disconnect();
                }
                else
                {
                    Console.WriteLine($"Received: {responseData}");
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
        }

        public static void Disconnect()
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}

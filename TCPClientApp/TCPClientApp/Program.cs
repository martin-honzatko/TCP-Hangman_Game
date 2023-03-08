using System;

namespace TCPClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = MyConsoleUI.ReadString("Write server's hostname (default: 127.0.0.1):", "127.0.0.1");
            var port = MyConsoleUI.ReadInt32("Write server's port:");
            
            TcpClientClass.Connect(server, port);

            var startGame = MyConsoleUI.ReadBool("Do you want to play the game? (Yes|No):");
            if (startGame)
            {
                TcpClientClass.Send("Begin game");
                do
                {
                    var message = MyConsoleUI.ReadString("Write your letter:");
                    TcpClientClass.Send(message.ToLower());
                } while (TcpClientClass.gameOverOrWon == false);
            }
            else
            {
                TcpClientClass.Disconnect();
            }
        }
    }
}

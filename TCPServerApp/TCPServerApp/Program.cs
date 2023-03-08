using System;

namespace TCPServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = MyConsoleUI.ReadInt32("Set Server's port:");
            TcpServerClass.Main(port);

            Console.ReadKey();
        }
    }
}

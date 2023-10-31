using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace WinningNumberSimulator
{
    internal class Program
    {
        const int port = 4948;
        const string ip = "127.0.0.1";
        const string title = "Winning Number Simulator";

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        static void Main(string[] args)
        {
            Console.Title = title;
            Console.ForegroundColor = ConsoleColor.Yellow;
            SetWindowPos(GetConsoleWindow(), IntPtr.Zero, 1300, 400, 600, 200, 0x4 | 0x10);

            while (true)
            {
                Msg("Spin the wheel please - press any key...", true);
                try
                {
                    using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ip, port);
                    var json = JsonConvert.SerializeObject(new
                        {Qualifier = "showWinningNumber", Data = new {WinningNumber = new Random().Next(1, 34)}});
                    var bytes = Encoding.UTF8.GetBytes(json);
                    socket.Send(bytes);
                   Console.Clear();
                }
                catch (Exception ex)
                {
                    Msg($"Exception occurred: {ex.Message}");
                }
            }
        }

        static void Msg(string msg, bool isKey = false)
        {
            Console.WriteLine();
            Console.Write(msg);
            if (isKey)
                Console.ReadKey();
        }
    }
}

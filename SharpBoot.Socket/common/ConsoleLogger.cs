using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.common
{
    public static class ConsoleLogger
    {
        public static void Info(string str)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {str}");
        }
    }
}

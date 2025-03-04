
using SharpBoot.Starter.SuperSockets.attribute;
using System;
using System.Net;
using System.Net.Sockets;

namespace SharpBoot.Sockets.Demo.Server
{

    [EnableSuperSocket]
    public class Program
    {
        public static void Main(string[] args)
        {
            SharpBootApplication.Run<Program>(args);
        }
    }
}

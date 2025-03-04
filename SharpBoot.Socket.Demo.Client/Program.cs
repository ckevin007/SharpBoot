
using SharpBoot.Sockets.Demo.Common.filter;
using SharpBoot.Sockets.Demo.Common.model;
using SuperSocket.Client;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.Demo.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadLine();
        }


        static async Task Run()
        {
            var client = new EasyClient<MyPackageInfo>(new MyPackagePipelineFilter()).AsClient();

            if (!await client.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5600)))
            {
                Console.WriteLine("Failed to connect the target server.");
                return;
            }

            Console.WriteLine("connect success");




            while (true)
            {
                var length = GetRandInt(100, 1024 * 100);
                length = 1024 * 1024 * 1000;
                var buffers = new MyPackageInfo()
                {
                    BodyLength = length,
                    Body = new byte[length],
                }.ToBytes();

                await client.SendAsync(buffers);

                var p = await client.ReceiveAsync();

                if (p == null) // connection dropped
                    break;

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [接收] 字节={p.Body.Length}");

                await Task.Delay(1000);
            }

        }

        static int GetRandInt(int min, int max)
        {
            Guid temp = Guid.NewGuid();
            int guidseed = BitConverter.ToInt32(temp.ToByteArray(), 0);
            Random r = new Random(guidseed);
            return r.Next(min, max);
        }

    }
}

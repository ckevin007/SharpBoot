using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Service;
using SharpBoot.Models;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SharpBoot.Startups
{
    [WebHostBuilderConfiguration]
    public class MyHostBuilderConfigurationer : IWebHostBuilderConfigurationer
    {
        public void BeforeBuild(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, b) =>
            {
                var c = b.Build();
                var tmp = c.GetSection("Nacos");
                Ssl ssl = c.GetSection("SSL").Get<Ssl>();
                if (ssl != null && ssl.Enable)
                {
                    builder.UseKestrel(option =>
                    {
                        option.ConfigureHttpsDefaults(i =>
                        {
                            i.ServerCertificate = GetX509Certificate2(ssl);
                        });
                    });
                }
            });
        }

        private static X509Certificate2 GetX509Certificate2(Ssl ssl)
        {
            string pfxFile = ssl.PfxPath;
            string key = ssl.Key;
            if (string.IsNullOrEmpty(key))
            {
                key = File.ReadAllText(ssl.KeyPath);
            }
            return new X509Certificate2(pfxFile, key);
        }
    }
}

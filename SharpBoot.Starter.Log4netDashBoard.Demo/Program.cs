using SharpBoot.Starter.Log4net.DashBoard.Attributes;

namespace SharpBoot.Starter.Log4netDashBoard.Demo
{
    [EnableLog4netDashBoard]
    public class Program
    {
        static void Main(string[] args)
        {
            SharpBootApplication.Run<Program>(args);
        }
    }
}
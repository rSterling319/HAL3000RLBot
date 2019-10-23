using RLBotDotNet;
using System.IO;

namespace HAL3000
{
    class Program
    {
        static void Main()
        {
            // Read the port from port.cfg.
            const string file = "port.cfg";
            string text = File.ReadAllLines(file)[0];
            int port = int.Parse(text);

            // BotManager is a generic which takes in your bot as its T type.
            BotManager<HAL3000> botManager = new BotManager<HAL3000>(0);
            // Start the server on the port given in the port.cfg file.
            botManager.Start(port);
        }
    }
}

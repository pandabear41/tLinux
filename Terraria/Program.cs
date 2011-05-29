namespace Terraria
{
    using System;
    using System.Threading;

    internal static class Program
    {
        public static Terraria.Main main;

        private static void doUpdate()
        {
            while (true)
            {
                Thread.Sleep(0x11);
                main.Update();
            }
        }

        private static void Main(string[] args)
        {
            main = new Terraria.Main();
            string statusText = "";
            Thread thread = new Thread(new ThreadStart(Program.doUpdate));
            while (true)
            {
                if (Terraria.Main.statusText != statusText)
                {
                    statusText = Terraria.Main.statusText;
                    Console.WriteLine(statusText);
                    if (!(!(Terraria.Main.statusText.Substring(0, 7) == "Waiting") || thread.IsAlive))
                    {
                        Console.WriteLine("Started update thread!");
                        thread.Start();
                    }
                }
            }
        }
    }
}


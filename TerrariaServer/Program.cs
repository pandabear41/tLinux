using System;
namespace Terraria
{
    internal class ProgramServer
    {
        private static Main Game;
        private static void Main(string[] args)
        {
            ProgramServer.Game = new Main();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "-config")
                {
                    i++;
                    ProgramServer.Game.LoadDedConfig(args[i]);
                }
                if (args[i].ToLower() == "-port")
                {
                    i++;
                    try
                    {
                        Netplay.serverPort = System.Convert.ToInt32(args[i]);
                    }
                    catch
                    {
                    }
                }
                if (args[i].ToLower() == "-players" || args[i].ToLower() == "-maxplayers")
                {
                    i++;
                    try
                    {
                        int netPlayers = System.Convert.ToInt32(args[i]);
                        ProgramServer.Game.SetNetPlayers(netPlayers);
                    }
                    catch
                    {
                    }
                }
                if (args[i].ToLower() == "-pass" || args[i].ToLower() == "-password")
                {
                    i++;
                    Netplay.password = args[i];
                }
                if (args[i].ToLower() == "-world" || args[i].ToLower() == "-world")
                {
                    i++;
                    ProgramServer.Game.SetWorld(args[i]);
                }
                if (args[i].ToLower() == "-autoshutdown")
                {
                    ProgramServer.Game.autoShut();
                }
            }
            ProgramServer.Game.DedServ();
        }
    }
}

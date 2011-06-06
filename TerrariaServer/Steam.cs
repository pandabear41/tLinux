using System;
using System.Runtime.InteropServices;
namespace Terraria
{
    public class Steam
    {
        public static bool SteamInit = false;
        public static void Init()
        {
            Steam.SteamInit = Steam.SteamAPI_Init();
        }
        public static void Kill()
        {
            bool flag = Steam.SteamAPI_Shutdown();
        }
        [System.Runtime.InteropServices.DllImport("steam_api.dll")]
        private static extern bool SteamAPI_Init();
        [System.Runtime.InteropServices.DllImport("steam_api.dll")]
        private static extern bool SteamAPI_Shutdown();
    }
}

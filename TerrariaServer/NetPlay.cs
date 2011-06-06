using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Terraria
{
    public class Netplay
    {
        public const int bufferSize = 1024;
        public const int maxConnections = 256;
        public static ClientSock clientSock = new ClientSock();
        public static bool disconnect = false;
        public static string password = "";
        public static IPAddress serverIP;
        public static IPAddress serverListenIP;
        public static int serverPort = 7777;
        public static ServerSock[] serverSock = new ServerSock[256];
        public static bool ServerUp = false;
        public static bool stopListen = false;
        public static TcpListener tcpListener;
        public static Player GetPlayerByName(string name)
        {
            Player player = null;
            int num = 0;
            Player[] player2 = Main.player;
            Player result;
            for (int i = 0; i < player2.Length; i++)
            {
                Player player3 = player2[i];
                if (player3.name.ToLower() == name.ToLower())
                {
                    result = player3;
                    return result;
                }
                if (player3.name.CompareTo(name) > num && player3.name.IndexOf(name) > -1)
                {
                    num = player3.name.CompareTo(name);
                    player = player3;
                }
            }
            result = player;
            return result;
        }
        public static void ClientLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                System.DateTime now = System.DateTime.Now;
                Main.rand = new System.Random((int)now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                System.DateTime now = System.DateTime.Now;
                WorldGen.genRand = new System.Random((int)now.Ticks);
            }
            Main.player[Main.myPlayer].hostile = false;
            Main.clientPlayer = (Player)Main.player[Main.myPlayer].clientClone();
            Main.menuMode = 10;
            Main.menuMode = 14;
            if (!Main.autoPass)
            {
                Main.statusText = "Connecting to " + Netplay.serverIP;
            }
            Main.netMode = 1;
            Netplay.disconnect = false;
            Netplay.clientSock = new ClientSock();
            Netplay.clientSock.tcpClient.NoDelay = true;
            Netplay.clientSock.readBuffer = new byte[1024];
            Netplay.clientSock.writeBuffer = new byte[1024];
            bool flag = true;
            while (flag)
            {
                flag = false;
                try
                {
                    Netplay.clientSock.tcpClient.Connect(Netplay.serverIP, Netplay.serverPort);
                    Netplay.clientSock.networkStream = Netplay.clientSock.tcpClient.GetStream();
                    flag = false;
                }
                catch
                {
                    if (Netplay.disconnect || !Main.gameMenu)
                    {
                        Debug.WriteLine("   Exception normal: Player hit cancel before initiating a connection.");
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            NetMessage.buffer[256].Reset();
            int num = -1;
            while (!Netplay.disconnect)
            {
                if (Netplay.clientSock.tcpClient.Connected)
                {
                    if (NetMessage.buffer[256].checkBytes)
                    {
                        NetMessage.CheckBytes(256);
                    }
                    Netplay.clientSock.active = true;
                    if (Netplay.clientSock.state == 0)
                    {
                        Main.statusText = "Found server";
                        Netplay.clientSock.state = 1;
                        NetMessage.SendData(1, -1, -1, "", 0, 0f, 0f, 0f);
                    }
                    if (Netplay.clientSock.state == 2 && num != Netplay.clientSock.state)
                    {
                        Main.statusText = "Sending player data...";
                    }
                    if (Netplay.clientSock.state == 3 && num != Netplay.clientSock.state)
                    {
                        Main.statusText = "Requesting world information";
                    }
                    if (Netplay.clientSock.state == 4)
                    {
                        WorldGen.worldCleared = false;
                        Netplay.clientSock.state = 5;
                        WorldGen.clearWorld();
                    }
                    if (Netplay.clientSock.state == 5 && WorldGen.worldCleared)
                    {
                        Netplay.clientSock.state = 6;
                        Main.player[Main.myPlayer].FindSpawn();
                        NetMessage.SendData(8, -1, -1, "", Main.player[Main.myPlayer].SpawnX, (float)Main.player[Main.myPlayer].SpawnY, 0f, 0f);
                    }
                    if (Netplay.clientSock.state == 6 && num != Netplay.clientSock.state)
                    {
                        Main.statusText = "Requesting tile data";
                    }
                    if (!Netplay.clientSock.locked && !Netplay.disconnect && Netplay.clientSock.networkStream.DataAvailable)
                    {
                        Netplay.clientSock.locked = true;
                        Netplay.clientSock.networkStream.BeginRead(Netplay.clientSock.readBuffer, 0, Netplay.clientSock.readBuffer.Length, new System.AsyncCallback(Netplay.clientSock.ClientReadCallBack), Netplay.clientSock.networkStream);
                    }
                    if (Netplay.clientSock.statusMax > 0 && Netplay.clientSock.statusText != "")
                    {
                        if (Netplay.clientSock.statusCount >= Netplay.clientSock.statusMax)
                        {
                            Main.statusText = Netplay.clientSock.statusText + ": Complete!";
                            Netplay.clientSock.statusText = "";
                            Netplay.clientSock.statusMax = 0;
                            Netplay.clientSock.statusCount = 0;
                        }
                        else
                        {
                            Main.statusText = string.Concat(new object[]
							{
								Netplay.clientSock.statusText, 
								": ", 
								(int)((float)Netplay.clientSock.statusCount / (float)Netplay.clientSock.statusMax * 100f), 
								"%"
							});
                        }
                    }
                    System.Threading.Thread.Sleep(1);
                }
                else
                {
                    if (Netplay.clientSock.active)
                    {
                        Main.statusText = "Lost connection";
                        Netplay.disconnect = true;
                    }
                }
                num = Netplay.clientSock.state;
            }
            try
            {
                Netplay.clientSock.networkStream.Close();
                Netplay.clientSock.networkStream = Netplay.clientSock.tcpClient.GetStream();
            }
            catch
            {
                Debug.WriteLine("   Exception normal: Redundant closing of the TCP Client and/or Network Stream.");
            }
            if (!Main.gameMenu)
            {
                Main.netMode = 0;
                Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
                Main.gameMenu = true;
                Main.menuMode = 14;
            }
            NetMessage.buffer[256].Reset();
            if (Main.menuMode == 15 && Main.statusText == "Lost connection")
            {
                Main.menuMode = 14;
            }
            if (Netplay.clientSock.statusText != "" && Netplay.clientSock.statusText != null)
            {
                Main.statusText = "Lost connection";
            }
            Netplay.clientSock.statusCount = 0;
            Netplay.clientSock.statusMax = 0;
            Netplay.clientSock.statusText = "";
            Main.netMode = 0;
        }
        public static int GetSectionX(int x)
        {
            return x / 200;
        }
        public static int GetSectionY(int y)
        {
            return y / 150;
        }
        public static void Init()
        {
            for (int i = 0; i < 257; i++)
            {
                if (i < 256)
                {
                    Netplay.serverSock[i] = new ServerSock();
                    Netplay.serverSock[i].tcpClient.NoDelay = true;
                }
                NetMessage.buffer[i] = new messageBuffer();
                NetMessage.buffer[i].whoAmI = i;
            }
            Netplay.clientSock.tcpClient.NoDelay = true;
        }
        public static void ListenForClients(object threadContext)
        {
            while (!Netplay.disconnect && !Netplay.stopListen)
            {
                int num = -1;
                for (int i = 0; i < Main.maxNetPlayers; i++)
                {
                    if (!Netplay.serverSock[i].tcpClient.Connected)
                    {
                        num = i;
                        break;
                    }
                }
                if (num >= 0)
                {
                    try
                    {
                        Netplay.serverSock[num].tcpClient = Netplay.tcpListener.AcceptTcpClient();
                        Netplay.serverSock[num].tcpClient.NoDelay = true;
                        System.Console.WriteLine(Netplay.serverSock[num].tcpClient.Client.RemoteEndPoint + " is connecting...");
                    }
                    catch (System.Exception ex)
                    {
                        if (!Netplay.disconnect)
                        {
                            Main.menuMode = 15;
                            Main.statusText = ex.ToString();
                            Netplay.disconnect = true;
                        }
                        else
                        {
                            Debug.WriteLine("   Exception normal: Server shut down.");
                        }
                    }
                }
                else
                {
                    Netplay.stopListen = true;
                    Netplay.tcpListener.Stop();
                }
            }
        }
        public static void ServerLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                System.DateTime now = System.DateTime.Now;
                Main.rand = new System.Random((int)now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                System.DateTime now = System.DateTime.Now;
                WorldGen.genRand = new System.Random((int)now.Ticks);
            }
            Main.myPlayer = 255;
            Netplay.serverIP = IPAddress.Any;
            Netplay.serverListenIP = Netplay.serverIP;
            Main.menuMode = 14;
            Main.statusText = "Starting server...";
            Main.netMode = 2;
            Netplay.disconnect = false;
            for (int i = 0; i < 256; i++)
            {
                Netplay.serverSock[i] = new ServerSock();
                Netplay.serverSock[i].Reset();
                Netplay.serverSock[i].whoAmI = i;
                Netplay.serverSock[i].tcpClient = new TcpClient();
                Netplay.serverSock[i].tcpClient.NoDelay = true;
                Netplay.serverSock[i].readBuffer = new byte[1024];
                Netplay.serverSock[i].writeBuffer = new byte[1024];
            }
            Netplay.tcpListener = new TcpListener(Netplay.serverListenIP, Netplay.serverPort);
            try
            {
                Netplay.tcpListener.Start();
            }
            catch (System.Exception ex)
            {
                Main.menuMode = 15;
                Main.statusText = ex.ToString();
                Netplay.disconnect = true;
                Debug.WriteLine("   Exception normal: Tried to run two servers on the same PC");
            }
            if (!Netplay.disconnect)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Netplay.ListenForClients), 1);
                Main.statusText = "Server started";
            }
            while (!Netplay.disconnect)
            {
                if (Netplay.stopListen)
                {
                    int num = -1;
                    for (int i = 0; i < 255; i++)
                    {
                        if (!Netplay.serverSock[i].tcpClient.Connected)
                        {
                            num = i;
                            break;
                        }
                    }
                    if (num >= 0)
                    {
                        Netplay.tcpListener.Start();
                        Netplay.stopListen = false;
                        System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Netplay.ListenForClients), 1);
                    }
                }
                int num2 = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (NetMessage.buffer[i].checkBytes)
                    {
                        NetMessage.CheckBytes(i);
                    }
                    if (Netplay.serverSock[i].kill)
                    {
                        Netplay.serverSock[i].Reset();
                        NetMessage.syncPlayers();
                    }
                    else
                    {
                        if (Netplay.serverSock[i].tcpClient.Connected)
                        {
                            if (!Netplay.serverSock[i].active)
                            {
                                Netplay.serverSock[i].state = 0;
                            }
                            Netplay.serverSock[i].active = true;
                            num2++;
                            if (!Netplay.serverSock[i].locked)
                            {
                                try
                                {
                                    Netplay.serverSock[i].networkStream = Netplay.serverSock[i].tcpClient.GetStream();
                                    if (Netplay.serverSock[i].networkStream.DataAvailable)
                                    {
                                        Netplay.serverSock[i].locked = true;
                                        Netplay.serverSock[i].networkStream.BeginRead(Netplay.serverSock[i].readBuffer, 0, Netplay.serverSock[i].readBuffer.Length, new System.AsyncCallback(Netplay.serverSock[i].ServerReadCallBack), Netplay.serverSock[i].networkStream);
                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("   Exception normal: Tried to get data from a client after losing connection");
                                    Netplay.serverSock[i].kill = true;
                                }
                            }
                            if (Netplay.serverSock[i].statusMax > 0 && Netplay.serverSock[i].statusText2 != "")
                            {
                                if (Netplay.serverSock[i].statusCount >= Netplay.serverSock[i].statusMax)
                                {
                                    Netplay.serverSock[i].statusText = string.Concat(new object[]
									{
										"(", 
										Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
										") ", 
										Netplay.serverSock[i].name, 
										" ", 
										Netplay.serverSock[i].statusText2, 
										": Complete!"
									});
                                    Netplay.serverSock[i].statusText2 = "";
                                    Netplay.serverSock[i].statusMax = 0;
                                    Netplay.serverSock[i].statusCount = 0;
                                }
                                else
                                {
                                    Netplay.serverSock[i].statusText = string.Concat(new object[]
									{
										"(", 
										Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
										") ", 
										Netplay.serverSock[i].name, 
										" ", 
										Netplay.serverSock[i].statusText2, 
										": ", 
										(int)((float)Netplay.serverSock[i].statusCount / (float)Netplay.serverSock[i].statusMax * 100f), 
										"%"
									});
                                }
                            }
                            else
                            {
                                if (Netplay.serverSock[i].state == 0)
                                {
                                    Netplay.serverSock[i].statusText = string.Concat(new object[]
									{
										"(", 
										Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
										") ", 
										Netplay.serverSock[i].name, 
										" is connecting..."
									});
                                }
                                else
                                {

                                    if (Netplay.serverSock[i].state == 1)
                                    {
                                        Netplay.serverSock[i].statusText = string.Concat(new object[]
										{
											"(", 
											Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
											") ", 
											Netplay.serverSock[i].name, 
											" is sending player data..."
										});
                                    }
                                    else
                                    {
                                        if (Netplay.serverSock[i].state == 2)
                                        {
                                            Netplay.serverSock[i].statusText = string.Concat(new object[]
											{
												"(", 
												Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
												") ", 
												Netplay.serverSock[i].name, 
												" requested world information"
											});
                                        }
                                        else
                                        {
                                            if (Netplay.serverSock[i].state != 3 && Netplay.serverSock[i].state == 10)
                                            {
                                                Netplay.serverSock[i].statusText = string.Concat(new object[]
												{
													"(", 
													Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
													") ", 
													Netplay.serverSock[i].name, 
													" is playing"
												});
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Netplay.serverSock[i].active)
                            {
                                Netplay.serverSock[i].kill = true;
                            }
                            else
                            {
                                Netplay.serverSock[i].statusText2 = "";
                                if (i < 255)
                                {
                                    Main.player[i].active = false;
                                }
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(1);
                if (!WorldGen.saveLock && !Main.dedServ)
                {
                    if (num2 == 0)
                    {
                        Main.statusText = "Waiting for clients...";
                    }
                    else
                    {
                        Main.statusText = num2 + " clients connected";
                    }
                }
                Netplay.ServerUp = true;
            }
            Netplay.tcpListener.Stop();
            for (int i = 0; i < 256; i++)
            {
                Netplay.serverSock[i].Reset();
            }
            if (Main.menuMode != 15)
            {
                Main.netMode = 0;
                Main.menuMode = 10;
                WorldGen.saveWorld(false);
                while (WorldGen.saveLock)
                {
                }
                Main.menuMode = 0;
            }
            else
            {
                Main.netMode = 0;
            }
            Main.myPlayer = 0;
        }
        public static bool SetIP(string newIP)
        {
            bool result;
            try
            {
                Netplay.serverIP = IPAddress.Parse(newIP);
            }
            catch
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        public static bool SetIP2(string newIP)
        {
            bool result;
            try
            {
                IPAddress[] addressList = Dns.GetHostEntry(newIP).AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (addressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        Netplay.serverIP = addressList[i];
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static void StartClient()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Netplay.ClientLoop), 1);
        }
        public static void StartServer()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Netplay.ServerLoop), 1);
        }
    }
}

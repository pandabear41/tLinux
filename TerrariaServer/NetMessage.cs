using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace Terraria
{
    public class NetMessage
    {
        public static messageBuffer[] buffer = new messageBuffer[257];
        public static System.Collections.ArrayList bans = null;
        public static System.Collections.ArrayList whitelist = null;
        public static void CheckBytes(int i = 256)
        {
            lock (NetMessage.buffer[i])
            {
                int num = 0;
                if (NetMessage.buffer[i].totalData >= 4)
                {
                    if (NetMessage.buffer[i].messageLength == 0)
                    {
                        NetMessage.buffer[i].messageLength = System.BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, 0) + 4;
                    }
                    while (NetMessage.buffer[i].totalData >= NetMessage.buffer[i].messageLength + num && NetMessage.buffer[i].messageLength > 0)
                    {
                        if (!Main.ignoreErrors)
                        {
                            NetMessage.buffer[i].GetData(num + 4, NetMessage.buffer[i].messageLength - 4);
                        }
                        else
                        {
                            try
                            {
                                NetMessage.buffer[i].GetData(num + 4, NetMessage.buffer[i].messageLength - 4);
                            }
                            catch
                            {
                                Debug.WriteLine(string.Concat(new object[]
								{
									"Error: buffer[", 
									i, 
									"].GetData(", 
									num + 4, 
									",", 
									NetMessage.buffer[i].messageLength - 4, 
									")"
								}));
                            }
                        }
                        num += NetMessage.buffer[i].messageLength;
                        if (NetMessage.buffer[i].totalData - num >= 4)
                        {
                            NetMessage.buffer[i].messageLength = System.BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, num) + 4;
                        }
                        else
                        {
                            NetMessage.buffer[i].messageLength = 0;
                        }
                    }
                    if (num == NetMessage.buffer[i].totalData)
                    {
                        NetMessage.buffer[i].totalData = 0;
                    }
                    else
                    {
                        if (num > 0)
                        {
                            System.Buffer.BlockCopy(NetMessage.buffer[i].readBuffer, num, NetMessage.buffer[i].readBuffer, 0, NetMessage.buffer[i].totalData - num);
                            messageBuffer messageBuffer = NetMessage.buffer[i];
                            messageBuffer.totalData -= num;
                        }
                    }
                    NetMessage.buffer[i].checkBytes = false;
                }
            }
        }
        public static bool checkWhitelist(string ip)
        {
            if (NetMessage.whitelist == null)
            {
                NetMessage.loadWhitelist();
            }
            return !(Main.properties["whitelistEnabled"] == "true") || NetMessage.whitelist.Contains(ip);
        }
        public static void loadWhitelist()
        {
            if (!System.IO.File.Exists("whitelist.txt"))
            {
                System.IO.StreamWriter streamWriter = System.IO.File.CreateText("whitelist.txt");
                streamWriter.WriteLine("127.0.0.1");
                streamWriter.WriteLine("localhost");
                streamWriter.Close();
            }
            NetMessage.whitelist = new System.Collections.ArrayList();
            System.IO.StreamReader streamReader = System.IO.File.OpenText("whitelist.txt");
            while (!streamReader.EndOfStream)
            {
                NetMessage.whitelist.Add(streamReader.ReadLine());
            }
            streamReader.Close();
        }
        public static void addWhitelist(string ip)
        {
            NetMessage.whitelist.Add(ip);
        }
        public static void removeWhitelist(string ip)
        {
            if (NetMessage.whitelist.Contains(ip))
            {
                NetMessage.whitelist.Remove(ip);
            }
        }
        public static void saveWhitelist()
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("whitelist.txt");
            foreach (string value in NetMessage.whitelist)
            {
                streamWriter.WriteLine(value);
            }
            streamWriter.Close();
        }
        public static void broadcastMessage(string message)
        {
            for (int i = 0; i < Main.player.GetUpperBound(0); i++)
            {
                NetMessage.SendData(25, i, -1, message, 8, 255f, 240f, 20f);
            }
            System.Console.WriteLine(message);
        }
        public static bool checkBan(string ipaddress)
        {
            if (NetMessage.bans == null)
            {
                NetMessage.bans = new System.Collections.ArrayList();
                NetMessage.loadBans();
            }
            return NetMessage.bans.Contains(ipaddress.Split(new char[]
			{
				':'
			})[0]);
        }
        public static void greetPlayer(int plr)
        {
            string[] array = Main.properties["welcomeMessage"].Split("@@".ToCharArray());
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                NetMessage.SendData(25, plr, -1, text, 255, 255f, 240f, 20f);
            }
            if (Main.properties["advertEnabled"] == "true")
            {
                NetMessage.SendData(25, plr, -1, "Server is powered by tMod ~ tmod.us", 255, 255f, 240f, 20f);
            }
            string text2 = "";
            for (int j = 0; j < 8; j++)
            {
                if (Main.player[j].active)
                {
                    if (text2 == "")
                    {
                        text2 += Main.player[j].name;
                    }
                    else
                    {
                        text2 = text2 + ", " + Main.player[j].name;
                    }
                }
            }
            NetMessage.SendData(25, plr, -1, "Current players: " + text2 + ".", 255, 255f, 240f, 20f);
        }
        public static void loadBans()
        {
            NetMessage.bans.Clear();
            System.IO.StreamReader streamReader = new System.IO.StreamReader("bans.txt");
            while (!streamReader.EndOfStream)
            {
                NetMessage.bans.Add(streamReader.ReadLine());
            }
            streamReader.Close();
        }
        public static void banIP(string ipAddress)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("bans.txt");
            foreach (string value in NetMessage.bans)
            {
                streamWriter.WriteLine(value);
            }
            streamWriter.WriteLine(ipAddress);
            streamWriter.Close();
            NetMessage.bans.Add(ipAddress);
        }
        public static void RecieveBytes(byte[] bytes, int streamLength, int i = 256)
        {
            lock (NetMessage.buffer[i])
            {
                try
                {
                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
                    messageBuffer messageBuffer = NetMessage.buffer[i];
                    messageBuffer.totalData += streamLength;
                    NetMessage.buffer[i].checkBytes = true;
                }
                catch
                {
                    if (Main.netMode == 1)
                    {
                        Debug.WriteLine("    Exception cause: Bad header lead to a read buffer overflow.");
                        Main.menuMode = 15;
                        Main.statusText = "Bad header lead to a read buffer overflow.";
                        Netplay.disconnect = true;
                    }
                    else
                    {
                        Debug.WriteLine("    Exception cause: Bad header lead to a read buffer overflow.");
                        Netplay.serverSock[i].kill = true;
                    }
                }
            }
        }
        public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f)
        {
            int num = 256;
            if (Main.netMode == 2 && remoteClient >= 0)
            {
                num = remoteClient;
            }
            lock (NetMessage.buffer[num])
            {
                int num2 = 5;
                int num3 = num2;
                if (msgType == 1)
                {
                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                    byte[] bytes2 = System.Text.Encoding.ASCII.GetBytes("Terraria" + Main.curRelease);
                    num2 += bytes2.Length;
                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                    System.Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, 5, bytes2.Length);
                }
                else
                {
                    if (msgType == 2)
                    {
                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                        byte[] bytes2 = System.Text.Encoding.ASCII.GetBytes(text);
                        num2 += bytes2.Length;
                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                        System.Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, 5, bytes2.Length);
                        if (Main.dedServ)
                        {
                            System.Console.WriteLine(Netplay.serverSock[num].tcpClient.Client.RemoteEndPoint.ToString() + " was booted: " + text);
                        }
                    }
                    else
                    {
                        if (msgType == 3)
                        {
                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                            byte[] bytes2 = System.BitConverter.GetBytes(remoteClient);
                            num2 += bytes2.Length;
                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                            System.Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, 5, bytes2.Length);
                        }
                        else
                        {
                            if (msgType == 4)
                            {
                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                byte b = (byte)number;
                                byte b2 = (byte)Main.player[(int)b].hair;
                                byte[] bytes3 = System.Text.Encoding.ASCII.GetBytes(text);
                                num2 += 23 + bytes3.Length;
                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                NetMessage.buffer[num].writeBuffer[5] = b;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[6] = b2;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.B;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.R;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.G;
                                num3++;
                                NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.B;
                                num3++;
                                System.Buffer.BlockCopy(bytes3, 0, NetMessage.buffer[num].writeBuffer, num3, bytes3.Length);
                            }
                            else
                            {
                                if (msgType == 5)
                                {
                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                    byte b = (byte)number;
                                    byte b2 = (byte)number2;
                                    byte b3;
                                    if (number2 < 44f)
                                    {
                                        b3 = (byte)Main.player[number].inventory[(int)number2].stack;
                                        if (Main.player[number].inventory[(int)number2].stack < 0)
                                        {
                                            b3 = 0;
                                        }
                                    }
                                    else
                                    {
                                        b3 = (byte)Main.player[number].armor[(int)number2 - 44].stack;
                                        if (Main.player[number].armor[(int)number2 - 44].stack < 0)
                                        {
                                            b3 = 0;
                                        }
                                    }
                                    string text2 = text;
                                    if (text2 == null)
                                    {
                                        text2 = "";
                                    }
                                    byte[] bytes4 = System.Text.Encoding.ASCII.GetBytes(text2);
                                    num2 += 3 + bytes4.Length;
                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                    NetMessage.buffer[num].writeBuffer[5] = b;
                                    num3++;
                                    NetMessage.buffer[num].writeBuffer[6] = b2;
                                    num3++;
                                    NetMessage.buffer[num].writeBuffer[7] = b3;
                                    num3++;
                                    System.Buffer.BlockCopy(bytes4, 0, NetMessage.buffer[num].writeBuffer, num3, bytes4.Length);
                                }
                                else
                                {
                                    if (msgType == 6)
                                    {
                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                    }
                                    else
                                    {
                                        if (msgType == 7)
                                        {
                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                            byte[] bytes2 = System.BitConverter.GetBytes((int)Main.time);
                                            byte b2 = 0;
                                            if (Main.dayTime)
                                            {
                                                b2 = 1;
                                            }
                                            byte b3 = (byte)Main.moonPhase;
                                            byte b4 = 0;
                                            if (Main.bloodMoon)
                                            {
                                                b4 = 1;
                                            }
                                            byte[] bytes4 = System.BitConverter.GetBytes(Main.maxTilesX);
                                            byte[] bytes5 = System.BitConverter.GetBytes(Main.maxTilesY);
                                            byte[] bytes6 = System.BitConverter.GetBytes(Main.spawnTileX);
                                            byte[] bytes7 = System.BitConverter.GetBytes(Main.spawnTileY);
                                            byte[] bytes8 = System.BitConverter.GetBytes((int)Main.worldSurface);
                                            byte[] bytes9 = System.BitConverter.GetBytes((int)Main.rockLayer);
                                            byte[] bytes10 = System.BitConverter.GetBytes(Main.worldID);
                                            byte[] bytes11 = System.Text.Encoding.ASCII.GetBytes(Main.worldName);
                                            num2 += bytes2.Length + 1 + 1 + 1 + bytes4.Length + bytes5.Length + bytes6.Length + bytes7.Length + bytes8.Length + bytes9.Length + bytes10.Length + bytes11.Length;
                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                            System.Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, 5, bytes2.Length);
                                            num3 += bytes2.Length;
                                            NetMessage.buffer[num].writeBuffer[num3] = b2;
                                            num3++;
                                            NetMessage.buffer[num].writeBuffer[num3] = b3;
                                            num3++;
                                            NetMessage.buffer[num].writeBuffer[num3] = b4;
                                            num3++;
                                            System.Buffer.BlockCopy(bytes4, 0, NetMessage.buffer[num].writeBuffer, num3, bytes4.Length);
                                            num3 += bytes4.Length;
                                            System.Buffer.BlockCopy(bytes5, 0, NetMessage.buffer[num].writeBuffer, num3, bytes5.Length);
                                            num3 += bytes5.Length;
                                            System.Buffer.BlockCopy(bytes6, 0, NetMessage.buffer[num].writeBuffer, num3, bytes6.Length);
                                            num3 += bytes6.Length;
                                            System.Buffer.BlockCopy(bytes7, 0, NetMessage.buffer[num].writeBuffer, num3, bytes7.Length);
                                            num3 += bytes7.Length;
                                            System.Buffer.BlockCopy(bytes8, 0, NetMessage.buffer[num].writeBuffer, num3, bytes8.Length);
                                            num3 += bytes8.Length;
                                            System.Buffer.BlockCopy(bytes9, 0, NetMessage.buffer[num].writeBuffer, num3, bytes9.Length);
                                            num3 += bytes9.Length;
                                            System.Buffer.BlockCopy(bytes10, 0, NetMessage.buffer[num].writeBuffer, num3, bytes10.Length);
                                            num3 += bytes10.Length;
                                            System.Buffer.BlockCopy(bytes11, 0, NetMessage.buffer[num].writeBuffer, num3, bytes11.Length);
                                            num3 += bytes11.Length;
                                        }
                                        else
                                        {
                                            if (msgType == 8)
                                            {
                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                byte[] bytes12 = System.BitConverter.GetBytes(number);
                                                byte[] bytes13 = System.BitConverter.GetBytes((int)number2);
                                                num2 += bytes12.Length + bytes13.Length;
                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                System.Buffer.BlockCopy(bytes12, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                num3 += 4;
                                                System.Buffer.BlockCopy(bytes13, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                            }
                                            else
                                            {
                                                if (msgType == 9)
                                                {
                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                    byte[] bytes2 = System.BitConverter.GetBytes(number);
                                                    byte[] bytes14 = System.Text.Encoding.ASCII.GetBytes(text);
                                                    num2 += bytes2.Length + bytes14.Length;
                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                    System.Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                    num3 += 4;
                                                    System.Buffer.BlockCopy(bytes14, 0, NetMessage.buffer[num].writeBuffer, num3, bytes14.Length);
                                                }
                                                else
                                                {
                                                    if (msgType == 10)
                                                    {
                                                        short num4 = (short)number;
                                                        int i = (int)number2;
                                                        int j = (int)number3;
                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(msgType), 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num4), 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                        num3 += 2;
                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(i), 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                        num3 += 4;
                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(j), 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                        num3 += 4;
                                                        for (int k = i; k < i + (int)num4; k++)
                                                        {
                                                            byte b5 = 0;
                                                            if (Main.tile[k, j].active)
                                                            {
                                                                b5 += 1;
                                                            }
                                                            if (Main.tile[k, j].lighted)
                                                            {
                                                                b5 += 2;
                                                            }
                                                            if (Main.tile[k, j].wall > 0)
                                                            {
                                                                b5 += 4;
                                                            }
                                                            if (Main.tile[k, j].liquid > 0)
                                                            {
                                                                b5 += 8;
                                                            }
                                                            NetMessage.buffer[num].writeBuffer[num3] = b5;
                                                            num3++;
                                                            byte[] bytes15 = System.BitConverter.GetBytes(Main.tile[k, j].frameX);
                                                            byte[] bytes16 = System.BitConverter.GetBytes(Main.tile[k, j].frameY);
                                                            byte wall = Main.tile[k, j].wall;
                                                            if (Main.tile[k, j].active)
                                                            {
                                                                NetMessage.buffer[num].writeBuffer[num3] = Main.tile[k, j].type;
                                                                num3++;
                                                                if (Main.tileFrameImportant[(int)Main.tile[k, j].type])
                                                                {
                                                                    System.Buffer.BlockCopy(bytes15, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                    num3 += 2;
                                                                    System.Buffer.BlockCopy(bytes16, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                    num3 += 2;
                                                                }
                                                            }
                                                            if (wall > 0)
                                                            {
                                                                NetMessage.buffer[num].writeBuffer[num3] = wall;
                                                                num3++;
                                                            }
                                                            if (Main.tile[k, j].liquid > 0)
                                                            {
                                                                NetMessage.buffer[num].writeBuffer[num3] = Main.tile[k, j].liquid;
                                                                num3++;
                                                                byte b6 = 0;
                                                                if (Main.tile[k, j].lava)
                                                                {
                                                                    b6 = 1;
                                                                }
                                                                NetMessage.buffer[num].writeBuffer[num3] = b6;
                                                                num3++;
                                                            }
                                                        }
                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num3 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                        num2 = num3;
                                                    }
                                                    else
                                                    {
                                                        if (msgType == 11)
                                                        {
                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                            byte[] bytes17 = System.BitConverter.GetBytes(number);
                                                            byte[] bytes18 = System.BitConverter.GetBytes((int)number2);
                                                            byte[] bytes19 = System.BitConverter.GetBytes((int)number3);
                                                            byte[] bytes20 = System.BitConverter.GetBytes((int)number4);
                                                            num2 += bytes17.Length + bytes18.Length + bytes19.Length + bytes20.Length;
                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                            System.Buffer.BlockCopy(bytes17, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                            num3 += 4;
                                                            System.Buffer.BlockCopy(bytes18, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                            num3 += 4;
                                                            System.Buffer.BlockCopy(bytes19, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                            num3 += 4;
                                                            System.Buffer.BlockCopy(bytes20, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                            num3 += 4;
                                                        }
                                                        else
                                                        {
                                                            if (msgType == 12)
                                                            {
                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                byte b7 = (byte)number;
                                                                byte[] bytes21 = System.BitConverter.GetBytes(Main.player[(int)b7].SpawnX);
                                                                byte[] bytes22 = System.BitConverter.GetBytes(Main.player[(int)b7].SpawnY);
                                                                num2 += 1 + bytes21.Length + bytes22.Length;
                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                num3++;
                                                                System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                num3 += 4;
                                                                System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                num3 += 4;
                                                            }
                                                            else
                                                            {
                                                                if (msgType == 13)
                                                                {
                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                    byte b7 = (byte)number;
                                                                    byte b8 = 0;
                                                                    if (Main.player[(int)b7].controlUp)
                                                                    {
                                                                        b8 += 1;
                                                                    }
                                                                    if (Main.player[(int)b7].controlDown)
                                                                    {
                                                                        b8 += 2;
                                                                    }
                                                                    if (Main.player[(int)b7].controlLeft)
                                                                    {
                                                                        b8 += 4;
                                                                    }
                                                                    if (Main.player[(int)b7].controlRight)
                                                                    {
                                                                        b8 += 8;
                                                                    }
                                                                    if (Main.player[(int)b7].controlJump)
                                                                    {
                                                                        b8 += 16;
                                                                    }
                                                                    if (Main.player[(int)b7].controlUseItem)
                                                                    {
                                                                        b8 += 32;
                                                                    }
                                                                    if (Main.player[(int)b7].direction == 1)
                                                                    {
                                                                        b8 += 64;
                                                                    }
                                                                    byte b9 = (byte)Main.player[(int)b7].selectedItem;
                                                                    byte[] bytes21 = System.BitConverter.GetBytes(Main.player[number].position.X);
                                                                    byte[] bytes22 = System.BitConverter.GetBytes(Main.player[number].position.Y);
                                                                    byte[] bytes23 = System.BitConverter.GetBytes(Main.player[number].velocity.X);
                                                                    byte[] bytes24 = System.BitConverter.GetBytes(Main.player[number].velocity.Y);
                                                                    num2 += 3 + bytes21.Length + bytes22.Length + bytes23.Length + bytes24.Length;
                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                    NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                    num3++;
                                                                    NetMessage.buffer[num].writeBuffer[6] = b8;
                                                                    num3++;
                                                                    NetMessage.buffer[num].writeBuffer[7] = b9;
                                                                    num3++;
                                                                    System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                    num3 += 4;
                                                                    System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                    num3 += 4;
                                                                    System.Buffer.BlockCopy(bytes23, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                    num3 += 4;
                                                                    System.Buffer.BlockCopy(bytes24, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                }
                                                                else
                                                                {
                                                                    if (msgType == 14)
                                                                    {
                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                        byte b7 = (byte)number;
                                                                        byte b10 = (byte)number2;
                                                                        num2 += 2;
                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                        NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                        NetMessage.buffer[num].writeBuffer[6] = b10;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (msgType == 15)
                                                                        {
                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (msgType == 16)
                                                                            {
                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                byte b7 = (byte)number;
                                                                                byte[] bytes25 = System.BitConverter.GetBytes((short)Main.player[(int)b7].statLife);
                                                                                byte[] bytes26 = System.BitConverter.GetBytes((short)Main.player[(int)b7].statLifeMax);
                                                                                num2 += 1 + bytes25.Length + bytes26.Length;
                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                                num3++;
                                                                                System.Buffer.BlockCopy(bytes25, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                num3 += 2;
                                                                                System.Buffer.BlockCopy(bytes26, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (msgType == 17)
                                                                                {
                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                    byte b11 = (byte)number;
                                                                                    byte[] bytes21 = System.BitConverter.GetBytes((int)number2);
                                                                                    byte[] bytes22 = System.BitConverter.GetBytes((int)number3);
                                                                                    byte b12 = (byte)number4;
                                                                                    num2 += 1 + bytes21.Length + bytes22.Length + 1;
                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b11;
                                                                                    num3++;
                                                                                    System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                    num3 += 4;
                                                                                    System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                    num3 += 4;
                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b12;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (msgType == 18)
                                                                                    {
                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                        byte[] bytes2 = System.BitConverter.GetBytes((int)Main.time);
                                                                                        byte b13 = 0;
                                                                                        if (Main.dayTime)
                                                                                        {
                                                                                            b13 = 1;
                                                                                        }
                                                                                        byte[] bytes27 = System.BitConverter.GetBytes((int)Main.time);
                                                                                        byte[] bytes28 = System.BitConverter.GetBytes(Main.sunModY);
                                                                                        byte[] bytes29 = System.BitConverter.GetBytes(Main.moonModY);
                                                                                        num2 += 1 + bytes27.Length + bytes28.Length + bytes29.Length;
                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b13;
                                                                                        num3++;
                                                                                        System.Buffer.BlockCopy(bytes27, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                        num3 += 4;
                                                                                        System.Buffer.BlockCopy(bytes28, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                        num3 += 2;
                                                                                        System.Buffer.BlockCopy(bytes29, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                        num3 += 2;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (msgType == 19)
                                                                                        {
                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                            byte b11 = (byte)number;
                                                                                            byte[] bytes21 = System.BitConverter.GetBytes((int)number2);
                                                                                            byte[] bytes22 = System.BitConverter.GetBytes((int)number3);
                                                                                            byte b14 = 0;
                                                                                            if (number4 == 1f)
                                                                                            {
                                                                                                b14 = 1;
                                                                                            }
                                                                                            num2 += 1 + bytes21.Length + bytes22.Length + 1;
                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                            NetMessage.buffer[num].writeBuffer[num3] = b11;
                                                                                            num3++;
                                                                                            System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                            num3 += 4;
                                                                                            System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                            num3 += 4;
                                                                                            NetMessage.buffer[num].writeBuffer[num3] = b14;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (msgType == 20)
                                                                                            {
                                                                                                short num4 = (short)number;
                                                                                                int i = (int)number2;
                                                                                                int l = (int)number3;
                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(msgType), 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num4), 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                num3 += 2;
                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(i), 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                                num3 += 4;
                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(l), 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                                num3 += 4;
                                                                                                for (int k = i; k < i + (int)num4; k++)
                                                                                                {
                                                                                                    for (int j = l; j < l + (int)num4; j++)
                                                                                                    {
                                                                                                        byte b5 = 0;
                                                                                                        if (Main.tile[k, j].active)
                                                                                                        {
                                                                                                            b5 += 1;
                                                                                                        }
                                                                                                        if (Main.tile[k, j].lighted)
                                                                                                        {
                                                                                                            b5 += 2;
                                                                                                        }
                                                                                                        if (Main.tile[k, j].wall > 0)
                                                                                                        {
                                                                                                            b5 += 4;
                                                                                                        }
                                                                                                        if (Main.tile[k, j].liquid > 0 && Main.netMode == 2)
                                                                                                        {
                                                                                                            b5 += 8;
                                                                                                        }
                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b5;
                                                                                                        num3++;
                                                                                                        byte[] bytes15 = System.BitConverter.GetBytes(Main.tile[k, j].frameX);
                                                                                                        byte[] bytes16 = System.BitConverter.GetBytes(Main.tile[k, j].frameY);
                                                                                                        byte wall = Main.tile[k, j].wall;
                                                                                                        if (Main.tile[k, j].active)
                                                                                                        {
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = Main.tile[k, j].type;
                                                                                                            num3++;
                                                                                                            if (Main.tileFrameImportant[(int)Main.tile[k, j].type])
                                                                                                            {
                                                                                                                System.Buffer.BlockCopy(bytes15, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                num3 += 2;
                                                                                                                System.Buffer.BlockCopy(bytes16, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                num3 += 2;
                                                                                                            }
                                                                                                        }
                                                                                                        if (wall > 0)
                                                                                                        {
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = wall;
                                                                                                            num3++;
                                                                                                        }
                                                                                                        if (Main.tile[k, j].liquid > 0 && Main.netMode == 2)
                                                                                                        {
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = Main.tile[k, j].liquid;
                                                                                                            num3++;
                                                                                                            byte b6 = 0;
                                                                                                            if (Main.tile[k, j].lava)
                                                                                                            {
                                                                                                                b6 = 1;
                                                                                                            }
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = b6;
                                                                                                            num3++;
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num3 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                num2 = num3;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (msgType == 21)
                                                                                                {
                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                    byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                    byte[] bytes21 = System.BitConverter.GetBytes(Main.item[number].position.X);
                                                                                                    byte[] bytes22 = System.BitConverter.GetBytes(Main.item[number].position.Y);
                                                                                                    byte[] bytes23 = System.BitConverter.GetBytes(Main.item[number].velocity.X);
                                                                                                    byte[] bytes24 = System.BitConverter.GetBytes(Main.item[number].velocity.Y);
                                                                                                    byte b15 = (byte)Main.item[number].stack;
                                                                                                    string text3 = "0";
                                                                                                    if (Main.item[number].active && Main.item[number].stack > 0)
                                                                                                    {
                                                                                                        text3 = Main.item[number].name;
                                                                                                    }
                                                                                                    if (text3 == null)
                                                                                                    {
                                                                                                        text3 = "0";
                                                                                                    }
                                                                                                    byte[] bytes31 = System.Text.Encoding.ASCII.GetBytes(text3);
                                                                                                    num2 += bytes30.Length + bytes21.Length + bytes22.Length + bytes23.Length + bytes24.Length + 1 + bytes31.Length;
                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                    System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                    num3 += 2;
                                                                                                    System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                    num3 += 4;
                                                                                                    System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                    num3 += 4;
                                                                                                    System.Buffer.BlockCopy(bytes23, 0, NetMessage.buffer[num].writeBuffer, num3, bytes23.Length);
                                                                                                    num3 += 4;
                                                                                                    System.Buffer.BlockCopy(bytes24, 0, NetMessage.buffer[num].writeBuffer, num3, bytes24.Length);
                                                                                                    num3 += 4;
                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b15;
                                                                                                    num3++;
                                                                                                    System.Buffer.BlockCopy(bytes31, 0, NetMessage.buffer[num].writeBuffer, num3, bytes31.Length);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (msgType == 22)
                                                                                                    {
                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                        byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                        byte b16 = (byte)Main.item[number].owner;
                                                                                                        num2 += bytes30.Length + 1;
                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                        System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                        num3 += 2;
                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b16;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (msgType == 23)
                                                                                                        {
                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                            byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                            byte[] bytes21 = System.BitConverter.GetBytes(Main.npc[number].position.X);
                                                                                                            byte[] bytes22 = System.BitConverter.GetBytes(Main.npc[number].position.Y);
                                                                                                            byte[] bytes23 = System.BitConverter.GetBytes(Main.npc[number].velocity.X);
                                                                                                            byte[] bytes24 = System.BitConverter.GetBytes(Main.npc[number].velocity.Y);
                                                                                                            byte[] bytes32 = System.BitConverter.GetBytes((short)Main.npc[number].target);
                                                                                                            byte[] bytes25 = System.BitConverter.GetBytes((short)Main.npc[number].life);
                                                                                                            if (!Main.npc[number].active)
                                                                                                            {
                                                                                                                bytes25 = System.BitConverter.GetBytes(0);
                                                                                                            }
                                                                                                            byte[] bytes33 = System.Text.Encoding.ASCII.GetBytes(Main.npc[number].name);
                                                                                                            num2 += bytes30.Length + bytes21.Length + bytes22.Length + bytes23.Length + bytes24.Length + bytes32.Length + bytes25.Length + NPC.maxAI * 4 + bytes33.Length + 1 + 1;
                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                            System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                            num3 += 2;
                                                                                                            System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                            num3 += 4;
                                                                                                            System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                            num3 += 4;
                                                                                                            System.Buffer.BlockCopy(bytes23, 0, NetMessage.buffer[num].writeBuffer, num3, bytes23.Length);
                                                                                                            num3 += 4;
                                                                                                            System.Buffer.BlockCopy(bytes24, 0, NetMessage.buffer[num].writeBuffer, num3, bytes24.Length);
                                                                                                            num3 += 4;
                                                                                                            System.Buffer.BlockCopy(bytes32, 0, NetMessage.buffer[num].writeBuffer, num3, bytes32.Length);
                                                                                                            num3 += 2;
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].direction + 1);
                                                                                                            num3++;
                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].directionY + 1);
                                                                                                            num3++;
                                                                                                            System.Buffer.BlockCopy(bytes25, 0, NetMessage.buffer[num].writeBuffer, num3, bytes25.Length);
                                                                                                            num3 += 2;
                                                                                                            for (int l = 0; l < NPC.maxAI; l++)
                                                                                                            {
                                                                                                                byte[] bytes34 = System.BitConverter.GetBytes(Main.npc[number].ai[l]);
                                                                                                                System.Buffer.BlockCopy(bytes34, 0, NetMessage.buffer[num].writeBuffer, num3, bytes34.Length);
                                                                                                                num3 += 4;
                                                                                                            }
                                                                                                            System.Buffer.BlockCopy(bytes33, 0, NetMessage.buffer[num].writeBuffer, num3, bytes33.Length);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (msgType == 24)
                                                                                                            {
                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                                byte b7 = (byte)number2;
                                                                                                                num2 += bytes30.Length + 1;
                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                                num3 += 2;
                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (msgType == 25)
                                                                                                                {
                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                    byte b7 = (byte)number;
                                                                                                                    byte[] bytes35 = System.Text.Encoding.ASCII.GetBytes(text);
                                                                                                                    byte b17 = (byte)number2;
                                                                                                                    byte b18 = (byte)number3;
                                                                                                                    byte b19 = (byte)number4;
                                                                                                                    num2 += 1 + bytes35.Length + 3;
                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                    num3++;
                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b17;
                                                                                                                    num3++;
                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b18;
                                                                                                                    num3++;
                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b19;
                                                                                                                    num3++;
                                                                                                                    System.Buffer.BlockCopy(bytes35, 0, NetMessage.buffer[num].writeBuffer, num3, bytes35.Length);
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (msgType == 26)
                                                                                                                    {
                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                        byte b7 = (byte)number;
                                                                                                                        byte b14 = (byte)(number2 + 1f);
                                                                                                                        byte[] bytes36 = System.BitConverter.GetBytes((short)number3);
                                                                                                                        byte b20 = (byte)number4;
                                                                                                                        num2 += 2 + bytes36.Length + 1;
                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                        num3++;
                                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b14;
                                                                                                                        num3++;
                                                                                                                        System.Buffer.BlockCopy(bytes36, 0, NetMessage.buffer[num].writeBuffer, num3, bytes36.Length);
                                                                                                                        num3 += 2;
                                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b20;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (msgType == 27)
                                                                                                                        {
                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                            byte[] bytes30 = System.BitConverter.GetBytes((short)Main.projectile[number].identity);
                                                                                                                            byte[] bytes21 = System.BitConverter.GetBytes(Main.projectile[number].position.X);
                                                                                                                            byte[] bytes22 = System.BitConverter.GetBytes(Main.projectile[number].position.Y);
                                                                                                                            byte[] bytes23 = System.BitConverter.GetBytes(Main.projectile[number].velocity.X);
                                                                                                                            byte[] bytes24 = System.BitConverter.GetBytes(Main.projectile[number].velocity.Y);
                                                                                                                            byte[] bytes37 = System.BitConverter.GetBytes(Main.projectile[number].knockBack);
                                                                                                                            byte[] bytes36 = System.BitConverter.GetBytes((short)Main.projectile[number].damage);
                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                            System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                                            num3 += 2;
                                                                                                                            System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                                            num3 += 4;
                                                                                                                            System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                                            num3 += 4;
                                                                                                                            System.Buffer.BlockCopy(bytes23, 0, NetMessage.buffer[num].writeBuffer, num3, bytes23.Length);
                                                                                                                            num3 += 4;
                                                                                                                            System.Buffer.BlockCopy(bytes24, 0, NetMessage.buffer[num].writeBuffer, num3, bytes24.Length);
                                                                                                                            num3 += 4;
                                                                                                                            System.Buffer.BlockCopy(bytes37, 0, NetMessage.buffer[num].writeBuffer, num3, bytes37.Length);
                                                                                                                            num3 += 4;
                                                                                                                            System.Buffer.BlockCopy(bytes36, 0, NetMessage.buffer[num].writeBuffer, num3, bytes36.Length);
                                                                                                                            num3 += 2;
                                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.projectile[number].owner;
                                                                                                                            num3++;
                                                                                                                            NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.projectile[number].type;
                                                                                                                            num3++;
                                                                                                                            for (int l = 0; l < Projectile.maxAI; l++)
                                                                                                                            {
                                                                                                                                byte[] bytes34 = System.BitConverter.GetBytes(Main.projectile[number].ai[l]);
                                                                                                                                System.Buffer.BlockCopy(bytes34, 0, NetMessage.buffer[num].writeBuffer, num3, bytes34.Length);
                                                                                                                                num3 += 4;
                                                                                                                            }
                                                                                                                            num2 += num3;
                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (msgType == 28)
                                                                                                                            {
                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                                                byte[] bytes36 = System.BitConverter.GetBytes((short)number2);
                                                                                                                                byte[] bytes37 = System.BitConverter.GetBytes(number3);
                                                                                                                                byte b21 = (byte)(number4 + 1f);
                                                                                                                                num2 += bytes30.Length + bytes36.Length + bytes37.Length + 1;
                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                                                num3 += 2;
                                                                                                                                System.Buffer.BlockCopy(bytes36, 0, NetMessage.buffer[num].writeBuffer, num3, bytes36.Length);
                                                                                                                                num3 += 2;
                                                                                                                                System.Buffer.BlockCopy(bytes37, 0, NetMessage.buffer[num].writeBuffer, num3, bytes37.Length);
                                                                                                                                num3 += 4;
                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b21;
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (msgType == 29)
                                                                                                                                {
                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                    byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                                                    byte b16 = (byte)number2;
                                                                                                                                    num2 += bytes30.Length + 1;
                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                    System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                                                    num3 += 2;
                                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b16;
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (msgType == 30)
                                                                                                                                    {
                                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                        byte b7 = (byte)number;
                                                                                                                                        byte b22 = 0;
                                                                                                                                        if (Main.player[(int)b7].hostile)
                                                                                                                                        {
                                                                                                                                            b22 = 1;
                                                                                                                                        }
                                                                                                                                        num2 += 2;
                                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                                        num3++;
                                                                                                                                        NetMessage.buffer[num].writeBuffer[num3] = b22;
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (msgType == 31)
                                                                                                                                        {
                                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                            byte[] bytes21 = System.BitConverter.GetBytes(number);
                                                                                                                                            byte[] bytes22 = System.BitConverter.GetBytes((int)number2);
                                                                                                                                            num2 += bytes21.Length + bytes22.Length;
                                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                            System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                                                            num3 += 4;
                                                                                                                                            System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (msgType == 32)
                                                                                                                                            {
                                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                byte[] bytes38 = System.BitConverter.GetBytes((short)number);
                                                                                                                                                byte b23 = (byte)number2;
                                                                                                                                                byte b15 = (byte)Main.chest[number].item[(int)number2].stack;
                                                                                                                                                byte[] bytes39;
                                                                                                                                                if (Main.chest[number].item[(int)number2].name == null)
                                                                                                                                                {
                                                                                                                                                    bytes39 = System.Text.Encoding.ASCII.GetBytes("");
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    bytes39 = System.Text.Encoding.ASCII.GetBytes(Main.chest[number].item[(int)number2].name);
                                                                                                                                                }
                                                                                                                                                num2 += bytes38.Length + 1 + 1 + bytes39.Length;
                                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                System.Buffer.BlockCopy(bytes38, 0, NetMessage.buffer[num].writeBuffer, num3, bytes38.Length);
                                                                                                                                                num3 += 2;
                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b23;
                                                                                                                                                num3++;
                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b15;
                                                                                                                                                num3++;
                                                                                                                                                System.Buffer.BlockCopy(bytes39, 0, NetMessage.buffer[num].writeBuffer, num3, bytes39.Length);
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (msgType == 33)
                                                                                                                                                {
                                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                    byte[] bytes38 = System.BitConverter.GetBytes((short)number);
                                                                                                                                                    byte[] bytes21;
                                                                                                                                                    byte[] bytes22;
                                                                                                                                                    if (number > -1)
                                                                                                                                                    {
                                                                                                                                                        bytes21 = System.BitConverter.GetBytes(Main.chest[number].x);
                                                                                                                                                        bytes22 = System.BitConverter.GetBytes(Main.chest[number].y);
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        bytes21 = System.BitConverter.GetBytes(0);
                                                                                                                                                        bytes22 = System.BitConverter.GetBytes(0);
                                                                                                                                                    }
                                                                                                                                                    num2 += bytes38.Length + bytes21.Length + bytes22.Length;
                                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                    System.Buffer.BlockCopy(bytes38, 0, NetMessage.buffer[num].writeBuffer, num3, bytes38.Length);
                                                                                                                                                    num3 += 2;
                                                                                                                                                    System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                                                                    num3 += 4;
                                                                                                                                                    System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (msgType == 34)
                                                                                                                                                    {
                                                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                        byte[] bytes21 = System.BitConverter.GetBytes(number);
                                                                                                                                                        byte[] bytes22 = System.BitConverter.GetBytes((int)number2);
                                                                                                                                                        num2 += bytes21.Length + bytes22.Length;
                                                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                        System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                                                                        num3 += 4;
                                                                                                                                                        System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (msgType == 35)
                                                                                                                                                        {
                                                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                            byte b7 = (byte)number;
                                                                                                                                                            byte[] bytes40 = System.BitConverter.GetBytes((short)number2);
                                                                                                                                                            num2 += 1 + bytes40.Length;
                                                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                            NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                                                                                                            num3++;
                                                                                                                                                            System.Buffer.BlockCopy(bytes40, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (msgType == 36)
                                                                                                                                                            {
                                                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                byte b7 = (byte)number;
                                                                                                                                                                byte b24 = 0;
                                                                                                                                                                if (Main.player[(int)b7].zoneEvil)
                                                                                                                                                                {
                                                                                                                                                                    b24 = 1;
                                                                                                                                                                }
                                                                                                                                                                byte b25 = 0;
                                                                                                                                                                if (Main.player[(int)b7].zoneMeteor)
                                                                                                                                                                {
                                                                                                                                                                    b25 = 1;
                                                                                                                                                                }
                                                                                                                                                                byte b26 = 0;
                                                                                                                                                                if (Main.player[(int)b7].zoneDungeon)
                                                                                                                                                                {
                                                                                                                                                                    b26 = 1;
                                                                                                                                                                }
                                                                                                                                                                byte b27 = 0;
                                                                                                                                                                if (Main.player[(int)b7].zoneJungle)
                                                                                                                                                                {
                                                                                                                                                                    b27 = 1;
                                                                                                                                                                }
                                                                                                                                                                num2 += 4;
                                                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                                                                num3++;
                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b24;
                                                                                                                                                                num3++;
                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b25;
                                                                                                                                                                num3++;
                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b26;
                                                                                                                                                                num3++;
                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b27;
                                                                                                                                                                num3++;
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (msgType == 37)
                                                                                                                                                                {
                                                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (msgType == 38)
                                                                                                                                                                    {
                                                                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                        byte[] bytes41 = System.Text.Encoding.ASCII.GetBytes(text);
                                                                                                                                                                        num2 += bytes41.Length;
                                                                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                        System.Buffer.BlockCopy(bytes41, 0, NetMessage.buffer[num].writeBuffer, num3, bytes41.Length);
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (msgType == 39)
                                                                                                                                                                        {
                                                                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                            byte[] bytes30 = System.BitConverter.GetBytes((short)number);
                                                                                                                                                                            num2 += bytes30.Length;
                                                                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                            System.Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, bytes30.Length);
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if (msgType == 40)
                                                                                                                                                                            {
                                                                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                byte b7 = (byte)number;
                                                                                                                                                                                byte[] bytes42 = System.BitConverter.GetBytes((short)Main.player[(int)b7].talkNPC);
                                                                                                                                                                                num2 += 1 + bytes42.Length;
                                                                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                                                                                num3++;
                                                                                                                                                                                System.Buffer.BlockCopy(bytes42, 0, NetMessage.buffer[num].writeBuffer, num3, bytes42.Length);
                                                                                                                                                                                num3 += 2;
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (msgType == 41)
                                                                                                                                                                                {
                                                                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                    byte b7 = (byte)number;
                                                                                                                                                                                    byte[] bytes43 = System.BitConverter.GetBytes(Main.player[(int)b7].itemRotation);
                                                                                                                                                                                    byte[] bytes44 = System.BitConverter.GetBytes((short)Main.player[(int)b7].itemAnimation);
                                                                                                                                                                                    num2 += 1 + bytes43.Length + bytes44.Length;
                                                                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                                                                                    num3++;
                                                                                                                                                                                    System.Buffer.BlockCopy(bytes43, 0, NetMessage.buffer[num].writeBuffer, num3, bytes43.Length);
                                                                                                                                                                                    num3 += 4;
                                                                                                                                                                                    System.Buffer.BlockCopy(bytes44, 0, NetMessage.buffer[num].writeBuffer, num3, bytes44.Length);
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if (msgType == 42)
                                                                                                                                                                                    {
                                                                                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                        byte b7 = (byte)number;
                                                                                                                                                                                        byte[] bytes45 = System.BitConverter.GetBytes((short)Main.player[(int)b7].statMana);
                                                                                                                                                                                        byte[] bytes46 = System.BitConverter.GetBytes((short)Main.player[(int)b7].statManaMax);
                                                                                                                                                                                        num2 += 1 + bytes45.Length + bytes46.Length;
                                                                                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                        NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                                                                                                                                        num3++;
                                                                                                                                                                                        System.Buffer.BlockCopy(bytes45, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                                                                                        num3 += 2;
                                                                                                                                                                                        System.Buffer.BlockCopy(bytes46, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (msgType == 43)
                                                                                                                                                                                        {
                                                                                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                            byte b7 = (byte)number;
                                                                                                                                                                                            byte[] bytes47 = System.BitConverter.GetBytes((short)number2);
                                                                                                                                                                                            num2 += 1 + bytes47.Length;
                                                                                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                            NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                                                                                                                                            num3++;
                                                                                                                                                                                            System.Buffer.BlockCopy(bytes47, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if (msgType == 44)
                                                                                                                                                                                            {
                                                                                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                byte b7 = (byte)number;
                                                                                                                                                                                                byte b14 = (byte)(number2 + 1f);
                                                                                                                                                                                                byte[] bytes36 = System.BitConverter.GetBytes((short)number3);
                                                                                                                                                                                                byte b20 = (byte)number4;
                                                                                                                                                                                                num2 += 2 + bytes36.Length + 1;
                                                                                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b7;
                                                                                                                                                                                                num3++;
                                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b14;
                                                                                                                                                                                                num3++;
                                                                                                                                                                                                System.Buffer.BlockCopy(bytes36, 0, NetMessage.buffer[num].writeBuffer, num3, bytes36.Length);
                                                                                                                                                                                                num3 += 2;
                                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b20;
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (msgType == 45)
                                                                                                                                                                                                {
                                                                                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                    byte b7 = (byte)number;
                                                                                                                                                                                                    byte b28 = (byte)Main.player[(int)b7].team;
                                                                                                                                                                                                    num2 += 2;
                                                                                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                    NetMessage.buffer[num].writeBuffer[5] = b7;
                                                                                                                                                                                                    num3++;
                                                                                                                                                                                                    NetMessage.buffer[num].writeBuffer[num3] = b28;
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (msgType == 46)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                        byte[] bytes21 = System.BitConverter.GetBytes(number);
                                                                                                                                                                                                        byte[] bytes22 = System.BitConverter.GetBytes((int)number2);
                                                                                                                                                                                                        num2 += bytes21.Length + bytes22.Length;
                                                                                                                                                                                                        System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                        System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                        System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
                                                                                                                                                                                                        num3 += 4;
                                                                                                                                                                                                        System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (msgType == 47)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                            byte[] bytes48 = System.BitConverter.GetBytes((short)number);
                                                                                                                                                                                                            byte[] bytes49 = System.BitConverter.GetBytes(Main.sign[number].x);
                                                                                                                                                                                                            byte[] bytes50 = System.BitConverter.GetBytes(Main.sign[number].y);
                                                                                                                                                                                                            byte[] bytes51 = System.Text.Encoding.ASCII.GetBytes(Main.sign[number].text);
                                                                                                                                                                                                            num2 += bytes48.Length + bytes49.Length + bytes50.Length + bytes51.Length;
                                                                                                                                                                                                            System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                            System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                            System.Buffer.BlockCopy(bytes48, 0, NetMessage.buffer[num].writeBuffer, num3, bytes48.Length);
                                                                                                                                                                                                            num3 += bytes48.Length;
                                                                                                                                                                                                            System.Buffer.BlockCopy(bytes49, 0, NetMessage.buffer[num].writeBuffer, num3, bytes49.Length);
                                                                                                                                                                                                            num3 += bytes49.Length;
                                                                                                                                                                                                            System.Buffer.BlockCopy(bytes50, 0, NetMessage.buffer[num].writeBuffer, num3, bytes50.Length);
                                                                                                                                                                                                            num3 += bytes50.Length;
                                                                                                                                                                                                            System.Buffer.BlockCopy(bytes51, 0, NetMessage.buffer[num].writeBuffer, num3, bytes51.Length);
                                                                                                                                                                                                            num3 += bytes51.Length;
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (msgType == 48)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                                byte[] bytes21 = System.BitConverter.GetBytes(number);
                                                                                                                                                                                                                byte[] bytes22 = System.BitConverter.GetBytes((int)number2);
                                                                                                                                                                                                                byte liquid = Main.tile[number, (int)number2].liquid;
                                                                                                                                                                                                                byte b6 = 0;
                                                                                                                                                                                                                if (Main.tile[number, (int)number2].lava)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    b6 = 1;
                                                                                                                                                                                                                }
                                                                                                                                                                                                                num2 += bytes21.Length + bytes22.Length + 1 + 1;
                                                                                                                                                                                                                System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                                System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                                System.Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                                                                                                                                                num3 += 4;
                                                                                                                                                                                                                System.Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
                                                                                                                                                                                                                num3 += 4;
                                                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = liquid;
                                                                                                                                                                                                                num3++;
                                                                                                                                                                                                                NetMessage.buffer[num].writeBuffer[num3] = b6;
                                                                                                                                                                                                                num3++;
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (msgType == 49)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    byte[] bytes = System.BitConverter.GetBytes(msgType);
                                                                                                                                                                                                                    System.Buffer.BlockCopy(System.BitConverter.GetBytes(num2 - 4), 0, NetMessage.buffer[num].writeBuffer, 0, 4);
                                                                                                                                                                                                                    System.Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Main.netMode == 1)
                {
                    if (Netplay.clientSock.tcpClient.Connected)
                    {
                        try
                        {
                            messageBuffer messageBuffer = NetMessage.buffer[num];
                            messageBuffer.spamCount++;
                            Netplay.clientSock.networkStream.Write(NetMessage.buffer[num].writeBuffer, 0, num2);
                        }
                        catch
                        {
                            Debug.WriteLine("    Exception normal: Tried to send data to the server after losing connection");
                        }
                    }
                }
                else
                {
                    if (remoteClient == -1)
                    {
                        for (int i = 0; i < 256; i++)
                        {
                            if (i != ignoreClient && (NetMessage.buffer[i].broadcast || (Netplay.serverSock[i].state >= 3 && msgType == 10)) && Netplay.serverSock[i].tcpClient.Connected)
                            {
                                try
                                {
                                    messageBuffer messageBuffer2 = NetMessage.buffer[i];
                                    messageBuffer2.spamCount++;
                                    Netplay.serverSock[i].networkStream.Write(NetMessage.buffer[num].writeBuffer, 0, num2);
                                }
                                catch
                                {
                                    Debug.WriteLine("    Exception normal: Tried to send data to a client after losing connection");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Netplay.serverSock[remoteClient].tcpClient.Connected)
                        {
                            try
                            {
                                messageBuffer messageBuffer3 = NetMessage.buffer[remoteClient];
                                messageBuffer3.spamCount++;
                                Netplay.serverSock[remoteClient].networkStream.Write(NetMessage.buffer[num].writeBuffer, 0, num2);
                            }
                            catch
                            {
                                Debug.WriteLine("    Exception normal: Tried to send data to a client after losing connection");
                            }
                        }
                    }
                }
                if (Main.verboseNetplay)
                {
                    Debug.WriteLine("Sent:");
                    for (int i = 0; i < num2; i++)
                    {
                        Debug.Write(NetMessage.buffer[num].writeBuffer[i] + " ");
                    }
                    Debug.WriteLine("");
                    for (int i = 0; i < num2; i++)
                    {
                        char c = (char)NetMessage.buffer[num].writeBuffer[i];
                        Debug.Write(c);
                    }
                    Debug.WriteLine("");
                    Debug.WriteLine("");
                }
                NetMessage.buffer[num].writeLocked = false;
                if (msgType == 19 && Main.netMode == 1)
                {
                    int size = 5;
                    NetMessage.SendTileSquare(num, (int)number2, (int)number3, size);
                }
                if (msgType == 2 && Main.netMode == 2)
                {
                    Netplay.serverSock[num].kill = true;
                }
            }
        }
        public static void SendSection(int whoAmi, int sectionX, int sectionY)
        {
            try
            {
                if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
                {
                    Netplay.serverSock[whoAmi].tileSection[sectionX, sectionY] = true;
                    int num = sectionX * 200;
                    int num2 = sectionY * 150;
                    for (int i = num2; i < num2 + 150; i++)
                    {
                        NetMessage.SendData(10, whoAmi, -1, "", 200, (float)num, (float)i, 0f);
                    }
                }
            }
            catch
            {
            }
        }
        public static void SendTileSquare(int whoAmi, int tileX, int tileY, int size)
        {
            int num = (size - 1) / 2;
            NetMessage.SendData(20, whoAmi, -1, "", size, (float)(tileX - num), (float)(tileY - num), 0f);
        }
        public static void sendWater(int x, int y)
        {
            if (Main.netMode == 1)
            {
                NetMessage.SendData(48, -1, -1, "", x, (float)y, 0f, 0f);
            }
            else
            {
                for (int i = 0; i < 256; i++)
                {
                    if ((NetMessage.buffer[i].broadcast || Netplay.serverSock[i].state >= 3) && Netplay.serverSock[i].tcpClient.Connected)
                    {
                        int num = x / 200;
                        int num2 = y / 150;
                        if (Netplay.serverSock[i].tileSection[num, num2])
                        {
                            NetMessage.SendData(48, i, -1, "", x, (float)y, 0f, 0f);
                        }
                    }
                }
            }
        }
        public static void syncPlayers()
        {
            bool flag = false;
            for (int i = 0; i < 255; i++)
            {
                int num = 0;
                if (Main.player[i].active)
                {
                    num = 1;
                }
                if (Netplay.serverSock[i].state == 10)
                {
                    if (Main.autoShutdown && !flag)
                    {
                        string text = Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint.ToString();
                        string a = text;
                        for (int j = 0; j < text.Length; j++)
                        {
                            if (text.Substring(j, 1) == ":")
                            {
                                a = text.Substring(0, j);
                            }
                        }
                        if (a == "127.0.0.1")
                        {
                            flag = true;
                        }
                    }
                    NetMessage.SendData(14, -1, i, "", i, (float)num, 0f, 0f);
                    NetMessage.SendData(13, -1, i, "", i, 0f, 0f, 0f);
                    NetMessage.SendData(16, -1, i, "", i, 0f, 0f, 0f);
                    NetMessage.SendData(30, -1, i, "", i, 0f, 0f, 0f);
                    NetMessage.SendData(45, -1, i, "", i, 0f, 0f, 0f);
                    NetMessage.SendData(42, -1, i, "", i, 0f, 0f, 0f);
                    NetMessage.SendData(4, -1, i, Main.player[i].name, i, 0f, 0f, 0f);
                    for (int j = 0; j < 44; j++)
                    {
                        NetMessage.SendData(5, -1, i, Main.player[i].inventory[j].name, i, (float)j, 0f, 0f);
                    }
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[0].name, i, 44f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[1].name, i, 45f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[2].name, i, 46f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[3].name, i, 47f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[4].name, i, 48f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[5].name, i, 49f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[6].name, i, 50f, 0f, 0f);
                    NetMessage.SendData(5, -1, i, Main.player[i].armor[7].name, i, 51f, 0f, 0f);
                    if (!Netplay.serverSock[i].announced)
                    {
                        PlayerEvent playerEvent = new PlayerEvent(Main.player[i]);
                        PluginManager.callHook(Hook.PLAYER_JOIN, playerEvent);
                        if (!playerEvent.getState())
                        {
                            NetMessage.SendData(2, i, -1, "Disconnected.", 0, 0f, 0f, 0f);
                            return;
                        }
                        Netplay.serverSock[i].announced = true;
                        NetMessage.SendData(25, -1, i, Main.player[i].name + " has joined.", 255, 255f, 240f, 20f);
                        if (Main.dedServ)
                        {
                            System.Console.WriteLine(Main.player[i].name + " has joined.");
                        }
                    }
                }
                else
                {
                    NetMessage.SendData(14, -1, i, "", i, (float)num, 0f, 0f);
                    if (Netplay.serverSock[i].announced)
                    {
                        Netplay.serverSock[i].announced = false;
                        NetMessage.SendData(25, -1, i, Netplay.serverSock[i].oldName + " has left.", 255, 255f, 240f, 20f);
                        if (Main.dedServ)
                        {
                            System.Console.WriteLine(Netplay.serverSock[i].oldName + " has left.");
                        }
                    }
                }
            }
            if (Main.autoShutdown && !flag)
            {
                WorldGen.saveWorld(false);
                Netplay.disconnect = true;
                return;
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace Terraria
{
    public class messageBuffer
    {
        public const int readBufferMax = 65535;
        public const int writeBufferMax = 65535;
        public bool broadcast = false;
        public bool checkBytes = false;
        public int maxSpam;
        public int messageLength = 0;
        public byte[] readBuffer = new byte[65535];
        public int spamCount = 0;
        public int totalData = 0;
        public int whoAmI;
        public byte[] writeBuffer = new byte[65535];
        public bool writeLocked = false;
        public void GetData(int start, int length)
        {
            if (this.whoAmI < 256)
            {
                Netplay.serverSock[this.whoAmI].timeOut = 0;
            }
            else
            {
                Netplay.clientSock.timeOut = 0;
            }
            byte b = 0;
            int num = 0;
            num = start + 1;
            b = this.readBuffer[start];
            if (Main.netMode == 1 && Netplay.clientSock.statusMax > 0)
            {
                Netplay.clientSock.statusCount++;
            }
            if (Main.verboseNetplay)
            {
                Debug.WriteLine(Main.myPlayer + " Recieve:");
                for (int i = start; i < start + length; i++)
                {
                    Debug.Write(this.readBuffer[i] + " ");
                }
                Debug.WriteLine("");
                for (int i = start; i < start + length; i++)
                {
                    char c = (char)this.readBuffer[i];
                    Debug.Write(c);
                }
                Debug.WriteLine("");
                Debug.WriteLine("");
            }
            if (Main.netMode == 2 && b != 38 && Netplay.serverSock[this.whoAmI].state == -1)
            {
                NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f);
            }
            else
            {
                if (b == 1 && Main.netMode == 2)
                {
                    if (NetMessage.checkBan(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
                    {
                        NetMessage.SendData(2, this.whoAmI, -1, "You are banned from this server.", 0, 0f, 0f, 0f);
                    }
                    else
                    {
                        if (!NetMessage.checkWhitelist(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString().Split(new char[]
						{
							':'
						})[0]))
                        {
                            System.Console.WriteLine("Kicked: " + Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString() + " not on whitelist!");
                            NetMessage.SendData(2, this.whoAmI, -1, "You're not in the whitelist!", 0, 0f, 0f, 0f);
                        }
                        else
                        {
                            if (Netplay.serverSock[this.whoAmI].state == 0)
                            {
                                if (Netplay.password == null || Netplay.password == "")
                                {
                                    Netplay.serverSock[this.whoAmI].state = 1;
                                    NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                }
                                else
                                {
                                    Netplay.serverSock[this.whoAmI].state = -1;
                                    NetMessage.SendData(37, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (b == 2 && Main.netMode == 1)
                    {
                        Netplay.disconnect = true;
                        Main.statusText = System.Text.Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1);
                    }
                    else
                    {
                        if (b == 3 && Main.netMode == 1)
                        {
                            if (Netplay.clientSock.state == 1)
                            {
                                Netplay.clientSock.state = 2;
                            }
                            int num2 = (int)this.readBuffer[start + 1];
                            if (num2 != Main.myPlayer)
                            {
                                Main.player[num2] = (Player)Main.player[Main.myPlayer].Clone();
                                Main.player[Main.myPlayer] = new Player();
                                Main.player[num2].whoAmi = num2;
                                Main.myPlayer = num2;
                            }
                            NetMessage.SendData(4, -1, -1, Main.player[Main.myPlayer].name, Main.myPlayer, 0f, 0f, 0f);
                            NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                            NetMessage.SendData(42, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                            for (int i = 0; i < 44; i++)
                            {
                                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[i].name, Main.myPlayer, (float)i, 0f, 0f);
                            }
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 44f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 45f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 46f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 47f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 48f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 49f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 50f, 0f, 0f);
                            NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 51f, 0f, 0f);
                            NetMessage.SendData(6, -1, -1, "", 0, 0f, 0f, 0f);
                            if (Netplay.clientSock.state == 2)
                            {
                                Netplay.clientSock.state = 3;
                            }
                        }
                        else
                        {
                            switch (b)
                            {
                                case 4:
                                    {
                                        bool flag = false;
                                        int num2 = (int)this.readBuffer[start + 1];
                                        if (Main.netMode == 2)
                                        {
                                            num2 = this.whoAmI;
                                        }
                                        if (num2 != Main.myPlayer)
                                        {
                                            int hair = (int)this.readBuffer[start + 2];
                                            Main.player[num2].hair = hair;
                                            Main.player[num2].whoAmi = num2;
                                            num += 2;
                                            Main.player[num2].hairColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].hairColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].hairColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].skinColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].skinColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].skinColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].eyeColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].eyeColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].eyeColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shirtColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shirtColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shirtColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].underShirtColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].underShirtColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].underShirtColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].pantsColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].pantsColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].pantsColor.B = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shoeColor.R = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shoeColor.G = this.readBuffer[num];
                                            num++;
                                            Main.player[num2].shoeColor.B = this.readBuffer[num];
                                            num++;
                                            string @string = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                            Main.player[num2].name = @string;
                                            if (Main.netMode == 2)
                                            {
                                                if (Netplay.serverSock[this.whoAmI].state < 10)
                                                {
                                                    for (int i = 0; i < 255; i++)
                                                    {
                                                        if (i != num2 && @string == Main.player[i].name && Netplay.serverSock[i].active)
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                }
                                                if (flag)
                                                {
                                                    NetMessage.SendData(2, this.whoAmI, -1, @string + " is already on this server.", 0, 0f, 0f, 0f);
                                                }
                                                else
                                                {
                                                    Netplay.serverSock[this.whoAmI].oldName = @string;
                                                    Netplay.serverSock[this.whoAmI].name = @string;
                                                    NetMessage.SendData(4, -1, this.whoAmI, @string, num2, 0f, 0f, 0f);
                                                }
                                            }
                                        }
                                        return;
                                    }
                                case 5:
                                    {
                                        int num2 = (int)this.readBuffer[start + 1];
                                        if (Main.netMode == 2)
                                        {
                                            num2 = this.whoAmI;
                                        }
                                        if (num2 != Main.myPlayer)
                                        {
                                            int num3 = (int)this.readBuffer[start + 2];
                                            int stack = (int)this.readBuffer[start + 3];
                                            string string2 = System.Text.Encoding.ASCII.GetString(this.readBuffer, start + 4, length - 4);
                                            if (num3 < 44)
                                            {
                                                Main.player[num2].inventory[num3] = new Item();
                                                Main.player[num2].inventory[num3].SetDefaults(string2);
                                                Main.player[num2].inventory[num3].stack = stack;
                                            }
                                            else
                                            {
                                                Main.player[num2].armor[num3 - 44] = new Item();
                                                Main.player[num2].armor[num3 - 44].SetDefaults(string2);
                                                Main.player[num2].armor[num3 - 44].stack = stack;
                                            }
                                            if (Main.netMode == 2 && num2 == this.whoAmI)
                                            {
                                                NetMessage.SendData(5, -1, this.whoAmI, string2, num2, (float)num3, 0f, 0f);
                                            }
                                        }
                                        return;
                                    }
                                case 6:
                                    {
                                        if (Main.netMode == 2)
                                        {
                                            if (Netplay.serverSock[this.whoAmI].state == 1)
                                            {
                                                Netplay.serverSock[this.whoAmI].state = 2;
                                            }
                                            NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                        }
                                        return;
                                    }
                                case 7:
                                    {
                                        if (Main.netMode == 1)
                                        {
                                            Main.time = (double)System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.dayTime = false;
                                            if (this.readBuffer[num] == 1)
                                            {
                                                Main.dayTime = true;
                                            }
                                            num++;
                                            Main.moonPhase = (int)this.readBuffer[num];
                                            num++;
                                            int num4 = (int)this.readBuffer[num];
                                            num++;
                                            if (num4 == 1)
                                            {
                                                Main.bloodMoon = true;
                                            }
                                            else
                                            {
                                                Main.bloodMoon = false;
                                            }
                                            Main.maxTilesX = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.maxTilesY = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.spawnTileX = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.spawnTileY = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.worldSurface = (double)System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.rockLayer = (double)System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.worldID = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            Main.worldName = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                            if (Netplay.clientSock.state == 3)
                                            {
                                                Netplay.clientSock.state = 4;
                                            }
                                        }
                                        return;
                                    }
                                case 8:
                                    {
                                        if (Main.netMode == 2)
                                        {
                                            int num5 = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            int j = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            bool flag2 = true;
                                            if (num5 == -1 || j == -1)
                                            {
                                                flag2 = false;
                                            }
                                            else
                                            {
                                                if (num5 < 10 || num5 > Main.maxTilesX - 10)
                                                {
                                                    flag2 = false;
                                                }
                                                else
                                                {
                                                    if (j < 10 || j > Main.maxTilesY - 10)
                                                    {
                                                        flag2 = false;
                                                    }
                                                }
                                            }
                                            int num6 = 1350;
                                            if (flag2)
                                            {
                                                num6 *= 2;
                                            }
                                            if (Netplay.serverSock[this.whoAmI].state == 2)
                                            {
                                                Netplay.serverSock[this.whoAmI].state = 3;
                                            }
                                            NetMessage.SendData(9, this.whoAmI, -1, "Receiving tile data", num6, 0f, 0f, 0f);
                                            Netplay.serverSock[this.whoAmI].statusText2 = "is receiving tile data";
                                            ServerSock serverSock = Netplay.serverSock[this.whoAmI];
                                            serverSock.statusMax += num6;
                                            int sectionX = Netplay.GetSectionX(Main.spawnTileX);
                                            int sectionY = Netplay.GetSectionY(Main.spawnTileY);
                                            for (int k = sectionX - 2; k < sectionX + 3; k++)
                                            {
                                                for (int l = sectionY - 1; l < sectionY + 2; l++)
                                                {
                                                    NetMessage.SendSection(this.whoAmI, k, l);
                                                }
                                            }
                                            if (flag2)
                                            {
                                                num5 = Netplay.GetSectionX(num5);
                                                j = Netplay.GetSectionY(j);
                                                for (int k = num5 - 2; k < num5 + 3; k++)
                                                {
                                                    for (int l = j - 1; l < j + 2; l++)
                                                    {
                                                        NetMessage.SendSection(this.whoAmI, k, l);
                                                    }
                                                }
                                                NetMessage.SendData(11, this.whoAmI, -1, "", num5 - 2, (float)(j - 1), (float)(num5 + 2), (float)(j + 1));
                                            }
                                            NetMessage.SendData(11, this.whoAmI, -1, "", sectionX - 2, (float)(sectionY - 1), (float)(sectionX + 2), (float)(sectionY + 1));
                                            NetMessage.SendData(49, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                            for (int i = 0; i < 200; i++)
                                            {
                                                if (Main.item[i].active)
                                                {
                                                    NetMessage.SendData(21, this.whoAmI, -1, "", i, 0f, 0f, 0f);
                                                    NetMessage.SendData(22, this.whoAmI, -1, "", i, 0f, 0f, 0f);
                                                }
                                            }
                                            for (int i = 0; i < 1000; i++)
                                            {
                                                if (Main.npc[i].active)
                                                {
                                                    NetMessage.SendData(23, this.whoAmI, -1, "", i, 0f, 0f, 0f);
                                                }
                                            }
                                        }
                                        return;
                                    }
                                case 9:
                                    {
                                        if (Main.netMode == 1)
                                        {
                                            int num7 = System.BitConverter.ToInt32(this.readBuffer, start + 1);
                                            string string3 = System.Text.Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
                                            Netplay.clientSock.statusMax += num7;
                                            Netplay.clientSock.statusText = string3;
                                        }
                                        return;
                                    }
                                case 10:
                                    {
                                        short num8 = System.BitConverter.ToInt16(this.readBuffer, start + 1);
                                        int num9 = System.BitConverter.ToInt32(this.readBuffer, start + 3);
                                        int l = System.BitConverter.ToInt32(this.readBuffer, start + 7);
                                        num = start + 11;
                                        for (int k = num9; k < num9 + (int)num8; k++)
                                        {
                                            if (Main.tile[k, l] == null)
                                            {
                                                Main.tile[k, l] = new Tile();
                                            }
                                            byte b2 = this.readBuffer[num];
                                            num++;
                                            bool active = Main.tile[k, l].active;
                                            if ((b2 & 1) == 1)
                                            {
                                                Main.tile[k, l].active = true;
                                            }
                                            else
                                            {
                                                Main.tile[k, l].active = false;
                                            }
                                            if ((b2 & 2) == 2)
                                            {
                                                Main.tile[k, l].lighted = true;
                                            }
                                            if ((b2 & 4) == 4)
                                            {
                                                Main.tile[k, l].wall = 1;
                                            }
                                            else
                                            {
                                                Main.tile[k, l].wall = 0;
                                            }
                                            if ((b2 & 8) == 8)
                                            {
                                                Main.tile[k, l].liquid = 1;
                                            }
                                            else
                                            {
                                                Main.tile[k, l].liquid = 0;
                                            }
                                            if (Main.tile[k, l].active)
                                            {
                                                int type = (int)Main.tile[k, l].type;
                                                Main.tile[k, l].type = this.readBuffer[num];
                                                num++;
                                                if (Main.tileFrameImportant[(int)Main.tile[k, l].type])
                                                {
                                                    Main.tile[k, l].frameX = System.BitConverter.ToInt16(this.readBuffer, num);
                                                    num += 2;
                                                    Main.tile[k, l].frameY = System.BitConverter.ToInt16(this.readBuffer, num);
                                                    num += 2;
                                                }
                                                else
                                                {
                                                    if (!active || (int)Main.tile[k, l].type != type)
                                                    {
                                                        Main.tile[k, l].frameX = -1;
                                                        Main.tile[k, l].frameY = -1;
                                                    }
                                                }
                                            }
                                            if (Main.tile[k, l].wall > 0)
                                            {
                                                Main.tile[k, l].wall = this.readBuffer[num];
                                                num++;
                                            }
                                            if (Main.tile[k, l].liquid > 0)
                                            {
                                                Main.tile[k, l].liquid = this.readBuffer[num];
                                                num++;
                                                byte b3 = this.readBuffer[num];
                                                num++;
                                                if (b3 == 1)
                                                {
                                                    Main.tile[k, l].lava = true;
                                                }
                                                else
                                                {
                                                    Main.tile[k, l].lava = false;
                                                }
                                            }
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData((int)b, -1, this.whoAmI, "", (int)num8, (float)num9, (float)l, 0f);
                                        }
                                        return;
                                    }
                                case 11:
                                    {
                                        if (Main.netMode == 1)
                                        {
                                            int startX = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 4;
                                            int startY = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 4;
                                            int num9 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 4;
                                            int num10 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 4;
                                            WorldGen.SectionTileFrame(startX, startY, num9, num10);
                                        }
                                        return;
                                    }
                                case 12:
                                    {
                                        int num11 = (int)this.readBuffer[num];
                                        num++;
                                        Main.player[num11].SpawnX = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        Main.player[num11].SpawnY = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        Main.player[num11].Spawn();
                                        if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state >= 3)
                                        {
                                            NetMessage.buffer[this.whoAmI].broadcast = true;
                                            NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                            if (Netplay.serverSock[this.whoAmI].state == 3)
                                            {
                                                Netplay.serverSock[this.whoAmI].state = 10;
                                                NetMessage.greetPlayer(this.whoAmI);
                                                NetMessage.syncPlayers();
                                            }
                                        }
                                        return;
                                    }
                                case 13:
                                    {
                                        int num11 = (int)this.readBuffer[num];
                                        if (Main.netMode == 1 && !Main.player[num11].active)
                                        {
                                            NetMessage.SendData(15, -1, -1, "", 0, 0f, 0f, 0f);
                                        }
                                        num++;
                                        int num12 = (int)this.readBuffer[num];
                                        num++;
                                        int selectedItem = (int)this.readBuffer[num];
                                        num++;
                                        float num13 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float num14 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float x = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float y = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        Main.player[num11].selectedItem = selectedItem;
                                        Main.player[num11].position.X = num13;
                                        Main.player[num11].position.Y = num14;
                                        Main.player[num11].velocity.X = x;
                                        Main.player[num11].velocity.Y = y;
                                        Main.player[num11].oldVelocity = Main.player[num11].velocity;
                                        Main.player[num11].fallStart = (int)(num14 / 16f);
                                        Main.player[num11].controlUp = false;
                                        Main.player[num11].controlDown = false;
                                        Main.player[num11].controlLeft = false;
                                        Main.player[num11].controlRight = false;
                                        Main.player[num11].controlJump = false;
                                        Main.player[num11].controlUseItem = false;
                                        Main.player[num11].direction = -1;
                                        if ((num12 & 1) == 1)
                                        {
                                            Main.player[num11].controlUp = true;
                                        }
                                        if ((num12 & 2) == 2)
                                        {
                                            Main.player[num11].controlDown = true;
                                        }
                                        if ((num12 & 4) == 4)
                                        {
                                            Main.player[num11].controlLeft = true;
                                        }
                                        if ((num12 & 8) == 8)
                                        {
                                            Main.player[num11].controlRight = true;
                                        }
                                        if ((num12 & 16) == 16)
                                        {
                                            Main.player[num11].controlJump = true;
                                        }
                                        if ((num12 & 32) == 32)
                                        {
                                            Main.player[num11].controlUseItem = true;
                                        }
                                        if ((num12 & 64) == 64)
                                        {
                                            Main.player[num11].direction = 1;
                                        }
                                        if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state == 10)
                                        {
                                            NetMessage.SendData(13, -1, this.whoAmI, "", num11, 0f, 0f, 0f);
                                        }
                                        return;
                                    }
                                case 14:
                                    {
                                        if (Main.netMode == 1)
                                        {
                                            int num11 = (int)this.readBuffer[num];
                                            num++;
                                            int num15 = (int)this.readBuffer[num];
                                            if (num15 == 1)
                                            {
                                                if (Main.player[num11].active)
                                                {
                                                    Main.player[num11] = new Player();
                                                }
                                                Main.player[num11].active = true;
                                            }
                                            else
                                            {
                                                Main.player[num11].active = false;
                                            }
                                        }
                                        return;
                                    }
                                case 15:
                                    {
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.syncPlayers();
                                        }
                                        return;
                                    }
                                case 16:
                                    {
                                        int num11 = (int)this.readBuffer[num];
                                        num++;
                                        int num16 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        int statLifeMax = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                        if (Main.netMode == 2)
                                        {
                                            num11 = this.whoAmI;
                                        }
                                        Main.player[num11].statLife = num16;
                                        Main.player[num11].statLifeMax = statLifeMax;
                                        if (Main.player[num11].statLife <= 0)
                                        {
                                            Main.player[num11].dead = true;
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(16, -1, this.whoAmI, "", num11, 0f, 0f, 0f);
                                        }
                                        return;
                                    }
                                case 17:
                                    {
                                        byte b4 = this.readBuffer[num];
                                        num++;
                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        byte b5 = this.readBuffer[num];
                                        bool fail = false;
                                        if (b5 == 1)
                                        {
                                            fail = true;
                                        }
                                        Tile tile = new Tile();
                                        if (Main.tile[k, l] != null)
                                        {
                                            tile = WorldGen.cloneTile(Main.tile[k, l]);
                                        }
                                        if (Main.tile[k, l] == null)
                                        {
                                            Main.tile[k, l] = new Tile();
                                        }
                                        if (Main.netMode == 2 && !Netplay.serverSock[this.whoAmI].tileSection[Netplay.GetSectionX(k), Netplay.GetSectionY(l)])
                                        {
                                            fail = true;
                                        }
                                        tile.mX = k;
                                        tile.mY = l;
                                        TileEvent tileEvent = new TileEvent(tile, Main.player[this.whoAmI], (int)b5);
                                        PluginManager.callHook(Hook.TILE_CHANGE, tileEvent);
                                        if (tileEvent.getState())
                                        {
                                            NetMessage.SendTileSquare(this.whoAmI, k, l, 1);
                                            return;
                                        }
                                        switch (b4)
                                        {
                                            case 0:
                                                {
                                                    WorldGen.KillTile(k, l, fail, false, false);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    WorldGen.PlaceTile(k, l, (int)b5, false, true, -1);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    WorldGen.KillWall(k, l, fail);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    WorldGen.PlaceWall(k, l, (int)b5, false);
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    WorldGen.KillTile(k, l, fail, false, true);
                                                    break;
                                                }
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(17, -1, this.whoAmI, "", (int)b4, (float)k, (float)l, (float)b5);
                                            if (b4 == 1 && b5 == 53)
                                            {
                                                NetMessage.SendTileSquare(-1, k, l, 1);
                                            }
                                        }
                                        return;
                                    }
                                case 18:
                                    {
                                        if (Main.netMode == 1)
                                        {
                                            byte b6 = this.readBuffer[num];
                                            num++;
                                            int num17 = System.BitConverter.ToInt32(this.readBuffer, num);
                                            num += 4;
                                            short sunModY = System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 2;
                                            short moonModY = System.BitConverter.ToInt16(this.readBuffer, num);
                                            num += 2;
                                            if (b6 == 1)
                                            {
                                                Main.dayTime = true;
                                            }
                                            else
                                            {
                                                Main.dayTime = false;
                                            }
                                            Main.time = (double)num17;
                                            Main.sunModY = sunModY;
                                            Main.moonModY = moonModY;
                                            if (Main.netMode == 2)
                                            {
                                                NetMessage.SendData(18, -1, this.whoAmI, "", 0, 0f, 0f, 0f);
                                            }
                                        }
                                        return;
                                    }
                                case 19:
                                    {
                                        byte b4 = this.readBuffer[num];
                                        num++;
                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                        num += 4;
                                        int num18 = (int)this.readBuffer[num];
                                        int direction = 0;
                                        if (num18 == 0)
                                        {
                                            direction = -1;
                                        }
                                        switch (b4)
                                        {
                                            case 0:
                                                {
                                                    WorldGen.OpenDoor(k, l, direction);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    WorldGen.CloseDoor(k, l, true);
                                                    break;
                                                }
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(19, -1, this.whoAmI, "", (int)b4, (float)k, (float)l, (float)num18);
                                        }
                                        return;
                                    }
                                case 20:
                                    {
                                        short num8 = System.BitConverter.ToInt16(this.readBuffer, start + 1);
                                        int num9 = System.BitConverter.ToInt32(this.readBuffer, start + 3);
                                        int num10 = System.BitConverter.ToInt32(this.readBuffer, start + 7);
                                        num = start + 11;
                                        for (int k = num9; k < num9 + (int)num8; k++)
                                        {
                                            for (int l = num10; l < num10 + (int)num8; l++)
                                            {
                                                if (Main.tile[k, l] == null)
                                                {
                                                    Main.tile[k, l] = new Tile();
                                                }
                                                byte b2 = this.readBuffer[num];
                                                num++;
                                                bool active = Main.tile[k, l].active;
                                                if ((b2 & 1) == 1)
                                                {
                                                    Main.tile[k, l].active = true;
                                                }
                                                else
                                                {
                                                    Main.tile[k, l].active = false;
                                                }
                                                if ((b2 & 2) == 2)
                                                {
                                                    Main.tile[k, l].lighted = true;
                                                }
                                                if ((b2 & 4) == 4)
                                                {
                                                    Main.tile[k, l].wall = 1;
                                                }
                                                else
                                                {
                                                    Main.tile[k, l].wall = 0;
                                                }
                                                if ((b2 & 8) == 8)
                                                {
                                                    Main.tile[k, l].liquid = 1;
                                                }
                                                else
                                                {
                                                    Main.tile[k, l].liquid = 0;
                                                }
                                                if (Main.tile[k, l].active)
                                                {
                                                    int type = (int)Main.tile[k, l].type;
                                                    Main.tile[k, l].type = this.readBuffer[num];
                                                    num++;
                                                    if (Main.tileFrameImportant[(int)Main.tile[k, l].type])
                                                    {
                                                        Main.tile[k, l].frameX = System.BitConverter.ToInt16(this.readBuffer, num);
                                                        num += 2;
                                                        Main.tile[k, l].frameY = System.BitConverter.ToInt16(this.readBuffer, num);
                                                        num += 2;
                                                    }
                                                    else
                                                    {
                                                        if (!active || (int)Main.tile[k, l].type != type)
                                                        {
                                                            Main.tile[k, l].frameX = -1;
                                                            Main.tile[k, l].frameY = -1;
                                                        }
                                                    }
                                                }
                                                if (Main.tile[k, l].wall > 0)
                                                {
                                                    Main.tile[k, l].wall = this.readBuffer[num];
                                                    num++;
                                                }
                                                if (Main.tile[k, l].liquid > 0)
                                                {
                                                    Main.tile[k, l].liquid = this.readBuffer[num];
                                                    num++;
                                                    byte b3 = this.readBuffer[num];
                                                    num++;
                                                    if (b3 == 1)
                                                    {
                                                        Main.tile[k, l].lava = true;
                                                    }
                                                    else
                                                    {
                                                        Main.tile[k, l].lava = false;
                                                    }
                                                }
                                            }
                                        }
                                        WorldGen.RangeFrame(num9, num10, num9 + (int)num8, num10 + (int)num8);
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData((int)b, -1, this.whoAmI, "", (int)num8, (float)num9, (float)num10, 0f);
                                        }
                                        return;
                                    }
                                case 21:
                                    {
                                        short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        float num13 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float num14 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float x = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float y = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        byte stack2 = this.readBuffer[num];
                                        num++;
                                        string string2 = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                        if (Main.netMode == 1)
                                        {
                                            if (string2 == "0")
                                            {
                                                Main.item[(int)num19].active = false;
                                            }
                                            else
                                            {
                                                Main.item[(int)num19].SetDefaults(string2);
                                                Main.item[(int)num19].stack = (int)stack2;
                                                Main.item[(int)num19].position.X = num13;
                                                Main.item[(int)num19].position.Y = num14;
                                                Main.item[(int)num19].velocity.X = x;
                                                Main.item[(int)num19].velocity.Y = y;
                                                Main.item[(int)num19].active = true;
                                                Main.item[(int)num19].wet = Collision.WetCollision(Main.item[(int)num19].position, Main.item[(int)num19].width, Main.item[(int)num19].height);
                                            }
                                        }
                                        else
                                        {
                                            if (string2 == "0")
                                            {
                                                if (num19 < 200)
                                                {
                                                    Main.item[(int)num19].active = false;
                                                    NetMessage.SendData(21, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                                }
                                            }
                                            else
                                            {
                                                bool flag3 = false;
                                                if (num19 == 200)
                                                {
                                                    flag3 = true;
                                                }
                                                if (flag3)
                                                {
                                                    Item item = new Item();
                                                    item.SetDefaults(string2);
                                                    num19 = (short)Item.NewItem((int)num13, (int)num14, item.width, item.height, item.type, (int)stack2, true);
                                                }
                                                Main.item[(int)num19].SetDefaults(string2);
                                                Main.item[(int)num19].stack = (int)stack2;
                                                Main.item[(int)num19].position.X = num13;
                                                Main.item[(int)num19].position.Y = num14;
                                                Main.item[(int)num19].velocity.X = x;
                                                Main.item[(int)num19].velocity.Y = y;
                                                Main.item[(int)num19].active = true;
                                                Main.item[(int)num19].owner = Main.myPlayer;
                                                if (flag3)
                                                {
                                                    NetMessage.SendData(21, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                                    Main.item[(int)num19].ownIgnore = this.whoAmI;
                                                    Main.item[(int)num19].ownTime = 100;
                                                    Main.item[(int)num19].FindOwner((int)num19);
                                                }
                                                else
                                                {
                                                    NetMessage.SendData(21, -1, this.whoAmI, "", (int)num19, 0f, 0f, 0f);
                                                }
                                            }
                                        }
                                        return;
                                    }
                                case 22:
                                    {
                                        short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        byte b7 = this.readBuffer[num];
                                        Main.item[(int)num19].owner = (int)b7;
                                        if ((int)b7 == Main.myPlayer)
                                        {
                                            Main.item[(int)num19].keepTime = 15;
                                        }
                                        else
                                        {
                                            Main.item[(int)num19].keepTime = 0;
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            Main.item[(int)num19].owner = 255;
                                            Main.item[(int)num19].keepTime = 15;
                                            NetMessage.SendData(22, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                        }
                                        return;
                                    }
                                case 23:
                                    {
                                        short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        float num13 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float num14 = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float x = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        float y = System.BitConverter.ToSingle(this.readBuffer, num);
                                        num += 4;
                                        int target = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        int num18 = (int)(this.readBuffer[num] - 1);
                                        num++;
                                        int num20 = (int)(this.readBuffer[num] - 1);
                                        num++;
                                        int num16 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        float[] array = new float[NPC.maxAI];
                                        for (int m = 0; m < NPC.maxAI; m++)
                                        {
                                            array[m] = System.BitConverter.ToSingle(this.readBuffer, num);
                                            num += 4;
                                        }
                                        string string4 = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                        if (!Main.npc[(int)num19].active || Main.npc[(int)num19].name != string4)
                                        {
                                            Main.npc[(int)num19].active = true;
                                            Main.npc[(int)num19].SetDefaults(string4);
                                        }
                                        Main.npc[(int)num19].position.X = num13;
                                        Main.npc[(int)num19].position.Y = num14;
                                        Main.npc[(int)num19].velocity.X = x;
                                        Main.npc[(int)num19].velocity.Y = y;
                                        Main.npc[(int)num19].target = target;
                                        Main.npc[(int)num19].direction = num18;
                                        Main.npc[(int)num19].life = num16;
                                        if (num16 <= 0)
                                        {
                                            Main.npc[(int)num19].active = false;
                                        }
                                        for (int m = 0; m < NPC.maxAI; m++)
                                        {
                                            Main.npc[(int)num19].ai[m] = array[m];
                                        }
                                        return;
                                    }
                                case 24:
                                    {
                                        short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        byte b8 = this.readBuffer[num];
                                        Main.npc[(int)num19].StrikeNPC(Main.player[(int)b8].inventory[Main.player[(int)b8].selectedItem].damage, Main.player[(int)b8].inventory[Main.player[(int)b8].selectedItem].knockBack, Main.player[(int)b8].direction);
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(24, -1, this.whoAmI, "", (int)num19, (float)b8, 0f, 0f);
                                            NetMessage.SendData(23, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                        }
                                        return;
                                    }
                                case 25:
                                    {
                                        int num11 = (int)this.readBuffer[start + 1];
                                        if (Main.netMode == 2)
                                        {
                                            num11 = this.whoAmI;
                                        }
                                        byte b9 = this.readBuffer[start + 2];
                                        byte b10 = this.readBuffer[start + 3];
                                        byte b11 = this.readBuffer[start + 4];
                                        string string5 = System.Text.Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
                                        if (Main.netMode == 2)
                                        {
                                            string text = string5.ToLower();
                                            if (text == "/playing")
                                            {
                                                string text2 = "";
                                                for (int i = 0; i < 255; i++)
                                                {
                                                    if (Main.player[i].active)
                                                    {
                                                        if (text2 == "")
                                                        {
                                                            text2 += Main.player[i].name;
                                                        }
                                                        else
                                                        {
                                                            text2 = text2 + ", " + Main.player[i].name;
                                                        }
                                                    }
                                                }
                                                NetMessage.SendData(25, this.whoAmI, -1, "Current players: " + text2 + ".", 255, 255f, 240f, 20f);
                                            }
                                            else
                                            {
                                                if (text.Length >= 4 && text.Substring(0, 4) == "/me ")
                                                {
                                                    NetMessage.SendData(25, -1, -1, "*" + Main.player[this.whoAmI].name + " " + string5.Substring(4), 255, 200f, 100f, 0f);
                                                }
                                                else
                                                {
                                                    if (text.Length >= 3 && text.Substring(0, 3) == "/p ")
                                                    {
                                                        if (Main.player[this.whoAmI].team != 0)
                                                        {
                                                            for (int i = 0; i < 255; i++)
                                                            {
                                                                if (Main.player[i].team == Main.player[this.whoAmI].team)
                                                                {
                                                                    NetMessage.SendData(25, i, -1, string5.Substring(3), num11, (float)Main.teamColor[Main.player[this.whoAmI].team].R, (float)Main.teamColor[Main.player[this.whoAmI].team].G, (float)Main.teamColor[Main.player[this.whoAmI].team].B);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            NetMessage.SendData(25, this.whoAmI, -1, "You are not in a party!", 255, 255f, 240f, 20f);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (text.Substring(0, 1) == "/")
                                                        {
                                                            try
                                                            {
                                                                string[] array2 = string5.Split(new char[]
															{
																' '
															});
                                                                Player player = Main.player[this.whoAmI];
                                                                array2[0] = array2[0].ToLower();
                                                                CommandEvent commandEvent = new CommandEvent(array2, player);
                                                                commandEvent.setState(false);
                                                                PluginManager.callHook(Hook.PLAYER_COMMAND, commandEvent);
                                                                if (!commandEvent.getState())
                                                                {
                                                                    if (array2[0] == "/test")
                                                                    {
                                                                        player.sendMessage(string.Concat(new object[]
																	{
																		"hitTile: ", 
																		player.hitTileX, 
																		",", 
																		player.hitTileY
																	}), 255, 255, 255);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (array2[0] == "/invasion")
                                                                        {
                                                                            int num21 = 1;
                                                                            if (array2.Length > 1)
                                                                            {
                                                                                if (!int.TryParse(array2[1], out num21))
                                                                                {
                                                                                    player.sendMessage("Invasion size invalid! Returning to default size!", 255, 255, 255);
                                                                                    num21 = 1;
                                                                                }
                                                                            }
                                                                            Main.invasionType = 1;
                                                                            Main.invasionSize = 100 + 50 * num21;
                                                                            Main.invasionWarn = 0;
                                                                            Main.invasionX = (double)player.position.X;
                                                                            Main.invasionDelay = 0;
                                                                            break;
                                                                        }
                                                                        if (array2[0] == "/dbg")
                                                                        {
                                                                            Player player2 = Main.player[this.whoAmI];
                                                                            NetMessage.SendData(25, this.whoAmI, -1, string.Concat(new object[]
																		{
																			player2.width, 
																			",", 
																			player2.height, 
																			"!"
																		}), 255, (float)b9, (float)b10, (float)b11);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (array2[0] == "/whitelist" && player.isOP)
                                                                            {
                                                                                if (Main.properties["whitelistEnabled"] == "true")
                                                                                {
                                                                                    if (array2[1] == "add")
                                                                                    {
                                                                                        NetMessage.addWhitelist(array2[2]);
                                                                                        NetMessage.saveWhitelist();
                                                                                        player.sendMessage("Added IP to whitelist!", 255, 255, 255);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (array2[1] == "remove")
                                                                                        {
                                                                                            NetMessage.removeWhitelist(array2[2]);
                                                                                            NetMessage.saveWhitelist();
                                                                                            player.sendMessage("Removed IP from whitelist!", 255, 255, 255);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    player.sendMessage("The whitelist is disabled. Enable it in your server.properties!", 255, 255, 255);
                                                                                    player.sendMessage("Don't forget to add your IP to whitelist.txt first!", 255, 255, 255);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (array2[0] == "/spawnnpc" && player.isOP)
                                                                                {
                                                                                    Player player2 = Main.player[this.whoAmI];
                                                                                    int type2 = -1;
                                                                                    if (int.TryParse(array2[1], out type2))
                                                                                    {
                                                                                        int num22 = NPC.NewNPC((int)player2.position.X + 5, (int)player2.position.Y, type2, 0);
                                                                                        NetMessage.SendData(25, this.whoAmI, -1, "Spawned NPC: " + Main.npc[num22].name + "!", 255, (float)b9, (float)b10, (float)b11);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        NetMessage.SendData(25, this.whoAmI, -1, "Invalid NPC: " + array2[1] + "!", 255, (float)b9, (float)b10, (float)b11);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (array2[0] == "/rapiddig")
                                                                                    {
                                                                                        Player player2 = Main.player[this.whoAmI];
                                                                                        int num23 = int.Parse(array2[1]);
                                                                                        for (int j = player2.hitTileY; j > player2.hitTileY - num23; j--)
                                                                                        {
                                                                                            Main.tile[player2.hitTileX, j] = new Tile();
                                                                                            Main.tile[player2.hitTileX, j].active = false;
                                                                                            Main.tile[player2.hitTileX, j].type = 0;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (array2[0] == "/tp" && (Main.properties["tpEnabled"] != "false" || player.isOP))
                                                                                        {
                                                                                            Player playerByName = Netplay.GetPlayerByName(array2[1]);
                                                                                            player.position = playerByName.position;
                                                                                            NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                                                            NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                                                            NetMessage.syncPlayers();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (array2[0] == "/tphere" && player.isOP)
                                                                                            {
                                                                                                Player playerByName = Netplay.GetPlayerByName(array2[1]);
                                                                                                playerByName.position = player.position;
                                                                                                NetMessage.SendData(13, -1, playerByName.whoAmi, "", playerByName.whoAmi, 0f, 0f, 0f);
                                                                                                NetMessage.SendData(13, -1, -1, "", playerByName.whoAmi, 0f, 0f, 0f);
                                                                                                NetMessage.syncPlayers();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (array2[0] == "/oplogin" && !player.isOP)
                                                                                                {
                                                                                                    if (array2[1] == Main.properties["opPassword"])
                                                                                                    {
                                                                                                        NetMessage.broadcastMessage(player.name + " has logged in as an OP!");
                                                                                                        player.isOP = true;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        NetMessage.SendData(25, this.whoAmI, -1, "Invalid Command!", 255, (float)b9, (float)b10, (float)b11);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (array2[0] == "/spawnrate" && player.isOP)
                                                                                                    {
                                                                                                        int num24 = 0;
                                                                                                        if (!int.TryParse(array2[1], out num24))
                                                                                                        {
                                                                                                            player.sendMessage("Problem with input.", 255, 255, 255);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            NPC.defaultSpawnRate = num24;
                                                                                                            NPC.spawnRate = num24;
                                                                                                            player.sendMessage("Temporarily set the spawn rate. Will be reset on restart!", 255, 255, 255);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (array2[0] == "/maxspawns" && player.isOP)
                                                                                                        {
                                                                                                            int num24 = 0;
                                                                                                            if (!int.TryParse(array2[1], out num24))
                                                                                                            {
                                                                                                                player.sendMessage("Problem with input.", 255, 255, 255);
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                NPC.defaultMaxSpawns = num24;
                                                                                                                NPC.maxSpawns = num24;
                                                                                                                player.sendMessage("Temporarily set the max spawns. Will be reset on restart!", 255, 255, 255);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (array2[0] == "/ban" && player.isOP)
                                                                                                            {
                                                                                                                Player playerByName = Netplay.GetPlayerByName(array2[1]);
                                                                                                                string text3 = Netplay.serverSock[playerByName.whoAmi].tcpClient.Client.RemoteEndPoint.ToString().Split(new char[]
																											{
																												':'
																											})[0];
                                                                                                                NetMessage.broadcastMessage(player.name + " has banned ip: " + text3);
                                                                                                                NetMessage.SendData(2, playerByName.whoAmi, -1, "Banned.", 0, 0f, 0f, 0f);
                                                                                                                NetMessage.banIP(text3);
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (array2[0] == "/kick" && player.isOP)
                                                                                                                {
                                                                                                                    Player playerByName = Netplay.GetPlayerByName(array2[1]);
                                                                                                                    NetMessage.broadcastMessage(player.name + " has kicked: " + playerByName.name);
                                                                                                                    NetMessage.SendData(2, playerByName.whoAmi, -1, "Kicked.", 0, 0f, 0f, 0f);
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (array2[0] == "/time" && player.isOP)
                                                                                                                    {
                                                                                                                        int num25 = 0;
                                                                                                                        if (array2[1] == "day")
                                                                                                                        {
                                                                                                                            NetMessage.broadcastMessage("bzzzzt! What the hell was that? " + player.name + " made it day!");
                                                                                                                            num25 = 13500;
                                                                                                                            Main.dayTime = true;
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (array2[1] == "night")
                                                                                                                            {
                                                                                                                                NetMessage.broadcastMessage(player.name + " has driven his/her DeLorean to 88 MPH!");
                                                                                                                                Main.dayTime = false;
                                                                                                                                num25 = 0;
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (!int.TryParse(array2[1], out num25))
                                                                                                                                {
                                                                                                                                    NetMessage.broadcastMessage(player.name + "'s flux capacitor has malfunctioned!");
                                                                                                                                    return;
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                        Main.time = (double)num25;
                                                                                                                        if (num25 > 13500)
                                                                                                                        {
                                                                                                                            Main.dayTime = true;
                                                                                                                        }
                                                                                                                        NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (array2[0] == "/save" && player.isOP)
                                                                                                                        {
                                                                                                                            NetMessage.broadcastMessage("Saving world... hang on a second, this might lag!");
                                                                                                                            WorldGen.saveWorld(false);
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (array2[0] == "/sethome" && (Main.properties["homeEnabled"] != "false" || player.isOP))
                                                                                                                            {
                                                                                                                                if (!System.IO.File.Exists("homes\\" + player.name))
                                                                                                                                {
                                                                                                                                    System.IO.File.Create("homes\\" + player.name);
                                                                                                                                }
                                                                                                                                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("homes\\" + player.name);
                                                                                                                                streamWriter.Write(player.position.X + "," + player.position.Y);
                                                                                                                                streamWriter.Close();
                                                                                                                                NetMessage.SendData(25, this.whoAmI, -1, "Successfully set your home!", 255, (float)b9, (float)b10, (float)b11);
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (array2[0] == "/home" && (Main.properties["homeEnabled"] != "false" || player.isOP))
                                                                                                                                {
                                                                                                                                    if (System.IO.File.Exists("homes\\" + player.name))
                                                                                                                                    {
                                                                                                                                        System.IO.StreamReader streamReader = new System.IO.StreamReader("homes\\" + player.name);
                                                                                                                                        string[] array3 = streamReader.ReadToEnd().Split(new char[]
																																	{
																																		','
																																	});
                                                                                                                                        player.position.X = float.Parse(array3[0]);
                                                                                                                                        player.position.Y = float.Parse(array3[1]);
                                                                                                                                        streamReader.Close();
                                                                                                                                        NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                                                                                                        NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                                                                                                        NetMessage.syncPlayers();
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        NetMessage.SendData(25, this.whoAmI, -1, "You haven't set your home yet.", 8, (float)b9, 0f, 0f);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (array2[0] == "/setwarp" && player.isOP)
                                                                                                                                    {
                                                                                                                                        if (!System.IO.File.Exists("warps\\" + array2[1]))
                                                                                                                                        {
                                                                                                                                            System.IO.File.Create("warps\\" + array2[1]);
                                                                                                                                        }
                                                                                                                                        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("warps\\" + array2[1]);
                                                                                                                                        streamWriter.Write(player.position.X + "," + player.position.Y);
                                                                                                                                        streamWriter.Close();
                                                                                                                                        NetMessage.SendData(25, this.whoAmI, -1, "Successfully set warp!", 255, (float)b9, (float)b10, (float)b11);
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (array2[0] == "/warp")
                                                                                                                                        {
                                                                                                                                            if (System.IO.File.Exists("warps\\" + array2[1]))
                                                                                                                                            {
                                                                                                                                                System.IO.StreamReader streamReader = new System.IO.StreamReader("warps\\" + array2[1]);
                                                                                                                                                string[] array3 = streamReader.ReadToEnd().Split(new char[]
																																			{
																																				','
																																			});
                                                                                                                                                player.position.X = float.Parse(array3[0]);
                                                                                                                                                player.position.Y = float.Parse(array3[1]);
                                                                                                                                                streamReader.Close();
                                                                                                                                                NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                                                                                                                NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                                                                                                                NetMessage.syncPlayers();
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                NetMessage.SendData(25, this.whoAmI, -1, "Warp not found!", 255, (float)b9, (float)b10, (float)b11);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (array2[0] == "/commands" || array2[0] == "/help" || array2[0] == "/cmds")
                                                                                                                                            {
                                                                                                                                                int page;
                                                                                                                                                if (array2.Length < 2 || !int.TryParse(array2[1], out page))
                                                                                                                                                {
                                                                                                                                                    page = 1;
                                                                                                                                                }
                                                                                                                                                string helpPage = Help.getHelpPage(page);
                                                                                                                                                string[] array4 = helpPage.Split(System.Environment.NewLine.ToCharArray());
                                                                                                                                                string[] array5 = array4;
                                                                                                                                                for (int n = 0; n < array5.Length; n++)
                                                                                                                                                {
                                                                                                                                                    string text4 = array5[n];
                                                                                                                                                    if (text4 != "")
                                                                                                                                                    {
                                                                                                                                                        player.sendMessage(text4, 0, 191, 255);
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                NetMessage.SendData(25, this.whoAmI, -1, "Invalid Command!", 255, (float)b9, (float)b10, (float)b11);
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
                                                            catch (System.Exception ex)
                                                            {
                                                                System.IO.StreamWriter streamWriter2 = new System.IO.StreamWriter("crap.txt");
                                                                streamWriter2.Write(ex.ToString());
                                                                streamWriter2.Close();
                                                                NetMessage.SendData(25, this.whoAmI, -1, "ERROR PROCESSING COMMAND!", 255, (float)b9, (float)b10, (float)b11);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ChatEvent chatEvent = new ChatEvent(string5, Main.player[this.whoAmI]);
                                                            PluginManager.callHook(Hook.PLAYER_CHAT, chatEvent);
                                                            if (chatEvent.getState())
                                                            {
                                                                NetMessage.SendData(25, -1, -1, string5, num11, (float)b9, (float)b10, (float)b11);
                                                                if (Main.dedServ)
                                                                {
                                                                    System.Console.WriteLine("<" + Main.player[this.whoAmI].name + "> " + string5);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        return;
                                    }
                                case 26:
                                    {
                                        byte b8 = this.readBuffer[num];
                                        num++;
                                        int num18 = (int)(this.readBuffer[num] - 1);
                                        num++;
                                        short num26 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        byte b12 = this.readBuffer[num];
                                        bool pvp = false;
                                        if (b12 != 0)
                                        {
                                            pvp = true;
                                        }
                                        Main.player[(int)b8].Hurt((int)num26, num18, pvp, true);
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(26, -1, this.whoAmI, "", (int)b8, (float)num18, (float)num26, (float)b12);
                                        }
                                        return;
                                    }
                            }
                            if (b != 27)
                            {
                                if (b == 28)
                                {
                                    short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                    num += 2;
                                    short num26 = System.BitConverter.ToInt16(this.readBuffer, num);
                                    num += 2;
                                    float num27 = System.BitConverter.ToSingle(this.readBuffer, num);
                                    num += 4;
                                    int num28 = (int)(this.readBuffer[num] - 1);
                                    if (num26 >= 0)
                                    {
                                        Main.npc[(int)num19].StrikeNPC((int)num26, num27, num28);
                                    }
                                    else
                                    {
                                        Main.npc[(int)num19].life = 0;
                                        Main.npc[(int)num19].HitEffect(0, 10.0);
                                        Main.npc[(int)num19].active = false;
                                    }
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(28, -1, this.whoAmI, "", (int)num19, (float)num26, num27, (float)num28);
                                        NetMessage.SendData(23, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                    }
                                }
                                else
                                {
                                    if (b != 29)
                                    {
                                        switch (b)
                                        {
                                            case 30:
                                                {
                                                    byte b8 = this.readBuffer[num];
                                                    num++;
                                                    byte b13 = this.readBuffer[num];
                                                    if (b13 == 1)
                                                    {
                                                        Main.player[(int)b8].hostile = true;
                                                    }
                                                    else
                                                    {
                                                        Main.player[(int)b8].hostile = false;
                                                    }
                                                    if (Main.netMode == 2)
                                                    {
                                                        NetMessage.SendData(30, -1, this.whoAmI, "", (int)b8, 0f, 0f, 0f);
                                                        string str = " has enabled PvP!";
                                                        if (b13 == 0)
                                                        {
                                                            str = " has disabled PvP!";
                                                        }
                                                        NetMessage.SendData(25, -1, -1, Main.player[(int)b8].name + str, 255, (float)Main.teamColor[Main.player[(int)b8].team].R, (float)Main.teamColor[Main.player[(int)b8].team].G, (float)Main.teamColor[Main.player[(int)b8].team].B);
                                                    }
                                                    break;
                                                }
                                            case 31:
                                                {
                                                    if (Main.netMode == 2)
                                                    {
                                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                        num += 4;
                                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                        num += 4;
                                                        int num29 = Chest.FindChest(k, l);
                                                        if (num29 > -1 && Chest.UsingChest(num29) == -1)
                                                        {
                                                            for (int i = 0; i < Chest.maxItems; i++)
                                                            {
                                                                NetMessage.SendData(32, this.whoAmI, -1, "", num29, (float)i, 0f, 0f);
                                                            }
                                                            NetMessage.SendData(33, this.whoAmI, -1, "", num29, 0f, 0f, 0f);
                                                            Main.player[this.whoAmI].chest = num29;
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 32:
                                                {
                                                    int num29 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                    num += 2;
                                                    int num30 = (int)this.readBuffer[num];
                                                    num++;
                                                    int stack3 = (int)this.readBuffer[num];
                                                    num++;
                                                    string string6 = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                                    if (Main.chest[num29] == null)
                                                    {
                                                        Main.chest[num29] = new Chest();
                                                    }
                                                    if (Main.chest[num29].item[num30] == null)
                                                    {
                                                        Main.chest[num29].item[num30] = new Item();
                                                    }
                                                    Main.chest[num29].item[num30].SetDefaults(string6);
                                                    Main.chest[num29].item[num30].stack = stack3;
                                                    break;
                                                }
                                            case 33:
                                                {
                                                    int num29 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                    num += 2;
                                                    int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                    num += 4;
                                                    int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                    if (Main.netMode == 1)
                                                    {
                                                        if (Main.player[Main.myPlayer].chest == -1)
                                                        {
                                                            Main.playerInventory = true;
                                                        }
                                                        else
                                                        {
                                                            if (Main.player[Main.myPlayer].chest != num29 && num29 != -1)
                                                            {
                                                                Main.playerInventory = true;
                                                            }
                                                            else
                                                            {
                                                                if (Main.player[Main.myPlayer].chest != -1 && num29 == -1)
                                                                {
                                                                }
                                                            }
                                                        }
                                                        Main.player[Main.myPlayer].chest = num29;
                                                        Main.player[Main.myPlayer].chestX = k;
                                                        Main.player[Main.myPlayer].chestY = l;
                                                    }
                                                    else
                                                    {
                                                        Main.player[this.whoAmI].chest = num29;
                                                    }
                                                    break;
                                                }
                                            case 34:
                                                {
                                                    if (Main.netMode == 2)
                                                    {
                                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                        num += 4;
                                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                        WorldGen.KillTile(k, l, false, false, false);
                                                        if (!Main.tile[k, l].active)
                                                        {
                                                            NetMessage.SendData(17, -1, -1, "", 0, (float)k, (float)l, 0f);
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 35:
                                                {
                                                    int num11 = (int)this.readBuffer[num];
                                                    num++;
                                                    int num31 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                    num += 2;
                                                    if (num11 != Main.myPlayer)
                                                    {
                                                        Main.player[num11].HealEffect(num31);
                                                    }
                                                    if (Main.netMode == 2)
                                                    {
                                                        NetMessage.SendData(35, -1, this.whoAmI, "", num11, (float)num31, 0f, 0f);
                                                    }
                                                    break;
                                                }
                                            case 36:
                                                {
                                                    int num11 = (int)this.readBuffer[num];
                                                    num++;
                                                    int num32 = (int)this.readBuffer[num];
                                                    num++;
                                                    int num33 = (int)this.readBuffer[num];
                                                    num++;
                                                    int num34 = (int)this.readBuffer[num];
                                                    num++;
                                                    int num35 = (int)this.readBuffer[num];
                                                    num++;
                                                    if (num32 == 0)
                                                    {
                                                        Main.player[num11].zoneEvil = false;
                                                    }
                                                    else
                                                    {
                                                        Main.player[num11].zoneEvil = true;
                                                    }
                                                    if (num33 == 0)
                                                    {
                                                        Main.player[num11].zoneMeteor = false;
                                                    }
                                                    else
                                                    {
                                                        Main.player[num11].zoneMeteor = true;
                                                    }
                                                    if (num34 == 0)
                                                    {
                                                        Main.player[num11].zoneDungeon = false;
                                                    }
                                                    else
                                                    {
                                                        Main.player[num11].zoneDungeon = true;
                                                    }
                                                    if (num35 == 0)
                                                    {
                                                        Main.player[num11].zoneJungle = false;
                                                    }
                                                    else
                                                    {
                                                        Main.player[num11].zoneJungle = true;
                                                    }
                                                    break;
                                                }
                                            case 37:
                                                {
                                                    if (Main.netMode == 1)
                                                    {
                                                        if (Main.autoPass)
                                                        {
                                                            NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0f, 0f, 0f);
                                                            Main.autoPass = false;
                                                        }
                                                        else
                                                        {
                                                            Netplay.password = "";
                                                            Main.menuMode = 31;
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 38:
                                                {
                                                    if (Main.netMode == 2)
                                                    {
                                                        if (System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start) == Netplay.password)
                                                        {
                                                            Netplay.serverSock[this.whoAmI].state = 1;
                                                            NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                                        }
                                                        else
                                                        {
                                                            NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f);
                                                        }
                                                    }
                                                    break;
                                                }
                                            default:
                                                {
                                                    if (b == 39 && Main.netMode == 1)
                                                    {
                                                        short num19 = System.BitConverter.ToInt16(this.readBuffer, num);
                                                        Main.item[(int)num19].owner = 255;
                                                        NetMessage.SendData(22, -1, -1, "", (int)num19, 0f, 0f, 0f);
                                                    }
                                                    else
                                                    {
                                                        if (b == 40)
                                                        {
                                                            byte b8 = this.readBuffer[num];
                                                            num++;
                                                            int talkNPC = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                            num += 2;
                                                            Main.player[(int)b8].talkNPC = talkNPC;
                                                            if (Main.netMode == 2)
                                                            {
                                                                NetMessage.SendData(40, -1, this.whoAmI, "", (int)b8, 0f, 0f, 0f);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (b == 41)
                                                            {
                                                                byte b8 = this.readBuffer[num];
                                                                num++;
                                                                float itemRotation = System.BitConverter.ToSingle(this.readBuffer, num);
                                                                num += 4;
                                                                int itemAnimation = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                                Main.player[(int)b8].itemRotation = itemRotation;
                                                                Main.player[(int)b8].itemAnimation = itemAnimation;
                                                                if (Main.netMode == 2)
                                                                {
                                                                    NetMessage.SendData(41, -1, this.whoAmI, "", (int)b8, 0f, 0f, 0f);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (b == 42)
                                                                {
                                                                    int num11 = (int)this.readBuffer[num];
                                                                    num++;
                                                                    int statMana = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                                    num += 2;
                                                                    int statManaMax = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                                    if (Main.netMode == 2)
                                                                    {
                                                                        num11 = this.whoAmI;
                                                                    }
                                                                    Main.player[num11].statMana = statMana;
                                                                    Main.player[num11].statManaMax = statManaMax;
                                                                    if (Main.netMode == 2)
                                                                    {
                                                                        NetMessage.SendData(42, -1, this.whoAmI, "", num11, 0f, 0f, 0f);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (b == 43)
                                                                    {
                                                                        int num11 = (int)this.readBuffer[num];
                                                                        num++;
                                                                        int num36 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                                        num += 2;
                                                                        if (num11 != Main.myPlayer)
                                                                        {
                                                                            Main.player[num11].ManaEffect(num36);
                                                                        }
                                                                        if (Main.netMode == 2)
                                                                        {
                                                                            NetMessage.SendData(43, -1, this.whoAmI, "", num11, (float)num36, 0f, 0f);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (b == 44)
                                                                        {
                                                                            byte b8 = this.readBuffer[num];
                                                                            num++;
                                                                            int num18 = (int)(this.readBuffer[num] - 1);
                                                                            num++;
                                                                            short num26 = System.BitConverter.ToInt16(this.readBuffer, num);
                                                                            num += 2;
                                                                            byte b12 = this.readBuffer[num];
                                                                            bool pvp = false;
                                                                            if (b12 != 0)
                                                                            {
                                                                                pvp = true;
                                                                            }
                                                                            Main.player[(int)b8].KillMe((double)num26, num18, pvp);
                                                                            if (Main.netMode == 2)
                                                                            {
                                                                                NetMessage.SendData(44, -1, this.whoAmI, "", (int)b8, (float)num18, (float)num26, (float)b12);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            switch (b)
                                                                            {
                                                                                case 45:
                                                                                    {
                                                                                        int num11 = (int)this.readBuffer[num];
                                                                                        num++;
                                                                                        int num37 = (int)this.readBuffer[num];
                                                                                        num++;
                                                                                        int team = Main.player[num11].team;
                                                                                        Main.player[num11].team = num37;
                                                                                        if (Main.netMode == 2)
                                                                                        {
                                                                                            NetMessage.SendData(45, -1, this.whoAmI, "", num11, 0f, 0f, 0f);
                                                                                            string str2 = "";
                                                                                            switch (num37)
                                                                                            {
                                                                                                case 0:
                                                                                                    {
                                                                                                        str2 = " is no longer on a party.";
                                                                                                        break;
                                                                                                    }
                                                                                                case 1:
                                                                                                    {
                                                                                                        str2 = " has joined the red party.";
                                                                                                        break;
                                                                                                    }
                                                                                                case 2:
                                                                                                    {
                                                                                                        str2 = " has joined the green party.";
                                                                                                        break;
                                                                                                    }
                                                                                                case 3:
                                                                                                    {
                                                                                                        str2 = " has joined the blue party.";
                                                                                                        break;
                                                                                                    }
                                                                                                case 4:
                                                                                                    {
                                                                                                        str2 = " has joined the yellow party.";
                                                                                                        break;
                                                                                                    }
                                                                                            }
                                                                                            for (int i = 0; i < 255; i++)
                                                                                            {
                                                                                                if (i == this.whoAmI || (team > 0 && Main.player[i].team == team) || (num37 > 0 && Main.player[i].team == num37))
                                                                                                {
                                                                                                    NetMessage.SendData(25, i, -1, Main.player[num11].name + str2, 255, (float)Main.teamColor[num37].R, (float)Main.teamColor[num37].G, (float)Main.teamColor[num37].B);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                case 46:
                                                                                    {
                                                                                        if (Main.netMode == 2)
                                                                                        {
                                                                                            int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                            num += 4;
                                                                                            int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                            num += 4;
                                                                                            int num38 = Sign.ReadSign(k, l);
                                                                                            if (num38 >= 0)
                                                                                            {
                                                                                                NetMessage.SendData(47, this.whoAmI, -1, "", num38, 0f, 0f, 0f);
                                                                                            }
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                case 47:
                                                                                    {
                                                                                        int num38 = (int)System.BitConverter.ToInt16(this.readBuffer, num);
                                                                                        num += 2;
                                                                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                        num += 4;
                                                                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                        num += 4;
                                                                                        string string7 = System.Text.Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
                                                                                        Main.sign[num38] = new Sign();
                                                                                        Main.sign[num38].x = k;
                                                                                        Main.sign[num38].y = l;
                                                                                        Sign.TextSign(num38, string7);
                                                                                        if (Main.netMode == 1 && Main.sign[num38] != null && num38 != Main.player[Main.myPlayer].sign)
                                                                                        {
                                                                                            Main.playerInventory = false;
                                                                                            Main.player[Main.myPlayer].talkNPC = -1;
                                                                                            Main.editSign = false;
                                                                                            Main.player[Main.myPlayer].sign = num38;
                                                                                            Main.npcChatText = Main.sign[num38].text;
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                case 48:
                                                                                    {
                                                                                        int k = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                        num += 4;
                                                                                        int l = System.BitConverter.ToInt32(this.readBuffer, num);
                                                                                        num += 4;
                                                                                        byte liquid = this.readBuffer[num];
                                                                                        num++;
                                                                                        byte b3 = this.readBuffer[num];
                                                                                        num++;
                                                                                        if (Main.tile[k, l] == null)
                                                                                        {
                                                                                            Main.tile[k, l] = new Tile();
                                                                                        }
                                                                                        lock (Main.tile[k, l])
                                                                                        {
                                                                                            Main.tile[k, l].liquid = liquid;
                                                                                            if (b3 == 1)
                                                                                            {
                                                                                                Main.tile[k, l].lava = true;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Main.tile[k, l].lava = false;
                                                                                            }
                                                                                            if (Main.netMode == 2)
                                                                                            {
                                                                                                WorldGen.SquareTileFrame(k, l, true);
                                                                                            }
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                default:
                                                                                    {
                                                                                        if (b == 49 && Netplay.clientSock.state == 6)
                                                                                        {
                                                                                            Netplay.clientSock.state = 10;
                                                                                            Main.player[Main.myPlayer].Spawn();
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        short num39 = System.BitConverter.ToInt16(this.readBuffer, num);
                                        num += 2;
                                        byte b7 = this.readBuffer[num];
                                        for (int m = 0; m < 1000; m++)
                                        {
                                            if (Main.projectile[m].owner == (int)b7 && Main.projectile[m].identity == (int)num39 && Main.projectile[m].active)
                                            {
                                                Main.projectile[m].Kill();
                                                break;
                                            }
                                        }
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendData(29, -1, this.whoAmI, "", (int)num39, (float)b7, 0f, 0f);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                short num39 = System.BitConverter.ToInt16(this.readBuffer, num);
                                num += 2;
                                float num13 = System.BitConverter.ToSingle(this.readBuffer, num);
                                num += 4;
                                float num14 = System.BitConverter.ToSingle(this.readBuffer, num);
                                num += 4;
                                float x = System.BitConverter.ToSingle(this.readBuffer, num);
                                num += 4;
                                float y = System.BitConverter.ToSingle(this.readBuffer, num);
                                num += 4;
                                float num27 = System.BitConverter.ToSingle(this.readBuffer, num);
                                num += 4;
                                short num26 = System.BitConverter.ToInt16(this.readBuffer, num);
                                num += 2;
                                byte b7 = this.readBuffer[num];
                                num++;
                                byte b14 = this.readBuffer[num];
                                num++;
                                float[] array = new float[Projectile.maxAI];
                                for (int m = 0; m < Projectile.maxAI; m++)
                                {
                                    array[m] = System.BitConverter.ToSingle(this.readBuffer, num);
                                    num += 4;
                                }
                                int i = 1000;
                                for (int m = 0; m < 1000; m++)
                                {
                                    if (Main.projectile[m].owner == (int)b7 && Main.projectile[m].identity == (int)num39 && Main.projectile[m].active)
                                    {
                                        i = m;
                                        break;
                                    }
                                }
                                if (i == 1000)
                                {
                                    for (int m = 0; m < 1000; m++)
                                    {
                                        if (!Main.projectile[m].active)
                                        {
                                            i = m;
                                            break;
                                        }
                                    }
                                }
                                if (!Main.projectile[i].active || Main.projectile[i].type != (int)b14)
                                {
                                    Main.projectile[i].SetDefaults((int)b14);
                                }
                                if ((!Main.player[this.whoAmI].isOP && Main.properties["explosivesEnabled"] == "false" && (b14 == 28 || b14 == 29)) || b14 == 37)
                                {
                                    Main.player[this.whoAmI].kick("Explosives not allowed!");
                                }
                                else
                                {
                                    Main.projectile[i].identity = (int)num39;
                                    Main.projectile[i].position.X = num13;
                                    Main.projectile[i].position.Y = num14;
                                    Main.projectile[i].velocity.X = x;
                                    Main.projectile[i].velocity.Y = y;
                                    Main.projectile[i].damage = (int)num26;
                                    Main.projectile[i].type = (int)b14;
                                    Main.projectile[i].owner = (int)b7;
                                    Main.projectile[i].knockBack = num27;
                                    for (int m = 0; m < Projectile.maxAI; m++)
                                    {
                                        Main.projectile[i].ai[m] = array[m];
                                    }
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(27, -1, this.whoAmI, "", i, 0f, 0f, 0f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Reset()
        {
            byte[] array = new byte[65535];
            this.writeBuffer = new byte[65535];
            this.writeLocked = false;
            this.messageLength = 0;
            this.totalData = 0;
            this.spamCount = 0;
            this.broadcast = false;
            this.checkBytes = false;
        }
    }
}

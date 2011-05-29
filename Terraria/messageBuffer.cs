namespace Terraria
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    public class messageBuffer
    {
        public bool broadcast;
        public bool checkBytes;
        public int maxSpam;
        public int messageLength;
        public byte[] readBuffer = new byte[0xffff];
        public const int readBufferMax = 0xffff;
        public int spamCount;
        public int totalData;
        public int whoAmI;
        public byte[] writeBuffer = new byte[0xffff];
        public const int writeBufferMax = 0xffff;
        public bool writeLocked;

        public bool checkBan(string ipaddress)
        {
            if (NetMessage.bans == null)
            {
                NetMessage.bans = new ArrayList();
                NetMessage.loadBans();
            }
            return NetMessage.bans.Contains(ipaddress.Split(new char[] { ':' })[0]);
        }

        public void GetData(int start, int length)
        {
            byte num;
            int num2;
            int num3;
            byte num4;
            byte num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int team;
            string str;
            int num12;
            int num15;
            int num28;
            int num29;
            bool flag2;
            string str3;
            if (this.whoAmI < 9)
            {
                Netplay.serverSock[this.whoAmI].timeOut = 0;
            }
            else
            {
                Netplay.clientSock.timeOut = 0;
            }
            byte msgType = 0;
            int index = 0;
            index = start + 1;
            msgType = this.readBuffer[start];
            if ((Main.netMode == 1) && (Netplay.clientSock.statusMax > 0))
            {
                Netplay.clientSock.statusCount++;
            }
            if (Main.verboseNetplay)
            {
                for (num15 = start; num15 < (start + length); num15++)
                {
                }
                for (int m = start; m < (start + length); m++)
                {
                    byte num17 = this.readBuffer[m];
                }
            }
            if (((Main.netMode == 2) && (msgType != 0x26)) && (Netplay.serverSock[this.whoAmI].state == -1))
            {
                NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f);
                return;
            }
            if ((msgType == 1) && (Main.netMode == 2))
            {
                if (Netplay.serverSock[this.whoAmI].state == 0)
                {
                    if (Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1) == ("Terraria" + Main.curRelease))
                    {
                        if (this.checkBan(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
                        {
                            NetMessage.SendData(2, this.whoAmI, -1, "You are banned from this server.", 0, 0f, 0f, 0f);
                        }
                        else if ((Netplay.password == null) || (Netplay.password == ""))
                        {
                            Netplay.serverSock[this.whoAmI].state = 1;
                            NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                        }
                        else
                        {
                            Netplay.serverSock[this.whoAmI].state = -1;
                            NetMessage.SendData(0x25, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                        }
                    }
                    else
                    {
                        NetMessage.SendData(2, this.whoAmI, -1, "You are not using the same version as this server.", 0, 0f, 0f, 0f);
                    }
                }
                return;
            }
            if ((msgType == 2) && (Main.netMode == 1))
            {
                Netplay.disconnect = true;
                Main.statusText = Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1);
                return;
            }
            if ((msgType == 3) && (Main.netMode == 1))
            {
                if (Netplay.clientSock.state == 1)
                {
                    Netplay.clientSock.state = 2;
                }
                int num18 = this.readBuffer[start + 1];
                if (num18 != Main.myPlayer)
                {
                    Main.player[num18] = (Player) Main.player[Main.myPlayer].Clone();
                    Main.player[Main.myPlayer] = new Player();
                    Main.player[num18].whoAmi = num18;
                    Main.myPlayer = num18;
                }
                NetMessage.SendData(4, -1, -1, Main.player[Main.myPlayer].name, Main.myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(0x10, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(0x2a, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                for (int n = 0; n < 0x2c; n++)
                {
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[n].name, Main.myPlayer, (float) n, 0f, 0f);
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
                return;
            }
            switch (msgType)
            {
                case 4:
                {
                    bool flag3 = false;
                    int whoAmI = this.readBuffer[start + 1];
                    int num61 = this.readBuffer[start + 2];
                    if (Main.netMode == 2)
                    {
                        whoAmI = this.whoAmI;
                    }
                    Main.player[whoAmI].hair = num61;
                    Main.player[whoAmI].whoAmi = whoAmI;
                    index += 2;
                    Main.player[whoAmI].hairColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].hairColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].hairColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].skinColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].skinColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].skinColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].eyeColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].eyeColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].eyeColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shirtColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shirtColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shirtColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].underShirtColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].underShirtColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].underShirtColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].pantsColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].pantsColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].pantsColor.B = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shoeColor.R = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shoeColor.G = this.readBuffer[index];
                    index++;
                    Main.player[whoAmI].shoeColor.B = this.readBuffer[index];
                    index++;
                    string text = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                    Main.player[whoAmI].name = text;
                    if (Main.netMode == 2)
                    {
                        if (Netplay.serverSock[this.whoAmI].state < 10)
                        {
                            for (int num62 = 0; num62 < 8; num62++)
                            {
                                if (((num62 != whoAmI) && (text == Main.player[num62].name)) && Netplay.serverSock[num62].active)
                                {
                                    flag3 = true;
                                }
                            }
                        }
                        if (flag3)
                        {
                            NetMessage.SendData(2, this.whoAmI, -1, text + " is already on this server.", 0, 0f, 0f, 0f);
                        }
                        else
                        {
                            Netplay.serverSock[this.whoAmI].oldName = text;
                            Netplay.serverSock[this.whoAmI].name = text;
                            NetMessage.SendData(4, -1, this.whoAmI, text, whoAmI, 0f, 0f, 0f);
                        }
                    }
                    return;
                }
                case 5:
                {
                    int num57 = this.readBuffer[start + 1];
                    if (Main.netMode == 2)
                    {
                        num57 = this.whoAmI;
                    }
                    int num58 = this.readBuffer[start + 2];
                    int num59 = this.readBuffer[start + 3];
                    str3 = Encoding.ASCII.GetString(this.readBuffer, start + 4, length - 4);
                    if (num58 < 0x2c)
                    {
                        Main.player[num57].inventory[num58] = new Item();
                        Main.player[num57].inventory[num58].SetDefaults(str3);
                        Main.player[num57].inventory[num58].stack = num59;
                    }
                    else
                    {
                        Main.player[num57].armor[num58 - 0x2c] = new Item();
                        Main.player[num57].armor[num58 - 0x2c].SetDefaults(str3);
                        Main.player[num57].armor[num58 - 0x2c].stack = num59;
                    }
                    if ((Main.netMode == 2) && (num57 == this.whoAmI))
                    {
                        NetMessage.SendData(5, -1, this.whoAmI, str3, num57, (float) num58, 0f, 0f);
                    }
                    return;
                }
                case 6:
                    if (Main.netMode == 2)
                    {
                        if (Netplay.serverSock[this.whoAmI].state == 1)
                        {
                            Netplay.serverSock[this.whoAmI].state = 2;
                        }
                        NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                    }
                    return;

                case 7:
                    if (Main.netMode == 1)
                    {
                        Main.time = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.dayTime = false;
                        if (this.readBuffer[index] == 1)
                        {
                            Main.dayTime = true;
                        }
                        index++;
                        Main.moonPhase = this.readBuffer[index];
                        index++;
                        int num56 = this.readBuffer[index];
                        index++;
                        if (num56 == 1)
                        {
                            Main.bloodMoon = true;
                        }
                        else
                        {
                            Main.bloodMoon = false;
                        }
                        Main.maxTilesX = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.maxTilesY = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.spawnTileX = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.spawnTileY = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.worldSurface = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.rockLayer = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.worldID = BitConverter.ToInt32(this.readBuffer, index);
                        index += 4;
                        Main.worldName = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                        if (Netplay.clientSock.state == 3)
                        {
                            Netplay.clientSock.state = 4;
                        }
                    }
                    return;

                case 8:
                    if (Main.netMode != 2)
                    {
                        return;
                    }
                    num28 = BitConverter.ToInt32(this.readBuffer, index);
                    index += 4;
                    num29 = BitConverter.ToInt32(this.readBuffer, index);
                    index += 4;
                    flag2 = true;
                    if ((num28 != -1) && (num29 != -1))
                    {
                        if ((num28 < 10) || (num28 > (Main.maxTilesX - 10)))
                        {
                            flag2 = false;
                        }
                        else if ((num29 < 10) || (num29 > (Main.maxTilesY - 10)))
                        {
                            flag2 = false;
                        }
                        break;
                    }
                    flag2 = false;
                    break;

                case 9:
                    if (Main.netMode == 1)
                    {
                        int num20 = BitConverter.ToInt32(this.readBuffer, start + 1);
                        string str2 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
                        Netplay.clientSock.statusMax += num20;
                        Netplay.clientSock.statusText = str2;
                    }
                    return;

                case 10:
                {
                    short num21 = BitConverter.ToInt16(this.readBuffer, start + 1);
                    int num22 = BitConverter.ToInt32(this.readBuffer, start + 3);
                    int num23 = BitConverter.ToInt32(this.readBuffer, start + 7);
                    index = start + 11;
                    for (int num24 = num22; num24 < (num22 + num21); num24++)
                    {
                        if (Main.tile[num24, num23] == null)
                        {
                            Main.tile[num24, num23] = new Tile();
                        }
                        byte num25 = this.readBuffer[index];
                        index++;
                        bool active = Main.tile[num24, num23].active;
                        if ((num25 & 1) == 1)
                        {
                            Main.tile[num24, num23].active = true;
                        }
                        else
                        {
                            Main.tile[num24, num23].active = false;
                        }
                        if ((num25 & 2) == 2)
                        {
                            Main.tile[num24, num23].lighted = true;
                        }
                        if ((num25 & 4) == 4)
                        {
                            Main.tile[num24, num23].wall = 1;
                        }
                        else
                        {
                            Main.tile[num24, num23].wall = 0;
                        }
                        if ((num25 & 8) == 8)
                        {
                            Main.tile[num24, num23].liquid = 1;
                        }
                        else
                        {
                            Main.tile[num24, num23].liquid = 0;
                        }
                        if (Main.tile[num24, num23].active)
                        {
                            int type = Main.tile[num24, num23].type;
                            Main.tile[num24, num23].type = this.readBuffer[index];
                            index++;
                            if (Main.tileFrameImportant[Main.tile[num24, num23].type])
                            {
                                Main.tile[num24, num23].frameX = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                Main.tile[num24, num23].frameY = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                            }
                            else if (!(active && (Main.tile[num24, num23].type == type)))
                            {
                                Main.tile[num24, num23].frameX = -1;
                                Main.tile[num24, num23].frameY = -1;
                            }
                        }
                        if (Main.tile[num24, num23].wall > 0)
                        {
                            Main.tile[num24, num23].wall = this.readBuffer[index];
                            index++;
                        }
                        if (Main.tile[num24, num23].liquid > 0)
                        {
                            Main.tile[num24, num23].liquid = this.readBuffer[index];
                            index++;
                            byte num27 = this.readBuffer[index];
                            index++;
                            if (num27 == 1)
                            {
                                Main.tile[num24, num23].lava = true;
                            }
                            else
                            {
                                Main.tile[num24, num23].lava = false;
                            }
                        }
                    }
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(msgType, -1, this.whoAmI, "", num21, (float) num22, (float) num23, 0f);
                    }
                    return;
                }
                case 11:
                    if (Main.netMode == 1)
                    {
                        int startX = BitConverter.ToInt16(this.readBuffer, index);
                        index += 4;
                        int startY = BitConverter.ToInt16(this.readBuffer, index);
                        index += 4;
                        int endX = BitConverter.ToInt16(this.readBuffer, index);
                        index += 4;
                        int endY = BitConverter.ToInt16(this.readBuffer, index);
                        index += 4;
                        WorldGen.SectionTileFrame(startX, startY, endX, endY);
                    }
                    return;

                case 12:
                {
                    int num43 = this.readBuffer[index];
                    index++;
                    Main.player[num43].SpawnX = BitConverter.ToInt32(this.readBuffer, index);
                    index += 4;
                    Main.player[num43].SpawnY = BitConverter.ToInt32(this.readBuffer, index);
                    index += 4;
                    Main.player[num43].Spawn();
                    if ((Main.netMode == 2) && (Netplay.serverSock[this.whoAmI].state >= 3))
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
                    int num44 = this.readBuffer[index];
                    if (!((Main.netMode != 1) || Main.player[num44].active))
                    {
                        NetMessage.SendData(15, -1, -1, "", 0, 0f, 0f, 0f);
                    }
                    index++;
                    int num45 = this.readBuffer[index];
                    index++;
                    int num46 = this.readBuffer[index];
                    index++;
                    float num47 = BitConverter.ToSingle(this.readBuffer, index);
                    index += 4;
                    float num48 = BitConverter.ToSingle(this.readBuffer, index);
                    index += 4;
                    float num49 = BitConverter.ToSingle(this.readBuffer, index);
                    index += 4;
                    float num50 = BitConverter.ToSingle(this.readBuffer, index);
                    index += 4;
                    Main.player[num44].selectedItem = num46;
                    Main.player[num44].position.X = num47;
                    Main.player[num44].position.Y = num48;
                    Main.player[num44].velocity.X = num49;
                    Main.player[num44].velocity.Y = num50;
                    Main.player[num44].oldVelocity = Main.player[num44].velocity;
                    Main.player[num44].fallStart = (int) (num48 / 16f);
                    Main.player[num44].controlUp = false;
                    Main.player[num44].controlDown = false;
                    Main.player[num44].controlLeft = false;
                    Main.player[num44].controlRight = false;
                    Main.player[num44].controlJump = false;
                    Main.player[num44].controlUseItem = false;
                    Main.player[num44].direction = -1;
                    if ((num45 & 1) == 1)
                    {
                        Main.player[num44].controlUp = true;
                    }
                    if ((num45 & 2) == 2)
                    {
                        Main.player[num44].controlDown = true;
                    }
                    if ((num45 & 4) == 4)
                    {
                        Main.player[num44].controlLeft = true;
                    }
                    if ((num45 & 8) == 8)
                    {
                        Main.player[num44].controlRight = true;
                    }
                    if ((num45 & 0x10) == 0x10)
                    {
                        Main.player[num44].controlJump = true;
                    }
                    if ((num45 & 0x20) == 0x20)
                    {
                        Main.player[num44].controlUseItem = true;
                    }
                    if ((num45 & 0x40) == 0x40)
                    {
                        Main.player[num44].direction = 1;
                    }
                    if ((Main.netMode == 2) && (Netplay.serverSock[this.whoAmI].state == 10))
                    {
                        NetMessage.SendData(13, -1, this.whoAmI, "", num44, 0f, 0f, 0f);
                    }
                    return;
                }
                case 14:
                    if (Main.netMode == 1)
                    {
                        int num51 = this.readBuffer[index];
                        index++;
                        int num52 = this.readBuffer[index];
                        if (num52 != 1)
                        {
                            Main.player[num51].active = false;
                            return;
                        }
                        if (Main.player[num51].active)
                        {
                            Main.player[num51] = new Player();
                        }
                        Main.player[num51].active = true;
                    }
                    return;

                case 15:
                    if (Main.netMode == 2)
                    {
                        NetMessage.syncPlayers();
                    }
                    return;

                case 0x10:
                {
                    int num53 = this.readBuffer[index];
                    index++;
                    int num54 = BitConverter.ToInt16(this.readBuffer, index);
                    index += 2;
                    int num55 = BitConverter.ToInt16(this.readBuffer, index);
                    if (Main.netMode == 2)
                    {
                        num53 = this.whoAmI;
                    }
                    Main.player[num53].statLife = num54;
                    Main.player[num53].statLifeMax = num55;
                    if (Main.player[num53].statLife <= 0)
                    {
                        Main.player[num53].dead = true;
                    }
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x10, -1, this.whoAmI, "", num53, 0f, 0f, 0f);
                    }
                    return;
                }
                default:
                    switch (msgType)
                    {
                        case 0x11:
                        {
                            num = this.readBuffer[index];
                            index++;
                            num2 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            num3 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            num4 = this.readBuffer[index];
                            bool fail = false;
                            if (num4 == 1)
                            {
                                fail = true;
                            }
                            if (Main.tile[num2, num3] == null)
                            {
                                Main.tile[num2, num3] = new Tile();
                            }
                            if (!((Main.netMode != 2) || Netplay.serverSock[this.whoAmI].tileSection[Netplay.GetSectionX(num2), Netplay.GetSectionY(num3)]))
                            {
                                fail = true;
                            }
                            switch (num)
                            {
                                case 0:
                                    WorldGen.KillTile(num2, num3, fail, false, false);
                                    goto Label_5068;

                                case 1:
                                    WorldGen.PlaceTile(num2, num3, num4, false, true, -1);
                                    goto Label_5068;

                                case 2:
                                    WorldGen.KillWall(num2, num3, fail);
                                    goto Label_5068;

                                case 3:
                                    WorldGen.PlaceWall(num2, num3, num4, false);
                                    goto Label_5068;

                                case 4:
                                    WorldGen.KillTile(num2, num3, fail, false, true);
                                    goto Label_5068;
                            }
                            goto Label_5068;
                        }
                        case 0x12:
                            if (Main.netMode == 1)
                            {
                                byte num63 = this.readBuffer[index];
                                index++;
                                int num64 = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                short num65 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                short num66 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                if (num63 == 1)
                                {
                                    Main.dayTime = true;
                                }
                                else
                                {
                                    Main.dayTime = false;
                                }
                                Main.time = num64;
                                Main.sunModY = num65;
                                Main.moonModY = num66;
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x12, -1, this.whoAmI, "", 0, 0f, 0f, 0f);
                                }
                            }
                            return;

                        case 20:
                        {
                            short num67 = BitConverter.ToInt16(this.readBuffer, start + 1);
                            int num68 = BitConverter.ToInt32(this.readBuffer, start + 3);
                            int num69 = BitConverter.ToInt32(this.readBuffer, start + 7);
                            index = start + 11;
                            for (int num70 = num68; num70 < (num68 + num67); num70++)
                            {
                                for (int num71 = num69; num71 < (num69 + num67); num71++)
                                {
                                    if (Main.tile[num70, num71] == null)
                                    {
                                        Main.tile[num70, num71] = new Tile();
                                    }
                                    byte num72 = this.readBuffer[index];
                                    index++;
                                    bool flag4 = Main.tile[num70, num71].active;
                                    if ((num72 & 1) == 1)
                                    {
                                        Main.tile[num70, num71].active = true;
                                    }
                                    else
                                    {
                                        Main.tile[num70, num71].active = false;
                                    }
                                    if ((num72 & 2) == 2)
                                    {
                                        Main.tile[num70, num71].lighted = true;
                                    }
                                    if ((num72 & 4) == 4)
                                    {
                                        Main.tile[num70, num71].wall = 1;
                                    }
                                    else
                                    {
                                        Main.tile[num70, num71].wall = 0;
                                    }
                                    if ((num72 & 8) == 8)
                                    {
                                        Main.tile[num70, num71].liquid = 1;
                                    }
                                    else
                                    {
                                        Main.tile[num70, num71].liquid = 0;
                                    }
                                    if (Main.tile[num70, num71].active)
                                    {
                                        int num73 = Main.tile[num70, num71].type;
                                        Main.tile[num70, num71].type = this.readBuffer[index];
                                        index++;
                                        if (Main.tileFrameImportant[Main.tile[num70, num71].type])
                                        {
                                            Main.tile[num70, num71].frameX = BitConverter.ToInt16(this.readBuffer, index);
                                            index += 2;
                                            Main.tile[num70, num71].frameY = BitConverter.ToInt16(this.readBuffer, index);
                                            index += 2;
                                        }
                                        else if (!(flag4 && (Main.tile[num70, num71].type == num73)))
                                        {
                                            Main.tile[num70, num71].frameX = -1;
                                            Main.tile[num70, num71].frameY = -1;
                                        }
                                    }
                                    if (Main.tile[num70, num71].wall > 0)
                                    {
                                        Main.tile[num70, num71].wall = this.readBuffer[index];
                                        index++;
                                    }
                                    if (Main.tile[num70, num71].liquid > 0)
                                    {
                                        Main.tile[num70, num71].liquid = this.readBuffer[index];
                                        index++;
                                        byte num74 = this.readBuffer[index];
                                        index++;
                                        if (num74 == 1)
                                        {
                                            Main.tile[num70, num71].lava = true;
                                        }
                                        else
                                        {
                                            Main.tile[num70, num71].lava = false;
                                        }
                                    }
                                }
                            }
                            WorldGen.RangeFrame(num68, num69, num68 + num67, num69 + num67);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(msgType, -1, this.whoAmI, "", num67, (float) num68, (float) num69, 0f);
                            }
                            return;
                        }
                        case 0x15:
                        {
                            short num75 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            float num76 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num77 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num78 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num79 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            byte stack = this.readBuffer[index];
                            index++;
                            string itemName = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                            if (Main.netMode != 1)
                            {
                                if (itemName != "0")
                                {
                                    bool flag5 = false;
                                    if (num75 == 200)
                                    {
                                        flag5 = true;
                                    }
                                    if (flag5)
                                    {
                                        Item item = new Item();
                                        item.SetDefaults(itemName);
                                        num75 = (short) Item.NewItem((int) num76, (int) num77, item.width, item.height, item.type, stack, true);
                                    }
                                    Main.item[num75].SetDefaults(itemName);
                                    Main.item[num75].stack = stack;
                                    Main.item[num75].position.X = num76;
                                    Main.item[num75].position.Y = num77;
                                    Main.item[num75].velocity.X = num78;
                                    Main.item[num75].velocity.Y = num79;
                                    Main.item[num75].active = true;
                                    Main.item[num75].owner = Main.myPlayer;
                                    if (flag5)
                                    {
                                        NetMessage.SendData(0x15, -1, -1, "", num75, 0f, 0f, 0f);
                                        Main.item[num75].ownIgnore = this.whoAmI;
                                        Main.item[num75].ownTime = 100;
                                        Main.item[num75].FindOwner(num75);
                                    }
                                    else if ((Main.item[num75].type == 0xa6) || (Main.item[num75].type == 0xa5))
                                    {
                                        Main.item[num75].active = false;
                                    }
                                    else
                                    {
                                        NetMessage.SendData(0x15, -1, this.whoAmI, "", num75, 0f, 0f, 0f);
                                    }
                                }
                                else if (num75 < 200)
                                {
                                    Main.item[num75].active = false;
                                    NetMessage.SendData(0x15, -1, -1, "", num75, 0f, 0f, 0f);
                                }
                                return;
                            }
                            if (!(itemName == "0"))
                            {
                                Main.item[num75].SetDefaults(itemName);
                                Main.item[num75].stack = stack;
                                Main.item[num75].position.X = num76;
                                Main.item[num75].position.Y = num77;
                                Main.item[num75].velocity.X = num78;
                                Main.item[num75].velocity.Y = num79;
                                Main.item[num75].active = true;
                                Main.item[num75].wet = Collision.WetCollision(Main.item[num75].position, Main.item[num75].width, Main.item[num75].height);
                                return;
                            }
                            Main.item[num75].active = false;
                            return;
                        }
                        case 0x16:
                        {
                            short num81 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            byte num82 = this.readBuffer[index];
                            Main.item[num81].owner = num82;
                            if (num82 != Main.myPlayer)
                            {
                                Main.item[num81].keepTime = 0;
                            }
                            else
                            {
                                Main.item[num81].keepTime = 15;
                            }
                            if (Main.netMode == 2)
                            {
                                Main.item[num81].owner = 8;
                                Main.item[num81].keepTime = 15;
                                NetMessage.SendData(0x16, -1, -1, "", num81, 0f, 0f, 0f);
                            }
                            return;
                        }
                        case 0x17:
                        {
                            short num83 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            float num84 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num85 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num86 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num87 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            int num88 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            int num89 = this.readBuffer[index] - 1;
                            index++;
                            byte num90 = this.readBuffer[index];
                            index++;
                            int num91 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            float[] numArray = new float[NPC.maxAI];
                            for (int num92 = 0; num92 < NPC.maxAI; num92++)
                            {
                                numArray[num92] = BitConverter.ToSingle(this.readBuffer, index);
                                index += 4;
                            }
                            string name = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                            if (!(Main.npc[num83].active && !(Main.npc[num83].name != name)))
                            {
                                Main.npc[num83].active = true;
                                Main.npc[num83].SetDefaults(name);
                            }
                            Main.npc[num83].position.X = num84;
                            Main.npc[num83].position.Y = num85;
                            Main.npc[num83].velocity.X = num86;
                            Main.npc[num83].velocity.Y = num87;
                            Main.npc[num83].target = num88;
                            Main.npc[num83].direction = num89;
                            Main.npc[num83].life = num91;
                            if (num91 <= 0)
                            {
                                Main.npc[num83].active = false;
                            }
                            for (int num93 = 0; num93 < NPC.maxAI; num93++)
                            {
                                Main.npc[num83].ai[num93] = numArray[num93];
                            }
                            return;
                        }
                        case 0x18:
                        {
                            short num94 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            byte num95 = this.readBuffer[index];
                            Main.npc[num94].StrikeNPC(Main.player[num95].inventory[Main.player[num95].selectedItem].damage, Main.player[num95].inventory[Main.player[num95].selectedItem].knockBack, Main.player[num95].direction);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x18, -1, this.whoAmI, "", num94, (float) num95, 0f, 0f);
                                NetMessage.SendData(0x17, -1, -1, "", num94, 0f, 0f, 0f);
                            }
                            return;
                        }
                        case 0x19:
                        {
                            int num96 = this.readBuffer[start + 1];
                            if (Main.netMode == 2)
                            {
                                num96 = this.whoAmI;
                            }
                            byte r = this.readBuffer[start + 2];
                            byte g = this.readBuffer[start + 3];
                            byte b = this.readBuffer[start + 4];
                            string str7 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
                            if (Main.netMode != 1)
                            {
                                if (Main.netMode == 2)
                                {
                                    string str9 = str7.ToLower();
                                    if (str9 == "/playing")
                                    {
                                        string str10 = "";
                                        for (int num100 = 0; num100 < 8; num100++)
                                        {
                                            if (Main.player[num100].active)
                                            {
                                                if (str10 == "")
                                                {
                                                    str10 = str10 + Main.player[num100].name;
                                                }
                                                else
                                                {
                                                    str10 = str10 + ", " + Main.player[num100].name;
                                                }
                                            }
                                        }
                                        NetMessage.SendData(0x19, this.whoAmI, -1, "Current players: " + str10 + ".", 8, 255f, 240f, 20f);
                                    }
                                    else if ((str9.Length >= 4) && (str9.Substring(0, 4) == "/me "))
                                    {
                                        NetMessage.SendData(0x19, -1, -1, "*" + Main.player[this.whoAmI].name + " " + str7.Substring(4), 8, 200f, 100f, 0f);
                                    }
                                    else if ((str9.Length >= 3) && (str9.Substring(0, 3) == "/p "))
                                    {
                                        if (Main.player[this.whoAmI].team != 0)
                                        {
                                            for (int num101 = 0; num101 < 8; num101++)
                                            {
                                                if (Main.player[num101].team == Main.player[this.whoAmI].team)
                                                {
                                                    NetMessage.SendData(0x19, num101, -1, str7.Substring(3), num96, (float) Main.teamColor[Main.player[this.whoAmI].team].R, (float) Main.teamColor[Main.player[this.whoAmI].team].G, (float) Main.teamColor[Main.player[this.whoAmI].team].B);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            NetMessage.SendData(0x19, this.whoAmI, -1, "You are not in a party!", 8, 255f, 240f, 20f);
                                        }
                                    }
                                    else if (str9.Substring(0, 1) == "/")
                                    {
                                        try
                                        {
                                            Player player2;
                                            string[] strArray = str7.Split(new char[] { ' ' });
                                            Player player = Main.player[this.whoAmI];
                                            strArray[0] = strArray[0].ToLower();
                                            if ((strArray[0] == "/give") && ((Main.properties["giveEnabled"] != "false") || player.isOP))
                                            {
                                                int result = 1;
                                                str3 = "";
                                                foreach (string str11 in strArray)
                                                {
                                                    if (int.TryParse(str11, out result))
                                                    {
                                                        break;
                                                    }
                                                    if (str11 != strArray[0])
                                                    {
                                                        string str12 = str11.Substring(0, 1).ToUpper() + str11.Substring(1);
                                                        str3 = str3 + str12 + " ";
                                                    }
                                                }
                                                if (result == 0)
                                                {
                                                    result = 1;
                                                }
                                                str3 = str3.Substring(0, str3.Length - 1);
                                                if (((str3 == "Dynamite") || (str3 == "Bomb")) && !player.isOP)
                                                {
                                                    NetMessage.SendData(0x19, this.whoAmI, -1, "Item: " + str3 + " is restricted!", 8, (float) r, (float) g, (float) b);
                                                }
                                                else
                                                {
                                                    if (!(player.isOP || (result <= 0x40)))
                                                    {
                                                        result = 0x40;
                                                    }
                                                    player2 = Main.player[this.whoAmI];
                                                    for (num15 = 0; num15 < result; num15++)
                                                    {
                                                        int num103 = Item.NewItem((int) player2.position.X, (int) player2.position.Y, player2.width, player2.height, 0, 1, false);
                                                        Main.item[num103].SetDefaults(str3);
                                                    }
                                                    NetMessage.SendData(0x19, this.whoAmI, -1, "Given item " + str3 + "!", 8, (float) r, (float) g, (float) b);
                                                }
                                                return;
                                            }
                                            if (strArray[0] == "/dbg")
                                            {
                                                player2 = Main.player[this.whoAmI];
                                                NetMessage.SendData(0x19, this.whoAmI, -1, string.Concat(new object[] { player2.width, ",", player2.height, "!" }), 8, (float) r, (float) g, (float) b);
                                            }
                                            else if ((strArray[0] == "/spawnnpc") && player.isOP)
                                            {
                                                player2 = Main.player[this.whoAmI];
                                                int num104 = -1;
                                                if (int.TryParse(strArray[1], out num104))
                                                {
                                                    int num105 = NPC.NewNPC(((int) player2.position.X) + 5, (int) player2.position.Y, num104, 0);
                                                    NetMessage.SendData(0x19, this.whoAmI, -1, "Spawned NPC: " + Main.npc[num105].name + "!", 8, (float) r, (float) g, (float) b);
                                                }
                                                else
                                                {
                                                    NetMessage.SendData(0x19, this.whoAmI, -1, "Invalid NPC: " + strArray[1] + "!", 8, (float) r, (float) g, (float) b);
                                                }
                                            }
                                            else if (strArray[0] == "/rapiddig")
                                            {
                                                player2 = Main.player[this.whoAmI];
                                                int num106 = int.Parse(strArray[1]);
                                                for (num29 = player2.hitTileY; num29 > (player2.hitTileY - num106); num29--)
                                                {
                                                    Main.tile[player2.hitTileX, num29] = new Tile();
                                                    Main.tile[player2.hitTileX, num29].active = false;
                                                    Main.tile[player2.hitTileX, num29].type = 0;
                                                }
                                            }

                                            else
                                            {
                                                Player playerByName;
                                                if ((strArray[0] == "/tp") && ((Main.properties["tpEnabled"] != "false") || player.isOP))
                                                {
                                                    playerByName = this.GetPlayerByName(strArray[1]);
                                                    player.position = playerByName.position;
                                                    NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                    NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                    NetMessage.syncPlayers();
                                                }
                                                else if ((strArray[0] == "/tphere") && player.isOP)
                                                {
                                                    playerByName = this.GetPlayerByName(strArray[1]);
                                                    playerByName.position = player.position;
                                                    NetMessage.SendData(13, -1, playerByName.whoAmi, "", playerByName.whoAmi, 0f, 0f, 0f);
                                                    NetMessage.SendData(13, -1, -1, "", playerByName.whoAmi, 0f, 0f, 0f);
                                                    NetMessage.syncPlayers();
                                                }
                                                else if (strArray[0] == "/hi")
                                                {
                                                   NetMessage.broadcastMessage(player.name + " said hi!");
                                                }
                                                else if ((strArray[0] == "/oplogin") && !player.isOP)
                                                {
                                                    if (strArray[1] == Main.properties["opPassword"])
                                                    {
                                                        NetMessage.broadcastMessage(player.name + " has logged in as an OP!");
                                                        player.isOP = true;
                                                    }
                                                    else
                                                    {
                                                        NetMessage.SendData(0x19, this.whoAmI, -1, "Invalid Command!", 8, (float) r, (float) g, (float) b);
                                                    }
                                                }
                                                else if ((strArray[0] == "/ban") && player.isOP)
                                                {
                                                    playerByName = this.GetPlayerByName(strArray[1]);
                                                    string ipAddress = Netplay.serverSock[playerByName.whoAmi].tcpClient.Client.RemoteEndPoint.ToString().Split(new char[] { ':' })[0];
                                                    NetMessage.broadcastMessage(player.name + " has banned ip: " + ipAddress);
                                                    NetMessage.SendData(2, playerByName.whoAmi, -1, "Banned.", 0, 0f, 0f, 0f);
                                                    NetMessage.banIP(ipAddress);
                                                }
                                                else if ((strArray[0] == "/kick") && player.isOP)
                                                {
                                                    playerByName = this.GetPlayerByName(strArray[1]);
                                                    NetMessage.broadcastMessage(player.name + " has kicked: " + playerByName.name);
                                                    NetMessage.SendData(2, playerByName.whoAmi, -1, "Kicked.", 0, 0f, 0f, 0f);
                                                }
                                                else if ((strArray[0] == "/time") && player.isOP)
                                                {
                                                    int num107 = 0;
                                                    if (strArray[1] == "day")
                                                    {
                                                        NetMessage.broadcastMessage("bzzzzt! What the hell was that? " + player.name + " made it day!");
                                                        num107 = 0x34bc;
                                                    }
                                                    else if (strArray[1] == "night")
                                                    {
                                                        NetMessage.broadcastMessage(player.name + " has driven his/her DeLorean to 88 MPH!");
                                                        Main.dayTime = false;
                                                        num107 = 0;
                                                    }
                                                    else if (!int.TryParse(strArray[1], out num107))
                                                    {
                                                        NetMessage.broadcastMessage(player.name + "'s flux capacitor has malfunctioned!");
                                                        return;
                                                    }
                                                    Main.time = num107;
                                                    NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                                                }
                                                else if ((strArray[0] == "/save") && player.isOP)
                                                {
                                                    NetMessage.broadcastMessage("Saving world... hang on a second, this might lag!");
                                                    WorldGen.saveWorld(false);
                                                }
                                                else
                                                {
                                                    StreamWriter writer;
                                                    if ((strArray[0] == "/sethome") && ((Main.properties["homeEnabled"] != "false") || player.isOP))
                                                    {
                                                        if (!File.Exists(@"homes\" + player.name))
                                                        {
                                                            File.Create(@"homes\" + player.name);
                                                        }
                                                        writer = new StreamWriter(@"homes\" + player.name);
                                                        writer.Write(player.position.X + "," + player.position.Y);
                                                        writer.Close();
                                                        NetMessage.SendData(0x19, this.whoAmI, -1, "Successfully set your home!", 8, (float) r, (float) g, (float) b);
                                                    }
                                                    else
                                                    {
                                                        StreamReader reader;
                                                        string[] strArray2;
                                                        if ((strArray[0] == "/home") && ((Main.properties["homeEnabled"] != "false") || player.isOP))
                                                        {
                                                            if (File.Exists(@"homes\" + player.name))
                                                            {
                                                                reader = new StreamReader(@"homes\" + player.name);
                                                                strArray2 = reader.ReadToEnd().Split(new char[] { ',' });
                                                                player.position.X = float.Parse(strArray2[0]);
                                                                player.position.Y = float.Parse(strArray2[1]);
                                                                reader.Close();
                                                                NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                                NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                                NetMessage.syncPlayers();
                                                            }
                                                            else
                                                            {
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "You haven't set your home yet.", 8, (float) r, 0f, 0f);
                                                            }
                                                        }
                                                        else if ((strArray[0] == "/setwarp") && player.isOP)
                                                        {
                                                            if (!File.Exists(@"warps\" + strArray[1]))
                                                            {
                                                                File.Create(@"warps\" + strArray[1]);
                                                            }
                                                            writer = new StreamWriter(@"warps\" + strArray[1]);
                                                            writer.Write(player.position.X + "," + player.position.Y);
                                                            writer.Close();
                                                            NetMessage.SendData(0x19, this.whoAmI, -1, "Successfully set warp!", 8, (float) r, (float) g, (float) b);
                                                        }
                                                        else if (strArray[0] != "/warplist")
                                                        {
                                                            if (strArray[0] == "/warp")
                                                            {
                                                                if (File.Exists(@"warps\" + strArray[1]))
                                                                {
                                                                    reader = new StreamReader(@"warps\" + strArray[1]);
                                                                    strArray2 = reader.ReadToEnd().Split(new char[] { ',' });
                                                                    player.position.X = float.Parse(strArray2[0]);
                                                                    player.position.Y = float.Parse(strArray2[1]);
                                                                    reader.Close();
                                                                    NetMessage.SendData(13, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                                                                    NetMessage.SendData(13, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                                                                    NetMessage.syncPlayers();
                                                                }
                                                                else
                                                                {
                                                                    NetMessage.SendData(0x19, this.whoAmI, -1, "Warp not found!", 8, (float) r, (float) g, (float) b);
                                                                }
                                                            }
                                                            else if (((strArray[0] == "/commands") || (strArray[0] == "/help")) || (strArray[0] == "/cmds"))
                                                            {
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "/tp {name} - teleport to user", 8, (float) r, (float) g, (float) b);
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "/sethome | /home - teleport to or set your home", 8, (float) r, (float) g, (float) b);
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "/give {name} {amount} - give item", 8, (float) r, (float) g, (float) b);
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "/warp {warpname}", 8, (float) r, (float) g, (float) b);
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "More coming soon!", 8, 255f, 0f, 0f);
                                                            }
                                                            else
                                                            {
                                                                NetMessage.SendData(0x19, this.whoAmI, -1, "Invalid Command!", 8, (float) r, (float) g, (float) b);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            StreamWriter writer2 = new StreamWriter("crap.txt");
                                            writer2.Write(exception.ToString());
                                            writer2.Close();
                                            NetMessage.SendData(0x19, this.whoAmI, -1, "ERROR PROCESSING COMMAND!", 8, (float) r, (float) g, (float) b);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("<" + Main.player[this.whoAmI].name + "> " + str7);
                                        NetMessage.SendData(0x19, -1, -1, str7, num96, (float) r, (float) g, (float) b);
                                    }
                                }
                                return;
                            }
                            string newText = str7;
                            if (num96 < 8)
                            {
                                newText = "<" + Main.player[num96].name + "> " + str7;
                                Main.player[num96].chatText = str7;
                                Main.player[num96].chatShowTime = Main.chatLength / 2;
                            }
                            Main.NewText(newText, r, g, b);
                            return;
                        }
                        case 0x1a:
                        {
                            byte num108 = this.readBuffer[index];
                            index++;
                            int hitDirection = this.readBuffer[index] - 1;
                            index++;
                            short damage = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            byte num111 = this.readBuffer[index];
                            bool pvp = false;
                            if (num111 != 0)
                            {
                                pvp = true;
                            }
                            Main.player[num108].Hurt(damage, hitDirection, pvp, true);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x1a, -1, this.whoAmI, "", num108, (float) hitDirection, (float) damage, (float) num111);
                            }
                            return;
                        }
                        case 0x1b:
                        {
                            short num112 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            float num113 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num114 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num115 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num116 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            float num117 = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            short num118 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            byte num119 = this.readBuffer[index];
                            index++;
                            byte num120 = this.readBuffer[index];
                            index++;
                            float[] numArray2 = new float[Projectile.maxAI];
                            for (int num121 = 0; num121 < Projectile.maxAI; num121++)
                            {
                                numArray2[num121] = BitConverter.ToSingle(this.readBuffer, index);
                                index += 4;
                            }
                            int num122 = 0x3e8;
                            for (int num123 = 0; num123 < 0x3e8; num123++)
                            {
                                if (((Main.projectile[num123].owner == num119) && (Main.projectile[num123].identity == num112)) && Main.projectile[num123].active)
                                {
                                    num122 = num123;
                                    break;
                                }
                            }
                            if (num122 == 0x3e8)
                            {
                                for (int num124 = 0; num124 < 0x3e8; num124++)
                                {
                                    if (!Main.projectile[num124].active)
                                    {
                                        num122 = num124;
                                        break;
                                    }
                                }
                            }
                            if (!(Main.projectile[num122].active && (Main.projectile[num122].type == num120)))
                            {
                                Main.projectile[num122].SetDefaults(num120);
                            }
                            Main.projectile[num122].identity = num112;
                            Main.projectile[num122].position.X = num113;
                            Main.projectile[num122].position.Y = num114;
                            Main.projectile[num122].velocity.X = num115;
                            Main.projectile[num122].velocity.Y = num116;
                            Main.projectile[num122].damage = num118;
                            Main.projectile[num122].type = num120;
                            Main.projectile[num122].owner = num119;
                            Main.projectile[num122].knockBack = num117;
                            for (int num125 = 0; num125 < Projectile.maxAI; num125++)
                            {
                                Main.projectile[num122].ai[num125] = numArray2[num125];
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x1b, -1, this.whoAmI, "", num122, 0f, 0f, 0f);
                            }
                            return;
                        }
                        case 0x1c:
                        {
                            short num126 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            short num127 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            float knockBack = BitConverter.ToSingle(this.readBuffer, index);
                            index += 4;
                            int num129 = this.readBuffer[index] - 1;
                            if (num127 < 0)
                            {
                                Main.npc[num126].life = 0;
                                Main.npc[num126].HitEffect(0, 10.0);
                                Main.npc[num126].active = false;
                            }
                            else
                            {
                                Main.npc[num126].StrikeNPC(num127, knockBack, num129);
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x1c, -1, this.whoAmI, "", num126, (float) num127, knockBack, (float) num129);
                                NetMessage.SendData(0x17, -1, -1, "", num126, 0f, 0f, 0f);
                            }
                            return;
                        }
                        case 0x1d:
                        {
                            short num151 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            byte num152 = this.readBuffer[index];
                            for (int num153 = 0; num153 < 0x3e8; num153++)
                            {
                                if (((Main.projectile[num153].owner == num152) && (Main.projectile[num153].identity == num151)) && Main.projectile[num153].active)
                                {
                                    Main.projectile[num153].Kill();
                                    break;
                                }
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x1d, -1, this.whoAmI, "", num151, (float) num152, 0f, 0f);
                            }
                            return;
                        }
                        case 30:
                        {
                            byte num137 = this.readBuffer[index];
                            index++;
                            byte num138 = this.readBuffer[index];
                            if (num138 != 1)
                            {
                                Main.player[num137].hostile = false;
                            }
                            else
                            {
                                Main.player[num137].hostile = true;
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(30, -1, this.whoAmI, "", num137, 0f, 0f, 0f);
                                string str15 = " has enabled PvP!";
                                if (num138 == 0)
                                {
                                    str15 = " has disabled PvP!";
                                }
                                NetMessage.SendData(0x19, -1, -1, Main.player[num137].name + str15, 8, (float) Main.teamColor[Main.player[num137].team].R, (float) Main.teamColor[Main.player[num137].team].G, (float) Main.teamColor[Main.player[num137].team].B);
                            }
                            return;
                        }
                        case 0x1f:
                            if (Main.netMode == 2)
                            {
                                int x = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                int y = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                int num132 = Chest.FindChest(x, y);
                                if ((num132 <= -1) || (Chest.UsingChest(num132) != -1))
                                {
                                    return;
                                }
                                for (int num133 = 0; num133 < Chest.maxItems; num133++)
                                {
                                    NetMessage.SendData(0x20, this.whoAmI, -1, "", num132, (float) num133, 0f, 0f);
                                }
                                NetMessage.SendData(0x21, this.whoAmI, -1, "", num132, 0f, 0f, 0f);
                                Main.player[this.whoAmI].chest = num132;
                            }
                            return;

                        case 0x20:
                        {
                            int num134 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            int num135 = this.readBuffer[index];
                            index++;
                            int num136 = this.readBuffer[index];
                            index++;
                            string str14 = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                            if (Main.chest[num134] == null)
                            {
                                Main.chest[num134] = new Chest();
                            }
                            if (Main.chest[num134].item[num135] == null)
                            {
                                Main.chest[num134].item[num135] = new Item();
                            }
                            Main.chest[num134].item[num135].SetDefaults(str14);
                            Main.chest[num134].item[num135].stack = num136;
                            return;
                        }
                        case 0x21:
                        {
                            int num139 = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            int num140 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            int num141 = BitConverter.ToInt32(this.readBuffer, index);
                            if (Main.netMode != 1)
                            {
                                Main.player[this.whoAmI].chest = num139;
                                return;
                            }
                            if (Main.player[Main.myPlayer].chest != -1)
                            {
                                if ((Main.player[Main.myPlayer].chest != num139) && (num139 != -1))
                                {
                                    Main.playerInventory = true;
                                    Main.PlaySound(12, -1, -1, 1);
                                }
                                else if ((Main.player[Main.myPlayer].chest != -1) && (num139 == -1))
                                {
                                    Main.PlaySound(11, -1, -1, 1);
                                }
                            }
                            else
                            {
                                Main.playerInventory = true;
                                Main.PlaySound(10, -1, -1, 1);
                            }
                            Main.player[Main.myPlayer].chest = num139;
                            Main.player[Main.myPlayer].chestX = num140;
                            Main.player[Main.myPlayer].chestY = num141;
                            return;
                        }
                        case 0x22:
                            if (Main.netMode == 2)
                            {
                                int num142 = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                int num143 = BitConverter.ToInt32(this.readBuffer, index);
                                WorldGen.KillTile(num142, num143, false, false, false);
                                if (!Main.tile[num142, num143].active)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 0, (float) num142, (float) num143, 0f);
                                }
                            }
                            return;

                        case 0x23:
                        {
                            int num144 = this.readBuffer[index];
                            index++;
                            int healAmount = BitConverter.ToInt16(this.readBuffer, index);
                            index += 2;
                            if (num144 != Main.myPlayer)
                            {
                                Main.player[num144].HealEffect(healAmount);
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x23, -1, this.whoAmI, "", num144, (float) healAmount, 0f, 0f);
                            }
                            return;
                        }
                        case 0x24:
                        {
                            int num146 = this.readBuffer[index];
                            index++;
                            int num147 = this.readBuffer[index];
                            index++;
                            int num148 = this.readBuffer[index];
                            index++;
                            int num149 = this.readBuffer[index];
                            index++;
                            int num150 = this.readBuffer[index];
                            index++;
                            if (num147 != 0)
                            {
                                Main.player[num146].zoneEvil = true;
                            }
                            else
                            {
                                Main.player[num146].zoneEvil = false;
                            }
                            if (num148 == 0)
                            {
                                Main.player[num146].zoneMeteor = false;
                            }
                            else
                            {
                                Main.player[num146].zoneMeteor = true;
                            }
                            if (num149 == 0)
                            {
                                Main.player[num146].zoneDungeon = false;
                            }
                            else
                            {
                                Main.player[num146].zoneDungeon = true;
                            }
                            if (num150 == 0)
                            {
                                Main.player[num146].zoneJungle = false;
                            }
                            else
                            {
                                Main.player[num146].zoneJungle = true;
                            }
                            return;
                        }
                        case 0x25:
                            if (Main.netMode == 1)
                            {
                                Netplay.password = "";
                                Main.menuMode = 0x1f;
                            }
                            return;

                        case 0x26:
                            if (Main.netMode == 2)
                            {
                                if (!(Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start) == Netplay.password))
                                {
                                    NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f);
                                    return;
                                }
                                Netplay.serverSock[this.whoAmI].state = 1;
                                NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
                            }
                            return;

                        case 0x13:
                        {
                            num5 = this.readBuffer[index];
                            index++;
                            num6 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            num7 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            num8 = this.readBuffer[index];
                            int direction = 0;
                            if (num8 == 0)
                            {
                                direction = -1;
                            }
                            switch (num5)
                            {
                                case 0:
                                    WorldGen.OpenDoor(num6, num7, direction);
                                    goto Label_50BF;

                                case 1:
                                    WorldGen.CloseDoor(num6, num7, true);
                                    goto Label_50BF;
                            }
                            goto Label_50BF;
                        }
                    }
                    if ((msgType == 0x27) && (Main.netMode == 1))
                    {
                        short num154 = BitConverter.ToInt16(this.readBuffer, index);
                        Main.item[num154].owner = 8;
                        NetMessage.SendData(0x16, -1, -1, "", num154, 0f, 0f, 0f);
                    }
                    else
                    {
                        switch (msgType)
                        {
                            case 40:
                            {
                                byte num155 = this.readBuffer[index];
                                index++;
                                int num156 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                Main.player[num155].talkNPC = num156;
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(40, -1, this.whoAmI, "", num155, 0f, 0f, 0f);
                                }
                                return;
                            }
                            case 0x29:
                            {
                                byte num157 = this.readBuffer[index];
                                index++;
                                float num158 = BitConverter.ToSingle(this.readBuffer, index);
                                index += 4;
                                int num159 = BitConverter.ToInt16(this.readBuffer, index);
                                Main.player[num157].itemRotation = num158;
                                Main.player[num157].itemAnimation = num159;
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x29, -1, this.whoAmI, "", num157, 0f, 0f, 0f);
                                }
                                return;
                            }
                            case 0x2a:
                            {
                                int num160 = this.readBuffer[index];
                                index++;
                                int num161 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                int num162 = BitConverter.ToInt16(this.readBuffer, index);
                                if (Main.netMode == 2)
                                {
                                    num160 = this.whoAmI;
                                }
                                Main.player[num160].statMana = num161;
                                Main.player[num160].statManaMax = num162;
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x2a, -1, this.whoAmI, "", num160, 0f, 0f, 0f);
                                }
                                return;
                            }
                            case 0x2b:
                            {
                                int num163 = this.readBuffer[index];
                                index++;
                                int manaAmount = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                if (num163 != Main.myPlayer)
                                {
                                    Main.player[num163].ManaEffect(manaAmount);
                                }
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x2b, -1, this.whoAmI, "", num163, (float) manaAmount, 0f, 0f);
                                }
                                return;
                            }
                            case 0x2c:
                            {
                                byte num165 = this.readBuffer[index];
                                index++;
                                int num166 = this.readBuffer[index] - 1;
                                index++;
                                short num167 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                byte num168 = this.readBuffer[index];
                                bool flag7 = false;
                                if (num168 != 0)
                                {
                                    flag7 = true;
                                }
                                Main.player[num165].KillMe((double) num167, num166, flag7);
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x2c, -1, this.whoAmI, "", num165, (float) num166, (float) num167, (float) num168);
                                }
                                return;
                            }
                            case 0x2e:
                                if (Main.netMode == 2)
                                {
                                    int num169 = BitConverter.ToInt32(this.readBuffer, index);
                                    index += 4;
                                    int num170 = BitConverter.ToInt32(this.readBuffer, index);
                                    index += 4;
                                    int num171 = Sign.ReadSign(num169, num170);
                                    if (num171 >= 0)
                                    {
                                        NetMessage.SendData(0x2f, this.whoAmI, -1, "", num171, 0f, 0f, 0f);
                                    }
                                }
                                return;

                            case 0x2f:
                            {
                                int num172 = BitConverter.ToInt16(this.readBuffer, index);
                                index += 2;
                                int num173 = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                int num174 = BitConverter.ToInt32(this.readBuffer, index);
                                index += 4;
                                string str16 = Encoding.ASCII.GetString(this.readBuffer, index, (length - index) + start);
                                Main.sign[num172] = new Sign();
                                Main.sign[num172].x = num173;
                                Main.sign[num172].y = num174;
                                Sign.TextSign(num172, str16);
                                if (((Main.netMode == 1) && (Main.sign[num172] != null)) && (num172 != Main.player[Main.myPlayer].sign))
                                {
                                    Main.playerInventory = false;
                                    Main.player[Main.myPlayer].talkNPC = -1;
                                    Main.editSign = false;
                                    Main.PlaySound(10, -1, -1, 1);
                                    Main.player[Main.myPlayer].sign = num172;
                                    Main.npcChatText = Main.sign[num172].text;
                                }
                                return;
                            }
                            case 0x2d:
                                num9 = this.readBuffer[index];
                                index++;
                                num10 = this.readBuffer[index];
                                index++;
                                team = Main.player[num9].team;
                                Main.player[num9].team = num10;
                                if (Main.netMode != 2)
                                {
                                    return;
                                }
                                NetMessage.SendData(0x2d, -1, this.whoAmI, "", num9, 0f, 0f, 0f);
                                str = "";
                                switch (num10)
                                {
                                    case 0:
                                        str = " is no longer on a party.";
                                        goto Label_50F6;

                                    case 1:
                                        str = " has joined the red party.";
                                        goto Label_50F6;

                                    case 2:
                                        str = " has joined the green party.";
                                        goto Label_50F6;

                                    case 3:
                                        str = " has joined the blue party.";
                                        goto Label_50F6;

                                    case 4:
                                        str = " has joined the yellow party.";
                                        goto Label_50F6;
                                }
                                goto Label_50F6;
                        }
                        if (msgType == 0x30)
                        {
                            int num175 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            int num176 = BitConverter.ToInt32(this.readBuffer, index);
                            index += 4;
                            byte num177 = this.readBuffer[index];
                            index++;
                            byte num178 = this.readBuffer[index];
                            index++;
                            if (Main.tile[num175, num176] == null)
                            {
                                Main.tile[num175, num176] = new Tile();
                            }
                            lock (Main.tile[num175, num176])
                            {
                                Main.tile[num175, num176].liquid = num177;
                                if (num178 == 1)
                                {
                                    Main.tile[num175, num176].lava = true;
                                }
                                else
                                {
                                    Main.tile[num175, num176].lava = false;
                                }
                                if (Main.netMode == 2)
                                {
                                    WorldGen.SquareTileFrame(num175, num176, true);
                                }
                                return;
                            }
                        }
                        if ((msgType == 0x31) && (Netplay.clientSock.state == 6))
                        {
                            Netplay.clientSock.state = 10;
                            Main.player[Main.myPlayer].Spawn();
                        }
                    }
                    return;
            }
            int number = 0x546;
            if (flag2)
            {
                number *= 2;
            }
            if (Netplay.serverSock[this.whoAmI].state == 2)
            {
                Netplay.serverSock[this.whoAmI].state = 3;
            }
            NetMessage.SendData(9, this.whoAmI, -1, "Receiving tile data", number, 0f, 0f, 0f);
            Netplay.serverSock[this.whoAmI].statusText2 = "is receiving tile data";
            ServerSock sock = Netplay.serverSock[this.whoAmI];
            sock.statusMax += number;
            int sectionX = Netplay.GetSectionX(Main.spawnTileX);
            int sectionY = Netplay.GetSectionY(Main.spawnTileY);
            for (int i = sectionX - 2; i < (sectionX + 3); i++)
            {
                for (int num34 = sectionY - 1; num34 < (sectionY + 2); num34++)
                {
                    NetMessage.SendSection(this.whoAmI, i, num34);
                }
            }
            if (flag2)
            {
                num28 = Netplay.GetSectionX(num28);
                num29 = Netplay.GetSectionY(num29);
                for (int num35 = num28 - 2; num35 < (num28 + 3); num35++)
                {
                    for (int num36 = num29 - 1; num36 < (num29 + 2); num36++)
                    {
                        NetMessage.SendSection(this.whoAmI, num35, num36);
                    }
                }
                NetMessage.SendData(11, this.whoAmI, -1, "", num28 - 2, (float) (num29 - 1), (float) (num28 + 2), (float) (num29 + 1));
            }
            NetMessage.SendData(11, this.whoAmI, -1, "", sectionX - 2, (float) (sectionY - 1), (float) (sectionX + 2), (float) (sectionY + 1));
            for (int j = 0; j < 200; j++)
            {
                if (Main.item[j].active)
                {
                    NetMessage.SendData(0x15, this.whoAmI, -1, "", j, 0f, 0f, 0f);
                    NetMessage.SendData(0x16, this.whoAmI, -1, "", j, 0f, 0f, 0f);
                }
            }
            for (int k = 0; k < 0x3e8; k++)
            {
                if (Main.npc[k].active)
                {
                    NetMessage.SendData(0x17, this.whoAmI, -1, "", k, 0f, 0f, 0f);
                }
            }
            NetMessage.SendData(0x31, this.whoAmI, -1, "", 0, 0f, 0f, 0f);
            return;
        Label_5068:;
            if (Main.netMode == 2)
            {
                NetMessage.SendData(0x11, -1, this.whoAmI, "", num, (float) num2, (float) num3, (float) num4);
                if ((num == 1) && (num4 == 0x35))
                {
                    NetMessage.SendTileSquare(-1, num2, num3, 1);
                }
            }
            return;
        Label_50BF:
            if (Main.netMode == 2)
            {
                NetMessage.SendData(0x13, -1, this.whoAmI, "", num5, (float) num6, (float) num7, (float) num8);
            }
            return;
        Label_50F6:
            num12 = 0;
            while (num12 < 8)
            {
                if (((num12 == this.whoAmI) || ((team > 0) && (Main.player[num12].team == team))) || ((num10 > 0) && (Main.player[num12].team == num10)))
                {
                    NetMessage.SendData(0x19, num12, -1, Main.player[num9].name + str, 8, (float) Main.teamColor[num10].R, (float) Main.teamColor[num10].G, (float) Main.teamColor[num10].B);
                }
                num12++;
            }
        }

        public Player GetPlayerByName(string name)
        {
            Player player = null;
            int num = 0;
            foreach (Player player2 in Main.player)
            {
                if (player2.name.ToLower() == name.ToLower())
                {
                    return player2;
                }
                if ((player2.name.CompareTo(name) > num) && (player2.name.IndexOf(name) > -1))
                {
                    num = player2.name.CompareTo(name);
                    player = player2;
                }
            }
            return player;
        }

        public void god()
        {
            Player player = Main.player[this.whoAmI];
            player.statManaMax = 10;
            player.statLifeMax = 20;
            while (true)
            {
                bool flag = false;
                if ((player.statMana != 10) || (player.statLife != 10))
                {
                    flag = true;
                }
                player.statMana = 10;
                player.statLife = 20;
                player.immune = true;
                if (flag)
                {
                    NetMessage.SendData(0x10, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                    NetMessage.SendData(0x2a, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f);
                }
            }
        }

        public void Reset()
        {
            this.writeBuffer = new byte[0xffff];
            this.writeLocked = false;
            this.messageLength = 0;
            this.totalData = 0;
            this.spamCount = 0;
            this.broadcast = false;
            this.checkBytes = false;
        }
    }
}


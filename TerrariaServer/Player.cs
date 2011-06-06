using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Terraria
{
    public class Player
    {
        public int accDepthMeter;
        public bool accFlipper;
        public int accWatch;
        public bool active;
        public bool isOP;
        public int activeNPCs;
        public bool[] adjTile;
        public Item[] armor;
        public int attackCD;
        public Item[] bank;
        public int body;
        public Rectangle bodyFrame;
        public double bodyFrameCounter;
        public Vector2 bodyPosition;
        public float bodyRotation;
        public Vector2 bodyVelocity;
        public bool boneArmor;
        public int breath;
        public int breathCD;
        public int breathMax;
        public bool canRocket;
        public int changeItem;
        public bool channel;
        public int chatShowTime;
        public string chatText;
        public int chest;
        public int chestX;
        public int chestY;
        public bool controlDown;
        public bool controlInv;
        public bool controlJump;
        public bool controlLeft;
        public bool controlRight;
        public bool controlThrow;
        public bool controlUp;
        public bool controlUseItem;
        public bool controlUseTile;
        public bool dead;
        public bool delayUseItem;
        public int direction;
        public bool doubleJump;
        public Color eyeColor;
        public int fallStart;
        public bool fireWalk;
        public int grapCount;
        public int[] grappling;
        public int hair;
        public Color hairColor;
        public Rectangle hairFrame;
        public int head;
        public Rectangle headFrame;
        public double headFrameCounter;
        public Vector2 headPosition;
        public float headRotation;
        public Vector2 headVelocity;
        public int height;
        public int hitTile;
        public int hitTileX;
        public int hitTileY;
        public bool hostile;
        public bool immune;
        public int immuneAlpha;
        public int immuneAlphaDirection;
        public int immuneTime;
        public Item[] inventory;
        public int itemAnimation;
        public int itemAnimationMax;
        private static int itemGrabRange = 38;
        private static float itemGrabSpeed = 0.45f;
        private static float itemGrabSpeedMax = 4f;
        public int itemHeight;
        public Vector2 itemLocation;
        public float itemRotation;
        public int itemTime;
        public int itemWidth;
        public int jump;
        public bool jumpAgain;
        public bool jumpBoost;
        private static int jumpHeight = 15;
        private static float jumpSpeed = 5.01f;
        public bool lavaWet;
        public Rectangle legFrame;
        public double legFrameCounter;
        public Vector2 legPosition;
        public float legRotation;
        public int legs;
        public Vector2 legVelocity;
        public int lifeRegen;
        public int lifeRegenCount;
        public float magicBoost;
        public float manaCost;
        public int manaRegen;
        public int manaRegenCount;
        public int manaRegenDelay;
        public float meleeSpeed;
        public bool mouseInterface;
        public string name;
        public bool noFallDmg;
        public bool noKnockback;
        public bool[] oldAdjTile;
        public Vector2 oldVelocity;
        public Color pantsColor;
        public Vector2 position;
        public int potionDelay;
        public bool pvpDeath;
        public bool releaseInventory;
        public bool releaseJump;
        public bool releaseUseItem;
        public bool releaseUseTile;
        public int respawnTimer;
        public bool rocketBoots;
        public int rocketDelay;
        public int rocketDelay2;
        public bool rocketFrame;
        public bool rocketRelease;
        public int runSoundDelay;
        public int selectedItem;
        public string setBonus;
        public float shadow;
        public int shadowCount;
        public Vector2[] shadowPos;
        public Color shirtColor;
        public Color shoeColor;
        public bool showItemIcon;
        public int showItemIcon2;
        public int sign;
        public Color skinColor;
        public int slowCount;
        public bool spaceGun;
        public bool spawnMax;
        public int SpawnX;
        public int SpawnY;
        public int[] spI;
        public string[] spN;
        public int[] spX;
        public int[] spY;
        public int statAttack;
        public int statDefense;
        public int statLife;
        public int statLifeMax;
        public int statMana;
        public int statManaMax;
        public int step;
        public int swimTime;
        public int talkNPC;
        public int team;
        public static int tileRangeX = 5;
        public static int tileRangeY = 4;
        private static int tileTargetX;
        private static int tileTargetY;
        public int townNPCs;
        public Color underShirtColor;
        public Vector2 velocity;
        public bool wet;
        public byte wetCount;
        public int whoAmi;
        public int width;
        public bool zoneDungeon;
        public bool zoneEvil;
        public bool zoneJungle;
        public bool zoneMeteor;
        public Player()
        {
            this.pvpDeath = false;
            this.zoneDungeon = false;
            this.zoneEvil = false;
            this.zoneMeteor = false;
            this.zoneJungle = false;
            this.boneArmor = false;
            this.townNPCs = 0;
            this.team = 0;
            this.chatText = "";
            this.sign = -1;
            this.chatShowTime = 0;
            this.changeItem = -1;
            this.selectedItem = 0;
            this.armor = new Item[8];
            this.breathCD = 0;
            this.breathMax = 200;
            this.breath = 200;
            this.setBonus = "";
            this.inventory = new Item[44];
            this.bank = new Item[Chest.maxItems];
            this.dead = false;
            this.name = "";
            this.potionDelay = 0;
            this.wet = false;
            this.wetCount = 0;
            this.lavaWet = false;
            this.head = -1;
            this.body = -1;
            this.legs = -1;
            this.width = 20;
            this.height = 42;
            this.direction = 1;
            this.showItemIcon = false;
            this.showItemIcon2 = 0;
            this.whoAmi = 0;
            this.runSoundDelay = 0;
            this.shadow = 0f;
            this.manaCost = 1f;
            this.fireWalk = false;
            this.shadowPos = new Vector2[3];
            this.shadowCount = 0;
            this.channel = false;
            this.step = -1;
            this.meleeSpeed = 1f;
            this.statDefense = 0;
            this.statAttack = 0;
            this.statLifeMax = 100;
            this.statLife = 100;
            this.statMana = 0;
            this.statManaMax = 0;
            this.lifeRegen = 0;
            this.lifeRegenCount = 0;
            this.manaRegen = 0;
            this.manaRegenCount = 0;
            this.manaRegenDelay = 0;
            this.noKnockback = false;
            this.spaceGun = false;
            this.magicBoost = 1f;
            this.SpawnX = -1;
            this.SpawnY = -1;
            this.spX = new int[200];
            this.spY = new int[200];
            this.spN = new string[200];
            this.spI = new int[200];
            this.adjTile = new bool[80];
            this.oldAdjTile = new bool[80];
            this.hairColor = new Color(215, 90, 55);
            this.skinColor = new Color(255, 125, 90);
            this.eyeColor = new Color(105, 90, 75);
            this.shirtColor = new Color(175, 165, 140);
            this.underShirtColor = new Color(160, 180, 215);
            this.pantsColor = new Color(255, 230, 175);
            this.shoeColor = new Color(160, 105, 60);
            this.hair = 0;
            this.hostile = false;
            this.accWatch = 0;
            this.accDepthMeter = 0;
            this.accFlipper = false;
            this.doubleJump = false;
            this.jumpAgain = false;
            this.spawnMax = false;
            this.grappling = new int[20];
            this.grapCount = 0;
            this.rocketDelay = 0;
            this.rocketDelay2 = 0;
            this.rocketRelease = false;
            this.rocketFrame = false;
            this.rocketBoots = false;
            this.canRocket = false;
            this.jumpBoost = false;
            this.noFallDmg = false;
            this.swimTime = 0;
            this.chest = -1;
            this.chestX = 0;
            this.chestY = 0;
            this.talkNPC = -1;
            this.fallStart = 0;
            this.slowCount = 0;
            for (int i = 0; i < 44; i++)
            {
                if (i < 8)
                {
                    this.armor[i] = new Item();
                    this.armor[i].name = "";
                }
                this.inventory[i] = new Item();
                this.inventory[i].name = "";
            }
            for (int i = 0; i < Chest.maxItems; i++)
            {
                this.bank[i] = new Item();
                this.bank[i].name = "";
            }
            this.grappling[0] = -1;
            this.inventory[0].SetDefaults("Copper Pickaxe");
            this.inventory[1].SetDefaults("Copper Axe");
            for (int i = 0; i < 80; i++)
            {
                this.adjTile[i] = false;
                this.oldAdjTile[i] = false;
            }
        }
        public void AdjTiles()
        {
            int num = 4;
            int num2 = 3;
            for (int i = 0; i < 80; i++)
            {
                this.oldAdjTile[i] = this.adjTile[i];
                this.adjTile[i] = false;
            }
            int num3 = (int)((this.position.X + (float)(this.width / 2)) / 16f);
            int num4 = (int)((this.position.Y + (float)this.height) / 16f);
            for (int num5 = num3 - num; num5 <= num3 + num; num5++)
            {
                for (int j = num4 - num2; j < num4 + num2; j++)
                {
                    if (Main.tile[num5, j].active)
                    {
                        this.adjTile[(int)Main.tile[num5, j].type] = true;
                        if (Main.tile[num5, j].type == 77)
                        {
                            this.adjTile[17] = true;
                        }
                    }
                }
            }
            if (Main.playerInventory)
            {
                bool flag = false;
                for (int i = 0; i < 80; i++)
                {
                    if (this.oldAdjTile[i] != this.adjTile[i])
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    Recipe.FindRecipes();
                }
            }
        }
        public bool BuyItem(int price)
        {
            bool result;
            if (price != 0)
            {
                int num = 0;
                int i = price;
                Item[] array = new Item[44];
                for (int j = 0; j < 44; j++)
                {
                    array[j] = new Item();
                    array[j] = (Item)this.inventory[j].Clone();
                    if (this.inventory[j].type == 71)
                    {
                        num += this.inventory[j].stack;
                    }
                    if (this.inventory[j].type == 72)
                    {
                        num += this.inventory[j].stack * 100;
                    }
                    if (this.inventory[j].type == 73)
                    {
                        num += this.inventory[j].stack * 10000;
                    }
                    if (this.inventory[j].type == 74)
                    {
                        num += this.inventory[j].stack * 1000000;
                    }
                }
                if (num >= price)
                {
                    i = price;
                    while (i > 0)
                    {
                        if (i >= 1000000)
                        {
                            for (int j = 0; j < 44; j++)
                            {
                                if (this.inventory[j].type == 74)
                                {
                                    while (this.inventory[j].stack > 0 && i >= 1000000)
                                    {
                                        i -= 1000000;
                                        Item item = this.inventory[j];
                                        item.stack--;
                                        if (this.inventory[j].stack == 0)
                                        {
                                            this.inventory[j].type = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (i >= 10000)
                        {
                            for (int j = 0; j < 44; j++)
                            {
                                if (this.inventory[j].type == 73)
                                {
                                    while (this.inventory[j].stack > 0 && i >= 10000)
                                    {
                                        i -= 10000;
                                        Item item2 = this.inventory[j];
                                        item2.stack--;
                                        if (this.inventory[j].stack == 0)
                                        {
                                            this.inventory[j].type = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (i >= 100)
                        {
                            for (int j = 0; j < 44; j++)
                            {
                                if (this.inventory[j].type == 72)
                                {
                                    while (this.inventory[j].stack > 0 && i >= 100)
                                    {
                                        i -= 100;
                                        Item item3 = this.inventory[j];
                                        item3.stack--;
                                        if (this.inventory[j].stack == 0)
                                        {
                                            this.inventory[j].type = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (i >= 1)
                        {
                            for (int j = 0; j < 44; j++)
                            {
                                if (this.inventory[j].type == 71)
                                {
                                    while (this.inventory[j].stack > 0 && i >= 1)
                                    {
                                        i--;
                                        Item item4 = this.inventory[j];
                                        item4.stack--;
                                        if (this.inventory[j].stack == 0)
                                        {
                                            this.inventory[j].type = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (i > 0)
                        {
                            int num2 = -1;
                            for (int j = 43; j >= 0; j--)
                            {
                                if (this.inventory[j].type == 0 || this.inventory[j].stack == 0)
                                {
                                    num2 = j;
                                    break;
                                }
                            }
                            if (num2 < 0)
                            {
                                for (int j = 0; j < 44; j++)
                                {
                                    this.inventory[j] = (Item)array[j].Clone();
                                }
                                result = false;
                                return result;
                            }
                            bool flag = true;
                            if (i >= 10000)
                            {
                                for (int j = 0; j < 44; j++)
                                {
                                    if (this.inventory[j].type == 74 && this.inventory[j].stack >= 1)
                                    {
                                        Item item5 = this.inventory[j];
                                        item5.stack--;
                                        if (this.inventory[j].stack == 0)
                                        {
                                            this.inventory[j].type = 0;
                                        }
                                        this.inventory[num2].SetDefaults(73);
                                        this.inventory[num2].stack = 100;
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (i >= 100)
                                {
                                    for (int j = 0; j < 44; j++)
                                    {
                                        if (this.inventory[j].type == 73 && this.inventory[j].stack >= 1)
                                        {
                                            Item item6 = this.inventory[j];
                                            item6.stack--;
                                            if (this.inventory[j].stack == 0)
                                            {
                                                this.inventory[j].type = 0;
                                            }
                                            this.inventory[num2].SetDefaults(72);
                                            this.inventory[num2].stack = 100;
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (i >= 1)
                                    {
                                        for (int j = 0; j < 44; j++)
                                        {
                                            if (this.inventory[j].type == 72 && this.inventory[j].stack >= 1)
                                            {
                                                Item item7 = this.inventory[j];
                                                item7.stack--;
                                                if (this.inventory[j].stack == 0)
                                                {
                                                    this.inventory[j].type = 0;
                                                }
                                                this.inventory[num2].SetDefaults(71);
                                                this.inventory[num2].stack = 100;
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (flag)
                            {
                                if (i < 10000)
                                {
                                    for (int j = 0; j < 44; j++)
                                    {
                                        if (this.inventory[j].type == 73 && this.inventory[j].stack >= 1)
                                        {
                                            Item item8 = this.inventory[j];
                                            item8.stack--;
                                            if (this.inventory[j].stack == 0)
                                            {
                                                this.inventory[j].type = 0;
                                            }
                                            this.inventory[num2].SetDefaults(72);
                                            this.inventory[num2].stack = 100;
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                                if (flag && i < 1000000)
                                {
                                    for (int j = 0; j < 44; j++)
                                    {
                                        if (this.inventory[j].type == 74 && this.inventory[j].stack >= 1)
                                        {
                                            Item item9 = this.inventory[j];
                                            item9.stack--;
                                            if (this.inventory[j].stack == 0)
                                            {
                                                this.inventory[j].type = 0;
                                            }
                                            this.inventory[num2].SetDefaults(73);
                                            this.inventory[num2].stack = 100;
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }
        public void ChangeSpawn(int x, int y)
        {
            for (int i = 0; i < 200; i++)
            {
                if (this.spN[i] == null)
                {
                    break;
                }
                if (this.spN[i] == Main.worldName && this.spI[i] == Main.worldID)
                {
                    for (int j = i; j > 0; j--)
                    {
                        this.spN[j] = this.spN[j - 1];
                        this.spI[j] = this.spI[j - 1];
                        this.spX[j] = this.spX[j - 1];
                        this.spY[j] = this.spY[j - 1];
                    }
                    this.spN[0] = Main.worldName;
                    this.spI[0] = Main.worldID;
                    this.spX[0] = x;
                    this.spY[0] = y;
                    return;
                }
            }
            for (int j = 199; j > 0; j--)
            {
                if (this.spN[j - 1] != null)
                {
                    this.spN[j] = this.spN[j - 1];
                    this.spI[j] = this.spI[j - 1];
                    this.spX[j] = this.spX[j - 1];
                    this.spY[j] = this.spY[j - 1];
                }
            }
            this.spN[0] = Main.worldName;
            this.spI[0] = Main.worldID;
            this.spX[0] = x;
            this.spY[0] = y;
        }
        public static bool CheckSpawn(int x, int y)
        {
            bool result;
            if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesX - 10)
            {
                result = false;
            }
            else
            {
                if (Main.tile[x, y - 1] == null)
                {
                    result = false;
                }
                else
                {
                    if (!Main.tile[x, y - 1].active || Main.tile[x, y - 1].type != 79)
                    {
                        result = false;
                    }
                    else
                    {
                        for (int num = x - 1; num <= x + 1; num++)
                        {
                            for (int i = y - 3; i < y; i++)
                            {
                                if (Main.tile[num, i] == null)
                                {
                                    result = false;
                                    return result;
                                }
                                if (Main.tile[num, i].active && Main.tileSolid[(int)Main.tile[num, i].type] && !Main.tileSolidTop[(int)Main.tile[num, i].type])
                                {
                                    result = false;
                                    return result;
                                }
                            }
                        }
                        result = WorldGen.StartRoomCheck(x, y - 1);
                    }
                }
            }
            return result;
        }
        public object clientClone()
        {
            Player player = new Player
            {
                zoneEvil = this.zoneEvil,
                zoneMeteor = this.zoneMeteor,
                zoneDungeon = this.zoneDungeon,
                zoneJungle = this.zoneJungle,
                direction = this.direction,
                selectedItem = this.selectedItem,
                controlUp = this.controlUp,
                controlDown = this.controlDown,
                controlLeft = this.controlLeft,
                controlRight = this.controlRight,
                controlJump = this.controlJump,
                controlUseItem = this.controlUseItem,
                statLife = this.statLife,
                statLifeMax = this.statLifeMax,
                statMana = this.statMana,
                statManaMax = this.statManaMax
            };
            player.position.X = this.position.X;
            player.chest = this.chest;
            player.talkNPC = this.talkNPC;
            for (int i = 0; i < 44; i++)
            {
                player.inventory[i] = (Item)this.inventory[i].Clone();
                if (i < 8)
                {
                    player.armor[i] = (Item)this.armor[i].Clone();
                }
            }
            return player;
        }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
        private static bool DecryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            byte[] bytes = new System.Text.UnicodeEncoding().GetBytes(s);
            System.IO.FileStream fileStream = new System.IO.FileStream(inputFile, System.IO.FileMode.Open);
            System.Security.Cryptography.RijndaelManaged rijndaelManaged = new System.Security.Cryptography.RijndaelManaged();
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), System.Security.Cryptography.CryptoStreamMode.Read);
            System.IO.FileStream fileStream2 = new System.IO.FileStream(outputFile, System.IO.FileMode.Create);
            bool result;
            try
            {
                int num;
                while ((num = cryptoStream.ReadByte()) != -1)
                {
                    fileStream2.WriteByte((byte)num);
                }
                fileStream2.Close();
                cryptoStream.Close();
                fileStream.Close();
            }
            catch
            {
                fileStream2.Close();
                fileStream.Close();
                System.IO.File.Delete(outputFile);
                result = true;
                return result;
            }
            result = false;
            return result;
        }
        public void DoCoins(int i)
        {
            if (this.inventory[i].stack == 100 && (this.inventory[i].type == 71 || this.inventory[i].type == 72 || this.inventory[i].type == 73))
            {
                this.inventory[i].SetDefaults(this.inventory[i].type + 1);
                for (int j = 0; j < 44; j++)
                {
                    if (this.inventory[j].IsTheSameAs(this.inventory[i]) && j != i && this.inventory[j].stack < this.inventory[j].maxStack)
                    {
                        Item item = this.inventory[j];
                        item.stack++;
                        this.inventory[i].SetDefaults("");
                        this.inventory[i].active = false;
                        this.inventory[i].name = "";
                        this.inventory[i].type = 0;
                        this.inventory[i].stack = 0;
                        this.DoCoins(j);
                    }
                }
            }
        }
        public void DropItems()
        {
            for (int i = 0; i < 44; i++)
            {
                if (this.inventory[i].type >= 71 && this.inventory[i].type <= 74)
                {
                    int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false);
                    int num2 = this.inventory[i].stack / 2;
                    num2 = this.inventory[i].stack - num2;
                    Item item = this.inventory[i];
                    item.stack -= num2;
                    if (this.inventory[i].stack <= 0)
                    {
                        this.inventory[i] = new Item();
                    }
                    Main.item[num].stack = num2;
                    Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                    Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                    Main.item[num].noGrabDelay = 100;
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f);
                    }
                }
            }
        }
        private static void EncryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            byte[] bytes = new System.Text.UnicodeEncoding().GetBytes(s);
            System.IO.FileStream fileStream = new System.IO.FileStream(outputFile, System.IO.FileMode.Create);
            System.Security.Cryptography.RijndaelManaged rijndaelManaged = new System.Security.Cryptography.RijndaelManaged();
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(bytes, bytes), System.Security.Cryptography.CryptoStreamMode.Write);
            System.IO.FileStream fileStream2 = new System.IO.FileStream(inputFile, System.IO.FileMode.Open);
            int num;
            while ((num = fileStream2.ReadByte()) != -1)
            {
                cryptoStream.WriteByte((byte)num);
            }
            fileStream2.Close();
            cryptoStream.Close();
            fileStream.Close();
        }
        public static byte FindClosest(Vector2 Position, int Width, int Height)
        {
            byte result = 0;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active)
                {
                    result = (byte)i;
                    break;
                }
            }
            float num = -1f;
            for (int j = 0; j < 255; j++)
            {
                if (Main.player[j].active && !Main.player[j].dead && (num == -1f || System.Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - Position.X + (float)(Width / 2)) + System.Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - Position.Y + (float)(Height / 2)) < num))
                {
                    num = System.Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - Position.X + (float)(Width / 2)) + System.Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - Position.Y + (float)(Height / 2));
                    result = (byte)j;
                }
            }
            return result;
        }
        public void FindSpawn()
        {
            for (int i = 0; i < 200; i++)
            {
                if (this.spN[i] == null)
                {
                    this.SpawnX = -1;
                    this.SpawnY = -1;
                    break;
                }
                if (this.spN[i] == Main.worldName && this.spI[i] == Main.worldID)
                {
                    this.SpawnX = this.spX[i];
                    this.SpawnY = this.spY[i];
                    break;
                }
            }
        }
        public Color GetDeathAlpha(Color newColor)
        {
            int r = (int)newColor.R + (int)((double)this.immuneAlpha * 0.9);
            int g = (int)newColor.G + (int)((double)this.immuneAlpha * 0.5);
            int b = (int)newColor.B + (int)((double)this.immuneAlpha * 0.5);
            int num = (int)newColor.A + (int)((double)this.immuneAlpha * 0.4);
            if (num < 0)
            {
                num = 0;
            }
            if (num > 255)
            {
                num = 255;
            }
            return new Color(r, g, b, num);
        }
        public Color GetImmuneAlpha(Color newColor)
        {
            float num = (float)(255 - this.immuneAlpha) / 255f;
            if (this.shadow > 0f)
            {
                num *= 1f - this.shadow;
            }
            int r = (int)((float)newColor.R * num);
            int g = (int)((float)newColor.G * num);
            int b = (int)((float)newColor.B * num);
            int num2 = (int)((float)newColor.A * num);
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num2 > 255)
            {
                num2 = 255;
            }
            return new Color(r, g, b, num2);
        }
        public Item GetItem(int plr, Item newItem)
        {
            Item result;
            if (newItem.noGrabDelay <= 0)
            {
                int num = 0;
                if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
                {
                    num = -4;
                }
                for (int i = num; i < 40; i++)
                {
                    int num2 = i;
                    if (num2 < 0)
                    {
                        num2 = 44 + i;
                    }
                    if (this.inventory[num2].type > 0 && this.inventory[num2].stack < this.inventory[num2].maxStack && newItem.IsTheSameAs(this.inventory[num2]))
                    {
                        if (newItem.stack + this.inventory[num2].stack <= this.inventory[num2].maxStack)
                        {
                            Item item = this.inventory[num2];
                            item.stack += newItem.stack;
                            this.DoCoins(num2);
                            if (plr == Main.myPlayer)
                            {
                                Recipe.FindRecipes();
                            }
                            result = new Item();
                            return result;
                        }
                        newItem.stack -= this.inventory[num2].maxStack - this.inventory[num2].stack;
                        this.inventory[num2].stack = this.inventory[num2].maxStack;
                        this.DoCoins(num2);
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                    }
                }
                for (int i = num; i < 40; i++)
                {
                    int num2 = i;
                    if (num2 < 0)
                    {
                        num2 = 44 + i;
                    }
                    if (this.inventory[num2].type == 0)
                    {
                        this.inventory[num2] = newItem;
                        this.DoCoins(num2);
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                        result = new Item();
                        return result;
                    }
                }
            }
            result = newItem;
            return result;
        }
        public void HealEffect(int healAmount)
        {
            if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
            {
                NetMessage.SendData(35, -1, -1, "", this.whoAmi, (float)healAmount, 0f, 0f);
            }
        }
        public double Hurt(int Damage, int hitDirection, bool pvp = false, bool quiet = false)
        {
            double result;
            if (this.immune || Main.godMode)
            {
                result = 0.0;
            }
            else
            {
                PlayerHurtEvent playerHurtEvent = new PlayerHurtEvent(this, Damage);
                PluginManager.callHook(Hook.PLAYER_HURT, playerHurtEvent);
                if (!playerHurtEvent.getState())
                {
                    result = 0.0;
                }
                else
                {
                    int num = Damage;
                    if (pvp)
                    {
                        num *= 2;
                    }
                    double num2 = Main.CalculateDamage(num, this.statDefense);
                    if (num2 >= 1.0)
                    {
                        if (Main.netMode == 1 && this.whoAmi == Main.myPlayer && !quiet)
                        {
                            int num3 = 0;
                            if (pvp)
                            {
                                num3 = 1;
                            }
                            NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f);
                            NetMessage.SendData(16, -1, -1, "", this.whoAmi, 0f, 0f, 0f);
                            NetMessage.SendData(26, -1, -1, "", this.whoAmi, (float)hitDirection, (float)Damage, (float)num3);
                        }
                        this.statLife -= (int)num2;
                        this.immune = true;
                        this.immuneTime = 40;
                        if (pvp)
                        {
                            this.immuneTime = 8;
                        }
                        if (!this.noKnockback && hitDirection != 0)
                        {
                            this.velocity.X = 4.5f * (float)hitDirection;
                            this.velocity.Y = -3.5f;
                        }
                        this.statLife = 0;
                        if (this.whoAmi == Main.myPlayer)
                        {
                            this.KillMe(num2, hitDirection, pvp);
                        }
                    }
                    if (pvp)
                    {
                        num2 = Main.CalculateDamage(num, this.statDefense);
                    }
                    result = num2;
                }
            }
            return result;
        }
        public void sendMessage(string msg, int r = 255, int g = 255, int b = 255)
        {
            NetMessage.SendData(25, this.whoAmi, -1, msg, 255, (float)r, (float)g, (float)b);
        }
        public void kick(string reason)
        {
            NetMessage.SendData(2, this.whoAmi, -1, reason, 0, 0f, 0f, 0f);
        }
        public void ItemCheck(int i)
        {
            if (this.inventory[this.selectedItem].autoReuse)
            {
                this.releaseUseItem = true;
                if (this.itemAnimation == 1 && this.inventory[this.selectedItem].stack > 0)
                {
                    this.itemAnimation = 0;
                }
            }
            if (this.controlUseItem && this.itemAnimation == 0 && this.releaseUseItem && this.inventory[this.selectedItem].useStyle > 0)
            {
                bool flag = true;
                if (this.inventory[this.selectedItem].shoot == 6 || this.inventory[this.selectedItem].shoot == 19 || this.inventory[this.selectedItem].shoot == 33)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == this.inventory[this.selectedItem].shoot)
                        {
                            flag = false;
                        }
                    }
                }
                if (this.inventory[this.selectedItem].potion)
                {
                    if (this.potionDelay <= 0)
                    {
                        this.potionDelay = Item.potionDelay;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (this.inventory[this.selectedItem].mana > 0 && (this.inventory[this.selectedItem].type != 127 || !this.spaceGun))
                {
                    if (this.statMana >= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost))
                    {
                        this.statMana -= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost);
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (this.inventory[this.selectedItem].type == 43 && Main.dayTime)
                {
                    flag = false;
                }
                if (this.inventory[this.selectedItem].type == 70 && !this.zoneEvil)
                {
                    flag = false;
                }
                if (this.inventory[this.selectedItem].shoot == 17 && flag && i == Main.myPlayer)
                {
                    int k = (int)Main.screenPosition.X / 16;
                    int l = (int)Main.screenPosition.Y / 16;
                    if (Main.tile[k, l].active && (Main.tile[k, l].type == 0 || Main.tile[k, l].type == 2 || Main.tile[k, l].type == 23))
                    {
                        WorldGen.KillTile(k, l, false, false, true);
                        if (!Main.tile[k, l].active)
                        {
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(17, -1, -1, "", 4, (float)k, (float)l, 0f);
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag && this.inventory[this.selectedItem].useAmmo > 0)
                {
                    flag = false;
                    for (int j = 0; j < 44; j++)
                    {
                        if (this.inventory[j].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[j].stack > 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    if (this.grappling[0] > -1)
                    {
                        if (this.controlRight)
                        {
                            this.direction = 1;
                        }
                        else
                        {
                            if (this.controlLeft)
                            {
                                this.direction = -1;
                            }
                        }
                    }
                    this.channel = this.inventory[this.selectedItem].channel;
                    this.attackCD = 0;
                    if (this.inventory[this.selectedItem].shoot > 0 || this.inventory[this.selectedItem].damage == 0)
                    {
                        this.meleeSpeed = 1f;
                    }
                    this.itemAnimation = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
                    this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
                    if (this.inventory[this.selectedItem].useSound > 0)
                    {
                    }
                }
                if (flag && this.inventory[this.selectedItem].shoot == 18)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        if (Main.projectile[j].active && Main.projectile[j].owner == i && Main.projectile[j].type == this.inventory[this.selectedItem].shoot)
                        {
                            Main.projectile[j].Kill();
                        }
                    }
                }
            }
            if (!this.controlUseItem)
            {
                this.channel = false;
            }
            if (!Main.dedServ)
            {
                if (this.itemAnimation > 0)
                {
                    if (this.inventory[this.selectedItem].mana > 0)
                    {
                        this.manaRegenDelay = 180;
                    }
                    if (Main.dedServ)
                    {
                        this.itemHeight = this.inventory[this.selectedItem].height;
                        this.itemWidth = this.inventory[this.selectedItem].width;
                    }
                    else
                    {
                        this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
                        this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
                    }
                    this.itemAnimation--;
                    if (this.inventory[this.selectedItem].useStyle == 1)
                    {
                        if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
                        {
                            float num = 10f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                            {
                                num = 14f;
                            }
                            this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num) * (float)this.direction;
                            this.itemLocation.Y = this.position.Y + 24f;
                        }
                        else
                        {
                            if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
                            {
                                float num = 10f;
                                if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                                {
                                    num = 18f;
                                }
                                this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num) * (float)this.direction;
                                num = 10f;
                                if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
                                {
                                    num = 8f;
                                }
                                this.itemLocation.Y = this.position.Y + num;
                            }
                            else
                            {
                                float num = 6f;
                                if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                                {
                                    num = 14f;
                                }
                                this.itemLocation.X = this.position.X + (float)this.width * 0.5f - ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num) * (float)this.direction;
                                num = 10f;
                                if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
                                {
                                    num = 10f;
                                }
                                this.itemLocation.Y = this.position.Y + num;
                            }
                        }
                        this.itemRotation = ((float)this.itemAnimation / (float)this.itemAnimationMax - 0.5f) * (float)(-(float)this.direction) * 3.5f - (float)this.direction * 0.3f;
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].useStyle == 2)
                        {
                            this.itemRotation = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)this.direction * 2f + -1.4f * (float)this.direction;
                            if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.5)
                            {
                                this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 12f * (float)this.direction) * (float)this.direction;
                                this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction * 4f;
                            }
                            else
                            {
                                this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 16f * (float)this.direction) * (float)this.direction;
                                this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction;
                            }
                        }
                        else
                        {
                            if (this.inventory[this.selectedItem].useStyle == 3)
                            {
                                if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
                                {
                                    this.itemLocation.X = -1000f;
                                    this.itemLocation.Y = -1000f;
                                    this.itemRotation = -1.3f * (float)this.direction;
                                }
                                else
                                {
                                    this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 4f) * (float)this.direction;
                                    this.itemLocation.Y = this.position.Y + 24f;
                                    float num2 = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * (float)this.direction * this.inventory[this.selectedItem].scale * 1.2f - (float)(10 * this.direction);
                                    if (num2 > -4f && this.direction == -1)
                                    {
                                        num2 = -8f;
                                    }
                                    if (num2 < 4f && this.direction == 1)
                                    {
                                        num2 = 8f;
                                    }
                                    this.itemLocation.X = this.itemLocation.X - num2;
                                    this.itemRotation = 0.8f * (float)this.direction;
                                }
                            }
                            else
                            {
                                if (this.inventory[this.selectedItem].useStyle == 4)
                                {
                                    this.itemRotation = 0f;
                                    this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 14f * (float)this.direction) * (float)this.direction;
                                    this.itemLocation.Y = this.position.Y + (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
                                }
                                else
                                {
                                    if (this.inventory[this.selectedItem].useStyle == 5)
                                    {
                                        this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - (float)(this.direction * 2);
                                        this.itemLocation.Y = this.position.Y + (float)this.height * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.inventory[this.selectedItem].holdStyle == 1)
                    {
                        this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f + 4f) * (float)this.direction;
                        this.itemLocation.Y = this.position.Y + 24f;
                        this.itemRotation = 0f;
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].holdStyle == 2)
                        {
                            this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(6 * this.direction);
                            this.itemLocation.Y = this.position.Y + 16f;
                            this.itemRotation = 0.79f * (float)(-(float)this.direction);
                        }
                    }
                }
            }
            if (this.inventory[this.selectedItem].type == 8)
            {
                int maxValue = 20;
                if (this.itemAnimation > 0)
                {
                    maxValue = 7;
                }
                if (this.direction == -1)
                {
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        Color color = default(Color);
                    }
                    Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                }
                else
                {
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        Color color = default(Color);
                    }
                    Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                }
            }
            else
            {
                if (this.inventory[this.selectedItem].type == 105)
                {
                    int maxValue = 20;
                    if (this.itemAnimation > 0)
                    {
                        maxValue = 7;
                    }
                    if (this.direction == -1)
                    {
                        if (Main.rand.Next(maxValue) == 0)
                        {
                            Color color = default(Color);
                        }
                        Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                    }
                    else
                    {
                        if (Main.rand.Next(maxValue) == 0)
                        {
                            Color color = default(Color);
                        }
                        Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                    }
                }
                else
                {
                    if (this.inventory[this.selectedItem].type == 148)
                    {
                        int maxValue = 10;
                        if (this.itemAnimation > 0)
                        {
                            maxValue = 7;
                        }
                        if (this.direction == -1)
                        {
                            if (Main.rand.Next(maxValue) == 0)
                            {
                                Color color = default(Color);
                            }
                            Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                        }
                        else
                        {
                            if (Main.rand.Next(maxValue) == 0)
                            {
                                Color color = default(Color);
                            }
                            Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
                        }
                    }
                }
            }
            if (this.controlUseItem)
            {
                this.releaseUseItem = false;
            }
            else
            {
                this.releaseUseItem = true;
            }
            if (this.itemTime > 0)
            {
                this.itemTime--;
            }
            if (i == Main.myPlayer)
            {
                if (this.inventory[this.selectedItem].shoot > 0 && this.itemAnimation > 0 && this.itemTime == 0)
                {
                    int num3 = this.inventory[this.selectedItem].shoot;
                    float num4 = this.inventory[this.selectedItem].shootSpeed;
                    bool flag2 = false;
                    int num5 = this.inventory[this.selectedItem].damage;
                    float num6 = this.inventory[this.selectedItem].knockBack;
                    int num7 = num3;
                    if (num7 == 13 || num7 == 32)
                    {
                        this.grappling[0] = -1;
                        this.grapCount = 0;
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == i && Main.projectile[j].type == 13)
                            {
                                Main.projectile[j].Kill();
                            }
                        }
                    }
                    if (this.inventory[this.selectedItem].useAmmo > 0)
                    {
                        for (int j = 0; j < 44; j++)
                        {
                            if (this.inventory[j].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[j].stack > 0)
                            {
                                if (this.inventory[j].shoot > 0)
                                {
                                    num3 = this.inventory[j].shoot;
                                }
                                num4 += this.inventory[j].shootSpeed;
                                num5 += this.inventory[j].damage;
                                num6 += this.inventory[j].knockBack;
                                Item item = this.inventory[j];
                                item.stack--;
                                if (this.inventory[j].stack <= 0)
                                {
                                    this.inventory[j].active = false;
                                    this.inventory[j].name = "";
                                    this.inventory[j].type = 0;
                                }
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                    }
                    if (num3 == 9 && (double)this.position.Y > Main.worldSurface * 16.0 + (double)(Main.screenHeight / 2))
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        if (this.inventory[this.selectedItem].mana > 0)
                        {
                            num5 = (int)System.Math.Round((double)((float)num5 * this.magicBoost));
                        }
                        if (num3 == 1 && this.inventory[this.selectedItem].type == 120)
                        {
                            num3 = 2;
                        }
                        this.itemTime = this.inventory[this.selectedItem].useTime;
                        if (0f + Main.screenPosition.X > this.position.X + (float)this.width * 0.5f)
                        {
                            this.direction = 1;
                        }
                        else
                        {
                            this.direction = -1;
                        }
                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                        if (num3 == 9)
                        {
                            vector = new Vector2(this.position.X + (float)this.width * 0.5f + (float)(Main.rand.Next(601) * -(float)this.direction), this.position.Y + (float)this.height * 0.5f - 300f - (float)Main.rand.Next(100));
                            num6 = 0f;
                        }
                        float num8 = 0f + Main.screenPosition.X - vector.X;
                        float num9 = 0f + Main.screenPosition.Y - vector.Y;
                        float num10 = (float)System.Math.Sqrt((double)(num8 * num8 + num9 * num9));
                        num10 = num4 / num10;
                        num8 *= num10;
                        num9 *= num10;
                        if (num3 == 12)
                        {
                            vector.X += num8 * 3f;
                            vector.Y += num9 * 3f;
                        }
                        if (this.inventory[this.selectedItem].useStyle == 5)
                        {
                            this.itemRotation = (float)System.Math.Atan2((double)(num9 * (float)this.direction), (double)(num8 * (float)this.direction));
                            NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f);
                            NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f);
                        }
                        if (num3 == 17)
                        {
                            vector.X = 0f + Main.screenPosition.X;
                            vector.Y = 0f + Main.screenPosition.Y;
                        }
                        Projectile.NewProjectile(vector.X, vector.Y, num8, num9, num3, num5, num6, i);
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].useStyle == 5)
                        {
                            this.itemRotation = 0f;
                            NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f);
                        }
                    }
                }
                if (this.inventory[this.selectedItem].type >= 205 && this.inventory[this.selectedItem].type <= 207 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
                {
                    this.showItemIcon = true;
                    if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
                    {
                        if (this.inventory[this.selectedItem].type == 205)
                        {
                            bool lava = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
                            int num11 = 0;
                            for (int k = Player.tileTargetX - 1; k <= Player.tileTargetX + 1; k++)
                            {
                                for (int l = Player.tileTargetY - 1; l <= Player.tileTargetY + 1; l++)
                                {
                                    if (Main.tile[k, l].lava == lava)
                                    {
                                        num11 += (int)Main.tile[k, l].liquid;
                                    }
                                }
                            }
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && num11 > 100)
                            {
                                bool lava2 = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
                                if (!Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
                                {
                                    this.inventory[this.selectedItem].SetDefaults(206);
                                }
                                else
                                {
                                    this.inventory[this.selectedItem].SetDefaults(207);
                                }
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                                int num12 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;
                                Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 0;
                                Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
                                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                                }
                                else
                                {
                                    Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
                                }
                                for (int k = Player.tileTargetX - 1; k <= Player.tileTargetX + 1; k++)
                                {
                                    for (int l = Player.tileTargetY - 1; l <= Player.tileTargetY + 1; l++)
                                    {
                                        if (num12 < 256 && Main.tile[k, l].lava == lava)
                                        {
                                            int num13 = (int)Main.tile[k, l].liquid;
                                            if (num13 + num12 > 255)
                                            {
                                                num13 = 255 - num12;
                                            }
                                            num12 += num13;
                                            Tile tile = Main.tile[k, l];
                                            tile.liquid -= (byte)num13;
                                            Main.tile[k, l].lava = lava2;
                                            if (Main.tile[k, l].liquid == 0)
                                            {
                                                Main.tile[k, l].lava = false;
                                            }
                                            WorldGen.SquareTileFrame(k, l, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.sendWater(k, l);
                                            }
                                            else
                                            {
                                                Liquid.AddWater(k, l);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].active || !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || !Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
                            {
                                if (this.inventory[this.selectedItem].type == 207)
                                {
                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
                                    {
                                        Main.tile[Player.tileTargetX, Player.tileTargetY].lava = true;
                                        Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
                                        WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                                        this.inventory[this.selectedItem].SetDefaults(205);
                                        this.itemTime = this.inventory[this.selectedItem].useTime;
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                                        }
                                    }
                                }
                                else
                                {
                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || !Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
                                    {
                                        Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
                                        Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
                                        WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                                        this.inventory[this.selectedItem].SetDefaults(205);
                                        this.itemTime = this.inventory[this.selectedItem].useTime;
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if ((this.inventory[this.selectedItem].pick > 0 || this.inventory[this.selectedItem].axe > 0 || this.inventory[this.selectedItem].hammer > 0) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
                {
                    this.showItemIcon = true;
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
                    {
                        if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
                        {
                            this.hitTile = 0;
                            this.hitTileX = Player.tileTargetX;
                            this.hitTileY = Player.tileTargetY;
                        }
                        if (Main.tileNoFail[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
                        {
                            this.hitTile = 100;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type != 27)
                        {
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 12 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 14 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 15 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 16 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 17 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 18 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 19 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 28 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 31 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 34 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 35 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 36 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 42 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 54 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 78 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
                            {
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48)
                                {
                                    this.hitTile += this.inventory[this.selectedItem].hammer / 3;
                                }
                                else
                                {
                                    this.hitTile += this.inventory[this.selectedItem].hammer;
                                }
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 && this.inventory[this.selectedItem].hammer < 60)
                                {
                                    this.hitTile = 0;
                                }
                                if (this.inventory[this.selectedItem].hammer > 0)
                                {
                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26)
                                    {
                                        this.Hurt(this.statLife / 2, -this.direction, false, false);
                                        WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                        }
                                    }
                                    else
                                    {
                                        if (this.hitTile >= 100)
                                        {
                                            if (Main.netMode == 1 && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
                                            {
                                                WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                                NetMessage.SendData(34, -1, -1, "", Player.tileTargetX, (float)Player.tileTargetY, 0f, 0f);
                                            }
                                            else
                                            {
                                                this.hitTile = 0;
                                                WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                                                if (Main.netMode == 1)
                                                {
                                                    NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                            }
                                        }
                                    }
                                    this.itemTime = this.inventory[this.selectedItem].useTime;
                                }
                            }
                            else
                            {
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 5 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 72)
                                {
                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30)
                                    {
                                        this.hitTile += this.inventory[this.selectedItem].axe * 5;
                                    }
                                    else
                                    {
                                        this.hitTile += this.inventory[this.selectedItem].axe;
                                    }
                                    if (this.inventory[this.selectedItem].axe > 0)
                                    {
                                        if (this.hitTile >= 100)
                                        {
                                            this.hitTile = 0;
                                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f);
                                            }
                                        }
                                        else
                                        {
                                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                            }
                                        }
                                        this.itemTime = this.inventory[this.selectedItem].useTime;
                                    }
                                }
                                else
                                {
                                    if (this.inventory[this.selectedItem].pick > 0)
                                    {
                                        if (Main.tileDungeon[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58)
                                        {
                                            this.hitTile += this.inventory[this.selectedItem].pick / 2;
                                        }
                                        else
                                        {
                                            this.hitTile += this.inventory[this.selectedItem].pick;
                                        }
                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 && this.inventory[this.selectedItem].pick < 65)
                                        {
                                            this.hitTile = 0;
                                        }
                                        else
                                        {
                                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 && this.inventory[this.selectedItem].pick < 55)
                                            {
                                                this.hitTile = 0;
                                            }
                                            else
                                            {
                                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 56 && this.inventory[this.selectedItem].pick < 65)
                                                {
                                                    this.hitTile = 0;
                                                }
                                                else
                                                {
                                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58 && this.inventory[this.selectedItem].pick < 65)
                                                    {
                                                        this.hitTile = 0;
                                                    }
                                                }
                                            }
                                        }
                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 40 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 53 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
                                        {
                                            this.hitTile += this.inventory[this.selectedItem].pick;
                                        }
                                        if (this.hitTile >= 100)
                                        {
                                            this.hitTile = 0;
                                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f);
                                            }
                                        }
                                        else
                                        {
                                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                            }
                                        }
                                        this.itemTime = this.inventory[this.selectedItem].useTime;
                                    }
                                }
                            }
                        }
                    }
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].wall > 0 && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && this.inventory[this.selectedItem].hammer > 0)
                    {
                        bool flag3 = true;
                        if (!Main.wallHouse[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall])
                        {
                            flag3 = false;
                            for (int k = Player.tileTargetX - 1; k < Player.tileTargetX + 2; k++)
                            {
                                for (int l = Player.tileTargetY - 1; l < Player.tileTargetY + 2; l++)
                                {
                                    if (Main.tile[k, l].wall != Main.tile[Player.tileTargetX, Player.tileTargetY].wall)
                                    {
                                        flag3 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag3)
                        {
                            if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
                            {
                                this.hitTile = 0;
                                this.hitTileX = Player.tileTargetX;
                                this.hitTileY = Player.tileTargetY;
                            }
                            this.hitTile += this.inventory[this.selectedItem].hammer;
                            if (this.hitTile >= 100)
                            {
                                this.hitTile = 0;
                                WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, "", 2, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f);
                                }
                            }
                            else
                            {
                                WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, true);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, "", 2, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f);
                                }
                            }
                            this.itemTime = this.inventory[this.selectedItem].useTime;
                        }
                    }
                }
                if (this.inventory[this.selectedItem].type == 29 && this.itemAnimation > 0 && this.statLifeMax < 400 && this.itemTime == 0)
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    this.statLifeMax += 20;
                    this.statLife += 20;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.HealEffect(20);
                    }
                }
                if (this.inventory[this.selectedItem].type == 109 && this.itemAnimation > 0 && this.statManaMax < 200 && this.itemTime == 0)
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    this.statManaMax += 20;
                    this.statMana += 20;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.ManaEffect(20);
                    }
                }
                if (this.inventory[this.selectedItem].createTile >= 0 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
                {
                    this.showItemIcon = true;
                    if ((!Main.tile[Player.tileTargetX, Player.tileTargetY].active || this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2 || this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70) && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
                    {
                        bool flag4 = false;
                        if (this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2)
                        {
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0)
                            {
                                flag4 = true;
                            }
                        }
                        else
                        {
                            if (this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70)
                            {
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
                                {
                                    flag4 = true;
                                }
                            }
                            else
                            {
                                if (this.inventory[this.selectedItem].createTile == 4)
                                {
                                    int num14 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type;
                                    int num15 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type;
                                    int num16 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type;
                                    int num17 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
                                    int num18 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].type;
                                    int num19 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
                                    int num20 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].type;
                                    if (!Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active)
                                    {
                                        num14 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active)
                                    {
                                        num15 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active)
                                    {
                                        num16 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].active)
                                    {
                                        num17 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].active)
                                    {
                                        num18 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY + 1].active)
                                    {
                                        num19 = -1;
                                    }
                                    if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].active)
                                    {
                                        num20 = -1;
                                    }
                                    if (num14 >= 0 && Main.tileSolid[num14] && !Main.tileNoAttach[num14])
                                    {
                                        flag4 = true;
                                    }
                                    else
                                    {
                                        if ((num15 >= 0 && Main.tileSolid[num15] && !Main.tileNoAttach[num15]) || (num15 == 5 && num17 == 5 && num19 == 5))
                                        {
                                            flag4 = true;
                                        }
                                        else
                                        {
                                            if ((num16 >= 0 && Main.tileSolid[num16] && !Main.tileNoAttach[num16]) || (num16 == 5 && num18 == 5 && num20 == 5))
                                            {
                                                flag4 = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.inventory[this.selectedItem].createTile == 78)
                                    {
                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && (Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type] || Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type]))
                                        {
                                            flag4 = true;
                                        }
                                    }
                                    else
                                    {
                                        if (this.inventory[this.selectedItem].createTile == 13 || this.inventory[this.selectedItem].createTile == 29 || this.inventory[this.selectedItem].createTile == 33 || this.inventory[this.selectedItem].createTile == 49)
                                        {
                                            if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type])
                                            {
                                                flag4 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (this.inventory[this.selectedItem].createTile == 51)
                                            {
                                                if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
                                                {
                                                    flag4 = true;
                                                }
                                            }
                                            else
                                            {
                                                if ((Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type]) || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type]) || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type]) || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || (Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY - 1].type]) || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
                                                {
                                                    flag4 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (flag4 && WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createTile, false, false, this.whoAmi))
                        {
                            this.itemTime = this.inventory[this.selectedItem].useTime;
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(17, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createTile);
                            }
                            if (this.inventory[this.selectedItem].createTile == 15)
                            {
                                if (this.direction == 1)
                                {
                                    Tile tile2 = Main.tile[Player.tileTargetX, Player.tileTargetY];
                                    tile2.frameX += 18;
                                    Tile tile3 = Main.tile[Player.tileTargetX, Player.tileTargetY - 1];
                                    tile3.frameX += 18;
                                }
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, Player.tileTargetX - 1, Player.tileTargetY - 1, 3);
                                }
                            }
                            else
                            {
                                if (this.inventory[this.selectedItem].createTile == 79 && Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 5);
                                }
                            }
                        }
                    }
                }
                if (this.inventory[this.selectedItem].createWall >= 0)
                {
                    Player.tileTargetX = (int)((0f + Main.screenPosition.X) / 16f);
                    Player.tileTargetY = (int)((0f + Main.screenPosition.Y) / 16f);
                    if (this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
                    {
                        this.showItemIcon = true;
                        if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0) && (int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall != this.inventory[this.selectedItem].createWall)
                        {
                            WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createWall, false);
                            if ((int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall == this.inventory[this.selectedItem].createWall)
                            {
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, "", 3, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createWall);
                                }
                            }
                        }
                    }
                }
            }
            if (this.inventory[this.selectedItem].damage >= 0 && this.inventory[this.selectedItem].type > 0 && !this.inventory[this.selectedItem].noMelee && this.itemAnimation > 0)
            {
                bool flag5 = false;
                Rectangle rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, 32, 32);
                if (!Main.dedServ)
                {
                    rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, Main.itemTexture[this.inventory[this.selectedItem].type].Width, Main.itemTexture[this.inventory[this.selectedItem].type].Height);
                }
                rectangle.Width = (int)((float)rectangle.Width * this.inventory[this.selectedItem].scale);
                rectangle.Height = (int)((float)rectangle.Height * this.inventory[this.selectedItem].scale);
                if (this.direction == -1)
                {
                    rectangle.X -= rectangle.Width;
                }
                rectangle.Y -= rectangle.Height;
                if (this.inventory[this.selectedItem].useStyle == 1)
                {
                    if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
                    {
                        if (this.direction == -1)
                        {
                            rectangle.X -= (int)((double)rectangle.Width * 1.4) - rectangle.Width;
                        }
                        rectangle.Width = (int)((double)rectangle.Width * 1.4);
                        rectangle.Y += (int)((double)rectangle.Height * 0.5);
                        rectangle.Height = (int)((double)rectangle.Height * 1.1);
                    }
                    else
                    {
                        if ((double)this.itemAnimation >= (double)this.itemAnimationMax * 0.666)
                        {
                            if (this.direction == 1)
                            {
                                rectangle.X -= (int)((double)rectangle.Width * 1.2);
                            }
                            rectangle.Width *= 2;
                            rectangle.Y -= (int)((double)rectangle.Height * 1.4) - rectangle.Height;
                            rectangle.Height = (int)((double)rectangle.Height * 1.4);
                        }
                    }
                }
                else
                {
                    if (this.inventory[this.selectedItem].useStyle == 3)
                    {
                        if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
                        {
                            flag5 = true;
                        }
                        else
                        {
                            if (this.direction == -1)
                            {
                                rectangle.X -= (int)((double)rectangle.Width * 1.4) - rectangle.Width;
                            }
                            rectangle.Width = (int)((double)rectangle.Width * 1.4);
                            rectangle.Y += (int)((double)rectangle.Height * 0.6);
                            rectangle.Height = (int)((double)rectangle.Height * 0.6);
                        }
                    }
                }
                if (!flag5)
                {
                    if ((this.inventory[this.selectedItem].type == 44 || this.inventory[this.selectedItem].type == 45 || this.inventory[this.selectedItem].type == 46 || this.inventory[this.selectedItem].type == 103 || this.inventory[this.selectedItem].type == 104) && Main.rand.Next(15) == 0)
                    {
                        Color color = default(Color);
                    }
                    if (this.inventory[this.selectedItem].type == 65)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            Color color = default(Color);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            Gore.NewGore(new Vector2((float)rectangle.X, (float)rectangle.Y), default(Vector2), Main.rand.Next(16, 18));
                        }
                    }
                    if (this.inventory[this.selectedItem].type == 190 || this.inventory[this.selectedItem].type == 213)
                    {
                        Color color = default(Color);
                        int num21 = 0;
                        Main.dust[num21].noGravity = true;
                    }
                    if (this.inventory[this.selectedItem].type == 121)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            Color color = default(Color);
                            int num21 = 0;
                            Main.dust[num21].noGravity = true;
                            Dust expr_469E_cp_0 = Main.dust[num21];
                            expr_469E_cp_0.velocity.X = expr_469E_cp_0.velocity.X * 2f;
                            Dust expr_46BC_cp_0 = Main.dust[num21];
                            expr_46BC_cp_0.velocity.Y = expr_46BC_cp_0.velocity.Y * 2f;
                        }
                    }
                    if (this.inventory[this.selectedItem].type == 122 || this.inventory[this.selectedItem].type == 217)
                    {
                        Color color = default(Color);
                        int num21 = 0;
                        Main.dust[num21].noGravity = true;
                    }
                    if (this.inventory[this.selectedItem].type == 155)
                    {
                        Color color = default(Color);
                        int num21 = 0;
                        Main.dust[num21].noGravity = true;
                        Dust expr_477C_cp_0 = Main.dust[num21];
                        expr_477C_cp_0.velocity.X = expr_477C_cp_0.velocity.X / 2f;
                        Dust expr_479A_cp_0 = Main.dust[num21];
                        expr_479A_cp_0.velocity.Y = expr_479A_cp_0.velocity.Y / 2f;
                    }
                    if (this.inventory[this.selectedItem].type >= 198 && this.inventory[this.selectedItem].type <= 203)
                    {
                        Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.5f);
                    }
                    if (Main.myPlayer == i)
                    {
                        int num22 = rectangle.X / 16;
                        int num23 = (rectangle.X + rectangle.Width) / 16 + 1;
                        int num24 = rectangle.Y / 16;
                        int num25 = (rectangle.Y + rectangle.Height) / 16 + 1;
                        for (int k = num22; k < num23; k++)
                        {
                            for (int l = num24; l < num25; l++)
                            {
                                if (Main.tile[k, l].type == 3 || Main.tile[k, l].type == 24 || Main.tile[k, l].type == 28 || Main.tile[k, l].type == 32 || Main.tile[k, l].type == 51 || Main.tile[k, l].type == 52 || Main.tile[k, l].type == 61 || Main.tile[k, l].type == 62 || Main.tile[k, l].type == 69 || Main.tile[k, l].type == 71 || Main.tile[k, l].type == 73 || Main.tile[k, l].type == 74)
                                {
                                    WorldGen.KillTile(k, l, false, false, false);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(17, -1, -1, "", 0, (float)k, (float)l, 0f);
                                    }
                                }
                            }
                        }
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.npc[j].active && Main.npc[j].immune[i] == 0 && this.attackCD == 0 && !Main.npc[j].friendly)
                            {
                                Rectangle value = new Rectangle((int)Main.npc[j].position.X, (int)Main.npc[j].position.Y, Main.npc[j].width, Main.npc[j].height);
                                if (rectangle.Intersects(value) && (Main.npc[j].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height)))
                                {
                                    Main.npc[j].StrikeNPC(this.inventory[this.selectedItem].damage, this.inventory[this.selectedItem].knockBack, this.direction);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(24, -1, -1, "", j, (float)i, 0f, 0f);
                                    }
                                    Main.npc[j].immune[i] = this.itemAnimation;
                                    this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
                                }
                            }
                        }
                        if (this.hostile)
                        {
                            for (int j = 0; j < 255; j++)
                            {
                                if (j != i && Main.player[j].active && Main.player[j].hostile && !Main.player[j].immune && !Main.player[j].dead && (Main.player[i].team == 0 || Main.player[i].team != Main.player[j].team))
                                {
                                    Rectangle value2 = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
                                    if (rectangle.Intersects(value2) && Collision.CanHit(this.position, this.width, this.height, Main.player[j].position, Main.player[j].width, Main.player[j].height))
                                    {
                                        Main.player[j].Hurt(this.inventory[this.selectedItem].damage, this.direction, true, false);
                                        if (Main.netMode != 0)
                                        {
                                            NetMessage.SendData(26, -1, -1, "", j, (float)this.direction, (float)this.inventory[this.selectedItem].damage, 1f);
                                        }
                                        this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0)
            {
                if (this.inventory[this.selectedItem].healLife > 0)
                {
                    this.statLife += this.inventory[this.selectedItem].healLife;
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.HealEffect(this.inventory[this.selectedItem].healLife);
                    }
                }
                if (this.inventory[this.selectedItem].healMana > 0)
                {
                    this.statMana += this.inventory[this.selectedItem].healMana;
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.ManaEffect(this.inventory[this.selectedItem].healMana);
                    }
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0 && (this.inventory[this.selectedItem].type == 43 || this.inventory[this.selectedItem].type == 70))
            {
                this.itemTime = this.inventory[this.selectedItem].useTime;
                bool flag6 = false;
                int num26 = 4;
                if (this.inventory[this.selectedItem].type == 43)
                {
                    num26 = 4;
                }
                else
                {
                    if (this.inventory[this.selectedItem].type == 70)
                    {
                        num26 = 13;
                    }
                }
                for (int j = 0; j < 1000; j++)
                {
                    if (Main.npc[j].active && Main.npc[j].type == num26)
                    {
                        flag6 = true;
                        break;
                    }
                }
                if (flag6)
                {
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.Hurt(this.statLife * (this.statDefense + 1), -this.direction, false, false);
                    }
                }
                else
                {
                    if (this.inventory[this.selectedItem].type == 43)
                    {
                        if (!Main.dayTime)
                        {
                            if (Main.netMode != 1)
                            {
                                NPC.SpawnOnPlayer(i, 4);
                            }
                        }
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].type == 70 && this.zoneEvil)
                        {
                            if (Main.netMode != 1)
                            {
                                NPC.SpawnOnPlayer(i, 13);
                            }
                        }
                    }
                }
            }
            if (this.inventory[this.selectedItem].type == 50 && this.itemAnimation > 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Color color = default(Color);
                }
                if (this.itemTime == 0)
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                }
                else
                {
                    if (this.itemTime == this.inventory[this.selectedItem].useTime / 2)
                    {
                        for (int j = 0; j < 70; j++)
                        {
                            Color color = default(Color);
                        }
                        this.grappling[0] = -1;
                        this.grapCount = 0;
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == i && Main.projectile[j].aiStyle == 7)
                            {
                                Main.projectile[j].Kill();
                            }
                        }
                        this.Spawn();
                        for (int j = 0; j < 70; j++)
                        {
                            Color color = default(Color);
                        }
                    }
                }
            }
            if (i == Main.myPlayer)
            {
                if (this.itemTime == this.inventory[this.selectedItem].useTime && this.inventory[this.selectedItem].consumable)
                {
                    Item item2 = this.inventory[this.selectedItem];
                    item2.stack--;
                    if (this.inventory[this.selectedItem].stack <= 0)
                    {
                        this.itemTime = this.itemAnimation;
                    }
                }
                if (this.inventory[this.selectedItem].stack <= 0 && this.itemAnimation == 0)
                {
                    this.inventory[this.selectedItem] = new Item();
                }
            }
        }
        public bool ItemSpace(Item newItem)
        {
            bool result;
            if (newItem.type == 58)
            {
                result = true;
            }
            else
            {
                if (newItem.type == 184)
                {
                    result = true;
                }
                else
                {
                    int num = 40;
                    if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
                    {
                        num = 44;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        if (this.inventory[i].type == 0)
                        {
                            result = true;
                            return result;
                        }
                    }
                    for (int i = 0; i < num; i++)
                    {
                        if (this.inventory[i].type > 0 && this.inventory[i].stack < this.inventory[i].maxStack && newItem.IsTheSameAs(this.inventory[i]))
                        {
                            result = true;
                            return result;
                        }
                    }
                    result = false;
                }
            }
            return result;
        }
        public void KillMe(double dmg, int hitDirection, bool pvp = false)
        {
            if ((!Main.godMode || Main.myPlayer != this.whoAmi) && !this.dead)
            {
                if (pvp)
                {
                    this.pvpDeath = true;
                }
                PlayerEvent playerEvent = new PlayerEvent(this);
                PluginManager.callHook(Hook.PLAYER_DEATH, playerEvent);
                if (!playerEvent.getState())
                {
                    this.pvpDeath = false;
                    this.statLife++;
                }
                else
                {
                    this.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
                    this.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
                    this.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
                    this.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
                    this.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
                    this.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
                    int num = 0;
                    while ((double)num < 20.0 + dmg / (double)this.statLifeMax * 100.0)
                    {
                        if (this.boneArmor)
                        {
                            Color color = default(Color);
                        }
                        else
                        {
                            Color color = default(Color);
                        }
                        num++;
                    }
                    this.dead = true;
                    this.respawnTimer = 600;
                    this.immuneAlpha = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(25, -1, -1, this.name + " was slain...", 255, 225f, 25f, 25f);
                    }
                    if (this.whoAmi == Main.myPlayer)
                    {
                        WorldGen.saveToonWhilePlaying();
                    }
                    if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
                    {
                        int num2 = 0;
                        if (pvp)
                        {
                            num2 = 1;
                        }
                        NetMessage.SendData(44, -1, -1, "", this.whoAmi, (float)hitDirection, (float)((int)dmg), (float)num2);
                    }
                    if (!pvp && this.whoAmi == Main.myPlayer)
                    {
                        this.DropItems();
                    }
                }
            }
        }
        public static Player LoadPlayer(string playerPath)
        {
            bool flag = false;
            if (Main.rand == null)
            {
                Main.rand = new System.Random((int)System.DateTime.Now.Ticks);
            }
            Player player = new Player();
            Player result;
            try
            {
                string text = playerPath + ".dat";
                flag = Player.DecryptFile(playerPath, text);
                if (!flag)
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(text, System.IO.FileMode.Open))
                    {
                        using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fileStream))
                        {
                            int release = binaryReader.ReadInt32();
                            player.name = binaryReader.ReadString();
                            player.hair = binaryReader.ReadInt32();
                            player.statLife = binaryReader.ReadInt32();
                            player.statLifeMax = binaryReader.ReadInt32();
                            if (player.statLife > player.statLifeMax)
                            {
                                player.statLife = player.statLifeMax;
                            }
                            player.statMana = binaryReader.ReadInt32();
                            player.statManaMax = binaryReader.ReadInt32();
                            if (player.statMana > player.statManaMax)
                            {
                                player.statMana = player.statManaMax;
                            }
                            player.hairColor.R = binaryReader.ReadByte();
                            player.hairColor.G = binaryReader.ReadByte();
                            player.hairColor.B = binaryReader.ReadByte();
                            player.skinColor.R = binaryReader.ReadByte();
                            player.skinColor.G = binaryReader.ReadByte();
                            player.skinColor.B = binaryReader.ReadByte();
                            player.eyeColor.R = binaryReader.ReadByte();
                            player.eyeColor.G = binaryReader.ReadByte();
                            player.eyeColor.B = binaryReader.ReadByte();
                            player.shirtColor.R = binaryReader.ReadByte();
                            player.shirtColor.G = binaryReader.ReadByte();
                            player.shirtColor.B = binaryReader.ReadByte();
                            player.underShirtColor.R = binaryReader.ReadByte();
                            player.underShirtColor.G = binaryReader.ReadByte();
                            player.underShirtColor.B = binaryReader.ReadByte();
                            player.pantsColor.R = binaryReader.ReadByte();
                            player.pantsColor.G = binaryReader.ReadByte();
                            player.pantsColor.B = binaryReader.ReadByte();
                            player.shoeColor.R = binaryReader.ReadByte();
                            player.shoeColor.G = binaryReader.ReadByte();
                            player.shoeColor.B = binaryReader.ReadByte();
                            for (int i = 0; i < 8; i++)
                            {
                                player.armor[i].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
                            }
                            for (int i = 0; i < 44; i++)
                            {
                                player.inventory[i].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
                                player.inventory[i].stack = binaryReader.ReadInt32();
                            }
                            for (int i = 0; i < Chest.maxItems; i++)
                            {
                                player.bank[i].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
                                player.bank[i].stack = binaryReader.ReadInt32();
                            }
                            for (int i = 0; i < 200; i++)
                            {
                                int num = binaryReader.ReadInt32();
                                if (num == -1)
                                {
                                    break;
                                }
                                player.spX[i] = num;
                                player.spY[i] = binaryReader.ReadInt32();
                                player.spI[i] = binaryReader.ReadInt32();
                                player.spN[i] = binaryReader.ReadString();
                            }
                            binaryReader.Close();
                        }
                    }
                    player.PlayerFrame();
                    System.IO.File.Delete(text);
                    result = player;
                    return result;
                }
            }
            catch
            {
                flag = true;
            }
            if (flag)
            {
                string text2 = playerPath + ".bak";
                if (System.IO.File.Exists(text2))
                {
                    System.IO.File.Delete(playerPath);
                    System.IO.File.Move(text2, playerPath);
                    result = Player.LoadPlayer(playerPath);
                }
                else
                {
                    result = new Player();
                }
            }
            else
            {
                result = new Player();
            }
            return result;
        }
        public void ManaEffect(int manaAmount)
        {
            if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
            {
                NetMessage.SendData(43, -1, -1, "", this.whoAmi, (float)manaAmount, 0f, 0f);
            }
        }
        public void PlayerFrame()
        {
            if (this.swimTime > 0)
            {
                this.swimTime--;
                if (!this.wet)
                {
                    this.swimTime = 0;
                }
            }
            this.head = this.armor[0].headSlot;
            this.body = this.armor[1].bodySlot;
            this.legs = this.armor[2].legSlot;
            this.bodyFrame.Width = 40;
            this.bodyFrame.Height = 56;
            this.legFrame.Width = 40;
            this.legFrame.Height = 56;
            this.bodyFrame.X = 0;
            this.legFrame.X = 0;
            if (this.itemAnimation > 0 && this.inventory[this.selectedItem].useStyle != 10)
            {
                if (this.inventory[this.selectedItem].useStyle == 1 || this.inventory[this.selectedItem].type == 0)
                {
                    if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                    }
                    else
                    {
                        if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height * 2;
                        }
                        else
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height;
                        }
                    }
                }
                else
                {
                    if (this.inventory[this.selectedItem].useStyle == 2)
                    {
                        if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.5)
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height * 4;
                        }
                        else
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height * 5;
                        }
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].useStyle == 3)
                        {
                            if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 3;
                            }
                            else
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 3;
                            }
                        }
                        else
                        {
                            if (this.inventory[this.selectedItem].useStyle == 4)
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 2;
                            }
                            else
                            {
                                if (this.inventory[this.selectedItem].useStyle == 5)
                                {
                                    float num = this.itemRotation * (float)this.direction;
                                    this.bodyFrame.Y = this.bodyFrame.Height * 3;
                                    if ((double)num < -0.75)
                                    {
                                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                                    }
                                    if ((double)num > 0.6)
                                    {
                                        this.bodyFrame.Y = this.bodyFrame.Height * 4;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.inventory[this.selectedItem].holdStyle == 1)
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 3;
                }
                else
                {
                    if (this.inventory[this.selectedItem].holdStyle == 2)
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    }
                    else
                    {
                        if (this.grappling[0] >= 0)
                        {
                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                            float num2 = 0f;
                            float num3 = 0f;
                            for (int i = 0; i < this.grapCount; i++)
                            {
                                num2 += Main.projectile[this.grappling[i]].position.X + (float)(Main.projectile[this.grappling[i]].width / 2);
                                num3 += Main.projectile[this.grappling[i]].position.Y + (float)(Main.projectile[this.grappling[i]].height / 2);
                            }
                            num2 /= (float)this.grapCount;
                            num3 /= (float)this.grapCount;
                            num2 -= vector.X;
                            num3 -= vector.Y;
                            if (num3 < 0f && System.Math.Abs(num3) > System.Math.Abs(num2))
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 2;
                            }
                            else
                            {
                                if (num3 > 0f && System.Math.Abs(num3) > System.Math.Abs(num2))
                                {
                                    this.bodyFrame.Y = this.bodyFrame.Height * 4;
                                }
                                else
                                {
                                    this.bodyFrame.Y = this.bodyFrame.Height * 3;
                                }
                            }
                        }
                        else
                        {
                            if (this.swimTime > 0)
                            {
                                if (this.swimTime > 20)
                                {
                                    this.bodyFrame.Y = 0;
                                }
                                else
                                {
                                    if (this.swimTime > 10)
                                    {
                                        this.bodyFrame.Y = this.bodyFrame.Height * 5;
                                    }
                                    else
                                    {
                                        this.bodyFrame.Y = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (this.velocity.Y != 0f)
                                {
                                    this.bodyFrameCounter = 0.0;
                                    this.bodyFrame.Y = this.bodyFrame.Height * 5;
                                }
                                else
                                {
                                    if (this.velocity.X != 0f)
                                    {
                                        this.bodyFrameCounter += (double)System.Math.Abs(this.velocity.X) * 1.5;
                                        this.bodyFrame.Y = this.legFrame.Y;
                                    }
                                    else
                                    {
                                        this.bodyFrameCounter = 0.0;
                                        this.bodyFrame.Y = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (this.swimTime > 0)
            {
                this.legFrameCounter += 2.0;
                while (this.legFrameCounter > 8.0)
                {
                    this.legFrameCounter -= 8.0;
                    this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
                }
                if (this.legFrame.Y < this.legFrame.Height * 7)
                {
                    this.legFrame.Y = this.legFrame.Height * 19;
                }
                else
                {
                    if (this.legFrame.Y > this.legFrame.Height * 19)
                    {
                        this.legFrame.Y = this.legFrame.Height * 7;
                    }
                }
            }
            else
            {
                if (this.velocity.Y != 0f || this.grappling[0] > -1)
                {
                    this.legFrameCounter = 0.0;
                    this.legFrame.Y = this.legFrame.Height * 5;
                }
                else
                {
                    if (this.velocity.X != 0f)
                    {
                        this.legFrameCounter += (double)System.Math.Abs(this.velocity.X) * 1.3;
                        while (this.legFrameCounter > 8.0)
                        {
                            this.legFrameCounter -= 8.0;
                            this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
                        }
                        if (this.legFrame.Y < this.legFrame.Height * 7)
                        {
                            this.legFrame.Y = this.legFrame.Height * 19;
                        }
                        else
                        {
                            if (this.legFrame.Y > this.legFrame.Height * 19)
                            {
                                this.legFrame.Y = this.legFrame.Height * 7;
                            }
                        }
                    }
                    else
                    {
                        this.legFrameCounter = 0.0;
                        this.legFrame.Y = 0;
                    }
                }
            }
        }
        public static void SavePlayer(Player newPlayer, string playerPath)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Main.SavePath);
            }
            catch
            {
            }
            if (playerPath != null)
            {
                string destFileName = playerPath + ".bak";
                if (System.IO.File.Exists(playerPath))
                {
                    System.IO.File.Copy(playerPath, destFileName, true);
                }
                string text = playerPath + ".dat";
                using (System.IO.FileStream fileStream = new System.IO.FileStream(text, System.IO.FileMode.Create))
                {
                    using (System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(fileStream))
                    {
                        binaryWriter.Write(Main.curRelease);
                        binaryWriter.Write(newPlayer.name);
                        binaryWriter.Write(newPlayer.hair);
                        binaryWriter.Write(newPlayer.statLife);
                        binaryWriter.Write(newPlayer.statLifeMax);
                        binaryWriter.Write(newPlayer.statMana);
                        binaryWriter.Write(newPlayer.statManaMax);
                        binaryWriter.Write(newPlayer.hairColor.R);
                        binaryWriter.Write(newPlayer.hairColor.G);
                        binaryWriter.Write(newPlayer.hairColor.B);
                        binaryWriter.Write(newPlayer.skinColor.R);
                        binaryWriter.Write(newPlayer.skinColor.G);
                        binaryWriter.Write(newPlayer.skinColor.B);
                        binaryWriter.Write(newPlayer.eyeColor.R);
                        binaryWriter.Write(newPlayer.eyeColor.G);
                        binaryWriter.Write(newPlayer.eyeColor.B);
                        binaryWriter.Write(newPlayer.shirtColor.R);
                        binaryWriter.Write(newPlayer.shirtColor.G);
                        binaryWriter.Write(newPlayer.shirtColor.B);
                        binaryWriter.Write(newPlayer.underShirtColor.R);
                        binaryWriter.Write(newPlayer.underShirtColor.G);
                        binaryWriter.Write(newPlayer.underShirtColor.B);
                        binaryWriter.Write(newPlayer.pantsColor.R);
                        binaryWriter.Write(newPlayer.pantsColor.G);
                        binaryWriter.Write(newPlayer.pantsColor.B);
                        binaryWriter.Write(newPlayer.shoeColor.R);
                        binaryWriter.Write(newPlayer.shoeColor.G);
                        binaryWriter.Write(newPlayer.shoeColor.B);
                        for (int i = 0; i < 8; i++)
                        {
                            if (newPlayer.armor[i].name == null)
                            {
                                newPlayer.armor[i].name = "";
                            }
                            binaryWriter.Write(newPlayer.armor[i].name);
                        }
                        for (int i = 0; i < 44; i++)
                        {
                            if (newPlayer.inventory[i].name == null)
                            {
                                newPlayer.inventory[i].name = "";
                            }
                            binaryWriter.Write(newPlayer.inventory[i].name);
                            binaryWriter.Write(newPlayer.inventory[i].stack);
                        }
                        for (int i = 0; i < Chest.maxItems; i++)
                        {
                            if (newPlayer.bank[i].name == null)
                            {
                                newPlayer.bank[i].name = "";
                            }
                            binaryWriter.Write(newPlayer.bank[i].name);
                            binaryWriter.Write(newPlayer.bank[i].stack);
                        }
                        for (int i = 0; i < 200; i++)
                        {
                            if (newPlayer.spN[i] == null)
                            {
                                binaryWriter.Write(-1);
                                break;
                            }
                            binaryWriter.Write(newPlayer.spX[i]);
                            binaryWriter.Write(newPlayer.spY[i]);
                            binaryWriter.Write(newPlayer.spI[i]);
                            binaryWriter.Write(newPlayer.spN[i]);
                        }
                        binaryWriter.Close();
                    }
                }
                Player.EncryptFile(text, playerPath);
                System.IO.File.Delete(text);
            }
        }
        public bool SellItem(int price)
        {
            bool result;
            if (price <= 0)
            {
                result = false;
            }
            else
            {
                Item[] array = new Item[44];
                for (int i = 0; i < 44; i++)
                {
                    array[i] = new Item();
                    array[i] = (Item)this.inventory[i].Clone();
                }
                int num = price / 5;
                if (num < 1)
                {
                    num = 1;
                }
                bool flag = false;
                while (num >= 1000000 && !flag)
                {
                    int num2 = -1;
                    for (int i = 43; i >= 0; i--)
                    {
                        if (num2 == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
                        {
                            num2 = i;
                        }
                        while (this.inventory[i].type == 74 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 1000000)
                        {
                            Item item = this.inventory[i];
                            item.stack++;
                            num -= 1000000;
                            this.DoCoins(i);
                            if (this.inventory[i].stack == 0 && num2 == -1)
                            {
                                num2 = i;
                            }
                        }
                    }
                    if (num >= 1000000)
                    {
                        if (num2 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num2].SetDefaults(74);
                            num -= 1000000;
                        }
                    }
                }
                while (num >= 10000 && !flag)
                {
                    int num2 = -1;
                    for (int i = 43; i >= 0; i--)
                    {
                        if (num2 == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
                        {
                            num2 = i;
                        }
                        while (this.inventory[i].type == 73 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 10000)
                        {
                            Item item2 = this.inventory[i];
                            item2.stack++;
                            num -= 10000;
                            this.DoCoins(i);
                            if (this.inventory[i].stack == 0 && num2 == -1)
                            {
                                num2 = i;
                            }
                        }
                    }
                    if (num >= 10000)
                    {
                        if (num2 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num2].SetDefaults(73);
                            num -= 10000;
                        }
                    }
                }
                while (num >= 100 && !flag)
                {
                    int num2 = -1;
                    for (int i = 43; i >= 0; i--)
                    {
                        if (num2 == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
                        {
                            num2 = i;
                        }
                        while (this.inventory[i].type == 72 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 100)
                        {
                            Item item3 = this.inventory[i];
                            item3.stack++;
                            num -= 100;
                            this.DoCoins(i);
                            if (this.inventory[i].stack == 0 && num2 == -1)
                            {
                                num2 = i;
                            }
                        }
                    }
                    if (num >= 100)
                    {
                        if (num2 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num2].SetDefaults(72);
                            num -= 100;
                        }
                    }
                }
                while (num >= 1 && !flag)
                {
                    int num2 = -1;
                    for (int i = 43; i >= 0; i--)
                    {
                        if (num2 == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
                        {
                            num2 = i;
                        }
                        while (this.inventory[i].type == 71 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 1)
                        {
                            Item item4 = this.inventory[i];
                            item4.stack++;
                            num--;
                            this.DoCoins(i);
                            if (this.inventory[i].stack == 0 && num2 == -1)
                            {
                                num2 = i;
                            }
                        }
                    }
                    if (num >= 1)
                    {
                        if (num2 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num2].SetDefaults(71);
                            num--;
                        }
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < 44; i++)
                    {
                        this.inventory[i] = (Item)array[i].Clone();
                    }
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
        public void Spawn()
        {
            PlayerEvent playerEvent = new PlayerEvent(this);
            PluginManager.callHook(Hook.PLAYER_SPAWN, playerEvent);
            if (playerEvent.getState())
            {
                if (this.whoAmi == Main.myPlayer)
                {
                    this.FindSpawn();
                    if (!Player.CheckSpawn(this.SpawnX, this.SpawnY))
                    {
                        this.SpawnX = -1;
                        this.SpawnY = -1;
                    }
                }
                if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
                {
                    NetMessage.SendData(12, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                    Main.gameMenu = false;
                }
                this.headPosition = default(Vector2);
                this.bodyPosition = default(Vector2);
                this.legPosition = default(Vector2);
                this.headRotation = 0f;
                this.bodyRotation = 0f;
                this.legRotation = 0f;
                if (this.statLife <= 0)
                {
                    this.statLife = 100;
                    this.breath = this.breathMax;
                    if (this.spawnMax)
                    {
                        this.statLife = this.statLifeMax;
                        this.statMana = this.statManaMax;
                    }
                }
                this.immune = true;
                this.dead = false;
                this.immuneTime = 0;
                this.active = true;
                if (this.SpawnX >= 0 && this.SpawnY >= 0)
                {
                    this.position.X = (float)(this.SpawnX * 16 + 8 - this.width / 2);
                    this.position.Y = (float)(this.SpawnY * 16 - this.height);
                }
                else
                {
                    this.position.X = (float)(Main.spawnTileX * 16 + 8 - this.width / 2);
                    this.position.Y = (float)(Main.spawnTileY * 16 - this.height);
                    for (int i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; i++)
                    {
                        for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
                        {
                            if (Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                            {
                                if (Main.tile[i, j].liquid > 0)
                                {
                                    Main.tile[i, j].lava = false;
                                    Main.tile[i, j].liquid = 0;
                                    WorldGen.SquareTileFrame(i, j, true);
                                }
                                WorldGen.KillTile(i, j, false, false, false);
                            }
                        }
                    }
                }
                this.wet = Collision.WetCollision(this.position, this.width, this.height);
                this.wetCount = 0;
                this.lavaWet = false;
                this.fallStart = (int)(this.position.Y / 16f);
                this.velocity.X = 0f;
                this.velocity.Y = 0f;
                this.talkNPC = -1;
                if (this.pvpDeath)
                {
                    this.pvpDeath = false;
                    this.immuneTime = 300;
                    this.statLife = this.statLifeMax;
                }
                if (this.whoAmi == Main.myPlayer)
                {
                    Lighting.lightCounter = Lighting.lightSkip + 1;
                    Main.screenPosition.X = this.position.X + (float)(this.width / 2) - (float)(Main.screenWidth / 2);
                    Main.screenPosition.Y = this.position.Y + (float)(this.height / 2) - (float)(Main.screenHeight / 2);
                }
            }
        }
        public void UpdatePlayer(int i)
        {
            float num = 10f;
            float num2 = 0.4f;
            Player.jumpHeight = 15;
            Player.jumpSpeed = 5.01f;
            if (this.wet)
            {
                num2 = 0.2f;
                num = 5f;
                Player.jumpHeight = 30;
                Player.jumpSpeed = 6.01f;
            }
            float num3 = 3f;
            float num4 = 0.08f;
            float num5 = 0.2f;
            float num6 = num3;
            if (this.active)
            {
                this.shadowCount++;
                if (this.shadowCount == 1)
                {
                    this.shadowPos[2] = this.shadowPos[1];
                }
                else
                {
                    if (this.shadowCount == 2)
                    {
                        this.shadowPos[1] = this.shadowPos[0];
                    }
                    else
                    {
                        if (this.shadowCount >= 3)
                        {
                            this.shadowCount = 0;
                            this.shadowPos[0] = this.position;
                        }
                    }
                }
                this.whoAmi = i;
                if (this.runSoundDelay > 0)
                {
                    this.runSoundDelay--;
                }
                if (this.attackCD > 0)
                {
                    this.attackCD--;
                }
                if (this.itemAnimation == 0)
                {
                    this.attackCD = 0;
                }
                if (this.chatShowTime > 0)
                {
                    this.chatShowTime--;
                }
                if (this.potionDelay > 0)
                {
                    this.potionDelay--;
                }
                if (this.dead)
                {
                    if (i == Main.myPlayer)
                    {
                        Main.npcChatText = "";
                        Main.editSign = false;
                    }
                    this.sign = -1;
                    this.talkNPC = -1;
                    this.statLife = 0;
                    this.channel = false;
                    this.potionDelay = 0;
                    this.chest = -1;
                    this.changeItem = -1;
                    this.itemAnimation = 0;
                    this.immuneAlpha += 2;
                    if (this.immuneAlpha > 255)
                    {
                        this.immuneAlpha = 255;
                    }
                    this.respawnTimer--;
                    this.headPosition += this.headVelocity;
                    this.bodyPosition += this.bodyVelocity;
                    this.legPosition += this.legVelocity;
                    this.headRotation += this.headVelocity.X * 0.1f;
                    this.bodyRotation += this.bodyVelocity.X * 0.1f;
                    this.legRotation += this.legVelocity.X * 0.1f;
                    this.headVelocity.Y = this.headVelocity.Y + 0.1f;
                    this.bodyVelocity.Y = this.bodyVelocity.Y + 0.1f;
                    this.legVelocity.Y = this.legVelocity.Y + 0.1f;
                    this.headVelocity.X = this.headVelocity.X * 0.99f;
                    this.bodyVelocity.X = this.bodyVelocity.X * 0.99f;
                    this.legVelocity.X = this.legVelocity.X * 0.99f;
                    if (this.respawnTimer <= 0 && Main.myPlayer == this.whoAmi)
                    {
                        this.Spawn();
                    }
                }
                else
                {
                    if (i == Main.myPlayer)
                    {
                        this.zoneEvil = false;
                        if (Main.evilTiles >= 500)
                        {
                            this.zoneEvil = true;
                        }
                        this.zoneMeteor = false;
                        if (Main.meteorTiles >= 50)
                        {
                            this.zoneMeteor = true;
                        }
                        this.zoneDungeon = false;
                        if (Main.dungeonTiles >= 250 && (double)this.position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
                        {
                            int num7 = (int)this.position.X / 16;
                            int num8 = (int)this.position.Y / 16;
                            if (Main.tile[num7, num8].wall > 0 && !Main.wallHouse[(int)Main.tile[num7, num8].wall])
                            {
                                this.zoneDungeon = true;
                            }
                        }
                        this.zoneJungle = false;
                        if (Main.jungleTiles >= 200)
                        {
                            this.zoneJungle = true;
                        }
                        this.controlUp = false;
                        this.controlLeft = false;
                        this.controlDown = false;
                        this.controlRight = false;
                        this.controlJump = false;
                        this.controlUseItem = false;
                        this.controlUseTile = false;
                        this.controlThrow = false;
                        this.controlInv = false;
                        if (Main.hasFocus)
                        {
                            if (!Main.chatMode && !Main.editSign)
                            {
                                if (this.controlLeft && this.controlRight)
                                {
                                    this.controlLeft = false;
                                    this.controlRight = false;
                                }
                            }
                            if (this.controlInv)
                            {
                                if (this.releaseInventory)
                                {
                                    if (this.talkNPC >= 0)
                                    {
                                        this.talkNPC = -1;
                                        Main.npcChatText = "";
                                    }
                                    else
                                    {
                                        if (this.sign >= 0)
                                        {
                                            this.sign = -1;
                                            Main.editSign = false;
                                            Main.npcChatText = "";
                                        }
                                        else
                                        {
                                            if (!Main.playerInventory)
                                            {
                                                Recipe.FindRecipes();
                                                Main.playerInventory = true;
                                            }
                                            else
                                            {
                                                Main.playerInventory = false;
                                            }
                                        }
                                    }
                                }
                                this.releaseInventory = false;
                            }
                            else
                            {
                                this.releaseInventory = true;
                            }
                            if (this.delayUseItem)
                            {
                                if (!this.controlUseItem)
                                {
                                    this.delayUseItem = false;
                                }
                                this.controlUseItem = false;
                            }
                            if (this.itemAnimation == 0 && this.itemTime == 0)
                            {
                                if (this.controlThrow && this.inventory[this.selectedItem].type > 0 && !Main.chatMode)
                                {
                                    Item item = new Item();
                                    bool flag = false;
                                    int num9 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[this.selectedItem].type, 1, false);
                                    if (!flag && this.inventory[this.selectedItem].type == 8 && this.inventory[this.selectedItem].stack > 1)
                                    {
                                        Item item2 = this.inventory[this.selectedItem];
                                        item2.stack--;
                                    }
                                    else
                                    {
                                        this.inventory[this.selectedItem].position = Main.item[num9].position;
                                        Main.item[num9] = this.inventory[this.selectedItem];
                                        this.inventory[this.selectedItem] = new Item();
                                    }
                                    if (Main.netMode == 0)
                                    {
                                        Main.item[num9].noGrabDelay = 100;
                                    }
                                    Main.item[num9].velocity.Y = -2f;
                                    Main.item[num9].velocity.X = (float)(4 * this.direction) + this.velocity.X;
                                    this.itemAnimation = 10;
                                    this.itemAnimationMax = 10;
                                    Recipe.FindRecipes();
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(21, -1, -1, "", num9, 0f, 0f, 0f);
                                    }
                                }
                                if (!Main.playerInventory)
                                {
                                    int num10 = this.selectedItem;
                                    int j;
                                    for (j = 42; j > 9; j -= 10)
                                    {
                                    }
                                    while (j < 0)
                                    {
                                        j += 10;
                                    }
                                    this.selectedItem -= j;
                                    if (j == 0)
                                    {
                                        goto IL_8A6;
                                    }
                                IL_8A6:
                                    if (this.changeItem >= 0)
                                    {
                                        if (this.selectedItem == this.changeItem)
                                        {
                                            goto IL_8CC;
                                        }
                                    IL_8CC:
                                        this.selectedItem = this.changeItem;
                                        this.changeItem = -1;
                                    }
                                    while (this.selectedItem > 9)
                                    {
                                        this.selectedItem -= 10;
                                    }
                                    while (this.selectedItem < 0)
                                    {
                                        this.selectedItem += 10;
                                    }
                                }
                                else
                                {
                                    int j = 42;
                                    Main.focusRecipe += j;
                                    if (Main.focusRecipe > Main.numAvailableRecipes - 1)
                                    {
                                        Main.focusRecipe = Main.numAvailableRecipes - 1;
                                    }
                                    if (Main.focusRecipe < 0)
                                    {
                                        Main.focusRecipe = 0;
                                    }
                                }
                            }
                        }
                        if (Main.netMode == 1)
                        {
                            bool flag2 = false;
                            if (this.statLife != Main.clientPlayer.statLife || this.statLifeMax != Main.clientPlayer.statLifeMax)
                            {
                                NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                                flag2 = true;
                            }
                            if (this.statMana != Main.clientPlayer.statMana || this.statManaMax != Main.clientPlayer.statManaMax)
                            {
                                NetMessage.SendData(42, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                                flag2 = true;
                            }
                            if (this.controlUp != Main.clientPlayer.controlUp)
                            {
                                flag2 = true;
                            }
                            if (this.controlDown != Main.clientPlayer.controlDown)
                            {
                                flag2 = true;
                            }
                            if (this.controlLeft != Main.clientPlayer.controlLeft)
                            {
                                flag2 = true;
                            }
                            if (this.controlRight != Main.clientPlayer.controlRight)
                            {
                                flag2 = true;
                            }
                            if (this.controlJump != Main.clientPlayer.controlJump)
                            {
                                flag2 = true;
                            }
                            if (this.controlUseItem != Main.clientPlayer.controlUseItem)
                            {
                                flag2 = true;
                            }
                            if (this.selectedItem != Main.clientPlayer.selectedItem)
                            {
                                flag2 = true;
                            }
                            if (flag2)
                            {
                                NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                            }
                        }
                        if (Main.playerInventory)
                        {
                            this.AdjTiles();
                        }
                        if (this.chest != -1)
                        {
                            int num11 = (int)(((double)this.position.X + (double)this.width * 0.5) / 16.0);
                            int num12 = (int)(((double)this.position.Y + (double)this.height * 0.5) / 16.0);
                            if (num11 < this.chestX - 5 || num11 > this.chestX + 6 || num12 < this.chestY - 4 || num12 > this.chestY + 5)
                            {
                                if (this.chest == -1)
                                {
                                    goto IL_C07;
                                }
                            IL_C07:
                                this.chest = -1;
                            }
                            if (!Main.tile[this.chestX, this.chestY].active)
                            {
                                this.chest = -1;
                            }
                        }
                        if (this.velocity.Y == 0f)
                        {
                            int num13 = (int)(this.position.Y / 16f) - this.fallStart;
                            if (num13 > 25 && !this.noFallDmg)
                            {
                                int damage = (num13 - 25) * 10;
                                this.immune = false;
                                this.Hurt(damage, -this.direction, false, false);
                            }
                            this.fallStart = (int)(this.position.Y / 16f);
                        }
                        if (this.velocity.Y < 0f || this.rocketDelay > 0 || this.wet)
                        {
                            this.fallStart = (int)(this.position.Y / 16f);
                        }
                    }
                    if (this.mouseInterface)
                    {
                        this.delayUseItem = true;
                    }
                    Player.tileTargetX = (int)((0f + Main.screenPosition.X) / 16f);
                    Player.tileTargetY = (int)((0f + Main.screenPosition.Y) / 16f);
                    if (this.immune)
                    {
                        this.immuneTime--;
                        if (this.immuneTime <= 0)
                        {
                            this.immune = false;
                        }
                        this.immuneAlpha += this.immuneAlphaDirection * 50;
                        if (this.immuneAlpha <= 50)
                        {
                            this.immuneAlphaDirection = 1;
                        }
                        else
                        {
                            if (this.immuneAlpha >= 205)
                            {
                                this.immuneAlphaDirection = -1;
                            }
                        }
                    }
                    else
                    {
                        this.immuneAlpha = 0;
                    }
                    if (this.manaRegenDelay > 0)
                    {
                        this.manaRegenDelay--;
                    }
                    this.statDefense = 0;
                    this.accWatch = 0;
                    this.accDepthMeter = 0;
                    this.lifeRegen = 0;
                    this.manaCost = 1f;
                    this.meleeSpeed = 1f;
                    this.boneArmor = false;
                    this.rocketBoots = false;
                    this.fireWalk = false;
                    this.noKnockback = false;
                    this.jumpBoost = false;
                    this.noFallDmg = false;
                    this.accFlipper = false;
                    this.spawnMax = false;
                    this.spaceGun = false;
                    this.magicBoost = 1f;
                    if (this.manaRegenDelay == 0)
                    {
                        this.manaRegen = this.statManaMax / 30 + 1;
                    }
                    else
                    {
                        this.manaRegen = 0;
                    }
                    this.doubleJump = false;
                    for (int k = 0; k < 8; k++)
                    {
                        this.statDefense += this.armor[k].defense;
                        this.lifeRegen += this.armor[k].lifeRegen;
                        this.manaRegen += this.armor[k].manaRegen;
                        if (this.armor[k].type == 193)
                        {
                            this.fireWalk = true;
                        }
                        if (this.armor[k].type == 238)
                        {
                            this.magicBoost *= 1.15f;
                        }
                    }
                    this.head = this.armor[0].headSlot;
                    this.body = this.armor[1].bodySlot;
                    this.legs = this.armor[2].legSlot;
                    for (int k = 3; k < 8; k++)
                    {
                        if (this.armor[k].type == 15 && this.accWatch < 1)
                        {
                            this.accWatch = 1;
                        }
                        if (this.armor[k].type == 16 && this.accWatch < 2)
                        {
                            this.accWatch = 2;
                        }
                        if (this.armor[k].type == 17 && this.accWatch < 3)
                        {
                            this.accWatch = 3;
                        }
                        if (this.armor[k].type == 18 && this.accDepthMeter < 1)
                        {
                            this.accDepthMeter = 1;
                        }
                        if (this.armor[k].type == 53)
                        {
                            this.doubleJump = true;
                        }
                        if (this.armor[k].type == 54)
                        {
                            num6 = 6f;
                        }
                        if (this.armor[k].type == 128)
                        {
                            this.rocketBoots = true;
                        }
                        if (this.armor[k].type == 156)
                        {
                            this.noKnockback = true;
                        }
                        if (this.armor[k].type == 158)
                        {
                            this.noFallDmg = true;
                        }
                        if (this.armor[k].type == 159)
                        {
                            this.jumpBoost = true;
                        }
                        if (this.armor[k].type == 187)
                        {
                            this.accFlipper = true;
                        }
                        if (this.armor[k].type == 211)
                        {
                            this.meleeSpeed *= 0.9f;
                        }
                        if (this.armor[k].type == 223)
                        {
                            this.spawnMax = true;
                        }
                        if (this.armor[k].type == 212)
                        {
                            num4 *= 1.1f;
                            num3 *= 1.1f;
                        }
                    }
                    this.lifeRegenCount += this.lifeRegen;
                    while (this.lifeRegenCount >= 120)
                    {
                        this.lifeRegenCount -= 120;
                        if (this.statLife < this.statLifeMax)
                        {
                            this.statLife++;
                        }
                        if (this.statLife > this.statLifeMax)
                        {
                            this.statLife = this.statLifeMax;
                        }
                    }
                    this.manaRegenCount += this.manaRegen;
                    while (this.manaRegenCount >= 120)
                    {
                        this.manaRegenCount -= 120;
                        if (this.statMana < this.statManaMax)
                        {
                            this.statMana++;
                        }
                        if (this.statMana > this.statManaMax)
                        {
                            this.statMana = this.statManaMax;
                        }
                    }
                    if (this.head == 11)
                    {
                        int i2 = ((int)this.position.X + this.width / 2 + 8 * this.direction) / 16;
                        int j2 = (int)(this.position.Y + 2f) / 16;
                        Lighting.addLight(i2, j2, 0.8f);
                    }
                    if (this.jumpBoost)
                    {
                        Player.jumpHeight = 20;
                        Player.jumpSpeed = 6.51f;
                    }
                    this.setBonus = "";
                    if ((this.head == 1 && this.body == 1 && this.legs == 1) || (this.head == 2 && this.body == 2 && this.legs == 2))
                    {
                        this.setBonus = "2 defense";
                        this.statDefense += 2;
                    }
                    if ((this.head == 3 && this.body == 3 && this.legs == 3) || (this.head == 4 && this.body == 4 && this.legs == 4))
                    {
                        this.setBonus = "3 defense";
                        this.statDefense += 3;
                    }
                    if (this.head == 5 && this.body == 5 && this.legs == 5)
                    {
                        this.setBonus = "15 % increased melee speed";
                        this.meleeSpeed *= 0.85f;
                        if (Main.rand.Next(10) == 0)
                        {
                            Color color = default(Color);
                        }
                    }
                    if (this.head == 6 && this.body == 6 && this.legs == 6)
                    {
                        this.setBonus = "Space Gun costs 0 mana";
                        this.spaceGun = true;
                        if (System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                Color color = default(Color);
                                int num14 = 0;
                                Main.dust[num14].noGravity = true;
                                Dust expr_1594_cp_0 = Main.dust[num14];
                                expr_1594_cp_0.velocity.X = expr_1594_cp_0.velocity.X - this.velocity.X * 0.5f;
                                Dust expr_15BE_cp_0 = Main.dust[num14];
                                expr_15BE_cp_0.velocity.Y = expr_15BE_cp_0.velocity.Y - this.velocity.Y * 0.5f;
                            }
                        }
                    }
                    if (this.head == 7 && this.body == 7 && this.legs == 7)
                    {
                        num4 *= 1.3f;
                        num3 *= 1.3f;
                        this.setBonus = "30% increased movement speed";
                        this.boneArmor = true;
                    }
                    if (this.head == 8 && this.body == 8 && this.legs == 8)
                    {
                        this.setBonus = "25% reduced mana usage";
                        this.manaCost *= 0.75f;
                        this.meleeSpeed *= 0.9f;
                        if (System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y) > 1f)
                        {
                            Color color = default(Color);
                            int num14 = 0;
                            Main.dust[num14].noGravity = true;
                            Main.dust[num14].velocity.X = this.velocity.X * 0.25f;
                            Main.dust[num14].velocity.Y = this.velocity.Y * 0.25f;
                        }
                    }
                    if (this.head == 9 && this.body == 9 && this.legs == 9)
                    {
                        this.setBonus = "5 defense";
                        this.statDefense += 5;
                        if (System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                Color color = default(Color);
                                int num14 = 0;
                                Main.dust[num14].noGravity = true;
                                Dust expr_17D5_cp_0 = Main.dust[num14];
                                expr_17D5_cp_0.velocity.X = expr_17D5_cp_0.velocity.X - this.velocity.X * 0.5f;
                                Dust expr_17FF_cp_0 = Main.dust[num14];
                                expr_17FF_cp_0.velocity.Y = expr_17FF_cp_0.velocity.Y - this.velocity.Y * 0.5f;
                            }
                        }
                    }
                    if (!this.doubleJump)
                    {
                        this.jumpAgain = false;
                    }
                    else
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.jumpAgain = true;
                        }
                    }
                    if ((double)this.meleeSpeed < 0.7)
                    {
                        this.meleeSpeed = 0.7f;
                    }
                    if (this.grappling[0] == -1)
                    {
                        if (this.controlLeft && this.velocity.X > -num3)
                        {
                            if (this.velocity.X > num5)
                            {
                                this.velocity.X = this.velocity.X - num5;
                            }
                            this.velocity.X = this.velocity.X - num4;
                            if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                            {
                                this.direction = -1;
                            }
                        }
                        else
                        {
                            if (this.controlRight && this.velocity.X < num3)
                            {
                                if (this.velocity.X < -num5)
                                {
                                    this.velocity.X = this.velocity.X + num5;
                                }
                                this.velocity.X = this.velocity.X + num4;
                                if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                                {
                                    this.direction = 1;
                                }
                            }
                            else
                            {
                                if (this.controlLeft && this.velocity.X > -num6)
                                {
                                    if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                                    {
                                        this.direction = -1;
                                    }
                                    if (this.velocity.Y == 0f)
                                    {
                                        if (this.velocity.X > num5)
                                        {
                                            this.velocity.X = this.velocity.X - num5;
                                        }
                                        this.velocity.X = this.velocity.X - num4 * 0.2f;
                                    }
                                    if (this.velocity.X < -(num6 + num3) / 2f && this.velocity.Y == 0f)
                                    {
                                        if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
                                        {
                                            this.runSoundDelay = 9;
                                        }
                                        Color color = default(Color);
                                        int num15 = 0;
                                        Dust expr_1B19_cp_0 = Main.dust[num15];
                                        expr_1B19_cp_0.velocity.X = expr_1B19_cp_0.velocity.X * 0.2f;
                                        Dust expr_1B37_cp_0 = Main.dust[num15];
                                        expr_1B37_cp_0.velocity.Y = expr_1B37_cp_0.velocity.Y * 0.2f;
                                    }
                                }
                                else
                                {
                                    if (this.controlRight && this.velocity.X < num6)
                                    {
                                        if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                                        {
                                            this.direction = 1;
                                        }
                                        if (this.velocity.Y == 0f)
                                        {
                                            if (this.velocity.X < -num5)
                                            {
                                                this.velocity.X = this.velocity.X + num5;
                                            }
                                            this.velocity.X = this.velocity.X + num4 * 0.2f;
                                        }
                                        if (this.velocity.X > (num6 + num3) / 2f && this.velocity.Y == 0f)
                                        {
                                            if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
                                            {
                                                this.runSoundDelay = 9;
                                            }
                                            Color color = default(Color);
                                            int num15 = 0;
                                            Dust expr_1C88_cp_0 = Main.dust[num15];
                                            expr_1C88_cp_0.velocity.X = expr_1C88_cp_0.velocity.X * 0.2f;
                                            Dust expr_1CA6_cp_0 = Main.dust[num15];
                                            expr_1CA6_cp_0.velocity.Y = expr_1CA6_cp_0.velocity.Y * 0.2f;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.Y == 0f)
                                        {
                                            if (this.velocity.X > num5)
                                            {
                                                this.velocity.X = this.velocity.X - num5;
                                            }
                                            else
                                            {
                                                if (this.velocity.X < -num5)
                                                {
                                                    this.velocity.X = this.velocity.X + num5;
                                                }
                                                else
                                                {
                                                    this.velocity.X = 0f;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if ((double)this.velocity.X > (double)num5 * 0.5)
                                            {
                                                this.velocity.X = this.velocity.X - num5 * 0.5f;
                                            }
                                            else
                                            {
                                                if ((double)this.velocity.X < (double)(-(double)num5) * 0.5)
                                                {
                                                    this.velocity.X = this.velocity.X + num5 * 0.5f;
                                                }
                                                else
                                                {
                                                    this.velocity.X = 0f;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (this.controlJump)
                        {
                            if (this.jump > 0)
                            {
                                if (this.velocity.Y > -Player.jumpSpeed + num2 * 2f)
                                {
                                    this.jump = 0;
                                }
                                else
                                {
                                    this.velocity.Y = -Player.jumpSpeed;
                                    this.jump--;
                                }
                            }
                            else
                            {
                                if ((this.velocity.Y == 0f || this.jumpAgain || (this.wet && this.accFlipper)) && this.releaseJump)
                                {
                                    bool flag3 = false;
                                    if (this.wet && this.accFlipper)
                                    {
                                        if (this.swimTime == 0)
                                        {
                                            this.swimTime = 30;
                                        }
                                        flag3 = true;
                                    }
                                    this.jumpAgain = false;
                                    this.canRocket = false;
                                    this.rocketRelease = false;
                                    if (this.velocity.Y == 0f && this.doubleJump)
                                    {
                                        this.jumpAgain = true;
                                    }
                                    if (this.velocity.Y == 0f || flag3)
                                    {
                                        this.velocity.Y = -Player.jumpSpeed;
                                        this.jump = Player.jumpHeight;
                                    }
                                    else
                                    {
                                        this.velocity.Y = -Player.jumpSpeed;
                                        this.jump = Player.jumpHeight / 2;
                                        for (int k = 0; k < 10; k++)
                                        {
                                            Color color = default(Color);
                                            int num15 = 0;
                                            Main.dust[num15].velocity.X = Main.dust[num15].velocity.X * 0.5f - this.velocity.X * 0.1f;
                                            Main.dust[num15].velocity.Y = Main.dust[num15].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
                                        }
                                        int num16 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 16f, this.position.Y + (float)this.height - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                                        Main.gore[num16].velocity.X = Main.gore[num16].velocity.X * 0.1f - this.velocity.X * 0.1f;
                                        Main.gore[num16].velocity.Y = Main.gore[num16].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
                                        num16 = Gore.NewGore(new Vector2(this.position.X - 36f, this.position.Y + (float)this.height - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                                        Main.gore[num16].velocity.X = Main.gore[num16].velocity.X * 0.1f - this.velocity.X * 0.1f;
                                        Main.gore[num16].velocity.Y = Main.gore[num16].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
                                        num16 = Gore.NewGore(new Vector2(this.position.X + (float)this.width + 4f, this.position.Y + (float)this.height - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                                        Main.gore[num16].velocity.X = Main.gore[num16].velocity.X * 0.1f - this.velocity.X * 0.1f;
                                        Main.gore[num16].velocity.Y = Main.gore[num16].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
                                    }
                                }
                            }
                            this.releaseJump = false;
                        }
                        else
                        {
                            this.jump = 0;
                            this.releaseJump = true;
                            this.rocketRelease = true;
                        }
                        if (this.doubleJump && !this.jumpAgain && this.velocity.Y < 0f && !this.rocketBoots && !this.accFlipper)
                        {
                            Color color = default(Color);
                            int num15 = 0;
                            Main.dust[num15].velocity.X = Main.dust[num15].velocity.X * 0.5f - this.velocity.X * 0.1f;
                            Main.dust[num15].velocity.Y = Main.dust[num15].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
                        }
                        if (this.velocity.Y > -Player.jumpSpeed && this.velocity.Y != 0f)
                        {
                            this.canRocket = true;
                        }
                        if (this.rocketBoots && this.controlJump && this.rocketDelay == 0 && this.canRocket && this.rocketRelease && !this.jumpAgain)
                        {
                            int num17 = 7;
                            if (this.statMana >= (int)((float)num17 * this.manaCost))
                            {
                                this.manaRegenDelay = 180;
                                this.statMana -= (int)((float)num17 * this.manaCost);
                                this.rocketDelay = 10;
                                if (this.rocketDelay2 <= 0)
                                {
                                    this.rocketDelay2 = 30;
                                }
                            }
                            else
                            {
                                this.canRocket = false;
                            }
                        }
                        if (this.rocketDelay2 > 0)
                        {
                            this.rocketDelay2--;
                        }
                        if (this.rocketDelay == 0)
                        {
                            this.rocketFrame = false;
                        }
                        if (this.rocketDelay > 0)
                        {
                            this.rocketFrame = true;
                            for (int k = 0; k < 2; k++)
                            {
                                if (k == 0)
                                {
                                    Color color = default(Color);
                                    int num14 = 0;
                                    Main.dust[num14].noGravity = true;
                                    Main.dust[num14].velocity.X = Main.dust[num14].velocity.X * 1f - 2f - this.velocity.X * 0.3f;
                                    Main.dust[num14].velocity.Y = Main.dust[num14].velocity.Y * 1f + 2f - this.velocity.Y * 0.3f;
                                }
                                else
                                {
                                    Color color = default(Color);
                                    int num14 = 0;
                                    Main.dust[num14].noGravity = true;
                                    Main.dust[num14].velocity.X = Main.dust[num14].velocity.X * 1f + 2f - this.velocity.X * 0.3f;
                                    Main.dust[num14].velocity.Y = Main.dust[num14].velocity.Y * 1f + 2f - this.velocity.Y * 0.3f;
                                }
                            }
                            if (this.rocketDelay == 0)
                            {
                                this.releaseJump = true;
                            }
                            this.rocketDelay--;
                            this.velocity.Y = this.velocity.Y - 0.1f;
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y = this.velocity.Y - 0.3f;
                            }
                            if (this.velocity.Y < -Player.jumpSpeed)
                            {
                                this.velocity.Y = -Player.jumpSpeed;
                            }
                        }
                        else
                        {
                            this.velocity.Y = this.velocity.Y + num2;
                        }
                        if (this.velocity.Y > num)
                        {
                            this.velocity.Y = num;
                        }
                    }
                    for (int k = 0; k < 200; k++)
                    {
                        if (Main.item[k].active && Main.item[k].noGrabDelay == 0 && Main.item[k].owner == i)
                        {
                            Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
                            if (rectangle.Intersects(new Rectangle((int)Main.item[k].position.X, (int)Main.item[k].position.Y, Main.item[k].width, Main.item[k].height)))
                            {
                                if (i == Main.myPlayer && (this.inventory[this.selectedItem].type != 0 || this.itemAnimation <= 0))
                                {
                                    if (Main.item[k].type == 58)
                                    {
                                        this.statLife += 20;
                                        if (Main.myPlayer == this.whoAmi)
                                        {
                                            this.HealEffect(20);
                                        }
                                        if (this.statLife > this.statLifeMax)
                                        {
                                            this.statLife = this.statLifeMax;
                                        }
                                        Main.item[k] = new Item();
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(21, -1, -1, "", k, 0f, 0f, 0f);
                                        }
                                    }
                                    else
                                    {
                                        if (Main.item[k].type == 184)
                                        {
                                            this.statMana += 20;
                                            if (Main.myPlayer == this.whoAmi)
                                            {
                                                this.ManaEffect(20);
                                            }
                                            if (this.statMana > this.statManaMax)
                                            {
                                                this.statMana = this.statManaMax;
                                            }
                                            Main.item[k] = new Item();
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(21, -1, -1, "", k, 0f, 0f, 0f);
                                            }
                                        }
                                        else
                                        {
                                            Main.item[k] = this.GetItem(i, Main.item[k]);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(21, -1, -1, "", k, 0f, 0f, 0f);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rectangle = new Rectangle((int)this.position.X - Player.itemGrabRange, (int)this.position.Y - Player.itemGrabRange, this.width + Player.itemGrabRange * 2, this.height + Player.itemGrabRange * 2);
                                if (rectangle.Intersects(new Rectangle((int)Main.item[k].position.X, (int)Main.item[k].position.Y, Main.item[k].width, Main.item[k].height)) && this.ItemSpace(Main.item[k]))
                                {
                                    Main.item[k].beingGrabbed = true;
                                    if ((double)this.position.X + (double)this.width * 0.5 > (double)Main.item[k].position.X + (double)Main.item[k].width * 0.5)
                                    {
                                        if (Main.item[k].velocity.X < Player.itemGrabSpeedMax + this.velocity.X)
                                        {
                                            Item expr_2B53_cp_0 = Main.item[k];
                                            expr_2B53_cp_0.velocity.X = expr_2B53_cp_0.velocity.X + Player.itemGrabSpeed;
                                        }
                                        if (Main.item[k].velocity.X < 0f)
                                        {
                                            Item expr_2B95_cp_0 = Main.item[k];
                                            expr_2B95_cp_0.velocity.X = expr_2B95_cp_0.velocity.X + Player.itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    else
                                    {
                                        if (Main.item[k].velocity.X > -Player.itemGrabSpeedMax + this.velocity.X)
                                        {
                                            Item expr_2BF1_cp_0 = Main.item[k];
                                            expr_2BF1_cp_0.velocity.X = expr_2BF1_cp_0.velocity.X - Player.itemGrabSpeed;
                                        }
                                        if (Main.item[k].velocity.X > 0f)
                                        {
                                            Item expr_2C33_cp_0 = Main.item[k];
                                            expr_2C33_cp_0.velocity.X = expr_2C33_cp_0.velocity.X - Player.itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    if ((double)this.position.Y + (double)this.height * 0.5 > (double)Main.item[k].position.Y + (double)Main.item[k].height * 0.5)
                                    {
                                        if (Main.item[k].velocity.Y < Player.itemGrabSpeedMax)
                                        {
                                            Item expr_2CD5_cp_0 = Main.item[k];
                                            expr_2CD5_cp_0.velocity.Y = expr_2CD5_cp_0.velocity.Y + Player.itemGrabSpeed;
                                        }
                                        if (Main.item[k].velocity.Y < 0f)
                                        {
                                            Item expr_2D17_cp_0 = Main.item[k];
                                            expr_2D17_cp_0.velocity.Y = expr_2D17_cp_0.velocity.Y + Player.itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    else
                                    {
                                        if (Main.item[k].velocity.Y > -Player.itemGrabSpeedMax)
                                        {
                                            Item expr_2D67_cp_0 = Main.item[k];
                                            expr_2D67_cp_0.velocity.Y = expr_2D67_cp_0.velocity.Y - Player.itemGrabSpeed;
                                        }
                                        if (Main.item[k].velocity.Y > 0f)
                                        {
                                            Item expr_2DA9_cp_0 = Main.item[k];
                                            expr_2DA9_cp_0.velocity.Y = expr_2DA9_cp_0.velocity.Y - Player.itemGrabSpeed * 0.75f;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (this.position.X / 16f - (float)Player.tileRangeX <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY - 2f >= (float)Player.tileTargetY && Main.tile[Player.tileTargetX, Player.tileTargetY].active)
                    {
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 224;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 48;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 8;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)
                        {
                            this.showItemIcon = true;
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 18)
                            {
                                this.showItemIcon2 = 28;
                            }
                            else
                            {
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 36)
                                {
                                    this.showItemIcon2 = 110;
                                }
                                else
                                {
                                    this.showItemIcon2 = 31;
                                }
                            }
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 87;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 105;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 148;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 165;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55)
                        {
                            int l = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
                            int num18 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
                            while (l > 1)
                            {
                                l -= 2;
                            }
                            int num11 = Player.tileTargetX - l;
                            int num12 = Player.tileTargetY - num18;
                            Main.signBubble = true;
                            Main.signX = num11 * 16 + 16;
                            Main.signY = num12 * 16;
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 25;
                        }
                        if (this.controlUseTile)
                        {
                            if (this.releaseUseTile)
                            {
                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90))
                                {
                                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f);
                                    }
                                }
                                else
                                {
                                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
                                    {
                                        int num11 = Player.tileTargetX;
                                        int num12 = Player.tileTargetY;
                                        num11 += (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18 * -1);
                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 72)
                                        {
                                            num11 += 4;
                                            num11++;
                                        }
                                        else
                                        {
                                            num11 += 2;
                                        }
                                        num12 += (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18 * -1);
                                        num12 += 2;
                                        if (Player.CheckSpawn(num11, num12))
                                        {
                                            this.ChangeSpawn(num11, num12);
                                        }
                                    }
                                    else
                                    {
                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55)
                                        {
                                            bool flag4 = true;
                                            if (this.sign >= 0 && Sign.ReadSign(Player.tileTargetX, Player.tileTargetY) == this.sign)
                                            {
                                                this.sign = -1;
                                                Main.npcChatText = "";
                                                Main.editSign = false;
                                                flag4 = false;
                                            }
                                            if (flag4)
                                            {
                                                if (Main.netMode == 0)
                                                {
                                                    this.talkNPC = -1;
                                                    Main.playerInventory = false;
                                                    Main.editSign = false;
                                                    int num19 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
                                                    this.sign = num19;
                                                    Main.npcChatText = Main.sign[num19].text;
                                                }
                                                else
                                                {
                                                    int l = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
                                                    int num18 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
                                                    while (l > 1)
                                                    {
                                                        l -= 2;
                                                    }
                                                    int num11 = Player.tileTargetX - l;
                                                    int num12 = Player.tileTargetY - num18;
                                                    if (Main.tile[num11, num12].type == 55)
                                                    {
                                                        NetMessage.SendData(46, -1, -1, "", num11, (float)num12, 0f, 0f);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10)
                                            {
                                                WorldGen.OpenDoor(Player.tileTargetX, Player.tileTargetY, this.direction);
                                                NetMessage.SendData(19, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction);
                                            }
                                            else
                                            {
                                                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
                                                {
                                                    if (WorldGen.CloseDoor(Player.tileTargetX, Player.tileTargetY, false))
                                                    {
                                                        NetMessage.SendData(19, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction);
                                                    }
                                                }
                                                else
                                                {
                                                    if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29) && this.talkNPC == -1)
                                                    {
                                                        bool flag5 = false;
                                                        int num11 = Player.tileTargetX - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
                                                        int num12 = Player.tileTargetY - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
                                                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
                                                        {
                                                            flag5 = true;
                                                        }
                                                        if (Main.netMode == 1 && !flag5)
                                                        {
                                                            if (num11 == this.chestX && num12 == this.chestY && this.chest != -1)
                                                            {
                                                                this.chest = -1;
                                                            }
                                                            else
                                                            {
                                                                NetMessage.SendData(31, -1, -1, "", num11, (float)num12, 0f, 0f);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            int num20 = -1;
                                                            if (flag5)
                                                            {
                                                                num20 = -2;
                                                            }
                                                            else
                                                            {
                                                                num20 = Chest.FindChest(num11, num12);
                                                            }
                                                            if (num20 != -1)
                                                            {
                                                                if (num20 == this.chest)
                                                                {
                                                                    this.chest = -1;
                                                                }
                                                                else
                                                                {
                                                                    if (num20 != this.chest && this.chest == -1)
                                                                    {
                                                                        this.chest = num20;
                                                                        Main.playerInventory = true;
                                                                        this.chestX = num11;
                                                                        this.chestY = num12;
                                                                    }
                                                                    else
                                                                    {
                                                                        this.chest = num20;
                                                                        Main.playerInventory = true;
                                                                        this.chestX = num11;
                                                                        this.chestY = num12;
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
                            this.releaseUseTile = false;
                        }
                        else
                        {
                            this.releaseUseTile = true;
                        }
                    }
                    if (Main.myPlayer == this.whoAmi)
                    {
                        if (this.talkNPC >= 0)
                        {
                            Rectangle rectangle2 = new Rectangle((int)this.position.X + this.width / 2 - Player.tileRangeX * 16, (int)this.position.Y + this.height / 2 - Player.tileRangeY * 16, Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
                            Rectangle value = new Rectangle((int)Main.npc[this.talkNPC].position.X, (int)Main.npc[this.talkNPC].position.Y, Main.npc[this.talkNPC].width, Main.npc[this.talkNPC].height);
                            if (!rectangle2.Intersects(value) || this.chest != -1 || !Main.npc[this.talkNPC].active)
                            {
                                if (this.chest != -1)
                                {
                                    goto IL_397D;
                                }
                            IL_397D:
                                this.talkNPC = -1;
                                Main.npcChatText = "";
                            }
                        }
                        if (this.sign >= 0)
                        {
                            Rectangle rectangle2 = new Rectangle((int)this.position.X + this.width / 2 - Player.tileRangeX * 16, (int)this.position.Y + this.height / 2 - Player.tileRangeY * 16, Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
                            Rectangle value2 = new Rectangle(Main.sign[this.sign].x * 16, Main.sign[this.sign].y * 16, 32, 32);
                            if (!rectangle2.Intersects(value2))
                            {
                                this.sign = -1;
                                Main.editSign = false;
                                Main.npcChatText = "";
                            }
                        }
                        if (Main.editSign)
                        {
                            if (this.sign == -1)
                            {
                                Main.editSign = false;
                            }
                        }
                        Rectangle rectangle3 = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
                        for (int k = 0; k < 1000; k++)
                        {
                            if (Main.npc[k].active && !Main.npc[k].friendly && rectangle3.Intersects(new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height)))
                            {
                                int hitDirection = -1;
                                if (Main.npc[k].position.X + (float)(Main.npc[k].width / 2) < this.position.X + (float)(this.width / 2))
                                {
                                    hitDirection = 1;
                                }
                                this.Hurt(Main.npc[k].damage, hitDirection, false, false);
                            }
                        }
                        Vector2 vector = Collision.HurtTiles(this.position, this.velocity, this.width, this.height, this.fireWalk);
                        if (vector.Y != 0f)
                        {
                            this.Hurt((int)vector.Y, (int)vector.X, false, false);
                        }
                    }
                    if (this.grappling[0] >= 0)
                    {
                        this.rocketDelay = 0;
                        this.rocketFrame = false;
                        this.canRocket = false;
                        this.rocketRelease = false;
                        this.fallStart = (int)(this.position.Y / 16f);
                        float num21 = 0f;
                        float num22 = 0f;
                        for (int m = 0; m < this.grapCount; m++)
                        {
                            num21 += Main.projectile[this.grappling[m]].position.X + (float)(Main.projectile[this.grappling[m]].width / 2);
                            num22 += Main.projectile[this.grappling[m]].position.Y + (float)(Main.projectile[this.grappling[m]].height / 2);
                        }
                        num21 /= (float)this.grapCount;
                        num22 /= (float)this.grapCount;
                        Vector2 vector2 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                        float num23 = num21 - vector2.X;
                        float num24 = num22 - vector2.Y;
                        float num25 = (float)System.Math.Sqrt((double)(num23 * num23 + num24 * num24));
                        float num26 = 11f;
                        float num27 = num25;
                        if (num25 > num26)
                        {
                            num27 = num26 / num25;
                        }
                        else
                        {
                            num27 = 1f;
                        }
                        num23 *= num27;
                        num24 *= num27;
                        this.velocity.X = num23;
                        this.velocity.Y = num24;
                        if (this.itemAnimation == 0)
                        {
                            if (this.velocity.X > 0f)
                            {
                                this.direction = 1;
                            }
                            if (this.velocity.X < 0f)
                            {
                                this.direction = -1;
                            }
                        }
                        if (this.controlJump)
                        {
                            if (this.releaseJump)
                            {
                                if (this.velocity.Y == 0f || (this.wet && (double)this.velocity.Y > -0.02 && (double)this.velocity.Y < 0.02))
                                {
                                    this.velocity.Y = -Player.jumpSpeed;
                                    this.jump = Player.jumpHeight / 2;
                                    this.releaseJump = false;
                                }
                                else
                                {
                                    this.velocity.Y = this.velocity.Y + 0.01f;
                                    this.releaseJump = false;
                                }
                                if (this.doubleJump)
                                {
                                    this.jumpAgain = true;
                                }
                                this.grappling[0] = 0;
                                this.grapCount = 0;
                                for (int k = 0; k < 1000; k++)
                                {
                                    if (Main.projectile[k].active && Main.projectile[k].owner == i && Main.projectile[k].aiStyle == 7)
                                    {
                                        Main.projectile[k].Kill();
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.releaseJump = true;
                        }
                    }
                    if (Collision.StickyTiles(this.position, this.velocity, this.width, this.height))
                    {
                        this.fallStart = (int)(this.position.Y / 16f);
                        this.jump = 0;
                        if (this.velocity.X > 1f)
                        {
                            this.velocity.X = 1f;
                        }
                        if (this.velocity.X < -1f)
                        {
                            this.velocity.X = -1f;
                        }
                        if (this.velocity.Y > 1f)
                        {
                            this.velocity.Y = 1f;
                        }
                        if (this.velocity.Y < -5f)
                        {
                            this.velocity.Y = -5f;
                        }
                        if ((double)this.velocity.X > 0.75 || (double)this.velocity.X < -0.75)
                        {
                            this.velocity.X = this.velocity.X * 0.85f;
                        }
                        else
                        {
                            this.velocity.X = this.velocity.X * 0.6f;
                        }
                        if (this.velocity.Y < 0f)
                        {
                            this.velocity.Y = this.velocity.Y * 0.96f;
                        }
                        else
                        {
                            this.velocity.Y = this.velocity.Y * 0.3f;
                        }
                    }
                    bool flag6 = Collision.DrownCollision(this.position, this.width, this.height);
                    if (this.inventory[this.selectedItem].type == 186)
                    {
                        try
                        {
                            int num28 = (int)((this.position.X + (float)(this.width / 2) + (float)(6 * this.direction)) / 16f);
                            int num29 = (int)((this.position.Y - 44f) / 16f);
                            if (Main.tile[num28, num29].liquid < 128)
                            {
                                if (Main.tile[num28, num29] == null)
                                {
                                    Main.tile[num28, num29] = new Tile();
                                }
                                if (!Main.tile[num28, num29].active || !Main.tileSolid[(int)Main.tile[num28, num29].type] || Main.tileSolidTop[(int)Main.tile[num28, num29].type])
                                {
                                    flag6 = false;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (Main.myPlayer == i)
                    {
                        if (flag6)
                        {
                            this.breathCD++;
                            int num30 = 7;
                            if (this.inventory[this.selectedItem].type == 186)
                            {
                                num30 *= 2;
                            }
                            if (this.breathCD >= num30)
                            {
                                this.breathCD = 0;
                                this.breath--;
                                if (this.breath <= 0)
                                {
                                    this.breath = 0;
                                    this.statLife -= 2;
                                    if (this.statLife <= 0)
                                    {
                                        this.statLife = 0;
                                        this.KillMe(10.0, 0, false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.breath += 3;
                            if (this.breath > this.breathMax)
                            {
                                this.breath = this.breathMax;
                            }
                            this.breathCD = 0;
                        }
                    }
                    if (flag6 && Main.rand.Next(20) == 0)
                    {
                        if (this.inventory[this.selectedItem].type == 186)
                        {
                            Color color = default(Color);
                        }
                        else
                        {
                            Color color = default(Color);
                        }
                    }
                    bool flag7 = Collision.LavaCollision(this.position, this.width, this.height);
                    if (flag7)
                    {
                        if (Main.myPlayer == i)
                        {
                            this.Hurt(100, 0, false, false);
                        }
                        this.lavaWet = true;
                    }
                    if (Collision.WetCollision(this.position, this.width, this.height))
                    {
                        if (!this.wet)
                        {
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!flag7)
                                {
                                    for (int n = 0; n < 50; n++)
                                    {
                                        Color color = default(Color);
                                        int num15 = 0;
                                        Dust expr_44A5_cp_0 = Main.dust[num15];
                                        expr_44A5_cp_0.velocity.Y = expr_44A5_cp_0.velocity.Y - 4f;
                                        Dust expr_44C3_cp_0 = Main.dust[num15];
                                        expr_44C3_cp_0.velocity.X = expr_44C3_cp_0.velocity.X * 2.5f;
                                        Main.dust[num15].scale = 1.3f;
                                        Main.dust[num15].alpha = 100;
                                        Main.dust[num15].noGravity = true;
                                    }
                                }
                                else
                                {
                                    for (int n = 0; n < 20; n++)
                                    {
                                        Color color = default(Color);
                                        int num15 = 0;
                                        Dust expr_453E_cp_0 = Main.dust[num15];
                                        expr_453E_cp_0.velocity.Y = expr_453E_cp_0.velocity.Y - 1.5f;
                                        Dust expr_455C_cp_0 = Main.dust[num15];
                                        expr_455C_cp_0.velocity.X = expr_455C_cp_0.velocity.X * 2.5f;
                                        Main.dust[num15].scale = 1.3f;
                                        Main.dust[num15].alpha = 100;
                                        Main.dust[num15].noGravity = true;
                                    }
                                }
                            }
                            this.wet = true;
                        }
                    }
                    else
                    {
                        if (this.wet)
                        {
                            this.wet = false;
                            if (this.jump > Player.jumpHeight / 5)
                            {
                                this.jump = Player.jumpHeight / 5;
                            }
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!this.lavaWet)
                                {
                                    for (int n = 0; n < 50; n++)
                                    {
                                        Color color = default(Color);
                                        int num15 = 0;
                                        Dust expr_464F_cp_0 = Main.dust[num15];
                                        expr_464F_cp_0.velocity.Y = expr_464F_cp_0.velocity.Y - 4f;
                                        Dust expr_466D_cp_0 = Main.dust[num15];
                                        expr_466D_cp_0.velocity.X = expr_466D_cp_0.velocity.X * 2.5f;
                                        Main.dust[num15].scale = 1.3f;
                                        Main.dust[num15].alpha = 100;
                                        Main.dust[num15].noGravity = true;
                                    }
                                }
                                else
                                {
                                    for (int n = 0; n < 20; n++)
                                    {
                                        Color color = default(Color);
                                        int num15 = 0;
                                        Dust expr_46E8_cp_0 = Main.dust[num15];
                                        expr_46E8_cp_0.velocity.Y = expr_46E8_cp_0.velocity.Y - 1.5f;
                                        Dust expr_4706_cp_0 = Main.dust[num15];
                                        expr_4706_cp_0.velocity.X = expr_4706_cp_0.velocity.X * 2.5f;
                                        Main.dust[num15].scale = 1.3f;
                                        Main.dust[num15].alpha = 100;
                                        Main.dust[num15].noGravity = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!this.wet)
                    {
                        this.lavaWet = false;
                    }
                    if (this.wetCount > 0)
                    {
                        this.wetCount -= 1;
                    }
                    if (this.wet)
                    {
                        if (this.wet)
                        {
                            Vector2 vector3 = this.velocity;
                            this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
                            Vector2 b = this.velocity * 0.5f;
                            if (this.velocity.X != vector3.X)
                            {
                                b.X = this.velocity.X;
                            }
                            if (this.velocity.Y != vector3.Y)
                            {
                                b.Y = this.velocity.Y;
                            }
                            this.position += b;
                        }
                    }
                    else
                    {
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
                        this.position += this.velocity;
                    }
                    if (this.position.X < Main.leftWorld + 336f + 16f)
                    {
                        this.position.X = Main.leftWorld + 336f + 16f;
                        this.velocity.X = 0f;
                    }
                    if (this.position.X + (float)this.width > Main.rightWorld - 336f - 32f)
                    {
                        this.position.X = Main.rightWorld - 336f - 32f - (float)this.width;
                        this.velocity.X = 0f;
                    }
                    if (this.position.Y < Main.topWorld + 336f + 16f)
                    {
                        this.position.Y = Main.topWorld + 336f + 16f;
                        if ((double)this.velocity.Y < -0.1)
                        {
                            this.velocity.Y = -0.1f;
                        }
                    }
                    if (this.position.Y > Main.bottomWorld - 336f - 32f - (float)this.height)
                    {
                        this.position.Y = Main.bottomWorld - 336f - 32f - (float)this.height;
                        this.velocity.Y = 0f;
                    }
                    this.ItemCheck(i);
                    this.PlayerFrame();
                    if (this.statLife > this.statLifeMax)
                    {
                        this.statLife = this.statLifeMax;
                    }
                    this.grappling[0] = -1;
                    this.grapCount = 0;
                }
            }
        }
    }
}

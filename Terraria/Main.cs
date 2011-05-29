namespace Terraria
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class Main
    {
        public static Texture2D[] armorArmTexture = new Texture2D[10];
        public static Texture2D[] armorBodyTexture = new Texture2D[10];
        public static Texture2D[] armorHeadTexture = new Texture2D[12];
        public static Texture2D[] armorLegTexture = new Texture2D[10];
        public static int[] availableRecipe = new int[Recipe.maxRecipes];
        public static float[] availableRecipeY = new float[Recipe.maxRecipes];
        public static int background = 0;
        public static int[] backgroundHeight = new int[7];
        public static Texture2D[] backgroundTexture = new Texture2D[7];
        public static int[] backgroundWidth = new int[7];
        private static int backSpaceCount = 0;
        private int bgScroll;
        public static Texture2D blackTileTexture;
        public static bool bloodMoon = false;
        public static Texture2D boneArmTexture;
        public static float bottomWorld = 38400f;
        public static Texture2D bubbleTexture;
        public static float caveParrallax = 1f;
        public static string cDown = "S";
        public static Texture2D cdTexture;
        public static Texture2D chain2Texture;
        public static Texture2D chain3Texture;
        public static Texture2D chain4Texture;
        public static Texture2D chain5Texture;
        public static Texture2D chain6Texture;
        public static Texture2D chainTexture;
        public static Texture2D chat2Texture;
        public static Texture2D chatBackTexture;
        public static int chatLength = 600;
        public static ChatLine[] chatLine = new ChatLine[numChatLines];
        public static bool chatMode = false;
        public static bool chatRelease = false;
        public static string chatText = "";
        public static Texture2D chatTexture;
        public static int checkForSpawns = 0;
        public static Chest[] chest = new Chest[0x3e8];
        public static string cInv = "Escape";
        public static string cJump = "Space";
        public static string cLeft = "A";
        public static Player clientPlayer = new Player();
        public static Cloud[] cloud = new Cloud[100];
        public static int cloudLimit = 100;
        public static Texture2D[] cloudTexture = new Texture2D[4];
        private int colorDelay;
        public static CombatText[] combatText = new CombatText[100];
        public static string cRight = "D";
        public static string cThrowItem = "Q";
        public static string cUp = "W";
        public int curMusic;
        public static int curRelease = 3;
        public static float cursorAlpha = 0f;
        public static Color cursorColor = Color.White;
        public static int cursorColorDirection = 1;
        public static float cursorScale = 0f;
        public static Texture2D cursorTexture;
        public const double dayLength = 54000.0;
        public static bool dayTime = true;
        public static bool debugMode = false;
        public static string defaultIP = "";
        public static int drawTime = 0;
        public static bool dumbAI = false;
        public static int dungeonTiles;
        public static int dungeonX;
        public static int dungeonY;
        public static Dust[] dust = new Dust[0x7d0];
        public static Texture2D dustTexture;
        public static bool editSign = false;
        public static int evilTiles;
        private static float exitScale = 0.8f;
        public static int fadeCounter = 0;
        public static Texture2D fadeTexture;
        public static bool fixedTiming = false;
        private int focusColor;
        private int focusMenu = -1;
        public static int focusRecipe;
        public static SpriteFont fontCombatText;
        public static SpriteFont fontDeathText;
        public static SpriteFont fontItemStack;
        public static SpriteFont fontMouseText;
        public static int frameRate = 0;
        public static bool frameRelease = false;
        public static bool gameMenu = true;
        public static string getIP = defaultIP;
        public static bool godMode = false;
        public static Gore[] gore = new Gore[0xc9];
        public static Texture2D[] goreTexture = new Texture2D[0x49];
        public static bool grabSky = false;
        public static bool grabSun = false;
        public static bool hasFocus = true;
        public static Texture2D heartTexture;
        public static int helpText = 0;
        public static bool hideUI = false;
        public static float[] hotbarScale = new float[] { 1f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f };
        public static bool ignoreErrors = true;
        private string inputText;
        public static bool inputTextEnter = false;
        public static int invasionDelay = 0;
        public static int invasionSize = 0;
        public static int invasionType = 0;
        public static int invasionWarn = 0;
        public static double invasionX = 0.0;
        public static Texture2D inventoryBackTexture;
        private static float inventoryScale = 0.75f;
        public static Item[] item = new Item[0xc9];
        public static Texture2D[] itemTexture = new Texture2D[0xec];
        public static int jungleTiles;
        public static int lastItemUpdate;
        public static int lastNPCUpdate;
        public static float leftWorld = 0f;
        public static bool lightTiles = false;
        public static Liquid[] liquid = new Liquid[Liquid.resLiquid];
        public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[0x2710];
        public static Texture2D[] liquidTexture = new Texture2D[2];
        public static Player[] loadPlayer = new Player[5];
        public static string[] loadPlayerPath = new string[5];
        public static string[] loadWorld = new string[5];
        public static string[] loadWorldPath = new string[5];
        private float logoRotation;
        private float logoRotationDirection = 1f;
        private float logoRotationSpeed = 1f;
        private float logoScale = 1f;
        private float logoScaleDirection = 1f;
        private float logoScaleSpeed = 1f;
        public static Texture2D logoTexture;
        public static int magmaBGFrame = 0;
        public static int magmaBGFrameCounter = 0;
        public static Texture2D manaTexture;
        public const int maxBackgrounds = 7;
        public const int maxChests = 0x3e8;
        public const int maxClouds = 100;
        public const int maxCloudTypes = 4;
        public const int maxCombatText = 100;
        public const int maxDust = 0x7d0;
        public const int maxGore = 200;
        public const int maxGoreTypes = 0x49;
        public const int maxHair = 0x11;
        public const int maxInventory = 0x2c;
        public const int maxItems = 200;
        public const int maxItemSounds = 0x10;
        public const int maxItemTypes = 0xec;
        public static int maxItemUpdates = 10;
        public const int maxLiquidTypes = 2;
        private static int maxMenuItems = 11;
        public const int maxMusic = 7;
        public const int maxNPCHitSounds = 3;
        public const int maxNPCKilledSounds = 3;
        public const int maxNPCs = 0x3e8;
        public const int maxNPCTypes = 0x2c;
        public static int maxNPCUpdates = 15;
        public const int maxPlayers = 8;
        public const int maxProjectiles = 0x3e8;
        public const int maxProjectileTypes = 0x26;
        public static int maxSectionsX = (maxTilesX / 200);
        public static int maxSectionsY = (maxTilesY / 150);
        public const int maxStars = 130;
        public const int maxStarTypes = 5;
        public const int maxTileSets = 80;
        public static int maxTilesX = ((((int) rightWorld) / 0x10) + 1);
        public static int maxTilesY = ((((int) bottomWorld) / 0x10) + 1);
        public const int maxWallTypes = 14;
        private float[] menuItemScale = new float[maxMenuItems];
        public static int menuMode = 0;
        public static bool menuMultiplayer = false;
        public static int meteorTiles;
        private const int MF_BYPOSITION = 0x400;
        public static short moonModY = 0;
        public static int moonPhase = 0;
        public static Texture2D moonTexture;
        public static Color mouseColor = new Color(0xff, 50, 0x5f);
        private static bool mouseExit = false;
        public static Item mouseItem = new Item();
        public static bool mouseLeftRelease = false;
        public static bool mouseRightRelease = false;
        public static byte mouseTextColor = 0;
        public static int mouseTextColorChange = 1;
        public static float[] musicFade = new float[7];
        public static float musicVolume = 0.75f;
        public static int myPlayer = 0;
        public static int netMode = 0;
        public static int netPlayCounter;
        public int newMusic;
        public static string newWorldName = "";
        public const double nightLength = 32400.0;
        public static NPC[] npc = new NPC[0x3e9];
        public static bool npcChatFocus1 = false;
        public static bool npcChatFocus2 = false;
        public static bool npcChatRelease = false;
        public static string npcChatText = "";
        public static int[] npcFrameCount = new int[] { 
            1, 2, 2, 3, 6, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            2, 0x10, 14, 0x10, 14, 14, 0x10, 2, 10, 1, 0x10, 0x10, 0x10, 3, 1, 14, 
            3, 1, 3, 1, 1, 0x10, 0x10, 1, 1, 1, 3, 3
         };
        public static int npcShop = 0;
        public static Texture2D[] npcTexture = new Texture2D[0x2c];
        public const int numArmorBody = 10;
        public const int numArmorHead = 12;
        public const int numArmorLegs = 10;
        public static int numAvailableRecipes;
        public static int numChatLines = 7;
        public static int numClouds = cloudLimit;
        private static int numLoadPlayers = 0;
        private static int numLoadWorlds = 0;
        public static int numStars;
        private string oldInputText;
        public static Player[] player = new Player[9];
        public static Texture2D playerBeltTexture;
        public static Texture2D playerEyesTexture;
        public static Texture2D playerEyeWhitesTexture;
        public static Texture2D[] playerHairTexture = new Texture2D[0x11];
        public static Texture2D playerHands2Texture;
        public static Texture2D playerHandsTexture;
        public static Texture2D playerHeadTexture;
        public static bool playerInventory = false;
        public static Texture2D playerPantsTexture;
        public static string PlayerPath = (SavePath + @"\Players");
        public static string playerPathName;
        public static Texture2D playerShirtTexture;
        public static Texture2D playerShoesTexture;
        public static Texture2D playerUnderShirt2Texture;
        public static Texture2D playerUnderShirtTexture;
        public static Projectile[] projectile = new Projectile[0x3e9];
        public static Texture2D[] projectileTexture = new Texture2D[0x26];
        public static Dictionary<string, string> properties = new Dictionary<string, string>();
        private static bool propertiesLoaded = false;
        [ThreadStatic]
        public static Random rand;
        public static Texture2D raTexture;
        private StreamReader reader; 
        public static Recipe[] recipe = new Recipe[Recipe.maxRecipes];
        public static bool releaseUI = false;
        public static bool resetClouds = true;
        public string resourceFile = "";
        public static Texture2D reTexture;
        public static float rightWorld = 134400f;
        public static double rockLayer;
        public static string SavePath = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\My Games\Terraria");
        public static int saveTimer = 0;
        public static int screenHeight = 600;
        public static screenPos screenLastPosition = new screenPos();
        public static screenPos screenPosition = new screenPos();
        public static int screenWidth = 800;
        public const int sectionHeight = 150;
        public const int sectionWidth = 200;
        private Color selColor = Color.White;
        private int selectedMenu = -1;
        private int selectedPlayer;
        private int selectedWorld;
        private int setKey = -1;
        public Chest[] shop = new Chest[5];
        public static bool showFrameRate = false;
        public static bool showItemOwner = false;
        public static bool showSpam = false;
        public static bool showSplash = true;
        public static Texture2D shroomCapTexture;
        public static Sign[] sign = new Sign[0x3e8];
        public static bool signBubble = false;
        public static string signText = "";
        public static int signX = 0;
        public static int signY = 0;
        public static bool skipMenu = false;
        public static int spawnTileX;
        public static int spawnTileY;
        private int splashCounter;
        public static Texture2D splashTexture;
        public static int stackCounter = 0;
        public static int stackDelay = 7;
        public static int stackSplit;
        public static Star[] star = new Star[130];
        public static Texture2D[] starTexture = new Texture2D[5];
        public static string statusText = "";
        public static bool stopSpawns = false;
        public static bool stopTimeOuts = false;
        public static short sunModY = 0;
        public static Texture2D sunTexture;
        public static Color[] teamColor = new Color[5];
        public static Texture2D teamTexture;
        public static Texture2D textBackTexture;
        private int textBlinkerCount;
        private int textBlinkerState;
        public static Tile[,] tile = new Tile[maxTilesX, maxTilesY];
        public static bool[] tileBlockLight = new bool[0x5b];
        public static Color tileColor;
        public static bool[] tileDungeon = new bool[0x5b];
        public static bool[] tileFrameImportant = new bool[0x5b];
        public static bool[] tileLavaDeath = new bool[0x5b];
        public static bool[] tileNoAttach = new bool[0x5b];
        public static bool[] tileNoFail = new bool[0x5b];
        public static bool tilesLoaded = false;
        public static bool[] tileSolid = new bool[0x5b];
        public static bool[] tileSolidTop = new bool[0x5b];
        public static bool[] tileStone = new bool[0x5b];
        public static bool[] tileTable = new bool[0x5b];
        public static Texture2D[] tileTexture = new Texture2D[0x5b];
        public static bool[] tileWaterDeath = new bool[0x5b];
        public static double time = 13500.0;
        public static int timeOut = 120;
        private static int tmpCounter = 0;
        public bool toggleFullscreen;
        private static Item toolTip = new Item();
        public static float topWorld = 0f;
        public static Texture2D treeBranchTexture;
        public static Texture2D treeTopTexture;
        public static int updateTime = 0;
        public static bool verboseNetplay = false;
        public static string versionNumber = "v1.0.2 tMod";
        public static bool[] wallHouse = new bool[14];
        public static Texture2D[] wallTexture = new Texture2D[14];
        private static bool webAuth = false;
        public static bool webProtect = false;
        public static float windSpeed = 0f;
        public static float windSpeedSpeed = 0f;
        public static int worldID;
        public static string worldName = "";
        public static string WorldPath = (SavePath + @"\Worlds");
        public static string worldPathName;
        public static double worldSurface;

        public Main()
        {
            if (!System.IO.File.Exists("tmod.resource"))
            {
                Console.WriteLine("[ERROR] You do not have tmod.resource in your directory.");
                Environment.Exit(0);
            }
            else
            {
                reader = new StreamReader("tmod.resource"); 
            }
            if (!System.IO.File.Exists("bans.txt"))
            {
                System.IO.File.Create("bans.txt");
            }
            loadProperties();
            while (!propertiesLoaded)
            {
            }
            this.Initialize();
            LoadWorlds();
            worldPathName = loadWorldPath[0];
            netMode = 2;
            showSplash = false;
            WorldGen.serverLoadWorld();
        }

        public static double CalculateDamage(int Damage, int Defense)
        {
            double num = Damage - (Defense * 0.5);
            if (num < 1.0)
            {
                num = 1.0;
            }
            return num;
        }

        public static void CursorColor()
        {
            cursorAlpha += cursorColorDirection * 0.015f;
            if (cursorAlpha >= 1f)
            {
                cursorAlpha = 1f;
                cursorColorDirection = -1;
            }
            if (cursorAlpha <= 0.6)
            {
                cursorAlpha = 0.6f;
                cursorColorDirection = 1;
            }
            float num = (cursorAlpha * 0.3f) + 0.7f;
            byte r = (byte) (mouseColor.R * cursorAlpha);
            byte g = (byte) (mouseColor.G * cursorAlpha);
            byte b = (byte) (mouseColor.B * cursorAlpha);
            byte a = (byte) (255f * num);
            cursorColor = new Color(r, g, b, a);
            cursorScale = ((cursorAlpha * 0.3f) + 0.7f) + 0.1f;
        }

        protected void getAuth()
        {
            try
            {
                string requestUriString = "";
                StringBuilder builder = new StringBuilder();
                byte[] buffer = new byte[0x2000];
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUriString);
                Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
                string str2 = null;
                int count = 0;
                do
                {
                    count = responseStream.Read(buffer, 0, buffer.Length);
                    if (count != 0)
                    {
                        str2 = Encoding.ASCII.GetString(buffer, 0, count);
                        builder.Append(str2);
                    }
                }
                while (count > 0);
                if (builder.ToString() == "")
                {
                    webAuth = true;
                }
            }
            catch
            {
                this.QuitGame();
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern short GetKeyState(int keyCode);
        [DllImport("User32")]
        private static extern int GetMenuItemCount(IntPtr hWnd);
        [DllImport("User32")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        private Texture2D getTextureFromResource()
        {
            string[] strArray = this.reader.ReadLine().Split(new char[] { ',' });
            return new Texture2D(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }

        private static void HelpText()
        {
            bool flag = false;
            if (player[myPlayer].statLifeMax > 100)
            {
                flag = true;
            }
            bool flag2 = false;
            if (player[myPlayer].statManaMax > 0)
            {
                flag2 = true;
            }
            bool flag3 = true;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            for (int i = 0; i < 0x2c; i++)
            {
                if ((player[myPlayer].inventory[i].pick > 0) && (player[myPlayer].inventory[i].name != "Copper Pickaxe"))
                {
                    flag3 = false;
                }
                if ((player[myPlayer].inventory[i].axe > 0) && (player[myPlayer].inventory[i].name != "Copper Axe"))
                {
                    flag3 = false;
                }
                if (player[myPlayer].inventory[i].hammer > 0)
                {
                    flag3 = false;
                }
                if (((player[myPlayer].inventory[i].type == 11) || (player[myPlayer].inventory[i].type == 12)) || ((player[myPlayer].inventory[i].type == 13) || (player[myPlayer].inventory[i].type == 14)))
                {
                    flag4 = true;
                }
                if (((player[myPlayer].inventory[i].type == 0x13) || (player[myPlayer].inventory[i].type == 20)) || ((player[myPlayer].inventory[i].type == 0x15) || (player[myPlayer].inventory[i].type == 0x16)))
                {
                    flag5 = true;
                }
                if (player[myPlayer].inventory[i].type == 0x4b)
                {
                    flag6 = true;
                }
                if (player[myPlayer].inventory[i].type == 0x4b)
                {
                    flag8 = true;
                }
                if ((player[myPlayer].inventory[i].type == 0x44) || (player[myPlayer].inventory[i].type == 70))
                {
                    flag9 = true;
                }
                if (player[myPlayer].inventory[i].type == 0x54)
                {
                    flag10 = true;
                }
                if (player[myPlayer].inventory[i].type == 0x75)
                {
                    flag7 = true;
                }
            }
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            bool flag14 = false;
            for (int j = 0; j < 0x3e8; j++)
            {
                if (npc[j].active)
                {
                    if (npc[j].type == 0x11)
                    {
                        flag11 = true;
                    }
                    if (npc[j].type == 0x12)
                    {
                        flag12 = true;
                    }
                    if (npc[j].type == 0x13)
                    {
                        flag14 = true;
                    }
                    if (npc[j].type == 20)
                    {
                        flag13 = true;
                    }
                }
            }
        Label_03F7:
            helpText++;
            if (flag3)
            {
                if (helpText == 1)
                {
                    npcChatText = "You can use your pickaxe to dig through dirt, and your axe to chop down trees. Just place your cursor over the tile and click!";
                    return;
                }
                if (helpText == 2)
                {
                    npcChatText = "If you want to survive, you will need to create weapons and shelter. Start by chopping down trees and gathering wood.";
                    return;
                }
                if (helpText == 3)
                {
                    npcChatText = "Press ESC to access your crafting menu. When you have enough wood, create a workbench. This will allow you to create more complicated things, as long as you are standing close to it.";
                    return;
                }
                if (helpText == 4)
                {
                    npcChatText = "You can build a shelter by placing wood or other blocks in the world. Don't forget to create and place walls.";
                    return;
                }
                if (helpText == 5)
                {
                    npcChatText = "Once you have a wooden sword, you might try to gather some gel from the slimes. Combine wood and gel to make a torch!";
                    return;
                }
                if (helpText == 6)
                {
                    npcChatText = "To interact with backgrounds and placed objects, use a hammer!";
                    return;
                }
            }
            if ((flag3 && !flag4) && (!flag5 && (helpText == 11)))
            {
                npcChatText = "You should do some mining to find metal ore. You can craft very useful things with it.";
            }
            else
            {
                if ((flag3 && flag4) && !flag5)
                {
                    if (helpText == 0x15)
                    {
                        npcChatText = "Now that you have some ore, you will need to turn it into a bar in order to make items with it. This requires a furnace!";
                        return;
                    }
                    if (helpText == 0x16)
                    {
                        npcChatText = "You can create a furnace out of torches, wood, and stone. Make sure you are standing near a work bench.";
                        return;
                    }
                }
                if (flag3 && flag5)
                {
                    if (helpText == 0x1f)
                    {
                        npcChatText = "You will need an anvil to make most things out of metal bars.";
                        return;
                    }
                    if (helpText == 0x20)
                    {
                        npcChatText = "Anvils can be crafted out of iron, or purchased from a merchant.";
                        return;
                    }
                }
                if (!(flag || (helpText != 0x29)))
                {
                    npcChatText = "Underground are crystal hearts that can be used to increase your max life. You will need a hammer to obtain them.";
                }
                else if (!(flag2 || (helpText != 0x2a)))
                {
                    npcChatText = "If you gather 10 fallen stars, they can be combined to create an item that will increase your magic capacity.";
                }
                else if ((!flag2 && !flag6) && (helpText == 0x2b))
                {
                    npcChatText = "Stars fall all over the world at night. They can be used for all sorts of usefull things. If you see one, be sure to grab it because they disappear after sunrise.";
                }
                else
                {
                    if (!flag11 && !flag12)
                    {
                        if (helpText == 0x33)
                        {
                            npcChatText = "There are many different ways you can attract people to move in to our town. They will of course need a home to live in.";
                            return;
                        }
                        if (helpText == 0x34)
                        {
                            npcChatText = "In order for a room to be considered a home, it needs to have a door, chair, table, and a light source.  Make sure the house has walls as well.";
                            return;
                        }
                        if (helpText == 0x35)
                        {
                            npcChatText = "Two people will not live in the same home. Also, if their home is destroyed, they will look for a new place to live.";
                            return;
                        }
                    }
                    if (!(flag11 || (helpText != 0x3d)))
                    {
                        npcChatText = "If you want a merchant to move in, you will need to gather plenty of money. 50 silver coins should do the trick!";
                    }
                    else if (!(flag12 || (helpText != 0x3e)))
                    {
                        npcChatText = "For a nurse to move in, you might want to increase your maximum life.";
                    }
                    else if (!(flag14 || (helpText != 0x3f)))
                    {
                        npcChatText = "If you had a gun, I bet an arms dealer might show up to sell you some ammo!";
                    }
                    else if (!(flag13 || (helpText != 0x40)))
                    {
                        npcChatText = "You should prove yourself by defeating a strong monster. That will get the attention of a dryad.";
                    }
                    else if (flag8 && (helpText == 0x47))
                    {
                        npcChatText = "If you combine lenses at a demon alter, you might be able to find a way to summon a powerful monster. You will want to wait until night before using it, though.";
                    }
                    else if (flag9 && (helpText == 0x48))
                    {
                        npcChatText = "You can create worm bait with rotten chunks and vile powder. Make sure you are in a corrupt area before using it.";
                    }
                    else if ((flag8 || flag9) && (helpText == 80))
                    {
                        npcChatText = "Demonic alters can usually be found in the corruption. You will need to be near them to craft some items.";
                    }
                    else if (!(flag10 || (helpText != 0xc9)))
                    {
                        npcChatText = "You can make a grappling hook from a hook and 3 chains. Skeletons found deep underground usually carry hooks, and chains can be made from iron bars.";
                    }
                    else if (flag7 && (helpText == 0xca))
                    {
                        npcChatText = "You can craft a space gun using a flintlock pistol, 10 fallen stars, and 30 meteorite bars.";
                    }
                    else if (helpText == 0x3e8)
                    {
                        npcChatText = "If you see a pot, be sure to smash it open. They contain all sorts of useful supplies.";
                    }
                    else if (helpText == 0x3e9)
                    {
                        npcChatText = "There is treasure hidden all over the world. Some amazing things can be found deep underground!";
                    }
                    else if (helpText == 0x3ea)
                    {
                        npcChatText = "Smashing a shadow orb will cause a meteor to fall out of the sky. Shadow orbs can usually be found in the chasms around corrupt areas.";
                    }
                    else
                    {
                        if (helpText > 0x44c)
                        {
                            helpText = 0;
                        }
                        goto Label_03F7;
                    }
                }
            }
        }

        protected void Initialize()
        {
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            if (webProtect)
            {
                this.getAuth();
                while (!webAuth)
                {
                }
            }
            if (rand == null)
            {
                rand = new Random((int) DateTime.Now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                WorldGen.genRand = new Random((int) DateTime.Now.Ticks);
            }
            this.OpenSettings();
            switch (rand.Next(5))
            {
            }
            tileSolid[0] = true;
            tileBlockLight[0] = true;
            tileSolid[1] = true;
            tileBlockLight[1] = true;
            tileSolid[2] = true;
            tileBlockLight[2] = true;
            tileSolid[3] = false;
            tileNoAttach[3] = true;
            tileNoFail[3] = true;
            tileSolid[4] = false;
            tileNoAttach[4] = true;
            tileNoFail[4] = true;
            tileNoFail[0x18] = true;
            tileSolid[5] = false;
            tileSolid[6] = true;
            tileBlockLight[6] = true;
            tileSolid[7] = true;
            tileBlockLight[7] = true;
            tileSolid[8] = true;
            tileBlockLight[8] = true;
            tileSolid[9] = true;
            tileBlockLight[9] = true;
            tileBlockLight[10] = true;
            tileSolid[10] = true;
            tileNoAttach[10] = true;
            tileBlockLight[10] = true;
            tileSolid[11] = false;
            tileSolidTop[0x13] = true;
            tileSolid[0x13] = true;
            tileSolid[0x16] = true;
            tileSolid[0x17] = true;
            tileSolid[0x19] = true;
            tileSolid[30] = true;
            tileNoFail[0x20] = true;
            tileBlockLight[0x20] = true;
            tileSolid[0x25] = true;
            tileBlockLight[0x25] = true;
            tileSolid[0x26] = true;
            tileBlockLight[0x26] = true;
            tileSolid[0x27] = true;
            tileBlockLight[0x27] = true;
            tileSolid[40] = true;
            tileBlockLight[40] = true;
            tileSolid[0x29] = true;
            tileBlockLight[0x29] = true;
            tileSolid[0x2b] = true;
            tileBlockLight[0x2b] = true;
            tileSolid[0x2c] = true;
            tileBlockLight[0x2c] = true;
            tileSolid[0x2d] = true;
            tileBlockLight[0x2d] = true;
            tileSolid[0x2e] = true;
            tileBlockLight[0x2e] = true;
            tileSolid[0x2f] = true;
            tileBlockLight[0x2f] = true;
            tileSolid[0x30] = true;
            tileBlockLight[0x30] = true;
            tileSolid[0x35] = true;
            tileBlockLight[0x35] = true;
            tileSolid[0x36] = true;
            tileBlockLight[0x34] = true;
            tileSolid[0x38] = true;
            tileBlockLight[0x38] = true;
            tileSolid[0x39] = true;
            tileBlockLight[0x39] = true;
            tileSolid[0x3a] = true;
            tileBlockLight[0x3a] = true;
            tileSolid[0x3b] = true;
            tileBlockLight[0x3b] = true;
            tileSolid[60] = true;
            tileBlockLight[60] = true;
            tileSolid[0x3f] = true;
            tileBlockLight[0x3f] = true;
            tileStone[0x3f] = true;
            tileSolid[0x40] = true;
            tileBlockLight[0x40] = true;
            tileStone[0x40] = true;
            tileSolid[0x41] = true;
            tileBlockLight[0x41] = true;
            tileStone[0x41] = true;
            tileSolid[0x42] = true;
            tileBlockLight[0x42] = true;
            tileStone[0x42] = true;
            tileSolid[0x43] = true;
            tileBlockLight[0x43] = true;
            tileStone[0x43] = true;
            tileSolid[0x44] = true;
            tileBlockLight[0x44] = true;
            tileStone[0x44] = true;
            tileSolid[0x4b] = true;
            tileBlockLight[0x4b] = true;
            tileSolid[0x4c] = true;
            tileBlockLight[0x4c] = true;
            tileSolid[70] = true;
            tileBlockLight[70] = true;
            tileBlockLight[0x33] = true;
            tileNoFail[50] = true;
            tileNoAttach[50] = true;
            tileDungeon[0x29] = true;
            tileDungeon[0x2b] = true;
            tileDungeon[0x2c] = true;
            tileBlockLight[30] = true;
            tileBlockLight[0x19] = true;
            tileBlockLight[0x17] = true;
            tileBlockLight[0x16] = true;
            tileBlockLight[0x3e] = true;
            tileSolidTop[0x12] = true;
            tileSolidTop[14] = true;
            tileSolidTop[0x10] = true;
            tileNoAttach[20] = true;
            tileNoAttach[0x13] = true;
            tileNoAttach[13] = true;
            tileNoAttach[14] = true;
            tileNoAttach[15] = true;
            tileNoAttach[0x10] = true;
            tileNoAttach[0x11] = true;
            tileNoAttach[0x12] = true;
            tileNoAttach[0x13] = true;
            tileNoAttach[0x15] = true;
            tileNoAttach[0x1b] = true;
            tileFrameImportant[3] = true;
            tileFrameImportant[5] = true;
            tileFrameImportant[10] = true;
            tileFrameImportant[11] = true;
            tileFrameImportant[12] = true;
            tileFrameImportant[13] = true;
            tileFrameImportant[14] = true;
            tileFrameImportant[15] = true;
            tileFrameImportant[0x10] = true;
            tileFrameImportant[0x11] = true;
            tileFrameImportant[0x12] = true;
            tileFrameImportant[20] = true;
            tileFrameImportant[0x15] = true;
            tileFrameImportant[0x18] = true;
            tileFrameImportant[0x1a] = true;
            tileFrameImportant[0x1b] = true;
            tileFrameImportant[0x1c] = true;
            tileFrameImportant[0x1d] = true;
            tileFrameImportant[0x1f] = true;
            tileFrameImportant[0x21] = true;
            tileFrameImportant[0x22] = true;
            tileFrameImportant[0x23] = true;
            tileFrameImportant[0x24] = true;
            tileFrameImportant[0x2a] = true;
            tileFrameImportant[50] = true;
            tileFrameImportant[0x37] = true;
            tileFrameImportant[0x3d] = true;
            tileFrameImportant[0x47] = true;
            tileFrameImportant[0x48] = true;
            tileFrameImportant[0x49] = true;
            tileFrameImportant[0x4a] = true;
            tileFrameImportant[0x4d] = true;
            tileFrameImportant[0x4e] = true;
            tileFrameImportant[0x4f] = true;
            tileTable[14] = true;
            tileTable[0x12] = true;
            tileTable[0x13] = true;
            tileWaterDeath[4] = true;
            tileWaterDeath[0x33] = true;
            tileLavaDeath[3] = true;
            tileLavaDeath[5] = true;
            tileLavaDeath[10] = true;
            tileLavaDeath[11] = true;
            tileLavaDeath[12] = true;
            tileLavaDeath[13] = true;
            tileLavaDeath[14] = true;
            tileLavaDeath[15] = true;
            tileLavaDeath[0x10] = true;
            tileLavaDeath[0x11] = true;
            tileLavaDeath[0x12] = true;
            tileLavaDeath[0x13] = true;
            tileLavaDeath[20] = true;
            tileLavaDeath[0x1b] = true;
            tileLavaDeath[0x1c] = true;
            tileLavaDeath[0x1d] = true;
            tileLavaDeath[0x20] = true;
            tileLavaDeath[0x21] = true;
            tileLavaDeath[0x22] = true;
            tileLavaDeath[0x23] = true;
            tileLavaDeath[0x24] = true;
            tileLavaDeath[0x2a] = true;
            tileLavaDeath[0x31] = true;
            tileLavaDeath[50] = true;
            tileLavaDeath[0x34] = true;
            tileLavaDeath[0x37] = true;
            tileLavaDeath[0x3d] = true;
            tileLavaDeath[0x3e] = true;
            tileLavaDeath[0x45] = true;
            tileLavaDeath[0x47] = true;
            tileLavaDeath[0x48] = true;
            tileLavaDeath[0x49] = true;
            tileLavaDeath[0x4a] = true;
            tileLavaDeath[0x4e] = true;
            tileLavaDeath[0x4f] = true;
            wallHouse[1] = true;
            wallHouse[4] = true;
            wallHouse[5] = true;
            wallHouse[6] = true;
            wallHouse[10] = true;
            wallHouse[11] = true;
            wallHouse[12] = true;
            for (int i = 0; i < maxMenuItems; i++)
            {
                this.menuItemScale[i] = 0.8f;
            }
            for (num2 = 0; num2 < 0x7d0; num2++)
            {
                dust[num2] = new Dust();
            }
            for (num3 = 0; num3 < 0xc9; num3++)
            {
                item[num3] = new Item();
            }
            for (num4 = 0; num4 < 0x3e9; num4++)
            {
                npc[num4] = new NPC();
                npc[num4].whoAmI = num4;
            }
            for (num5 = 0; num5 < 9; num5++)
            {
                player[num5] = new Player();
            }
            for (num6 = 0; num6 < 0x3e9; num6++)
            {
                projectile[num6] = new Projectile();
            }
            for (num7 = 0; num7 < 0xc9; num7++)
            {
                gore[num7] = new Gore();
            }
            for (num8 = 0; num8 < 100; num8++)
            {
                cloud[num8] = new Cloud();
            }
            for (num9 = 0; num9 < 100; num9++)
            {
                combatText[num9] = new CombatText();
            }
            for (num10 = 0; num10 < Recipe.maxRecipes; num10++)
            {
                recipe[num10] = new Recipe();
                availableRecipeY[num10] = 0x41 * num10;
            }
            Recipe.SetupRecipes();
            for (int j = 0; j < numChatLines; j++)
            {
                chatLine[j] = new ChatLine();
            }
            for (int k = 0; k < Liquid.resLiquid; k++)
            {
                liquid[k] = new Liquid();
            }
            for (int m = 0; m < 0x2710; m++)
            {
                liquidBuffer[m] = new LiquidBuffer();
            }
            this.shop[0] = new Chest();
            this.shop[1] = new Chest();
            this.shop[1].SetupShop(1);
            this.shop[2] = new Chest();
            this.shop[2].SetupShop(2);
            this.shop[3] = new Chest();
            this.shop[3].SetupShop(3);
            this.shop[4] = new Chest();
            this.shop[4].SetupShop(4);
            teamColor[0] = Color.White;
            teamColor[1] = new Color(230, 40, 20);
            teamColor[2] = new Color(20, 200, 30);
            teamColor[3] = new Color(0x4b, 90, 0xff);
            teamColor[4] = new Color(200, 180, 0);
            Netplay.Init();
            if (skipMenu)
            {
                WorldGen.clearWorld();
                gameMenu = false;
                LoadPlayers();
                player[myPlayer] = (Player) loadPlayer[0].Clone();
                PlayerPath = loadPlayerPath[0];
                LoadWorlds();
                WorldGen.generateWorld(-1);
                WorldGen.EveryTileFrame();
                player[myPlayer].Spawn();
            }
            if (this.resourceFile == "")
            {
                for (num2 = 0; num2 < 80; num2++)
                {
                    tileTexture[num2] = this.getTextureFromResource();
                }
                for (num3 = 1; num3 < 14; num3++)
                {
                    wallTexture[num3] = this.getTextureFromResource();
                }
                for (num4 = 0; num4 < 7; num4++)
                {
                    backgroundTexture[num4] = this.getTextureFromResource();
                    backgroundWidth[num4] = backgroundTexture[num4].Width;
                    backgroundHeight[num4] = backgroundTexture[num4].Height;
                }
                for (num5 = 0; num5 < 0xec; num5++)
                {
                    itemTexture[num5] = this.getTextureFromResource();
                }
                for (int n = 0; n < 0x2c; n++)
                {
                    npcTexture[n] = this.getTextureFromResource();
                }
                for (num6 = 0; num6 < 0x26; num6++)
                {
                    projectileTexture[num6] = this.getTextureFromResource();
                }
                for (num7 = 1; num7 < 0x49; num7++)
                {
                    goreTexture[num7] = this.getTextureFromResource();
                }
                for (num8 = 0; num8 < 4; num8++)
                {
                    cloudTexture[num8] = this.getTextureFromResource();
                }
                for (num9 = 0; num9 < 5; num9++)
                {
                    starTexture[num9] = this.getTextureFromResource();
                }
                for (num10 = 0; num10 < 2; num10++)
                {
                    liquidTexture[num10] = this.getTextureFromResource();
                }
                this.reader.Close();
            }
            Star.SpawnStars();
        }

        private static void InvasionWarning()
        {
            if (invasionType != 0)
            {
                string newText = "";
                if (invasionSize <= 0)
                {
                    newText = "The goblin army has been defeated!";
                }
                else if (invasionX < spawnTileX)
                {
                    newText = "A goblin army is approaching from the west!";
                }
                else if (invasionX > spawnTileX)
                {
                    newText = "A goblin army is approaching from the east!";
                }
                else
                {
                    newText = "The goblin army has arrived!";
                }
                if (netMode == 0)
                {
                    NewText(newText, 0xaf, 0x4b, 0xff);
                }
                else if (netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, newText, 8, 175f, 75f, 255f);
                }
            }
        }

        private static void LoadPlayers()
        {
            Directory.CreateDirectory(PlayerPath);
            string[] files = Directory.GetFiles(PlayerPath, "*.plr");
            int length = files.Length;
            if (length > 5)
            {
                length = 5;
            }
            for (int i = 0; i < 5; i++)
            {
                loadPlayer[i] = new Player();
                if (i < length)
                {
                    loadPlayerPath[i] = files[i];
                    loadPlayer[i] = Player.LoadPlayer(loadPlayerPath[i]);
                }
            }
            numLoadPlayers = length;
        }

        public static void loadProperties()
        {
            try
            {
                if (!System.IO.File.Exists("server.properties"))
                {
                    StreamWriter writer = System.IO.File.CreateText("server.properties");
                    writer.Write("giveEnabled=true" + Environment.NewLine + "opPassword=123" + Environment.NewLine + "tpEnabled=true" + Environment.NewLine + "homeEnabled=true" + Environment.NewLine + "welcomeMessage=Welcome!" + Environment.NewLine + "serverPassword=");
                    writer.Close();
                }
                foreach (string str in System.IO.File.ReadAllLines("server.properties"))
                {
                    if ((str.IndexOf('=') > -1) && (str.Substring(0, 1) != "#"))
                    {
                        string[] strArray = str.Split(new char[] { '=' });
                        if ((strArray[0] != "") && (strArray[1] != ""))
                        {
                            properties.Add(strArray[0], strArray[1]);
                        }
                        else if (strArray[0] != "")
                        {
                            properties.Add(strArray[0], "");
                        }
                    }
                }
                bool flag = false;
                Console.WriteLine("Loaded properties file!");
                if (!properties.ContainsKey("welcomeMessage"))
                {
                    properties.Add("welcomeMessage", "Welcome!");
                    Console.WriteLine("PROPERTY: welcomeMessage MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!properties.ContainsKey("serverPassword"))
                {
                    properties.Add("serverPassword", "");
                    Console.WriteLine("PROPERTY: serverPassword MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!properties.ContainsKey("maxPlayers"))
                {
                    properties.Add("maxPlayers", "8");
                    Console.WriteLine("PROPERTY: maxPlayers MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (properties["opPassword"] == "123")
                {
                    Console.WriteLine("OPERATOR PASSWORD IS SET TO THE DEFAULT VALUE OF 123, PLEASE CHANGE IT IN YOUR SERVER.PROPERTIES!");
                    flag = true;
                }
                if (properties["serverPassword"] != "")
                {
                    Netplay.password = properties["serverPassword"];
                    Console.WriteLine("Password set to: " + Netplay.password);
                }
                int result = 8;
                if (!int.TryParse(properties["maxPlayers"], out result))
                {
                    result = 8;
                }
                Netplay.maxConnections = result + 1;
                Netplay.serverSock = new ServerSock[result + 1];
                Console.WriteLine("Max players set to: " + result);
                propertiesLoaded = true;
                if (flag)
                {
                    Console.Beep();
                    Console.WriteLine("There were warnings whilst loading the properties file. Please read them. Resuming in 5 seconds!");
                    Console.WriteLine("To quicky add new properties: delete your server.properties! This will LOSE your configuration!");
                    Thread.Sleep(0x1388);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Error loading server.properties...");
            }
        }

        public static void LoadWorlds()
        {
            Directory.CreateDirectory(WorldPath);
            string[] files = Directory.GetFiles(WorldPath, "*.wld");
            int length = files.Length;
            if (length > 5)
            {
                length = 5;
            }
            for (int i = 0; i < length; i++)
            {
                loadWorldPath[i] = files[i];
                using (FileStream stream = new FileStream(loadWorldPath[i], FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        Console.WriteLine(loadWorldPath[i]);
                        reader.ReadInt32();
                        loadWorld[i] = reader.ReadString();
                        reader.Close();
                    }
                }
            }
            numLoadWorlds = length;
        }

        protected void MouseText(string cursorText, int rare = 0)
        {
            float num;
            int num2 = 12;
            int num3 = 12;
            Color color = new Color((int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor);
            if (toolTip.type > 0)
            {
                rare = toolTip.rare;
                int num4 = 20;
                int index = 1;
                string[] strArray = new string[num4];
                strArray[0] = toolTip.name;
                if (toolTip.stack > 1)
                {
                    string[] strArray2 = new string[1];
                    object obj2 = strArray2[0];
                    (strArray2 = strArray)[0] = string.Concat(new object[] { obj2, " (", toolTip.stack, ")" });
                }
                if (toolTip.damage > 0)
                {
                    strArray[index] = toolTip.damage + " damage";
                    index++;
                    if (toolTip.useStyle > 0)
                    {
                        string[] strArray3;
                        IntPtr ptr;
                        if (toolTip.useAnimation <= 8)
                        {
                            strArray[index] = "Insanely fast";
                        }
                        else if (toolTip.useAnimation <= 15)
                        {
                            strArray[index] = "Very fast";
                        }
                        else if (toolTip.useAnimation <= 20)
                        {
                            strArray[index] = "Fast";
                        }
                        else if (toolTip.useAnimation <= 0x19)
                        {
                            strArray[index] = "Average";
                        }
                        else if (toolTip.useAnimation <= 30)
                        {
                            strArray[index] = "Slow";
                        }
                        else if (toolTip.useAnimation <= 40)
                        {
                            strArray[index] = "Very slow";
                        }
                        else if (toolTip.useAnimation <= 50)
                        {
                            strArray[index] = "Extremly slow";
                        }
                        else
                        {
                            strArray[index] = "Snail";
                        }
                        (strArray3 = strArray)[(int) (ptr = (IntPtr) index)] = strArray3[(int) ptr] + " speed";
                        index++;
                    }
                }
                if (((toolTip.headSlot > 0) || (toolTip.bodySlot > 0)) || ((toolTip.legSlot > 0) || toolTip.accessory))
                {
                    strArray[index] = "Equipable";
                    index++;
                }
                if (toolTip.defense > 0)
                {
                    strArray[index] = toolTip.defense + " defense";
                    index++;
                }
                if (toolTip.pick > 0)
                {
                    strArray[index] = toolTip.pick + "% pickaxe power";
                    index++;
                }
                if (toolTip.axe > 0)
                {
                    strArray[index] = toolTip.axe + "% axe power";
                    index++;
                }
                if (toolTip.hammer > 0)
                {
                    strArray[index] = toolTip.hammer + "% hammer power";
                    index++;
                }
                if (toolTip.healLife > 0)
                {
                    strArray[index] = "Restores " + toolTip.healLife + " life";
                    index++;
                }
                if (toolTip.healMana > 0)
                {
                    strArray[index] = "Restores " + toolTip.healMana + " mana";
                    index++;
                }
                if (toolTip.mana > 0)
                {
                    strArray[index] = "Uses " + ((int) (toolTip.mana * player[myPlayer].manaCost)) + " mana";
                    index++;
                }
                if (((toolTip.createWall > 0) || (toolTip.createTile > -1)) && (toolTip.type != 0xd5))
                {
                    strArray[index] = "Can be placed";
                    index++;
                }
                if (toolTip.consumable)
                {
                    strArray[index] = "Consumable";
                    index++;
                }
                if (toolTip.toolTip != null)
                {
                    strArray[index] = toolTip.toolTip;
                    index++;
                }
                if (toolTip.wornArmor && (player[myPlayer].setBonus != ""))
                {
                    strArray[index] = "Set bonus: " + player[myPlayer].setBonus;
                    index++;
                }
                if (npcShop > 0)
                {
                    if (toolTip.value > 0)
                    {
                        string str = "";
                        int num6 = 0;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = toolTip.value * toolTip.stack;
                        if (!toolTip.buy)
                        {
                            num10 /= 5;
                        }
                        if (num10 < 1)
                        {
                            num10 = 1;
                        }
                        if (num10 >= 0xf4240)
                        {
                            num6 = num10 / 0xf4240;
                            num10 -= num6 * 0xf4240;
                        }
                        if (num10 >= 0x2710)
                        {
                            num7 = num10 / 0x2710;
                            num10 -= num7 * 0x2710;
                        }
                        if (num10 >= 100)
                        {
                            num8 = num10 / 100;
                            num10 -= num8 * 100;
                        }
                        if (num10 >= 1)
                        {
                            num9 = num10;
                        }
                        if (num6 > 0)
                        {
                            str = str + num6 + " platinum ";
                        }
                        if (num7 > 0)
                        {
                            str = str + num7 + " gold ";
                        }
                        if (num8 > 0)
                        {
                            str = str + num8 + " silver ";
                        }
                        if (num9 > 0)
                        {
                            str = str + num9 + " copper ";
                        }
                        if (!toolTip.buy)
                        {
                            strArray[index] = "Sell price: " + str;
                        }
                        else
                        {
                            strArray[index] = "Buy price: " + str;
                        }
                        index++;
                        num = ((float) mouseTextColor) / 255f;
                        if (num6 > 0)
                        {
                            color = new Color((int) ((byte) (220f * num)), (int) ((byte) (220f * num)), (int) ((byte) (198f * num)), (int) mouseTextColor);
                        }
                        else if (num7 > 0)
                        {
                            color = new Color((int) ((byte) (224f * num)), (int) ((byte) (201f * num)), (int) ((byte) (92f * num)), (int) mouseTextColor);
                        }
                        else if (num8 > 0)
                        {
                            color = new Color((int) ((byte) (181f * num)), (int) ((byte) (192f * num)), (int) ((byte) (193f * num)), (int) mouseTextColor);
                        }
                        else if (num9 > 0)
                        {
                            color = new Color((int) ((byte) (246f * num)), (int) ((byte) (138f * num)), (int) ((byte) (96f * num)), (int) mouseTextColor);
                        }
                    }
                    else
                    {
                        num = ((float) mouseTextColor) / 255f;
                        strArray[index] = "No value";
                        index++;
                        color = new Color((int) ((byte) (120f * num)), (int) ((byte) (120f * num)), (int) ((byte) (120f * num)), (int) mouseTextColor);
                    }
                }
                Vector2 vector = new Vector2();
                int num11 = 0;
                for (int i = 0; i < index; i++)
                {
                    Vector2 vector2 = fontMouseText.MeasureString(strArray[i]);
                    if (vector2.X > vector.X)
                    {
                        vector.X = vector2.X;
                    }
                    vector.Y += vector2.Y + num11;
                }
                if (((num2 + vector.X) + 4f) > screenWidth)
                {
                    num2 = (int) ((screenWidth - vector.X) - 4f);
                }
                if (((num3 + vector.Y) + 4f) > screenHeight)
                {
                    num3 = (int) ((screenHeight - vector.Y) - 4f);
                }
                int num13 = 0;
                num = ((float) mouseTextColor) / 255f;
                for (int j = 0; j < index; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        int num16 = num2;
                        int num17 = num3 + num13;
                        Color black = Color.Black;
                        switch (k)
                        {
                            case 0:
                                num16 -= 2;
                                break;

                            case 1:
                                num16 += 2;
                                break;

                            case 2:
                                num17 -= 2;
                                break;

                            case 3:
                                num17 += 2;
                                break;

                            default:
                                black = new Color((int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor);
                                if (j == 0)
                                {
                                    if (rare == 1)
                                    {
                                        black = new Color((int) ((byte) (150f * num)), (int) ((byte) (150f * num)), (int) ((byte) (255f * num)), (int) mouseTextColor);
                                    }
                                    if (rare == 2)
                                    {
                                        black = new Color((int) ((byte) (150f * num)), (int) ((byte) (255f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                                    }
                                    if (rare == 3)
                                    {
                                        black = new Color((int) ((byte) (255f * num)), (int) ((byte) (200f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                                    }
                                    if (rare == 4)
                                    {
                                        black = new Color((int) ((byte) (255f * num)), (int) ((byte) (150f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                                    }
                                }
                                else if (j == (index - 1))
                                {
                                    black = color;
                                }
                                break;
                        }
                        Vector2 vector3 = new Vector2();
                    }
                    num13 += ((int) fontMouseText.MeasureString(strArray[j]).Y) + num11;
                }
            }
            else
            {
                Vector2 vector4 = fontMouseText.MeasureString(cursorText);
                if (((num2 + vector4.X) + 4f) > screenWidth)
                {
                    num2 = (int) ((screenWidth - vector4.X) - 4f);
                }
                if (((num3 + vector4.Y) + 4f) > screenHeight)
                {
                    num3 = (int) ((screenHeight - vector4.Y) - 4f);
                }
                num = ((float) mouseTextColor) / 255f;
                Color color3 = new Color((int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor, (int) mouseTextColor);
                if (rare == 1)
                {
                    color3 = new Color((int) ((byte) (150f * num)), (int) ((byte) (150f * num)), (int) ((byte) (255f * num)), (int) mouseTextColor);
                }
                if (rare == 2)
                {
                    color3 = new Color((int) ((byte) (150f * num)), (int) ((byte) (255f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                }
                if (rare == 3)
                {
                    color3 = new Color((int) ((byte) (255f * num)), (int) ((byte) (200f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                }
                if (rare == 4)
                {
                    color3 = new Color((int) ((byte) (255f * num)), (int) ((byte) (150f * num)), (int) ((byte) (150f * num)), (int) mouseTextColor);
                }
            }
        }

        public static void NewText(string newText, byte R = 0xff, byte G = 0xff, byte B = 0xff)
        {
            for (int i = numChatLines - 1; i > 0; i--)
            {
                chatLine[i].text = chatLine[i - 1].text;
                chatLine[i].showTime = chatLine[i - 1].showTime;
                chatLine[i].color = chatLine[i - 1].color;
            }
            if (((R == 0) && (G == 0)) && (B == 0))
            {
                chatLine[0].color = Color.White;
            }
            else
            {
                chatLine[0].color = new Color(R, G, B);
            }
            chatLine[0].text = newText;
            chatLine[0].showTime = chatLength;
        }

        private static string nextLoadPlayer()
        {
            int num = 1;
            while (true)
            {
                if (!System.IO.File.Exists(string.Concat(new object[] { PlayerPath, @"\player", num, ".plr" })))
                {
                    break;
                }
                num++;
            }
            return string.Concat(new object[] { PlayerPath, @"\player", num, ".plr" });
        }

        private static string nextLoadWorld()
        {
            int num = 1;
            while (true)
            {
                if (!System.IO.File.Exists(string.Concat(new object[] { WorldPath, @"\world", num, ".wld" })))
                {
                    break;
                }
                num++;
            }
            return string.Concat(new object[] { WorldPath, @"\world", num, ".wld" });
        }

        protected void OpenSettings()
        {
            try
            {
                if (System.IO.File.Exists(SavePath + @"\config.dat"))
                {
                    using (FileStream stream = new FileStream(SavePath + @"\config.dat", FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int num = reader.ReadInt32();
                            bool flag = reader.ReadBoolean();
                            mouseColor.R = reader.ReadByte();
                            mouseColor.G = reader.ReadByte();
                            mouseColor.B = reader.ReadByte();
                            musicVolume = reader.ReadSingle();
                            cUp = reader.ReadString();
                            cDown = reader.ReadString();
                            cLeft = reader.ReadString();
                            cRight = reader.ReadString();
                            cJump = reader.ReadString();
                            cThrowItem = reader.ReadString();
                            if (num >= 1)
                            {
                                cInv = reader.ReadString();
                            }
                            caveParrallax = reader.ReadSingle();
                            if (num >= 2)
                            {
                                fixedTiming = reader.ReadBoolean();
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void PlaySound(int a, int b, int c, int d)
        {
        }

        protected void QuitGame()
        {
            Steam.Kill();
        }

        [DllImport("User32")]
        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        protected void SaveSettings()
        {
            Directory.CreateDirectory(SavePath);
            try
            {
                System.IO.File.SetAttributes(SavePath + @"\config.dat", FileAttributes.Normal);
            }
            catch
            {
            }
            try
            {
                using (FileStream stream = new FileStream(SavePath + @"\config.dat", FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(curRelease);
                        writer.Write(false);
                        writer.Write(mouseColor.R);
                        writer.Write(mouseColor.G);
                        writer.Write(mouseColor.B);
                        writer.Write(2);
                        writer.Write(musicVolume);
                        writer.Write(cUp);
                        writer.Write(cDown);
                        writer.Write(cLeft);
                        writer.Write(cRight);
                        writer.Write(cJump);
                        writer.Write(cThrowItem);
                        writer.Write(cInv);
                        writer.Write(caveParrallax);
                        writer.Write(fixedTiming);
                        writer.Close();
                    }
                }
            }
            catch
            {
            }
        }

        private static void StartInvasion()
        {
            if (WorldGen.shadowOrbSmashed && ((invasionType == 0) && (invasionDelay == 0)))
            {
                int num = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (player[i].active && (player[i].statLife >= 200))
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    invasionType = 1;
                    invasionSize = 100 + (50 * num);
                    invasionWarn = 0;
                    if (rand.Next(2) == 0)
                    {
                        invasionX = 0.0;
                    }
                    else
                    {
                        invasionX = maxTilesX;
                    }
                }
            }
        }

        protected void UnloadContent()
        {
        }

        public void Update()
        {
            bool flag;
            if (fixedTiming)
            {
                flag = true;
            }
            if (!showSplash)
            {
                if (!gameMenu && (Main.netMode != 2))
                {
                    saveTimer++;
                    if (saveTimer > 0x4650)
                    {
                        saveTimer = 0;
                        WorldGen.saveToonWhilePlaying();
                    }
                }
                else
                {
                    saveTimer = 0;
                }
                if ((rand == null) || (rand.Next(0x1869f) == 0))
                {
                    rand = new Random((int) DateTime.Now.Ticks);
                }
                updateTime++;
                if (updateTime >= 60)
                {
                    frameRate = drawTime;
                    updateTime = 0;
                    drawTime = 0;
                    if (frameRate == 60)
                    {
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 0;
                        cloudLimit = 100;
                        Gore.goreTime = 0x4b0;
                    }
                    else if (frameRate >= 0x3a)
                    {
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 0;
                        cloudLimit = 100;
                        Gore.goreTime = 600;
                    }
                    else if (frameRate >= 0x2b)
                    {
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 1;
                        cloudLimit = 0x4b;
                        Gore.goreTime = 300;
                    }
                    else if (frameRate >= 0x1c)
                    {
                        if (!gameMenu)
                        {
                            Liquid.maxLiquid = 0xbb8;
                            Liquid.cycles = 6;
                        }
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 2;
                        cloudLimit = 50;
                        Gore.goreTime = 180;
                    }
                    else
                    {
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 4;
                        cloudLimit = 0;
                        Gore.goreTime = 0;
                    }
                    if (Liquid.quickSettle)
                    {
                        Liquid.maxLiquid = Liquid.resLiquid;
                        Liquid.cycles = 1;
                    }
                    else if (frameRate == 60)
                    {
                        Liquid.maxLiquid = 0x1388;
                        Liquid.cycles = 7;
                    }
                    else if (frameRate >= 0x3a)
                    {
                        Liquid.maxLiquid = 0x1388;
                        Liquid.cycles = 12;
                    }
                    else if (frameRate >= 0x2b)
                    {
                        Liquid.maxLiquid = 0xfa0;
                        Liquid.cycles = 13;
                    }
                    else if (frameRate >= 0x1c)
                    {
                        Liquid.maxLiquid = 0xdac;
                        Liquid.cycles = 15;
                    }
                    else
                    {
                        Liquid.maxLiquid = 0xbb8;
                        Liquid.cycles = 0x11;
                    }
                    if (Main.netMode == 2)
                    {
                        cloudLimit = 0;
                    }
                }
                hasFocus = false;
                flag = false;
                if (!chatMode && !editSign)
                {
                    if (frameRelease)
                    {
                        if (showFrameRate)
                        {
                            showFrameRate = false;
                        }
                        else
                        {
                            showFrameRate = true;
                        }
                    }
                    frameRelease = false;
                }
                else
                {
                    frameRelease = true;
                }
                releaseUI = true;
                if (editSign)
                {
                    chatMode = false;
                }
                if (chatMode)
                {
                    string chatText = Main.chatText;
                    while (fontMouseText.MeasureString(Main.chatText).X > 470f)
                    {
                        Main.chatText = Main.chatText.Substring(0, Main.chatText.Length - 1);
                    }
                    if (chatText != Main.chatText)
                    {
                    }
                    if (inputTextEnter && chatRelease)
                    {
                        if (Main.chatText != "")
                        {
                            NetMessage.SendData(0x19, -1, -1, Main.chatText, myPlayer, 0f, 0f, 0f);
                        }
                        Main.chatText = "";
                        chatMode = false;
                        chatRelease = false;
                    }
                }
                chatRelease = true;
                for (int i = 0; i < 8; i++)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            player[i].UpdatePlayer(i);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        player[i].UpdatePlayer(i);
                    }
                }
                if (Main.netMode != 1)
                {
                    NPC.SpawnNPC();
                }
                for (int j = 0; j < 8; j++)
                {
                    player[j].activeNPCs = 0;
                    player[j].townNPCs = 0;
                }
                for (int k = 0; k < 0x3e8; k++)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            npc[k].UpdateNPC(k);
                        }
                        catch
                        {
                            npc[k] = new NPC();
                        }
                    }
                    else
                    {
                        npc[k].UpdateNPC(k);
                    }
                }
                for (int m = 0; m < 200; m++)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            gore[m].Update();
                        }
                        catch
                        {
                            gore[m] = new Gore();
                        }
                    }
                    else
                    {
                        gore[m].Update();
                    }
                }
                for (int n = 0; n < 0x3e8; n++)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            projectile[n].Update(n);
                        }
                        catch
                        {
                            projectile[n] = new Projectile();
                        }
                    }
                    else
                    {
                        projectile[n].Update(n);
                    }
                }
                for (int num6 = 0; num6 < 200; num6++)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            item[num6].UpdateItem(num6);
                        }
                        catch
                        {
                            item[num6] = new Item();
                        }
                    }
                    else
                    {
                        item[num6].UpdateItem(num6);
                    }
                }
                if (ignoreErrors)
                {
                    try
                    {
                        Dust.UpdateDust();
                    }
                    catch
                    {
                        for (int num7 = 0; num7 < 0x7d0; num7++)
                        {
                            dust[num7] = new Dust();
                        }
                    }
                }
                else
                {
                    Dust.UpdateDust();
                }
                if (Main.netMode != 2)
                {
                    CombatText.UpdateCombatText();
                }
                if (ignoreErrors)
                {
                    try
                    {
                        UpdateTime();
                    }
                    catch
                    {
                        checkForSpawns = 0;
                    }
                }
                else
                {
                    UpdateTime();
                }
                if (Main.netMode != 1)
                {
                    if (ignoreErrors)
                    {
                        try
                        {
                            WorldGen.UpdateWorld();
                            UpdateInvasion();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        WorldGen.UpdateWorld();
                        UpdateInvasion();
                    }
                }
                if (ignoreErrors)
                {
                    try
                    {
                        if (Main.netMode == 2)
                        {
                            UpdateServer();
                        }
                    }
                    catch
                    {
                        int netMode = Main.netMode;
                    }
                }
                else if (Main.netMode == 2)
                {
                    UpdateServer();
                }
                if (ignoreErrors)
                {
                    try
                    {
                        for (int num9 = 0; num9 < numChatLines; num9++)
                        {
                            if (chatLine[num9].showTime > 0)
                            {
                                ChatLine line = chatLine[num9];
                                line.showTime--;
                            }
                        }
                    }
                    catch
                    {
                        for (int num10 = 0; num10 < numChatLines; num10++)
                        {
                            chatLine[num10] = new ChatLine();
                        }
                    }
                }
                else
                {
                    for (int num11 = 0; num11 < numChatLines; num11++)
                    {
                        if (chatLine[num11].showTime > 0)
                        {
                            ChatLine line2 = chatLine[num11];
                            line2.showTime--;
                        }
                    }
                }
            }
        }

        private static void UpdateClient()
        {
            if (myPlayer == 8)
            {
                Netplay.disconnect = true;
            }
            netPlayCounter++;
            if (netPlayCounter > 0xe10)
            {
                netPlayCounter = 0;
            }
            if (Math.IEEERemainder((double) netPlayCounter, 300.0) == 0.0)
            {
                NetMessage.SendData(13, -1, -1, "", myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(0x24, -1, -1, "", myPlayer, 0f, 0f, 0f);
            }
            if (Math.IEEERemainder((double) netPlayCounter, 600.0) == 0.0)
            {
                NetMessage.SendData(0x10, -1, -1, "", myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(40, -1, -1, "", myPlayer, 0f, 0f, 0f);
            }
            if (Netplay.clientSock.active)
            {
                Netplay.clientSock.timeOut++;
                if (!(stopTimeOuts || (Netplay.clientSock.timeOut <= (60 * timeOut))))
                {
                    statusText = "Connection timed out";
                    Netplay.disconnect = true;
                }
            }
            for (int i = 0; i < 200; i++)
            {
                if (item[i].active && (item[i].owner == myPlayer))
                {
                    item[i].FindOwner(i);
                }
            }
        }

        private static void UpdateDebug()
        {
            if (netMode != 2)
            {
                int i = 0;
                int j = 0;
                i = 8;
                j = 8;
                if ((((2 < screenWidth) && (2 < screenHeight)) && ((i >= 0) && (j >= 0))) && ((i < maxTilesX) && (j < maxTilesY)))
                {
                    Lighting.addLight(i, j, 1f);
                }
            }
        }

        private static void UpdateInvasion()
        {
            if (invasionType > 0)
            {
                if (invasionSize <= 0)
                {
                    InvasionWarning();
                    invasionType = 0;
                    invasionDelay = 7;
                }
                if (invasionX != spawnTileX)
                {
                    float num = 0.2f;
                    if (invasionX > spawnTileX)
                    {
                        invasionX -= num;
                        if (invasionX <= spawnTileX)
                        {
                            invasionX = spawnTileX;
                            InvasionWarning();
                        }
                        else
                        {
                            invasionWarn--;
                        }
                    }
                    else if (invasionX < spawnTileX)
                    {
                        invasionX += num;
                        if (invasionX >= spawnTileX)
                        {
                            invasionX = spawnTileX;
                            InvasionWarning();
                        }
                        else
                        {
                            invasionWarn--;
                        }
                    }
                    if (invasionWarn <= 0)
                    {
                        invasionWarn = 0xe10;
                        InvasionWarning();
                    }
                }
            }
        }

        private static void UpdateMenu()
        {
            playerInventory = false;
            exitScale = 0.8f;
            if (netMode == 0)
            {
                if (!grabSky)
                {
                    time += 86.4;
                    if (dayTime)
                    {
                        if (time > 54000.0)
                        {
                            time = 0.0;
                            dayTime = false;
                        }
                    }
                    else if (time > 32400.0)
                    {
                        bloodMoon = false;
                        time = 0.0;
                        dayTime = true;
                        moonPhase++;
                        if (moonPhase >= 8)
                        {
                            moonPhase = 0;
                        }
                    }
                }
            }
            else if (netMode == 1)
            {
                UpdateTime();
            }
        }

        private static void UpdateServer()
        {
            netPlayCounter++;
            if (netPlayCounter > 0xe10)
            {
                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                NetMessage.syncPlayers();
                netPlayCounter = 0;
            }
            tmpCounter++;
            if (tmpCounter > 10)
            {
                tmpCounter = 0;
                bool flag = true;
                int lastItemUpdate = Main.lastItemUpdate;
                int num2 = 0;
                while (flag)
                {
                    lastItemUpdate++;
                    if (lastItemUpdate >= 200)
                    {
                        lastItemUpdate = 0;
                    }
                    num2++;
                    if (!(item[lastItemUpdate].active && (item[lastItemUpdate].owner != 8)))
                    {
                        NetMessage.SendData(0x15, -1, -1, "", lastItemUpdate, 0f, 0f, 0f);
                    }
                    if ((num2 >= maxItemUpdates) || (lastItemUpdate == Main.lastItemUpdate))
                    {
                        flag = false;
                    }
                }
                Main.lastItemUpdate = lastItemUpdate;
            }
            for (int i = 0; i < 200; i++)
            {
                if (item[i].active && ((item[i].owner == 8) || !player[item[i].owner].active))
                {
                    item[i].FindOwner(i);
                }
            }
            for (int j = 0; j < 8; j++)
            {
                if (Netplay.serverSock[j].active)
                {
                    ServerSock sock = Netplay.serverSock[j];
                    sock.timeOut++;
                    if (!(stopTimeOuts || (Netplay.serverSock[j].timeOut <= (60 * timeOut))))
                    {
                        Netplay.serverSock[j].kill = true;
                    }
                }
                if (player[j].active)
                {
                    int sectionX = Netplay.GetSectionX((int) (player[j].position.X / 16f));
                    int sectionY = Netplay.GetSectionY((int) (player[j].position.Y / 16f));
                    int num7 = 0;
                    for (int k = sectionX - 1; k < (sectionX + 2); k++)
                    {
                        for (int m = sectionY - 1; m < (sectionY + 2); m++)
                        {
                            if (!((((k < 0) || (k >= maxSectionsX)) || ((m < 0) || (m >= maxSectionsY))) || Netplay.serverSock[j].tileSection[k, m]))
                            {
                                num7++;
                            }
                        }
                    }
                    if (num7 > 0)
                    {
                        int number = num7 * 150;
                        NetMessage.SendData(9, j, -1, "Recieving tile data", number, 0f, 0f, 0f);
                        Netplay.serverSock[j].statusText2 = "is recieving tile data";
                        ServerSock sock2 = Netplay.serverSock[j];
                        sock2.statusMax += number;
                        for (int n = sectionX - 1; n < (sectionX + 2); n++)
                        {
                            for (int num12 = sectionY - 1; num12 < (sectionY + 2); num12++)
                            {
                                if (!((((n < 0) || (n >= maxSectionsX)) || ((num12 < 0) || (num12 >= maxSectionsY))) || Netplay.serverSock[j].tileSection[n, num12]))
                                {
                                    NetMessage.SendSection(j, n, num12);
                                    NetMessage.SendData(11, j, -1, "", n, (float) num12, (float) n, (float) num12);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void UpdateTime()
        {
            bool flag;
            time++;
            if (dayTime)
            {
                if (time <= 54000.0)
                {
                    goto Label_04B6;
                }
                WorldGen.spawnNPC = 0;
                checkForSpawns = 0;
                if (((rand.Next(50) == 0) && (netMode != 1)) && WorldGen.shadowOrbSmashed)
                {
                    WorldGen.spawnMeteor = true;
                }
                if (NPC.downedBoss1 || (netMode == 1))
                {
                    goto Label_0381;
                }
                flag = false;
                for (int i = 0; i < 8; i++)
                {
                    if (player[i].active && (player[i].statLifeMax >= 200))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            else
            {
                if ((WorldGen.spawnEye && (netMode != 1)) && (time > 4860.0))
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((player[j].active && !player[j].dead) && (player[j].position.Y < (worldSurface * 16.0)))
                        {
                            NPC.SpawnOnPlayer(j, 4);
                            WorldGen.spawnEye = false;
                            break;
                        }
                    }
                }
                if (time > 32400.0)
                {
                    if (invasionDelay > 0)
                    {
                        invasionDelay--;
                    }
                    WorldGen.spawnNPC = 0;
                    checkForSpawns = 0;
                    time = 0.0;
                    bloodMoon = false;
                    dayTime = true;
                    moonPhase++;
                    if (moonPhase >= 8)
                    {
                        moonPhase = 0;
                    }
                    if (netMode == 2)
                    {
                        NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                        WorldGen.saveAndPlay();
                    }
                    if ((netMode != 1) && (rand.Next(15) == 0))
                    {
                        StartInvasion();
                    }
                }
                if ((time > 16200.0) && WorldGen.spawnMeteor)
                {
                    WorldGen.spawnMeteor = false;
                    WorldGen.dropMeteor();
                }
                return;
            }
            if (flag && (rand.Next(3) == 0))
            {
                int num3 = 0;
                for (int k = 0; k < 0x3e8; k++)
                {
                    if (npc[k].active && npc[k].townNPC)
                    {
                        num3++;
                    }
                }
                if (num3 >= 4)
                {
                    WorldGen.spawnEye = true;
                    if (netMode == 0)
                    {
                        NewText("You feel an evil presence watching you...", 50, 0xff, 130);
                    }
                    else if (netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, "You feel an evil presence watching you...", 8, 50f, 255f, 130f);
                    }
                }
            }
        Label_0381:
            if ((!WorldGen.spawnEye && (moonPhase != 4)) && ((rand.Next(7) == 0) && (netMode != 1)))
            {
                for (int m = 0; m < 8; m++)
                {
                    if (player[m].active && (player[m].statLifeMax > 100))
                    {
                        bloodMoon = true;
                        break;
                    }
                }
                if (bloodMoon)
                {
                    if (netMode == 0)
                    {
                        NewText("The Blood Moon is rising...", 50, 0xff, 130);
                    }
                    else if (netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, "The Blood Moon is rising...", 8, 50f, 255f, 130f);
                    }
                }
            }
            time = 0.0;
            dayTime = false;
            if (netMode == 2)
            {
                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
            }
        Label_04B6:
            if (netMode != 1)
            {
                checkForSpawns++;
                if (checkForSpawns >= 0x1c20)
                {
                    int num6 = 0;
                    for (int n = 0; n < 8; n++)
                    {
                        if (player[n].active)
                        {
                            num6++;
                        }
                    }
                    checkForSpawns = 0;
                    WorldGen.spawnNPC = 0;
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = 0;
                    int num12 = 0;
                    int num13 = 0;
                    int num14 = 0;
                    int num15 = 0;
                    for (int num16 = 0; num16 < 0x3e8; num16++)
                    {
                        if (npc[num16].active && npc[num16].townNPC)
                        {
                            if (!((npc[num16].type == 0x25) || npc[num16].homeless))
                            {
                                WorldGen.QuickFindHome(num16);
                            }
                            else
                            {
                                num13++;
                            }
                            if (npc[num16].type == 0x11)
                            {
                                num8++;
                            }
                            if (npc[num16].type == 0x12)
                            {
                                num9++;
                            }
                            if (npc[num16].type == 0x13)
                            {
                                num11++;
                            }
                            if (npc[num16].type == 20)
                            {
                                num10++;
                            }
                            if (npc[num16].type == 0x16)
                            {
                                num12++;
                            }
                            if (npc[num16].type == 0x26)
                            {
                                num14++;
                            }
                            num15++;
                        }
                    }
                    if (WorldGen.spawnNPC == 0)
                    {
                        int num17 = 0;
                        bool flag2 = false;
                        int num18 = 0;
                        bool flag3 = false;
                        bool flag4 = false;
                        for (int num19 = 0; num19 < 8; num19++)
                        {
                            if (player[num19].active)
                            {
                                for (int num20 = 0; num20 < 0x2c; num20++)
                                {
                                    if ((player[num19].inventory[num20] != null) & (player[num19].inventory[num20].stack > 0))
                                    {
                                        if (player[num19].inventory[num20].type == 0x47)
                                        {
                                            num17 += player[num19].inventory[num20].stack;
                                        }
                                        if (player[num19].inventory[num20].type == 0x48)
                                        {
                                            num17 += player[num19].inventory[num20].stack * 100;
                                        }
                                        if (player[num19].inventory[num20].type == 0x49)
                                        {
                                            num17 += player[num19].inventory[num20].stack * 0x2710;
                                        }
                                        if (player[num19].inventory[num20].type == 0x4a)
                                        {
                                            num17 += player[num19].inventory[num20].stack * 0xf4240;
                                        }
                                        if (((player[num19].inventory[num20].type == 0x5f) || (player[num19].inventory[num20].type == 0x60)) || (((player[num19].inventory[num20].type == 0x61) || (player[num19].inventory[num20].type == 0x62)) || (player[num19].inventory[num20].useAmmo == 14)))
                                        {
                                            flag3 = true;
                                        }
                                        if ((player[num19].inventory[num20].type == 0xa6) || (player[num19].inventory[num20].type == 0xa7))
                                        {
                                            flag4 = true;
                                        }
                                    }
                                }
                                int num21 = player[num19].statLifeMax / 20;
                                if (num21 > 5)
                                {
                                    flag2 = true;
                                }
                                num18 += num21;
                            }
                        }
                        if ((WorldGen.spawnNPC == 0) && (num12 < 1))
                        {
                            WorldGen.spawnNPC = 0x16;
                        }
                        if (((WorldGen.spawnNPC == 0) && (num17 > 5000.0)) && (num8 < 1))
                        {
                            WorldGen.spawnNPC = 0x11;
                        }
                        if (((WorldGen.spawnNPC == 0) && flag2) && (num9 < 1))
                        {
                            WorldGen.spawnNPC = 0x12;
                        }
                        if (((WorldGen.spawnNPC == 0) && flag3) && (num11 < 1))
                        {
                            WorldGen.spawnNPC = 0x13;
                        }
                        if (((WorldGen.spawnNPC == 0) && ((NPC.downedBoss1 || NPC.downedBoss2) || NPC.downedBoss3)) && (num10 < 1))
                        {
                            WorldGen.spawnNPC = 20;
                        }
                        if (((WorldGen.spawnNPC == 0) && flag4) && ((num8 > 0) && (num14 < 1)))
                        {
                            WorldGen.spawnNPC = 0x26;
                        }
                        if (((WorldGen.spawnNPC == 0) && (num17 > 0x186a0)) && ((num8 < 2) && (num6 > 2)))
                        {
                            WorldGen.spawnNPC = 0x11;
                        }
                        if (((WorldGen.spawnNPC == 0) && (num18 >= 20)) && ((num9 < 2) && (num6 > 2)))
                        {
                            WorldGen.spawnNPC = 0x12;
                        }
                        if (((WorldGen.spawnNPC == 0) && (num17 > 0x4c4b40)) && ((num8 < 3) && (num6 > 4)))
                        {
                            WorldGen.spawnNPC = 0x11;
                        }
                        if (!(NPC.downedBoss3 || (num13 != 0)))
                        {
                            int index = NPC.NewNPC((dungeonX * 0x10) + 8, dungeonY * 0x10, 0x25, 0);
                            npc[index].homeless = false;
                            npc[index].homeTileX = dungeonX;
                            npc[index].homeTileY = dungeonY;
                        }
                    }
                }
            }
        }

        public class ButtonState
        {
            public bool pressed = false;
            public static Main.ButtonState Pressed = new Main.ButtonState(false);

            public ButtonState(bool asd)
            {
                this.pressed = asd;
            }
        }

        public class Keyboard
        {
            public string getState()
            {
                return "asda";
            }
        }

        public class keyState
        {
            public static Keys[] GetPressedKeys()
            {
                return new Keys[1];
            }

            public static bool IsKeyDown(object obj)
            {
                return false;
            }
        }

        public class mouseState
        {
            public static Main.ButtonState LeftButton = Main.ButtonState.Pressed;
            public static Main.ButtonState RightButton = Main.ButtonState.Pressed;
            public static int ScrollWheelValue = 0x2a;
            public static int X = 0;
            public static int Y = 0;
        }

        public class screenPos
        {
            public int height = 600;
            public int width = 800;
            public float X = 0f;
            public float Y = 0f;
        }
    }
}


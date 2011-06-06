using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
namespace Terraria
{
    public class Main
    {
        public const int numArmorBody = 10;
        public const int numArmorHead = 15;
        public const int numArmorLegs = 10;
        public const double dayLength = 54000.0;
        public const int maxBackgrounds = 7;
        public const int maxChests = 1000;
        public const int maxClouds = 100;
        public const int maxCloudTypes = 4;
        public const int maxCombatText = 100;
        public const int maxDust = 2000;
        public const int maxGore = 200;
        public const int maxGoreTypes = 76;
        public const int maxHair = 17;
        public const int maxInventory = 44;
        public const int maxItems = 200;
        public const int maxItemSounds = 16;
        public const int maxItemTypes = 239;
        public const int maxLiquidTypes = 2;
        public const int maxMusic = 7;
        public const int maxNPCHitSounds = 3;
        public const int maxNPCKilledSounds = 3;
        public const int maxNPCs = 1000;
        public const int maxNPCTypes = 46;
        public const int maxPlayers = 255;
        public const int maxProjectiles = 1000;
        public const int maxProjectileTypes = 38;
        public const int maxStars = 130;
        public const int maxStarTypes = 5;
        public const int maxTileSets = 80;
        public const int maxWallTypes = 14;
        private const int MF_BYPOSITION = 1024;
        public const double nightLength = 32400.0;
        public const int sectionHeight = 150;
        public const int sectionWidth = 200;
        public static Recipe[] recipe = new Recipe[5000];
        public static float bottomWorld = 38400f;
        public static float rightWorld = 134400f;
        public static int numAvailableRecipes;
        public static int numChatLines = 7;
        public static int numClouds = Main.cloudLimit;
        private static int numLoadPlayers = 0;
        private static int numLoadWorlds = 0;
        public static int numStars;
        public static Texture2D[] armorArmTexture = new Texture2D[10];
        public static Texture2D[] armorBodyTexture = new Texture2D[10];
        public static Texture2D[] armorHeadTexture = new Texture2D[15];
        public static Texture2D[] armorLegTexture = new Texture2D[10];
        public static bool autoJoin = false;
        public static bool autoPass = false;
        public static bool autoShutdown = false;
        public static int[] availableRecipe = new int[Recipe.maxRecipes];
        public static float[] availableRecipeY = new float[Recipe.maxRecipes];
        public static int background = 0;
        public static int[] backgroundHeight = new int[7];
        public static Texture2D[] backgroundTexture = new Texture2D[7];
        public static int[] backgroundWidth = new int[7];
        public static Texture2D blackTileTexture;
        public static bool bloodMoon = false;
        public static Texture2D boneArmTexture;
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
        public static ChatLine[] chatLine = new ChatLine[Main.numChatLines];
        public static bool chatMode = false;
        public static bool chatRelease = false;
        public static string chatText = "";
        public static Texture2D chatTexture;
        public static int checkForSpawns = 0;
        public static Chest[] chest = new Chest[1000];
        public static string cInv = "Escape";
        public static string cJump = "Space";
        public static string cLeft = "A";
        public static Player clientPlayer = new Player();
        public static Cloud[] cloud = new Cloud[100];
        public static int cloudLimit = 100;
        public static Texture2D[] cloudTexture = new Texture2D[4];
        public static CombatText[] combatText = new CombatText[100];
        public static string cRight = "D";
        public static string cThrowItem = "Q";
        public static string cUp = "W";
        public int curMusic = 0;
        public static int curRelease = 4;
        public static float cursorAlpha = 0f;
        public static Color cursorColor = Color.White;
        public static int cursorColorDirection = 1;
        public static float cursorScale = 0f;
        public static Texture2D cursorTexture;
        public static bool dayTime = true;
        public static bool debugMode = false;
        public static bool dedServ = false;
        public static string defaultIP = "";
        private int[] displayHeight = new int[99];
        private int[] displayWidth = new int[99];
        public static int drawTime = 0;
        public static bool dumbAI = false;
        public static int dungeonTiles;
        public static int dungeonX;
        public static int dungeonY;
        public static Dust[] dust = new Dust[2000];
        public static Texture2D dustTexture;
        public static bool editSign = false;
        public static int evilTiles;
        public static int fadeCounter = 0;
        public static Texture2D fadeTexture;
        public static bool fixedTiming = false;
        public static int focusRecipe;
        public static SpriteFont fontCombatText;
        public static SpriteFont fontDeathText;
        public static SpriteFont fontItemStack;
        public static SpriteFont fontMouseText;
        public static int frameRate = 0;
        public static bool frameRelease = false;
        public static bool gameMenu = true;
        public static string getIP = Main.defaultIP;
        public static string getPort = System.Convert.ToString(Netplay.serverPort);
        public static bool godMode = false;
        public static Gore[] gore = new Gore[201];
        public static Texture2D[] goreTexture = new Texture2D[76];
        public static bool grabSky = false;
        public static bool grabSun = false;
        public static bool hasFocus = true;
        public static Texture2D heartTexture;
        public static int helpText = 0;
        public static bool hideUI = false;
        public static float[] hotbarScale = new float[]
		{
			1f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f
		};
        public static bool ignoreErrors = true;
        public static bool inputTextEnter = false;
        public static int invasionDelay = 0;
        public static int invasionSize = 0;
        public static int invasionType = 0;
        public static int invasionWarn = 0;
        public static double invasionX = 0.0;
        public static Texture2D inventoryBackTexture;
        public static Item[] item = new Item[201];
        public static Texture2D[] itemTexture = new Texture2D[239];
        public static int jungleTiles;
        public static int lastItemUpdate;
        public static int lastNPCUpdate;
        public static float leftWorld = 0f;
        public static bool lightTiles = false;
        public static Liquid[] liquid = new Liquid[Liquid.resLiquid];
        public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[10000];
        public static Texture2D[] liquidTexture = new Texture2D[2];
        public static Player[] loadPlayer = new Player[5];
        public static string[] loadPlayerPath = new string[5];
        public static string[] loadWorld = new string[999];
        public static string[] loadWorldPath = new string[999];
        public static Texture2D logoTexture;
        public static int magmaBGFrame = 0;
        public static int magmaBGFrameCounter = 0;
        public static Texture2D manaTexture;
        public static int maxItemUpdates = 10;
        private static int maxMenuItems = 11;
        public static int maxNetPlayers = 255;
        public static int maxNPCUpdates = 15;
        public static int maxTilesX = (int)Main.rightWorld / 16 + 1;
        public static int maxTilesY = (int)Main.bottomWorld / 16 + 1;
        public static int maxSectionsX = Main.maxTilesX / 200;
        public static int maxSectionsY = Main.maxTilesY / 150;
        private float[] menuItemScale = new float[Main.maxMenuItems];
        public static int menuMode = 0;
        public static bool menuMultiplayer = false;
        public static bool menuServer = false;
        public static int meteorTiles;
        public static short moonModY = 0;
        public static int moonPhase = 0;
        public static Texture2D moonTexture;
        public static string motd = "";
        public static Color mouseColor = new Color(255, 50, 95);
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
        public int newMusic = 0;
        public static string newWorldName = "";
        public static NPC[] npc = new NPC[1001];
        public static bool npcChatFocus1 = false;
        public static bool npcChatFocus2 = false;
        public static bool npcChatRelease = false;
        public static string npcChatText = "";
        public static int[] npcFrameCount = new int[]
		{
			1, 
			2, 
			2, 
			3, 
			6, 
			2, 
			2, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			2, 
			16, 
			14, 
			16, 
			14, 
			14, 
			16, 
			2, 
			10, 
			1, 
			16, 
			16, 
			16, 
			3, 
			1, 
			14, 
			3, 
			1, 
			3, 
			1, 
			1, 
			16, 
			16, 
			1, 
			1, 
			1, 
			3, 
			3, 
			14, 
			3
		};
        public static int npcShop = 0;
        public static Texture2D[] npcTexture = new Texture2D[46];
        public static string oldStatusText = "";
        public static Player[] player = new Player[256];
        public static Texture2D playerBeltTexture;
        public static Texture2D playerEyesTexture;
        public static Texture2D playerEyeWhitesTexture;
        public static Texture2D[] playerHairTexture = new Texture2D[17];
        public static Texture2D playerHands2Texture;
        public static Texture2D playerHandsTexture;
        public static Texture2D playerHeadTexture;
        public static bool playerInventory = false;
        public static Texture2D playerPantsTexture;
        public static string PlayerPath = Main.SavePath + "\\Players";
        public static string playerPathName;
        public static Texture2D playerShirtTexture;
        public static Texture2D playerShoesTexture;
        public static Texture2D playerUnderShirt2Texture;
        public static Texture2D playerUnderShirtTexture;
        public static Projectile[] projectile = new Projectile[1001];
        public static Texture2D[] projectileTexture = new Texture2D[38];
        [System.ThreadStatic]
        public static System.Random rand;
        public static Texture2D raTexture;
        public static bool releaseUI = false;
        public static bool resetClouds = true;
        public static Texture2D reTexture;
        public static double rockLayer;
        public static string SavePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "\\My Games\\Terraria";
        public static int saveTimer = 0;
        public static int screenHeight = 600;
        public static Vector2 screenLastPosition;
        public static Vector2 screenPosition;
        public static int screenWidth = 800;
        private Color selColor = Color.White;
        public static bool serverStarting = false;
        public Chest[] shop = new Chest[5];
        public static bool showFrameRate = false;
        public static bool showItemOwner = false;
        public static bool showSpam = false;
        public static bool showSplash = true;
        public static Texture2D shroomCapTexture;
        public static Sign[] sign = new Sign[1000];
        public static bool signBubble = false;
        public static string signText = "";
        public static int signX = 0;
        public static int signY = 0;
        public static bool skipMenu = false;
        public static int spawnTileX;
        public static int spawnTileY;
        public static Texture2D splashTexture;
        public static int stackCounter = 0;
        public static int stackDelay = 7;
        public static int stackSplit;
        public static Star[] star = new Star[130];
        public static Texture2D[] starTexture = new Texture2D[5];
        public static string statusText = "";
        public static bool stopSpawns = false;
        public static bool stopTimeOuts = false;
        public static Texture2D sun2Texture;
        public static short sunModY = 0;
        public static Texture2D sunTexture;
        public static Color[] teamColor = new Color[5];
        public static Texture2D teamTexture;
        public static Texture2D textBackTexture;
        public static Tile[,] tile = new Tile[Main.maxTilesX, Main.maxTilesY];
        public static bool[] tileBlockLight = new bool[80];
        public static Color tileColor;
        public static bool[] tileDungeon = new bool[80];
        public static bool[] tileFrameImportant = new bool[80];
        public static bool[] tileLavaDeath = new bool[80];
        public static bool[] tileNoAttach = new bool[80];
        public static bool[] tileNoFail = new bool[80];
        public static bool tilesLoaded = false;
        public static bool[] tileSolid = new bool[80];
        public static bool[] tileSolidTop = new bool[80];
        public static bool[] tileStone = new bool[80];
        public static bool[] tileTable = new bool[80];
        public static Texture2D[] tileTexture = new Texture2D[80];
        public static bool[] tileWaterDeath = new bool[80];
        public static double time = 13500.0;
        public static int timeOut = 120;
        public bool toggleFullscreen;
        private static Item toolTip = new Item();
        public static float topWorld = 0f;
        public static Texture2D[] treeBranchTexture = new Texture2D[2];
        public static Texture2D[] treeTopTexture = new Texture2D[2];
        private Process tServer = new Process();
        public static int updateTime = 0;
        public static bool verboseNetplay = false;
        public static string versionNumber = "v1.0.3";
        public static string versionNumber2 = "v1.0.3";
        public static bool[] wallHouse = new bool[14];
        public static Texture2D[] wallTexture = new Texture2D[14];
        public static bool webProtect = false;
        public static float windSpeed = 0f;
        public static float windSpeedSpeed = 0f;
        public static int worldID;
        public static string worldName = "";
        public static string WorldPath = Main.SavePath + "\\Worlds";
        public static string worldPathName;
        public static double worldSurface;
        public static System.Collections.Generic.Dictionary<string, string> properties = new System.Collections.Generic.Dictionary<string, string>();
        private static bool propertiesLoaded = false;
        public Main()
        {
            if (!System.IO.File.Exists("bans.txt"))
            {
                System.IO.File.Create("bans.txt");
            }
            Main.loadProperties();
            while (!Main.propertiesLoaded)
            {
            }
            this.Initialize();
            if (System.IO.File.Exists(Main.properties["worldName"] + ".wld"))
            {
                Main.loadWorldPath[0] = System.IO.Path.GetFullPath(Main.properties["worldName"] + ".wld");
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine("WORLD NOT FOUND! CHECK YOUR SERVER.PROPERTIES. GENERATING NEW WORLD!");
                System.Console.ForegroundColor = System.ConsoleColor.Gray;
                System.Console.Beep();
                bool flag = true;
                while (flag)
                {
                    System.Console.WriteLine("tMod " + Main.versionNumber2);
                    System.Console.WriteLine("");
                    System.Console.WriteLine("1" + '\t' + "Small");
                    System.Console.WriteLine("2" + '\t' + "Medium");
                    System.Console.WriteLine("3" + '\t' + "Large");
                    System.Console.WriteLine("");
                    System.Console.Write("Choose size: ");
                    string value = System.Console.ReadLine();
                    try
                    {
                        switch (System.Convert.ToInt32(value))
                        {
                            case 1:
                                {
                                    Main.maxTilesX = 4200;
                                    Main.maxTilesY = 1200;
                                    flag = false;
                                    break;
                                }
                            case 2:
                                {
                                    Main.maxTilesX = 6300;
                                    Main.maxTilesY = 1800;
                                    flag = false;
                                    break;
                                }
                            case 3:
                                {
                                    Main.maxTilesX = 8400;
                                    Main.maxTilesY = 2400;
                                    flag = false;
                                    break;
                                }
                        }
                    }
                    catch
                    {
                    }
                }
                Main.worldName = Main.newWorldName;
                Main.worldPathName = Main.properties["worldName"] + ".wld";
                Main.menuMode = 10;
                WorldGen.CreateNewWorld();
                flag = false;
                while (Main.menuMode == 10)
                {
                    if (Main.oldStatusText != Main.statusText)
                    {
                        Main.oldStatusText = Main.statusText;
                        System.Console.WriteLine(Main.statusText);
                    }
                }
            }
            Main.worldPathName = Main.loadWorldPath[0];
            Main.netMode = 2;
            Main.showSplash = false;
            if (!System.IO.Directory.Exists("plugins\\"))
            {
                System.IO.Directory.CreateDirectory("plugins\\");
            }
            PluginManager.loadPlugins();
            Help.add("/kick {name}", "Kicks a player");
            Help.add("/ban {name}", "Bans a player");
            Help.add("/home", "Teleports you home");
            Help.add("/sethome", "Sets your home to your position");
            Help.add("/warp {name}", "Warps you to the specified warp");
            Help.add("/setwarp {name}", "Sets the specified warp to your position");
            Help.add("/save", "Saves the map");
            Help.add("/time {day | night | number}", "Sets the time");
            Help.add("/help", "Shows this prompt");
            Help.add("/maxspawns {amount}", "Sets the maximum spawns (amount of NPCs)");
            Help.add("/spawnrate {rate}", "Sets the spawnrate (lower = higher)");
            Help.add("/oplogin {password}", "Logs you in as an operator");
            Help.add("/tp {user}", "Teleports you to the specified user");
            Help.add("/tphere {user}", "Teleports the specified user to you");
            Help.add("/invasion {size}", "Starts a goblin invasion of the specified size");
            Help.add("/spawnnpc {id}", "Spawns an NPC/MOB.");
        }
        public static void loadProperties()
        {
            try
            {
                if (!System.IO.File.Exists("server.properties"))
                {
                    System.IO.StreamWriter streamWriter = System.IO.File.CreateText("server.properties");
                    streamWriter.Write(string.Concat(new string[]
					{
						"giveEnabled=true", 
						System.Environment.NewLine, 
						"opPassword=123", 
						System.Environment.NewLine, 
						"tpEnabled=true", 
						System.Environment.NewLine, 
						"homeEnabled=true", 
						System.Environment.NewLine, 
						"welcomeMessage=Welcome!", 
						System.Environment.NewLine, 
						"serverPassword=", 
						System.Environment.NewLine, 
						"whitelistEnabled=false", 
						System.Environment.NewLine, 
						"worldName=world1", 
						System.Environment.NewLine, 
						"advertEnabled=true", 
						System.Environment.NewLine, 
						"spawnMonsters=true", 
						System.Environment.NewLine, 
						"pvp=false", 
						System.Environment.NewLine, 
						"spawnRate=700", 
						System.Environment.NewLine, 
						"maxSpawns=4", 
						System.Environment.NewLine, 
						"explosivesEnabled=false", 
						System.Environment.NewLine, 
						"serverPort=7777"
					}));
                    streamWriter.Close();
                }
                string[] array = System.IO.File.ReadAllLines("server.properties");
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    if (text.IndexOf('=') > -1 && text.Substring(0, 1) != "#")
                    {
                        string[] array2 = text.Split(new char[]
						{
							'='
						});
                        if (array2[0] != "" && array2[1] != "")
                        {
                            Main.properties.Add(array2[0], array2[1]);
                        }
                        else
                        {
                            if (array2[0] != "")
                            {
                                Main.properties.Add(array2[0], "");
                            }
                        }
                    }
                }
                bool flag = false;
                System.Console.WriteLine("Loaded properties file!");
                if (!Main.properties.ContainsKey("welcomeMessage"))
                {
                    Main.properties.Add("welcomeMessage", "Welcome!");
                    System.Console.WriteLine("PROPERTY: welcomeMessage MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("serverPassword"))
                {
                    Main.properties.Add("serverPassword", "");
                    System.Console.WriteLine("PROPERTY: serverPassword MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("explosivesEnabled"))
                {
                    Main.properties.Add("explosivesEnabled", "false");
                    System.Console.WriteLine("PROPERTY: explosivesEnabled MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("maxPlayers"))
                {
                    Main.properties.Add("maxPlayers", "8");
                    System.Console.WriteLine("PROPERTY: maxPlayers MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("spawnMonsters"))
                {
                    Main.properties.Add("spawnMonsters", "true");
                    System.Console.WriteLine("PROPERTY: spawnMonsters MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("maxSpawns"))
                {
                    Main.properties.Add("maxSpawns", "4");
                    System.Console.WriteLine("PROPERTY: maxSpawns MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("serverPort"))
                {
                    Main.properties.Add("serverPort", "7777");
                    System.Console.WriteLine("PROPERTY: serverPort MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("spawnRate"))
                {
                    Main.properties.Add("spawnRate", "700");
                    System.Console.WriteLine("PROPERTY: spawnRate MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("pvp"))
                {
                    Main.properties.Add("pvp", "true");
                    System.Console.WriteLine("PROPERTY: pvp MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("worldName"))
                {
                    Main.properties.Add("worldName", "world1");
                    System.Console.WriteLine("PROPERTY: worldName MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("whitelistEnabled"))
                {
                    Main.properties.Add("whitelistEnabled", "false");
                    System.Console.WriteLine("PROPERTY: whitelistEnabled MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (!Main.properties.ContainsKey("advertEnabled"))
                {
                    Main.properties.Add("advertEnabled", "true");
                    System.Console.WriteLine("PROPERTY: advertEnabled MISSING! DEFAULT SET, PLEASE ADD IT TO YOUR server.properties");
                    flag = true;
                }
                if (Main.properties["opPassword"] == "123")
                {
                    System.Console.WriteLine("OPERATOR PASSWORD IS SET TO THE DEFAULT VALUE OF 123, PLEASE CHANGE IT IN YOUR SERVER.PROPERTIES!");
                    flag = true;
                }
                if (Main.properties["serverPassword"] != "")
                {
                    Netplay.password = Main.properties["serverPassword"];
                    System.Console.WriteLine("Password set to: " + Netplay.password);
                }
                int num = 8;
                if (!int.TryParse(Main.properties["maxPlayers"], out num))
                {
                    num = 8;
                }
                Main.maxNetPlayers = num + 1;
                int serverPort;
                if (!int.TryParse(Main.properties["serverPort"], out serverPort))
                {
                    System.Console.WriteLine("Problem with port number (serverPort)!");
                    flag = true;
                    serverPort = 7777;
                }
                Netplay.serverPort = serverPort;
                System.Console.WriteLine("Max players set to: " + num);
                Main.propertiesLoaded = true;
                if (Main.properties["spawnMonsters"] == "false")
                {
                    Main.stopSpawns = true;
                }
                int num2 = 4;
                int num3 = 700;
                if (!int.TryParse(Main.properties["maxSpawns"], out num2))
                {
                    flag = true;
                    System.Console.WriteLine("Problem with property value: maxSpawns: INVALID INTEGER");
                    num2 = 4;
                }
                if (!int.TryParse(Main.properties["maxSpawns"], out num3))
                {
                    flag = true;
                    System.Console.WriteLine("Problem with property value: maxSpawns: INVALID INTEGER");
                    num3 = 700;
                }
                NPC.defaultMaxSpawns = num2;
                NPC.defaultSpawnRate = num3;
                NPC.spawnRate = num3;
                NPC.maxSpawns = num2;
                if (flag)
                {
                    System.Console.Beep();
                    System.Console.WriteLine("There were warnings whilst loading the properties file. Please read them. Resuming in 5 seconds!");
                    System.Console.WriteLine("To quicky add new properties: delete your server.properties! This will LOSE your configuration!");
                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                System.Console.WriteLine("Error loading server.properties...");
            }
        }
        public void AutoJoin(string IP)
        {
            Main.defaultIP = IP;
            Main.getIP = IP;
            Netplay.SetIP(Main.defaultIP);
            Main.autoJoin = true;
        }
        public void AutoPass()
        {
            Main.autoPass = true;
        }
        public void autoShut()
        {
            Main.autoShutdown = true;
        }
        public static double CalculateDamage(int Damage, int Defense)
        {
            double num = (double)Damage - (double)Defense * 0.5;
            if (num < 1.0)
            {
                num = 1.0;
            }
            return num;
        }
        public static void CursorColor()
        {
            Main.cursorAlpha += (float)Main.cursorColorDirection * 0.015f;
            if (Main.cursorAlpha >= 1f)
            {
                Main.cursorAlpha = 1f;
                Main.cursorColorDirection = -1;
            }
            if ((double)Main.cursorAlpha <= 0.6)
            {
                Main.cursorAlpha = 0.6f;
                Main.cursorColorDirection = 1;
            }
            float num = Main.cursorAlpha * 0.3f + 0.7f;
            byte r = (byte)((float)Main.mouseColor.R * Main.cursorAlpha);
            byte g = (byte)((float)Main.mouseColor.G * Main.cursorAlpha);
            byte b = (byte)((float)Main.mouseColor.B * Main.cursorAlpha);
            byte a = (byte)(255f * num);
            Main.cursorColor = new Color(r, g, b, a);
            Main.cursorScale = Main.cursorAlpha * 0.3f + 0.7f + 0.1f;
        }
        public void DedServ()
        {
            //TimeBeginPeriod(1);
            rand = new Random();
            //if (autoShutdown)
            //{
            //    string lpWindowName = "terraria" + rand.Next(0x7fffffff);
            //    Console.Title = lpWindowName;
            //    IntPtr hWnd = FindWindow(null, lpWindowName);
            //    if (hWnd != IntPtr.Zero)
            //    {
            //        ShowWindow(hWnd, 0);
            //    }
            //}
            //else
            //{
                Console.Title = "Terraria Server " + versionNumber2;
            //}
            dedServ = true;
            showSplash = false;
            this.Initialize();
            while ((worldPathName == null) || (worldPathName == ""))
            {
                LoadWorlds();
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Terraria Server " + versionNumber2);
                    Console.WriteLine("");
                    int index = 0;
                    while (index < numLoadWorlds)
                    {
                        Console.WriteLine(string.Concat(new object[] { index + 1, '\t', '\t', loadWorld[index] }));
                        index++;
                    }
                    Console.WriteLine(string.Concat(new object[] { "n", '\t', '\t', "New World" }));
                    Console.WriteLine("d <number>" + '\t' + "Delete World");
                    Console.WriteLine("");
                    Console.Write("Choose World: ");
                    string str2 = Console.ReadLine();
                    if ((str2.Length >= 2) && (str2.Substring(0, 2).ToLower() == "d "))
                    {
                        try
                        {
                            index = Convert.ToInt32(str2.Substring(2)) - 1;
                            if (index < numLoadWorlds)
                            {
                                Console.WriteLine("Terraria Server " + versionNumber2);
                                Console.WriteLine("");
                                Console.WriteLine("Really delete " + loadWorld[index] + "?");
                                Console.Write("(y/n): ");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    EraseWorld(index);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        string str4 = str2;
                        if ((str4 != null) && ((str4 == "n") || (str4 == "N")))
                        {
                            bool flag2 = true;
                            while (flag2)
                            {
                                Console.WriteLine("tMod " + versionNumber2);
                                Console.WriteLine("");
                                Console.WriteLine("1" + '\t' + "Small");
                                Console.WriteLine("2" + '\t' + "Medium");
                                Console.WriteLine("3" + '\t' + "Large");
                                Console.WriteLine("");
                                Console.Write("Choose size: ");
                                string str3 = Console.ReadLine();
                                try
                                {
                                    switch (Convert.ToInt32(str3))
                                    {
                                        case 1:
                                            maxTilesX = 0x1068;
                                            maxTilesY = 0x4b0;
                                            flag2 = false;
                                            goto Label_03AB;

                                        case 2:
                                            maxTilesX = 0x189c;
                                            maxTilesY = 0x708;
                                            flag2 = false;
                                            goto Label_03AB;

                                        case 3:
                                            maxTilesX = 0x20d0;
                                            maxTilesY = 0x960;
                                            flag2 = false;
                                            goto Label_03AB;
                                    }
                                }
                                catch
                                {
                                }
                            Label_03AB: ;
                            }
                            flag2 = true;
                            while (flag2)
                            {
                                Console.WriteLine("Terraria Server " + versionNumber2);
                                Console.WriteLine("");
                                Console.Write("Enter world name: ");
                                newWorldName = Console.ReadLine();
                                if (((newWorldName != "") && (newWorldName != " ")) && (newWorldName != null))
                                {
                                    flag2 = false;
                                }
                            }
                            worldName = newWorldName;
                            worldPathName = nextLoadWorld();
                            menuMode = 10;
                            WorldGen.CreateNewWorld();
                            flag2 = false;
                            while (menuMode == 10)
                            {
                                if (oldStatusText != statusText)
                                {
                                    oldStatusText = statusText;
                                    Console.WriteLine(statusText);
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                int num2 = Convert.ToInt32(str2) - 1;
                                if ((num2 >= 0) && (num2 < numLoadWorlds))
                                {
                                    worldPathName = loadWorldPath[0];
                                    flag = false;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            WorldGen.serverLoadWorld();
            Console.WriteLine("Terraria Server " + versionNumber);
            Console.WriteLine("Removed the bad loop here, so less CPU when the server's starting (yes really, there was something here causing high CPU when it started up =/)");
            Console.WriteLine("tMod " + versionNumber);
            Console.WriteLine("");
            Console.WriteLine("Listening on port " + Netplay.serverPort);
            Console.WriteLine("Type 'help' for a list of commands.");
            Console.WriteLine("");
            Console.Title = "Terraria Server: " + worldName;
            Stopwatch stopwatch = new Stopwatch();
            double num3 = 16.666666666666668;
            if (!autoShutdown)
            {
                startDedInput();
            }
            stopwatch.Start();
            double num4 = 0.0;
            while (!Netplay.disconnect)
            {
                double num5 = stopwatch.ElapsedMilliseconds + num4;
                if (num5 >= num3)
                {
                    num4 = num5 - num3;
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (oldStatusText != statusText)
                    {
                        oldStatusText = statusText;
                        Console.WriteLine(statusText);
                    }
                    this.Update();
                    float elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    if (elapsedMilliseconds < num3)
                    {
                        int millisecondsTimeout = ((int)(num3 - elapsedMilliseconds)) - 1;
                        if (millisecondsTimeout > 1)
                        {
                            Thread.Sleep(millisecondsTimeout);
                        }
                    }
                }
            }
        }
        private static void ErasePlayer(int i)
        {
            try
            {
                System.IO.File.Delete(Main.loadPlayerPath[i]);
                System.IO.File.Delete(Main.loadPlayerPath[i] + ".bak");
                Main.LoadPlayers();
            }
            catch
            {
            }
        }
        private static void EraseWorld(int i)
        {
            try
            {
                System.IO.File.Delete(Main.loadWorldPath[i]);
                System.IO.File.Delete(Main.loadWorldPath[i] + ".bak");
                Main.LoadWorlds();
            }
            catch
            {
            }
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //public static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);
        protected void getAuth()
        {
            try
            {
                string requestUriString = "";
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                byte[] array = new byte[8192];
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
                System.IO.Stream responseStream = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
                int num;
                do
                {
                    num = responseStream.Read(array, 0, array.Length);
                    if (num != 0)
                    {
                        string @string = System.Text.Encoding.ASCII.GetString(array, 0, num);
                        stringBuilder.Append(@string);
                    }
                }
                while (num > 0);
            }
            catch
            {
                this.QuitGame();
            }
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern short GetKeyState(int keyCode);
        //[System.Runtime.InteropServices.DllImport("User32")]
        //private static extern int GetMenuItemCount(System.IntPtr hWnd);
        //[System.Runtime.InteropServices.DllImport("User32")]
        //private static extern System.IntPtr GetSystemMenu(System.IntPtr hWnd, bool bRevert);
        private static void HelpText()
        {
            bool flag = false;
            if (Main.player[Main.myPlayer].statLifeMax > 100)
            {
                flag = true;
            }
            bool flag2 = false;
            if (Main.player[Main.myPlayer].statManaMax > 0)
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
            for (int i = 0; i < 44; i++)
            {
                if (Main.player[Main.myPlayer].inventory[i].pick > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Pickaxe")
                {
                    flag3 = false;
                }
                if (Main.player[Main.myPlayer].inventory[i].axe > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Axe")
                {
                    flag3 = false;
                }
                if (Main.player[Main.myPlayer].inventory[i].hammer > 0)
                {
                    flag3 = false;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 11 || Main.player[Main.myPlayer].inventory[i].type == 12 || Main.player[Main.myPlayer].inventory[i].type == 13 || Main.player[Main.myPlayer].inventory[i].type == 14)
                {
                    flag4 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 19 || Main.player[Main.myPlayer].inventory[i].type == 20 || Main.player[Main.myPlayer].inventory[i].type == 21 || Main.player[Main.myPlayer].inventory[i].type == 22)
                {
                    flag5 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 75)
                {
                    flag6 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 75)
                {
                    flag8 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 68 || Main.player[Main.myPlayer].inventory[i].type == 70)
                {
                    flag9 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 84)
                {
                    flag10 = true;
                }
                if (Main.player[Main.myPlayer].inventory[i].type == 117)
                {
                    flag7 = true;
                }
            }
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            bool flag14 = false;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.npc[i].active)
                {
                    if (Main.npc[i].type == 17)
                    {
                        flag11 = true;
                    }
                    if (Main.npc[i].type == 18)
                    {
                        flag12 = true;
                    }
                    if (Main.npc[i].type == 19)
                    {
                        flag14 = true;
                    }
                    if (Main.npc[i].type == 20)
                    {
                        flag13 = true;
                    }
                }
            }
            while (true)
            {
                Main.helpText++;
                if (flag3)
                {
                    if (Main.helpText == 1)
                    {
                        break;
                    }
                    if (Main.helpText == 2)
                    {
                        goto Block_31;
                    }
                    if (Main.helpText == 3)
                    {
                        goto Block_32;
                    }
                    if (Main.helpText == 4)
                    {
                        goto Block_33;
                    }
                    if (Main.helpText == 5)
                    {
                        goto Block_34;
                    }
                    if (Main.helpText == 6)
                    {
                        goto Block_35;
                    }
                }
                if (flag3 && !flag4 && !flag5 && Main.helpText == 11)
                {
                    goto Block_39;
                }
                if (flag3 && flag4 && !flag5)
                {
                    if (Main.helpText == 21)
                    {
                        goto Block_43;
                    }
                    if (Main.helpText == 22)
                    {
                        goto Block_44;
                    }
                }
                if (flag3 && flag5)
                {
                    if (Main.helpText == 31)
                    {
                        goto Block_47;
                    }
                    if (Main.helpText == 32)
                    {
                        goto Block_48;
                    }
                }
                if (!flag && Main.helpText == 41)
                {
                    goto Block_50;
                }
                if (!flag2 && Main.helpText == 42)
                {
                    goto Block_52;
                }
                if (!flag2 && !flag6 && Main.helpText == 43)
                {
                    goto Block_55;
                }
                if (!flag11 && !flag12)
                {
                    if (Main.helpText == 51)
                    {
                        goto Block_58;
                    }
                    if (Main.helpText == 52)
                    {
                        goto Block_59;
                    }
                    if (Main.helpText == 53)
                    {
                        goto Block_60;
                    }
                }
                if (!flag11 && Main.helpText == 61)
                {
                    goto Block_62;
                }
                if (!flag12 && Main.helpText == 62)
                {
                    goto Block_64;
                }
                if (!flag14 && Main.helpText == 63)
                {
                    goto Block_66;
                }
                if (!flag13 && Main.helpText == 64)
                {
                    goto Block_68;
                }
                if (flag8 && Main.helpText == 71)
                {
                    goto Block_70;
                }
                if (flag9 && Main.helpText == 72)
                {
                    goto Block_72;
                }
                if ((flag8 || flag9) && Main.helpText == 80)
                {
                    goto Block_74;
                }
                if (!flag10 && Main.helpText == 201)
                {
                    goto Block_76;
                }
                if (flag7 && Main.helpText == 202)
                {
                    goto Block_78;
                }
                if (Main.helpText == 1000)
                {
                    goto Block_79;
                }
                if (Main.helpText == 1001)
                {
                    goto Block_80;
                }
                if (Main.helpText == 1002)
                {
                    goto Block_81;
                }
                if (Main.helpText > 1100)
                {
                    Main.helpText = 0;
                }
            }
            Main.npcChatText = "You can use your pickaxe to dig through dirt, and your axe to chop down trees. Just place your cursor over the tile and click!";
            return;
        Block_31:
            Main.npcChatText = "If you want to survive, you will need to create weapons and shelter. Start by chopping down trees and gathering wood.";
            return;
        Block_32:
            Main.npcChatText = "Press ESC to access your crafting menu. When you have enough wood, create a workbench. This will allow you to create more complicated things, as long as you are standing close to it.";
            return;
        Block_33:
            Main.npcChatText = "You can build a shelter by placing wood or other blocks in the world. Don't forget to create and place walls.";
            return;
        Block_34:
            Main.npcChatText = "Once you have a wooden sword, you might try to gather some gel from the slimes. Combine wood and gel to make a torch!";
            return;
        Block_35:
            Main.npcChatText = "To interact with backgrounds and placed objects, use a hammer!";
            return;
        Block_39:
            Main.npcChatText = "You should do some mining to find metal ore. You can craft very useful things with it.";
            return;
        Block_43:
            Main.npcChatText = "Now that you have some ore, you will need to turn it into a bar in order to make items with it. This requires a furnace!";
            return;
        Block_44:
            Main.npcChatText = "You can create a furnace out of torches, wood, and stone. Make sure you are standing near a work bench.";
            return;
        Block_47:
            Main.npcChatText = "You will need an anvil to make most things out of metal bars.";
            return;
        Block_48:
            Main.npcChatText = "Anvils can be crafted out of iron, or purchased from a merchant.";
            return;
        Block_50:
            Main.npcChatText = "Underground are crystal hearts that can be used to increase your max life. You will need a hammer to obtain them.";
            return;
        Block_52:
            Main.npcChatText = "If you gather 10 fallen stars, they can be combined to create an item that will increase your magic capacity.";
            return;
        Block_55:
            Main.npcChatText = "Stars fall all over the world at night. They can be used for all sorts of usefull things. If you see one, be sure to grab it because they disappear after sunrise.";
            return;
        Block_58:
            Main.npcChatText = "There are many different ways you can attract people to move in to our town. They will of course need a home to live in.";
            return;
        Block_59:
            Main.npcChatText = "In order for a room to be considered a home, it needs to have a door, chair, table, and a light source.  Make sure the house has walls as well.";
            return;
        Block_60:
            Main.npcChatText = "Two people will not live in the same home. Also, if their home is destroyed, they will look for a new place to live.";
            return;
        Block_62:
            Main.npcChatText = "If you want a merchant to move in, you will need to gather plenty of money. 50 silver coins should do the trick!";
            return;
        Block_64:
            Main.npcChatText = "For a nurse to move in, you might want to increase your maximum life.";
            return;
        Block_66:
            Main.npcChatText = "If you had a gun, I bet an arms dealer might show up to sell you some ammo!";
            return;
        Block_68:
            Main.npcChatText = "You should prove yourself by defeating a strong monster. That will get the attention of a dryad.";
            return;
        Block_70:
            Main.npcChatText = "If you combine lenses at a demon alter, you might be able to find a way to summon a powerful monster. You will want to wait until night before using it, though.";
            return;
        Block_72:
            Main.npcChatText = "You can create worm bait with rotten chunks and vile powder. Make sure you are in a corrupt area before using it.";
            return;
        Block_74:
            Main.npcChatText = "Demonic alters can usually be found in the corruption. You will need to be near them to craft some items.";
            return;
        Block_76:
            Main.npcChatText = "You can make a grappling hook from a hook and 3 chains. Skeletons found deep underground usually carry hooks, and chains can be made from iron bars.";
            return;
        Block_78:
            Main.npcChatText = "You can craft a space gun using a flintlock pistol, 10 fallen stars, and 30 meteorite bars.";
            return;
        Block_79:
            Main.npcChatText = "If you see a pot, be sure to smash it open. They contain all sorts of useful supplies.";
            return;
        Block_80:
            Main.npcChatText = "There is treasure hidden all over the world. Some amazing things can be found deep underground!";
            return;
        Block_81:
            Main.npcChatText = "Smashing a shadow orb will cause a meteor to fall out of the sky. Shadow orbs can usually be found in the chasms around corrupt areas.";
        }
        protected void Initialize()
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
            Main.tileSolid[0] = true;
            Main.tileBlockLight[0] = true;
            Main.tileSolid[1] = true;
            Main.tileBlockLight[1] = true;
            Main.tileSolid[2] = true;
            Main.tileBlockLight[2] = true;
            Main.tileSolid[3] = false;
            Main.tileNoAttach[3] = true;
            Main.tileNoFail[3] = true;
            Main.tileSolid[4] = false;
            Main.tileNoAttach[4] = true;
            Main.tileNoFail[4] = true;
            Main.tileNoFail[24] = true;
            Main.tileSolid[5] = false;
            Main.tileSolid[6] = true;
            Main.tileBlockLight[6] = true;
            Main.tileSolid[7] = true;
            Main.tileBlockLight[7] = true;
            Main.tileSolid[8] = true;
            Main.tileBlockLight[8] = true;
            Main.tileSolid[9] = true;
            Main.tileBlockLight[9] = true;
            Main.tileBlockLight[10] = true;
            Main.tileSolid[10] = true;
            Main.tileNoAttach[10] = true;
            Main.tileBlockLight[10] = true;
            Main.tileSolid[11] = false;
            Main.tileSolidTop[19] = true;
            Main.tileSolid[19] = true;
            Main.tileSolid[22] = true;
            Main.tileSolid[23] = true;
            Main.tileSolid[25] = true;
            Main.tileSolid[30] = true;
            Main.tileNoFail[32] = true;
            Main.tileBlockLight[32] = true;
            Main.tileSolid[37] = true;
            Main.tileBlockLight[37] = true;
            Main.tileSolid[38] = true;
            Main.tileBlockLight[38] = true;
            Main.tileSolid[39] = true;
            Main.tileBlockLight[39] = true;
            Main.tileSolid[40] = true;
            Main.tileBlockLight[40] = true;
            Main.tileSolid[41] = true;
            Main.tileBlockLight[41] = true;
            Main.tileSolid[43] = true;
            Main.tileBlockLight[43] = true;
            Main.tileSolid[44] = true;
            Main.tileBlockLight[44] = true;
            Main.tileSolid[45] = true;
            Main.tileBlockLight[45] = true;
            Main.tileSolid[46] = true;
            Main.tileBlockLight[46] = true;
            Main.tileSolid[47] = true;
            Main.tileBlockLight[47] = true;
            Main.tileSolid[48] = true;
            Main.tileBlockLight[48] = true;
            Main.tileSolid[53] = true;
            Main.tileBlockLight[53] = true;
            Main.tileSolid[54] = true;
            Main.tileBlockLight[52] = true;
            Main.tileSolid[56] = true;
            Main.tileBlockLight[56] = true;
            Main.tileSolid[57] = true;
            Main.tileBlockLight[57] = true;
            Main.tileSolid[58] = true;
            Main.tileBlockLight[58] = true;
            Main.tileSolid[59] = true;
            Main.tileBlockLight[59] = true;
            Main.tileSolid[60] = true;
            Main.tileBlockLight[60] = true;
            Main.tileSolid[63] = true;
            Main.tileBlockLight[63] = true;
            Main.tileStone[63] = true;
            Main.tileSolid[64] = true;
            Main.tileBlockLight[64] = true;
            Main.tileStone[64] = true;
            Main.tileSolid[65] = true;
            Main.tileBlockLight[65] = true;
            Main.tileStone[65] = true;
            Main.tileSolid[66] = true;
            Main.tileBlockLight[66] = true;
            Main.tileStone[66] = true;
            Main.tileSolid[67] = true;
            Main.tileBlockLight[67] = true;
            Main.tileStone[67] = true;
            Main.tileSolid[68] = true;
            Main.tileBlockLight[68] = true;
            Main.tileStone[68] = true;
            Main.tileSolid[75] = true;
            Main.tileBlockLight[75] = true;
            Main.tileSolid[76] = true;
            Main.tileBlockLight[76] = true;
            Main.tileSolid[70] = true;
            Main.tileBlockLight[70] = true;
            Main.tileBlockLight[51] = true;
            Main.tileNoFail[50] = true;
            Main.tileNoAttach[50] = true;
            Main.tileDungeon[41] = true;
            Main.tileDungeon[43] = true;
            Main.tileDungeon[44] = true;
            Main.tileBlockLight[30] = true;
            Main.tileBlockLight[25] = true;
            Main.tileBlockLight[23] = true;
            Main.tileBlockLight[22] = true;
            Main.tileBlockLight[62] = true;
            Main.tileSolidTop[18] = true;
            Main.tileSolidTop[14] = true;
            Main.tileSolidTop[16] = true;
            Main.tileNoAttach[20] = true;
            Main.tileNoAttach[19] = true;
            Main.tileNoAttach[13] = true;
            Main.tileNoAttach[14] = true;
            Main.tileNoAttach[15] = true;
            Main.tileNoAttach[16] = true;
            Main.tileNoAttach[17] = true;
            Main.tileNoAttach[18] = true;
            Main.tileNoAttach[19] = true;
            Main.tileNoAttach[21] = true;
            Main.tileNoAttach[27] = true;
            Main.tileFrameImportant[3] = true;
            Main.tileFrameImportant[5] = true;
            Main.tileFrameImportant[10] = true;
            Main.tileFrameImportant[11] = true;
            Main.tileFrameImportant[12] = true;
            Main.tileFrameImportant[13] = true;
            Main.tileFrameImportant[14] = true;
            Main.tileFrameImportant[15] = true;
            Main.tileFrameImportant[16] = true;
            Main.tileFrameImportant[17] = true;
            Main.tileFrameImportant[18] = true;
            Main.tileFrameImportant[20] = true;
            Main.tileFrameImportant[21] = true;
            Main.tileFrameImportant[24] = true;
            Main.tileFrameImportant[26] = true;
            Main.tileFrameImportant[27] = true;
            Main.tileFrameImportant[28] = true;
            Main.tileFrameImportant[29] = true;
            Main.tileFrameImportant[31] = true;
            Main.tileFrameImportant[33] = true;
            Main.tileFrameImportant[34] = true;
            Main.tileFrameImportant[35] = true;
            Main.tileFrameImportant[36] = true;
            Main.tileFrameImportant[42] = true;
            Main.tileFrameImportant[50] = true;
            Main.tileFrameImportant[55] = true;
            Main.tileFrameImportant[61] = true;
            Main.tileFrameImportant[71] = true;
            Main.tileFrameImportant[72] = true;
            Main.tileFrameImportant[73] = true;
            Main.tileFrameImportant[74] = true;
            Main.tileFrameImportant[77] = true;
            Main.tileFrameImportant[78] = true;
            Main.tileFrameImportant[79] = true;
            Main.tileTable[14] = true;
            Main.tileTable[18] = true;
            Main.tileTable[19] = true;
            Main.tileWaterDeath[4] = true;
            Main.tileWaterDeath[51] = true;
            Main.tileLavaDeath[3] = true;
            Main.tileLavaDeath[5] = true;
            Main.tileLavaDeath[10] = true;
            Main.tileLavaDeath[11] = true;
            Main.tileLavaDeath[12] = true;
            Main.tileLavaDeath[13] = true;
            Main.tileLavaDeath[14] = true;
            Main.tileLavaDeath[15] = true;
            Main.tileLavaDeath[16] = true;
            Main.tileLavaDeath[17] = true;
            Main.tileLavaDeath[18] = true;
            Main.tileLavaDeath[19] = true;
            Main.tileLavaDeath[20] = true;
            Main.tileLavaDeath[27] = true;
            Main.tileLavaDeath[28] = true;
            Main.tileLavaDeath[29] = true;
            Main.tileLavaDeath[32] = true;
            Main.tileLavaDeath[33] = true;
            Main.tileLavaDeath[34] = true;
            Main.tileLavaDeath[35] = true;
            Main.tileLavaDeath[36] = true;
            Main.tileLavaDeath[42] = true;
            Main.tileLavaDeath[49] = true;
            Main.tileLavaDeath[50] = true;
            Main.tileLavaDeath[52] = true;
            Main.tileLavaDeath[55] = true;
            Main.tileLavaDeath[61] = true;
            Main.tileLavaDeath[62] = true;
            Main.tileLavaDeath[69] = true;
            Main.tileLavaDeath[71] = true;
            Main.tileLavaDeath[72] = true;
            Main.tileLavaDeath[73] = true;
            Main.tileLavaDeath[74] = true;
            Main.tileLavaDeath[78] = true;
            Main.tileLavaDeath[79] = true;
            Main.wallHouse[1] = true;
            Main.wallHouse[4] = true;
            Main.wallHouse[5] = true;
            Main.wallHouse[6] = true;
            Main.wallHouse[10] = true;
            Main.wallHouse[11] = true;
            Main.wallHouse[12] = true;
            for (int i = 0; i < Main.maxMenuItems; i++)
            {
                this.menuItemScale[i] = 0.8f;
            }
            for (int i = 0; i < 2000; i++)
            {
                Main.dust[i] = new Dust();
            }
            for (int i = 0; i < 201; i++)
            {
                Main.item[i] = new Item();
            }
            for (int i = 0; i < 1001; i++)
            {
                Main.npc[i] = new NPC();
                Main.npc[i].whoAmI = i;
            }
            for (int i = 0; i < 256; i++)
            {
                Main.player[i] = new Player();
            }
            for (int i = 0; i < 1001; i++)
            {
                Main.projectile[i] = new Projectile();
            }
            for (int i = 0; i < 201; i++)
            {
                Main.gore[i] = new Gore();
            }
            for (int i = 0; i < 100; i++)
            {
                Main.cloud[i] = new Cloud();
            }
            for (int i = 0; i < 100; i++)
            {
                Main.combatText[i] = new CombatText();
            }
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.recipe[i] = new Recipe();
                Main.availableRecipeY[i] = (float)(65 * i);
            }
            Recipe.SetupRecipes();
            for (int i = 0; i < Main.numChatLines; i++)
            {
                Main.chatLine[i] = new ChatLine();
            }
            for (int i = 0; i < Liquid.resLiquid; i++)
            {
                Main.liquid[i] = new Liquid();
            }
            for (int i = 0; i < 10000; i++)
            {
                Main.liquidBuffer[i] = new LiquidBuffer();
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
            Main.teamColor[0] = Color.White;
            Main.teamColor[1] = new Color(230, 40, 20);
            Main.teamColor[2] = new Color(20, 200, 30);
            Main.teamColor[3] = new Color(75, 90, 255);
            Main.teamColor[4] = new Color(200, 180, 0);
            Netplay.Init();
            if (Main.skipMenu)
            {
                WorldGen.clearWorld();
                Main.gameMenu = false;
                Main.LoadPlayers();
                Main.player[Main.myPlayer] = (Player)Main.loadPlayer[0].Clone();
                Main.PlayerPath = Main.loadPlayerPath[0];
                Main.LoadWorlds();
                WorldGen.generateWorld(-1);
                WorldGen.EveryTileFrame();
                Main.player[Main.myPlayer].Spawn();
            }
        }
        private static void InvasionWarning()
        {
            if (Main.invasionType != 0)
            {
                string text = "";
                if (Main.invasionSize <= 0)
                {
                    text = "The goblin army has been defeated!";
                }
                else
                {
                    if (Main.invasionX < (double)Main.spawnTileX)
                    {
                        text = "A goblin army is approaching from the west!";
                    }
                    else
                    {
                        if (Main.invasionX > (double)Main.spawnTileX)
                        {
                            text = "A goblin army is approaching from the east!";
                        }
                        else
                        {
                            text = "The goblin army has arrived!";
                        }
                    }
                }
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(25, -1, -1, text, 255, 175f, 75f, 255f);
                }
            }
        }
        public void LoadDedConfig(string configPath)
        {
            if (System.IO.File.Exists(configPath))
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(configPath))
                {
                    string text;
                    while ((text = streamReader.ReadLine()) != null)
                    {
                        try
                        {
                            if (text.Length > 6 && text.Substring(0, 6).ToLower() == "world=")
                            {
                                Main.worldPathName = text.Substring(6);
                            }
                            if (text.Length > 5 && text.Substring(0, 5).ToLower() == "port=")
                            {
                                string value = text.Substring(5);
                                try
                                {
                                    Netplay.serverPort = System.Convert.ToInt32(value);
                                }
                                catch
                                {
                                }
                            }
                            if (text.Length > 11 && text.Substring(0, 11).ToLower() == "maxplayers=")
                            {
                                string value = text.Substring(11);
                                try
                                {
                                    Main.maxNetPlayers = System.Convert.ToInt32(value);
                                }
                                catch
                                {
                                }
                            }
                            if (text.Length > 9 && text.Substring(0, 9).ToLower() == "password=")
                            {
                                Netplay.password = text.Substring(9);
                            }
                            if (text.Length > 5 && text.Substring(0, 5).ToLower() == "motd=")
                            {
                                Main.motd = text.Substring(5);
                            }
                            if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldpath=")
                            {
                                Main.WorldPath = text.Substring(10);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        private static void LoadPlayers()
        {
            System.IO.Directory.CreateDirectory(Main.PlayerPath);
            string[] files = System.IO.Directory.GetFiles(Main.PlayerPath, "*.plr");
            int num = files.Length;
            if (num > 5)
            {
                num = 5;
            }
            for (int i = 0; i < 5; i++)
            {
                Main.loadPlayer[i] = new Player();
                if (i < num)
                {
                    Main.loadPlayerPath[i] = files[i];
                    Main.loadPlayer[i] = Player.LoadPlayer(Main.loadPlayerPath[i]);
                }
            }
            Main.numLoadPlayers = num;
        }
        public static void LoadWorlds()
        {
            System.IO.Directory.CreateDirectory(Main.WorldPath);
            string[] files = System.IO.Directory.GetFiles(Main.WorldPath, "*.wld");
            int num = files.Length;
            if (!Main.dedServ && num > 5)
            {
                num = 5;
            }
            for (int i = 0; i < num; i++)
            {
                Main.loadWorldPath[i] = files[i];
                try
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(Main.loadWorldPath[i], System.IO.FileMode.Open))
                    {
                        using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fileStream))
                        {
                            int num2 = binaryReader.ReadInt32();
                            Main.loadWorld[i] = binaryReader.ReadString();
                            binaryReader.Close();
                        }
                    }
                }
                catch
                {
                    Main.loadWorld[i] = Main.loadWorldPath[i];
                }
            }
            Main.numLoadWorlds = num;
        }
        private static string nextLoadPlayer()
        {
            int num = 1;
            while (System.IO.File.Exists(string.Concat(new object[]
			{
				Main.PlayerPath, 
				"\\player", 
				num, 
				".plr"
			})))
            {
                num++;
            }
            return string.Concat(new object[]
			{
				Main.PlayerPath, 
				"\\player", 
				num, 
				".plr"
			});
        }
        private static string nextLoadWorld()
        {
            int num = 1;
            while (System.IO.File.Exists(string.Concat(new object[]
			{
				Main.WorldPath, 
				"\\world", 
				num, 
				".wld"
			})))
            {
                num++;
            }
            return string.Concat(new object[]
			{
				Main.WorldPath, 
				"\\world", 
				num, 
				".wld"
			});
        }
        protected void OpenSettings()
        {
            try
            {
                if (System.IO.File.Exists(Main.SavePath + "\\config.dat"))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(Main.SavePath + "\\config.dat", System.IO.FileMode.Open))
                    {
                        using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fileStream))
                        {
                            int num = binaryReader.ReadInt32();
                            bool flag = binaryReader.ReadBoolean();
                            Main.mouseColor.R = binaryReader.ReadByte();
                            Main.mouseColor.G = binaryReader.ReadByte();
                            Main.mouseColor.B = binaryReader.ReadByte();
                            Main.musicVolume = binaryReader.ReadSingle();
                            Main.cUp = binaryReader.ReadString();
                            Main.cDown = binaryReader.ReadString();
                            Main.cLeft = binaryReader.ReadString();
                            Main.cRight = binaryReader.ReadString();
                            Main.cJump = binaryReader.ReadString();
                            Main.cThrowItem = binaryReader.ReadString();
                            if (num >= 1)
                            {
                                Main.cInv = binaryReader.ReadString();
                            }
                            Main.caveParrallax = binaryReader.ReadSingle();
                            if (num >= 2)
                            {
                                Main.fixedTiming = binaryReader.ReadBoolean();
                            }
                            binaryReader.Close();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        protected void QuitGame()
        {
            Steam.Kill();
        }
        //[System.Runtime.InteropServices.DllImport("User32")]
        //private static extern int RemoveMenu(System.IntPtr hMenu, int nPosition, int wFlags);
        protected void SaveSettings()
        {
            System.IO.Directory.CreateDirectory(Main.SavePath);
            try
            {
                System.IO.File.SetAttributes(Main.SavePath + "\\config.dat", System.IO.FileAttributes.Normal);
            }
            catch
            {
            }
            try
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(Main.SavePath + "\\config.dat", System.IO.FileMode.Create))
                {
                    using (System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(fileStream))
                    {
                        binaryWriter.Write(Main.curRelease);
                        binaryWriter.Write(0);
                        binaryWriter.Write(Main.mouseColor.R);
                        binaryWriter.Write(Main.mouseColor.G);
                        binaryWriter.Write(Main.mouseColor.B);
                        binaryWriter.Write(0);
                        binaryWriter.Write(Main.musicVolume);
                        binaryWriter.Write(Main.cUp);
                        binaryWriter.Write(Main.cDown);
                        binaryWriter.Write(Main.cLeft);
                        binaryWriter.Write(Main.cRight);
                        binaryWriter.Write(Main.cJump);
                        binaryWriter.Write(Main.cThrowItem);
                        binaryWriter.Write(Main.cInv);
                        binaryWriter.Write(Main.caveParrallax);
                        binaryWriter.Write(Main.fixedTiming);
                        binaryWriter.Write(42);
                        binaryWriter.Write(42);
                        binaryWriter.Close();
                    }
                }
            }
            catch
            {
            }
        }
        public void SetNetPlayers(int mPlayers)
        {
            Main.maxNetPlayers = mPlayers;
        }
        public void SetWorld(string wrold)
        {
            Main.worldPathName = wrold;
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);
        public static void startDedInput()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Main.startDedInputCallBack), 1);
        }
        public static void startDedInputCallBack(object threadContext)
        {
            while (!Netplay.disconnect)
            {
                System.Console.Write(": ");
                string text = System.Console.ReadLine();
                string text2 = text;
                text = text.ToLower();
                try
                {
                    string text3 = text;
                    switch (text3)
                    {
                        case "help":
                            {
                                System.Console.WriteLine("Available commands:");
                                System.Console.WriteLine("");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"help ", 
								'\t', 
								'\t', 
								" Displays a list of commands."
							}));
                                System.Console.WriteLine("playing " + '\t' + " Shows the list of players");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"clear ", 
								'\t', 
								'\t', 
								" Clear the console window."
							}));
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"exit ", 
								'\t', 
								'\t', 
								" Shutdown the server and save."
							}));
                                System.Console.WriteLine("exit-nosave " + '\t' + " Shutdown the server without saving.");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"save ", 
								'\t', 
								'\t', 
								" Save the game world."
							}));
                                System.Console.WriteLine("kick <player> " + '\t' + " Kicks a player from the server.");
                                System.Console.WriteLine("ban <player> " + '\t' + " Bans a player from the server.");
                                System.Console.WriteLine("password" + '\t' + " Show password.");
                                System.Console.WriteLine("password <pass>" + '\t' + " Change password.");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"version", 
								'\t', 
								'\t', 
								" Print version number."
							}));
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"time", 
								'\t', 
								'\t', 
								" Display game time."
							}));
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"port", 
								'\t', 
								'\t', 
								" Print the listening port."
							}));
                                System.Console.WriteLine("maxplayers" + '\t' + " Print the max number of players.");
                                System.Console.WriteLine("say <words>" + '\t' + " Send a message.");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"motd", 
								'\t', 
								'\t', 
								" Print MOTD."
							}));
                                System.Console.WriteLine("motd <words>" + '\t' + " Change MOTD.");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"dawn", 
								'\t', 
								'\t', 
								" Change time to dawn."
							}));
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"noon", 
								'\t', 
								'\t', 
								" Change time to noon."
							}));
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"dusk", 
								'\t', 
								'\t', 
								" Change time to dusk."
							}));
                                System.Console.WriteLine("midnight" + '\t' + " Change time to midnight.");
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"settle", 
								'\t', 
								'\t', 
								" Settle all water."
							}));
                                continue;
                            }
                        case "settle":
                            {
                                if (!Liquid.panicMode)
                                {
                                    Liquid.StartPanic();
                                }
                                else
                                {
                                    System.Console.WriteLine("Water is already settling");
                                }
                                continue;
                            }
                        case "dawn":
                            {
                                Main.dayTime = true;
                                Main.time = 0.0;
                                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                                continue;
                            }
                        case "dusk":
                            {
                                Main.dayTime = false;
                                Main.time = 0.0;
                                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                                continue;
                            }
                        case "noon":
                            {
                                Main.dayTime = true;
                                Main.time = 27000.0;
                                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                                continue;
                            }
                        case "midnight":
                            {
                                Main.dayTime = false;
                                Main.time = 16200.0;
                                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                                continue;
                            }
                        case "exit-nosave":
                            {
                                Netplay.disconnect = true;
                                continue;
                            }
                        case "exit":
                            {
                                WorldGen.saveWorld(false);
                                Netplay.disconnect = true;
                                continue;
                            }
                        case "save":
                            {
                                WorldGen.saveWorld(false);
                                continue;
                            }
                        case "time":
                            {
                                string text4 = "AM";
                                double num2 = Main.time;
                                if (!Main.dayTime)
                                {
                                    num2 += 54000.0;
                                }
                                num2 = num2 / 86400.0 * 24.0;
                                double num3 = 7.5;
                                num2 = num2 - num3 - 12.0;
                                if (num2 < 0.0)
                                {
                                    num2 += 24.0;
                                }
                                if (num2 >= 12.0)
                                {
                                    text4 = "PM";
                                }
                                int num4 = (int)num2;
                                double num5 = num2 - (double)num4;
                                num5 = (double)((int)(num5 * 60.0));
                                string text5 = num5.ToString();
                                if (num5 < 10.0)
                                {
                                    text5 = "0" + text5;
                                }
                                if (num4 > 12)
                                {
                                    num4 -= 12;
                                }
                                if (num4 == 0)
                                {
                                    num4 = 12;
                                }
                                System.Console.WriteLine(string.Concat(new object[]
							{
								"Time: ", 
								num4, 
								":", 
								text5, 
								" ", 
								text4
							}));
                                continue;
                            }
                        case "maxplayers":
                            {
                                System.Console.WriteLine("Player limit: " + Main.maxNetPlayers);
                                continue;
                            }
                        case "port":
                            {
                                System.Console.WriteLine("Port: " + Netplay.serverPort);
                                continue;
                            }
                        case "version":
                            {
                                System.Console.WriteLine("Terraria Server " + Main.versionNumber);
                                continue;
                            }
                        case "clear":
                            {
                                continue;
                            }
                        case "playing":
                            {
                                int num6 = 0;
                                for (int i = 0; i < 255; i++)
                                {
                                    if (Main.player[i].active)
                                    {
                                        num6++;
                                        System.Console.WriteLine(string.Concat(new object[]
									{
										Main.player[i].name, 
										" (", 
										Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
										")"
									}));
                                    }
                                }
                                if (num6 == 0)
                                {
                                    System.Console.WriteLine("No players connected.");
                                }
                                else
                                {
                                    if (num6 == 1)
                                    {
                                        System.Console.WriteLine("1 player connected.");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine(num6 + " players connected.");
                                    }
                                }
                                continue;
                            }
                        case "":
                            {
                                continue;
                            }
                        case "motd":
                            {
                                if (Main.motd == "")
                                {
                                    System.Console.WriteLine("Welcome to " + Main.worldName + "!");
                                }
                                else
                                {
                                    System.Console.WriteLine("MOTD: " + Main.motd);
                                }
                                continue;
                            }
                    }
                    if (text.Length >= 5 && text.Substring(0, 5) == "motd ")
                    {
                        Main.motd = text2.Substring(5);
                    }
                    else
                    {
                        if (text.Length == 8 && text.Substring(0, 8) == "password")
                        {
                            if (Netplay.password == "")
                            {
                                System.Console.WriteLine("No password set.");
                            }
                            else
                            {
                                System.Console.WriteLine("Password: " + Netplay.password);
                            }
                        }
                        else
                        {
                            if (text.Length >= 9 && text.Substring(0, 9) == "password ")
                            {
                                string text6 = text2.Substring(9);
                                if (text6 == "")
                                {
                                    Netplay.password = "";
                                    System.Console.WriteLine("Password disabled.");
                                }
                                else
                                {
                                    Netplay.password = text6;
                                    System.Console.WriteLine("Password: " + Netplay.password);
                                }
                            }
                            else
                            {
                                if (text == "say")
                                {
                                    System.Console.WriteLine("Usage: say <words>");
                                }
                                else
                                {
                                    if (text.Length >= 4 && text.Substring(0, 4) == "say ")
                                    {
                                        string text7 = text2.Substring(4);
                                        if (text7 == "")
                                        {
                                            System.Console.WriteLine("Usage: say <words>");
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("<Server> " + text7);
                                            NetMessage.SendData(25, -1, -1, "<Server> " + text7, 255, 255f, 240f, 20f);
                                        }
                                    }
                                    else
                                    {
                                        if (text.Length == 4 && text.Substring(0, 4) == "kick")
                                        {
                                            System.Console.WriteLine("Usage: kick <player>");
                                        }
                                        else
                                        {
                                            if (text.Length >= 5 && text.Substring(0, 5) == "kick ")
                                            {
                                                string text8 = text.Substring(5).ToLower();
                                                if (text8 == "")
                                                {
                                                    System.Console.WriteLine("Usage: kick <player>");
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < 255; i++)
                                                    {
                                                        if (Main.player[i].active && Main.player[i].name.ToLower() == text8)
                                                        {
                                                            NetMessage.SendData(2, i, -1, "Kicked from server.", 0, 0f, 0f, 0f);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (text.Length == 3 && text.Substring(0, 3) == "ban")
                                                {
                                                    System.Console.WriteLine("Usage: ban <player>");
                                                }
                                                else
                                                {
                                                    if (text.Length >= 4 && text.Substring(0, 4) == "ban ")
                                                    {
                                                        string text8 = text.Substring(4).ToLower();
                                                        if (text8 == "")
                                                        {
                                                            System.Console.WriteLine("Usage: ban <player>");
                                                        }
                                                        else
                                                        {
                                                            for (int i = 0; i < 255; i++)
                                                            {
                                                                if (Main.player[i].active && Main.player[i].name.ToLower() == text8)
                                                                {
                                                                    NetMessage.banIP(Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint.ToString().Split(new char[]
																	{
																		':'
																	})[0]);
                                                                    NetMessage.SendData(2, i, -1, "Banned from server.", 0, 0f, 0f, 0f);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("Invalid command.");
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
                catch
                {
                    System.Console.WriteLine("Invalid command.");
                }
            }
        }
        private static void StartInvasion()
        {
            if (WorldGen.shadowOrbSmashed && Main.invasionType == 0 && Main.invasionDelay == 0)
            {
                int num = 0;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i].statLife >= 200)
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    Main.invasionType = 1;
                    Main.invasionSize = 100 + 50 * num;
                    Main.invasionWarn = 0;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.invasionX = 0.0;
                    }
                    else
                    {
                        Main.invasionX = (double)Main.maxTilesX;
                    }
                }
            }
        }
        //[System.Runtime.InteropServices.DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        //private static extern uint TimeBeginPeriod(uint uMilliseconds);
        protected void UnloadContent()
        {
        }
        protected void Update()
        {
            if (!Main.showSplash)
            {
                if (!Main.gameMenu && Main.netMode != 2)
                {
                    Main.saveTimer++;
                    if (Main.saveTimer > 18000)
                    {
                        Main.saveTimer = 0;
                        WorldGen.saveToonWhilePlaying();
                    }
                }
                else
                {
                    Main.saveTimer = 0;
                }
                if (Main.rand.Next(99999) == 0)
                {
                    Main.rand = new System.Random((int)System.DateTime.Now.Ticks);
                }
                Main.updateTime++;
                if (Main.updateTime >= 60)
                {
                    Main.frameRate = Main.drawTime;
                    Main.updateTime = 0;
                    Main.drawTime = 0;
                    if (Main.frameRate == 60)
                    {
                        Lighting.lightPasses = 2;
                        Lighting.lightSkip = 0;
                        Main.cloudLimit = 100;
                        Gore.goreTime = 1200;
                    }
                    else
                    {
                        if (Main.frameRate >= 58)
                        {
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 0;
                            Main.cloudLimit = 100;
                            Gore.goreTime = 600;
                        }
                        else
                        {
                            if (Main.frameRate >= 43)
                            {
                                Lighting.lightPasses = 2;
                                Lighting.lightSkip = 1;
                                Main.cloudLimit = 75;
                                Gore.goreTime = 300;
                            }
                            else
                            {
                                if (Main.frameRate >= 28)
                                {
                                    if (!Main.gameMenu)
                                    {
                                        Liquid.maxLiquid = 3000;
                                        Liquid.cycles = 6;
                                    }
                                    Lighting.lightPasses = 2;
                                    Lighting.lightSkip = 2;
                                    Main.cloudLimit = 50;
                                    Gore.goreTime = 180;
                                }
                                else
                                {
                                    Lighting.lightPasses = 2;
                                    Lighting.lightSkip = 4;
                                    Main.cloudLimit = 0;
                                    Gore.goreTime = 0;
                                }
                            }
                        }
                    }
                    if (Liquid.quickSettle)
                    {
                        Liquid.maxLiquid = Liquid.resLiquid;
                        Liquid.cycles = 1;
                    }
                    else
                    {
                        if (Main.frameRate == 60)
                        {
                            Liquid.maxLiquid = 5000;
                            Liquid.cycles = 7;
                        }
                        else
                        {
                            if (Main.frameRate >= 58)
                            {
                                Liquid.maxLiquid = 5000;
                                Liquid.cycles = 12;
                            }
                            else
                            {
                                if (Main.frameRate >= 43)
                                {
                                    Liquid.maxLiquid = 4000;
                                    Liquid.cycles = 13;
                                }
                                else
                                {
                                    if (Main.frameRate >= 28)
                                    {
                                        Liquid.maxLiquid = 3500;
                                        Liquid.cycles = 15;
                                    }
                                    else
                                    {
                                        Liquid.maxLiquid = 3000;
                                        Liquid.cycles = 17;
                                    }
                                }
                            }
                        }
                    }
                    if (Main.netMode == 2)
                    {
                        Main.cloudLimit = 0;
                    }
                }
                Main.hasFocus = true;
                int i = 0;
                Main.chatRelease = true;
                for (i = 0; i < 255; i++)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.player[i].UpdatePlayer(i);
                        }
                        catch
                        {
                            Debug.WriteLine(string.Concat(new object[]
							{
								"Error: player[", 
								i, 
								"].UpdatePlayer(", 
								i, 
								")"
							}));
                        }
                    }
                    else
                    {
                        Main.player[i].UpdatePlayer(i);
                    }
                }
                if (Main.netMode != 1)
                {
                    NPC.SpawnNPC();
                }
                for (i = 0; i < 255; i++)
                {
                    Main.player[i].activeNPCs = 0;
                    Main.player[i].townNPCs = 0;
                }
                for (i = 0; i < 1000; i++)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.npc[i].UpdateNPC(i);
                        }
                        catch
                        {
                            Main.npc[i] = new NPC();
                            Debug.WriteLine(string.Concat(new object[]
							{
								"Error: npc[", 
								i, 
								"].UpdateNPC(", 
								i, 
								")"
							}));
                        }
                    }
                    else
                    {
                        Main.npc[i].UpdateNPC(i);
                    }
                }
                for (i = 0; i < 200; i++)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.gore[i].Update();
                        }
                        catch
                        {
                            Main.gore[i] = new Gore();
                            Debug.WriteLine("Error: gore[" + i + "].Update()");
                        }
                    }
                    else
                    {
                        Main.gore[i].Update();
                    }
                }
                for (i = 0; i < 1000; i++)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.projectile[i].Update(i);
                        }
                        catch
                        {
                            Main.projectile[i] = new Projectile();
                            Debug.WriteLine(string.Concat(new object[]
							{
								"Error: projectile[", 
								i, 
								"].Update(", 
								i, 
								")"
							}));
                        }
                    }
                    else
                    {
                        Main.projectile[i].Update(i);
                    }
                }
                for (i = 0; i < 200; i++)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.item[i].UpdateItem(i);
                        }
                        catch
                        {
                            Main.item[i] = new Item();
                            Debug.WriteLine(string.Concat(new object[]
							{
								"Error: item[", 
								i, 
								"].UpdateItem(", 
								i, 
								")"
							}));
                        }
                    }
                    else
                    {
                        Main.item[i].UpdateItem(i);
                    }
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        Dust.UpdateDust();
                    }
                    catch
                    {
                        for (i = 0; i < 2000; i++)
                        {
                            Main.dust[i] = new Dust();
                        }
                        Debug.WriteLine("Error: Dust.Update()");
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
                if (Main.ignoreErrors)
                {
                    try
                    {
                        Main.UpdateTime();
                    }
                    catch
                    {
                        Debug.WriteLine("Error: UpdateTime()");
                        Main.checkForSpawns = 0;
                    }
                }
                else
                {
                    Main.UpdateTime();
                }
                if (Main.netMode != 1)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            WorldGen.UpdateWorld();
                            Main.UpdateInvasion();
                        }
                        catch
                        {
                            Debug.WriteLine("Error: WorldGen.UpdateWorld()");
                        }
                    }
                    else
                    {
                        WorldGen.UpdateWorld();
                        Main.UpdateInvasion();
                    }
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        if (Main.netMode == 2)
                        {
                            Main.UpdateServer();
                        }
                        if (Main.netMode == 1)
                        {
                            Main.UpdateClient();
                        }
                    }
                    catch
                    {
                        if (Main.netMode == 2)
                        {
                            Debug.WriteLine("Error: UpdateServer()");
                        }
                        else
                        {
                            Debug.WriteLine("Error: UpdateClient();");
                        }
                    }
                }
                else
                {
                    if (Main.netMode == 2)
                    {
                        Main.UpdateServer();
                    }
                    if (Main.netMode == 1)
                    {
                        Main.UpdateClient();
                    }
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        for (i = 0; i < Main.numChatLines; i++)
                        {
                            if (Main.chatLine[i].showTime > 0)
                            {
                                ChatLine chatLine = Main.chatLine[i];
                                chatLine.showTime--;
                            }
                        }
                    }
                    catch
                    {
                        for (i = 0; i < Main.numChatLines; i++)
                        {
                            Main.chatLine[i] = new ChatLine();
                        }
                    }
                }
                else
                {
                    for (i = 0; i < Main.numChatLines; i++)
                    {
                        if (Main.chatLine[i].showTime > 0)
                        {
                            ChatLine chatLine2 = Main.chatLine[i];
                            chatLine2.showTime--;
                        }
                    }
                }
            }
        }
        private static void UpdateClient()
        {
            if (Main.myPlayer == 255)
            {
                Netplay.disconnect = true;
            }
            Main.netPlayCounter++;
            if (Main.netPlayCounter > 3600)
            {
                Main.netPlayCounter = 0;
            }
            if (System.Math.IEEERemainder((double)Main.netPlayCounter, 300.0) == 0.0)
            {
                NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
            }
            if (System.Math.IEEERemainder((double)Main.netPlayCounter, 600.0) == 0.0)
            {
                NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
            }
            if (Netplay.clientSock.active)
            {
                Netplay.clientSock.timeOut++;
                if (!Main.stopTimeOuts && Netplay.clientSock.timeOut > 60 * Main.timeOut)
                {
                    Main.statusText = "Connection timed out";
                    Netplay.disconnect = true;
                }
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.item[i].active && Main.item[i].owner == Main.myPlayer)
                {
                    Main.item[i].FindOwner(i);
                }
            }
        }
        private static void UpdateInvasion()
        {
            if (Main.invasionType > 0)
            {
                if (Main.invasionSize <= 0)
                {
                    Main.InvasionWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 7;
                }
                if (Main.invasionX != (double)Main.spawnTileX)
                {
                    float num = 0.2f;
                    if (Main.invasionX > (double)Main.spawnTileX)
                    {
                        Main.invasionX -= (double)num;
                        if (Main.invasionX <= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            Main.InvasionWarning();
                        }
                        else
                        {
                            Main.invasionWarn--;
                        }
                    }
                    else
                    {
                        if (Main.invasionX < (double)Main.spawnTileX)
                        {
                            Main.invasionX += (double)num;
                            if (Main.invasionX >= (double)Main.spawnTileX)
                            {
                                Main.invasionX = (double)Main.spawnTileX;
                                Main.InvasionWarning();
                            }
                            else
                            {
                                Main.invasionWarn--;
                            }
                        }
                    }
                    if (Main.invasionWarn <= 0)
                    {
                        Main.invasionWarn = 3600;
                        Main.InvasionWarning();
                    }
                }
            }
        }
        private static void UpdateMenu()
        {
            Main.playerInventory = false;
            if (Main.netMode == 0)
            {
                if (!Main.grabSky)
                {
                    Main.time += 86.4;
                    if (!Main.dayTime)
                    {
                        if (Main.time > 32400.0)
                        {
                            Main.bloodMoon = false;
                            Main.time = 0.0;
                            Main.dayTime = true;
                            Main.moonPhase++;
                            if (Main.moonPhase >= 8)
                            {
                                Main.moonPhase = 0;
                            }
                        }
                    }
                    else
                    {
                        if (Main.time > 54000.0)
                        {
                            Main.time = 0.0;
                            Main.dayTime = false;
                        }
                    }
                }
            }
            else
            {
                if (Main.netMode == 1)
                {
                    Main.UpdateTime();
                }
            }
        }
        private static void UpdateServer()
        {
            Main.netPlayCounter++;
            if (Main.netPlayCounter > 3600)
            {
                NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                NetMessage.syncPlayers();
                Main.netPlayCounter = 0;
            }
            System.Math.IEEERemainder((double)Main.netPlayCounter, 60.0);
            if (System.Math.IEEERemainder((double)Main.netPlayCounter, 360.0) == 0.0)
            {
                bool flag = true;
                int num = Main.lastItemUpdate;
                int num2 = 0;
                while (flag)
                {
                    num++;
                    if (num >= 200)
                    {
                        num = 0;
                    }
                    num2++;
                    if (!Main.item[num].active || Main.item[num].owner == 255)
                    {
                        NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f);
                    }
                    if (num2 >= Main.maxItemUpdates || num == Main.lastItemUpdate)
                    {
                        flag = false;
                    }
                }
                Main.lastItemUpdate = num;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.item[i].active && (Main.item[i].owner == 255 || !Main.player[Main.item[i].owner].active))
                {
                    Main.item[i].FindOwner(i);
                }
            }
            for (int i = 0; i < 255; i++)
            {
                if (Netplay.serverSock[i].active)
                {
                    ServerSock serverSock = Netplay.serverSock[i];
                    serverSock.timeOut++;
                    if (!Main.stopTimeOuts && Netplay.serverSock[i].timeOut > 60 * Main.timeOut)
                    {
                        Netplay.serverSock[i].kill = true;
                    }
                }
                if (Main.player[i].active)
                {
                    int sectionX = Netplay.GetSectionX((int)(Main.player[i].position.X / 16f));
                    int sectionY = Netplay.GetSectionY((int)(Main.player[i].position.Y / 16f));
                    int num3 = 0;
                    for (int j = sectionX - 1; j < sectionX + 2; j++)
                    {
                        for (int k = sectionY - 1; k < sectionY + 2; k++)
                        {
                            if (j >= 0 && j < Main.maxSectionsX && k >= 0 && k < Main.maxSectionsY && !Netplay.serverSock[i].tileSection[j, k])
                            {
                                num3++;
                            }
                        }
                    }
                    if (num3 > 0)
                    {
                        int num4 = num3 * 150;
                        NetMessage.SendData(9, i, -1, "Recieving tile data", num4, 0f, 0f, 0f);
                        Netplay.serverSock[i].statusText2 = "is recieving tile data";
                        ServerSock serverSock2 = Netplay.serverSock[i];
                        serverSock2.statusMax += num4;
                        for (int j = sectionX - 1; j < sectionX + 2; j++)
                        {
                            for (int k = sectionY - 1; k < sectionY + 2; k++)
                            {
                                if (j >= 0 && j < Main.maxSectionsX && k >= 0 && k < Main.maxSectionsY && !Netplay.serverSock[i].tileSection[j, k])
                                {
                                    NetMessage.SendSection(i, j, k);
                                    NetMessage.SendData(11, i, -1, "", j, (float)k, (float)j, (float)k);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void UpdateTime()
        {
            Main.time += 1.0;
            if (Main.dayTime)
            {
                if (Main.time > 54000.0)
                {
                    WorldGen.spawnNPC = 0;
                    Main.checkForSpawns = 0;
                    if (Main.rand.Next(50) == 0 && Main.netMode != 1 && WorldGen.shadowOrbSmashed)
                    {
                        WorldGen.spawnMeteor = true;
                    }
                    if (!NPC.downedBoss1 && Main.netMode != 1)
                    {
                        bool flag = false;
                        for (int i = 0; i < 255; i++)
                        {
                            if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag && Main.rand.Next(3) == 0)
                        {
                            int num = 0;
                            for (int i = 0; i < 1000; i++)
                            {
                                if (Main.npc[i].active && Main.npc[i].townNPC)
                                {
                                    num++;
                                }
                            }
                            if (num >= 4)
                            {
                                WorldGen.spawnEye = true;
                                NetMessage.SendData(25, -1, -1, "You feel an evil presence watching you...", 255, 50f, 255f, 130f);
                            }
                        }
                    }
                    if (!WorldGen.spawnEye && Main.moonPhase != 4 && Main.rand.Next(7) == 0 && Main.netMode != 1)
                    {
                        for (int i = 0; i < 255; i++)
                        {
                            if (Main.player[i].active && Main.player[i].statLifeMax > 100)
                            {
                                Main.bloodMoon = true;
                                break;
                            }
                        }
                        if (Main.bloodMoon)
                        {
                            NetMessage.SendData(25, -1, -1, "The Blood Moon is rising...", 255, 50f, 255f, 130f);
                        }
                    }
                    Main.time = 0.0;
                    Main.dayTime = false;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                    }
                }
                if (Main.netMode != 1)
                {
                    Main.checkForSpawns++;
                    if (Main.checkForSpawns >= 7200)
                    {
                        int num2 = 0;
                        for (int i = 0; i < 255; i++)
                        {
                            if (Main.player[i].active)
                            {
                                num2++;
                            }
                        }
                        Main.checkForSpawns = 0;
                        WorldGen.spawnNPC = 0;
                        int num3 = 0;
                        int num4 = 0;
                        int num5 = 0;
                        int num6 = 0;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = 0;
                        for (int i = 0; i < 1000; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].townNPC)
                            {
                                if (Main.npc[i].type != 37 && !Main.npc[i].homeless)
                                {
                                    WorldGen.QuickFindHome(i);
                                }
                                else
                                {
                                    num8++;
                                }
                                if (Main.npc[i].type == 17)
                                {
                                    num3++;
                                }
                                if (Main.npc[i].type == 18)
                                {
                                    num4++;
                                }
                                if (Main.npc[i].type == 19)
                                {
                                    num6++;
                                }
                                if (Main.npc[i].type == 20)
                                {
                                    num5++;
                                }
                                if (Main.npc[i].type == 22)
                                {
                                    num7++;
                                }
                                if (Main.npc[i].type == 38)
                                {
                                    num9++;
                                }
                                num10++;
                            }
                        }
                        if (WorldGen.spawnNPC == 0)
                        {
                            int num11 = 0;
                            bool flag2 = false;
                            int num12 = 0;
                            bool flag3 = false;
                            bool flag4 = false;
                            for (int i = 0; i < 255; i++)
                            {
                                if (Main.player[i].active)
                                {
                                    for (int j = 0; j < 44; j++)
                                    {
                                        if (Main.player[i].inventory[j] != null & Main.player[i].inventory[j].stack > 0)
                                        {
                                            if (Main.player[i].inventory[j].type == 71)
                                            {
                                                num11 += Main.player[i].inventory[j].stack;
                                            }
                                            if (Main.player[i].inventory[j].type == 72)
                                            {
                                                num11 += Main.player[i].inventory[j].stack * 100;
                                            }
                                            if (Main.player[i].inventory[j].type == 73)
                                            {
                                                num11 += Main.player[i].inventory[j].stack * 10000;
                                            }
                                            if (Main.player[i].inventory[j].type == 74)
                                            {
                                                num11 += Main.player[i].inventory[j].stack * 1000000;
                                            }
                                            if (Main.player[i].inventory[j].type == 95 || Main.player[i].inventory[j].type == 96 || Main.player[i].inventory[j].type == 97 || Main.player[i].inventory[j].type == 98 || Main.player[i].inventory[j].useAmmo == 14)
                                            {
                                                flag3 = true;
                                            }
                                            if (Main.player[i].inventory[j].type == 166 || Main.player[i].inventory[j].type == 167 || Main.player[i].inventory[j].type == 168 || Main.player[i].inventory[j].type == 235)
                                            {
                                                flag4 = true;
                                            }
                                        }
                                    }
                                    int num13 = Main.player[i].statLifeMax / 20;
                                    if (num13 > 5)
                                    {
                                        flag2 = true;
                                    }
                                    num12 += num13;
                                }
                            }
                            if (WorldGen.spawnNPC == 0 && num7 < 1)
                            {
                                WorldGen.spawnNPC = 22;
                            }
                            if (WorldGen.spawnNPC == 0 && (double)num11 > 5000.0 && num3 < 1)
                            {
                                WorldGen.spawnNPC = 17;
                            }
                            if (WorldGen.spawnNPC == 0 && flag2 && num4 < 1)
                            {
                                WorldGen.spawnNPC = 18;
                            }
                            if (WorldGen.spawnNPC == 0 && flag3 && num6 < 1)
                            {
                                WorldGen.spawnNPC = 19;
                            }
                            if (WorldGen.spawnNPC == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3) && num5 < 1)
                            {
                                WorldGen.spawnNPC = 20;
                            }
                            if (WorldGen.spawnNPC == 0 && flag4 && num3 > 0 && num9 < 1)
                            {
                                WorldGen.spawnNPC = 38;
                            }
                            if (WorldGen.spawnNPC == 0 && num11 > 100000 && num3 < 2 && num2 > 2)
                            {
                                WorldGen.spawnNPC = 17;
                            }
                            if (WorldGen.spawnNPC == 0 && num12 >= 20 && num4 < 2 && num2 > 2)
                            {
                                WorldGen.spawnNPC = 18;
                            }
                            if (WorldGen.spawnNPC == 0 && num11 > 5000000 && num3 < 3 && num2 > 4)
                            {
                                WorldGen.spawnNPC = 17;
                            }
                            if (!NPC.downedBoss3 && num8 == 0)
                            {
                                int num14 = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0);
                                Main.npc[num14].homeless = false;
                                Main.npc[num14].homeTileX = Main.dungeonX;
                                Main.npc[num14].homeTileY = Main.dungeonY;
                            }
                        }
                    }
                }
            }
            else
            {
                if (WorldGen.spawnEye && Main.netMode != 1 && Main.time > 4860.0)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active && !Main.player[i].dead && (double)Main.player[i].position.Y < Main.worldSurface * 16.0)
                        {
                            NPC.SpawnOnPlayer(i, 4);
                            WorldGen.spawnEye = false;
                            break;
                        }
                    }
                }
                if (Main.time > 32400.0)
                {
                    if (Main.invasionDelay > 0)
                    {
                        Main.invasionDelay--;
                    }
                    WorldGen.spawnNPC = 0;
                    Main.checkForSpawns = 0;
                    Main.time = 0.0;
                    Main.bloodMoon = false;
                    Main.dayTime = true;
                    Main.moonPhase++;
                    if (Main.moonPhase >= 8)
                    {
                        Main.moonPhase = 0;
                    }
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f);
                        WorldGen.saveAndPlay();
                    }
                    if (Main.netMode != 1 && Main.rand.Next(15) == 0)
                    {
                        Main.StartInvasion();
                    }
                }
                if (Main.time > 16200.0 && WorldGen.spawnMeteor)
                {
                    WorldGen.spawnMeteor = false;
                    WorldGen.dropMeteor();
                }
            }
        }
    }
}

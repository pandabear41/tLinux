using System;
namespace Terraria
{
    public class NPC
    {
        public bool active;
        public static int activeRangeX = Main.screenWidth * 2;
        public static int activeRangeY = Main.screenHeight * 2;
        public static int activeTime = 1000;
        public float[] ai = new float[NPC.maxAI];
        public int aiAction = 0;
        public int aiStyle;
        public int alpha;
        public bool behindTiles;
        public bool boss = false;
        public bool closeDoor = false;
        public bool collideX = false;
        public bool collideY = false;
        public Color color;
        public int damage;
        public static int defaultMaxSpawns = 4;
        public static int defaultSpawnRate = 700;
        public int defense;
        public int direction = 1;
        public int directionY = 1;
        public int doorX = 0;
        public int doorY = 0;
        public static bool downedBoss1 = false;
        public static bool downedBoss2 = false;
        public static bool downedBoss3 = false;
        public Rectangle frame;
        public double frameCounter;
        public bool friendly = false;
        public int friendlyRegen = 0;
        public int height;
        public bool homeless = false;
        public int homeTileX = -1;
        public int homeTileY = -1;
        public int[] immune = new int[256];
        public static int immuneTime = 20;
        public float knockBackResist = 1f;
        public bool lavaWet = false;
        public int life;
        public int lifeMax;
        public static int maxAI = 4;
        public static int maxSpawns = NPC.defaultMaxSpawns;
        public string name;
        public bool netUpdate = false;
        public bool noGravity = false;
        public bool noTileCollide = false;
        public int oldDirection = 0;
        public int oldDirectionY = 0;
        public Vector2 oldPosition;
        public int oldTarget = 0;
        public Vector2 oldVelocity;
        public Vector2 position;
        public float rotation = 0f;
        public static int safeRangeX = (int)((double)(Main.screenWidth / 16) * 0.55);
        public static int safeRangeY = (int)((double)(Main.screenHeight / 16) * 0.55);
        public float scale = 1f;
        public int soundDelay = 0;
        public int soundHit;
        public int soundKilled;
        public static int spawnRangeX = (int)((double)(Main.screenWidth / 16) * 1.2);
        public static int spawnRangeY = (int)((double)(Main.screenHeight / 16) * 1.2);
        public static int spawnRate = NPC.defaultSpawnRate;
        public static int spawnSpaceX = 4;
        public static int spawnSpaceY = 4;
        public int spriteDirection = -1;
        public int target = -1;
        public Rectangle targetRect;
        public int timeLeft;
        public bool townNPC = false;
        public static int townRangeX = Main.screenWidth * 3;
        public static int townRangeY = Main.screenHeight * 3;
        public int type;
        public float value;
        public Vector2 velocity;
        public bool wet = false;
        public byte wetCount = 0;
        public int whoAmI = 0;
        public int width;
        public void AI()
        {
            if (this.aiStyle == 0)
            {
                this.velocity.X = this.velocity.X * 0.93f;
                if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                {
                    this.velocity.X = 0f;
                }
            }
            else
            {
                if (this.aiStyle == 1)
                {
                    this.aiAction = 0;
                    if (this.ai[2] == 0f)
                    {
                        this.ai[0] = -100f;
                        this.ai[2] = 1f;
                        this.TargetClosest();
                    }
                    if (this.velocity.Y == 0f)
                    {
                        if (this.ai[3] == this.position.X)
                        {
                            this.direction *= -1;
                        }
                        this.ai[3] = 0f;
                        this.velocity.X = this.velocity.X * 0.8f;
                        if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                        {
                            this.velocity.X = 0f;
                        }
                        if (!Main.dayTime || this.life != this.lifeMax || (double)this.position.Y > Main.worldSurface * 16.0)
                        {
                            this.ai[0] += 1f;
                        }
                        this.ai[0] += 1f;
                        if (this.ai[0] >= 0f)
                        {
                            this.netUpdate = true;
                            if (!Main.dayTime || this.life != this.lifeMax || (double)this.position.Y > Main.worldSurface * 16.0)
                            {
                                this.TargetClosest();
                            }
                            if (this.ai[1] == 2f)
                            {
                                this.velocity.Y = -8f;
                                this.velocity.X = this.velocity.X + (float)(3 * this.direction);
                                this.ai[0] = -200f;
                                this.ai[1] = 0f;
                                this.ai[3] = this.position.X;
                            }
                            else
                            {
                                this.velocity.Y = -6f;
                                this.velocity.X = this.velocity.X + (float)(2 * this.direction);
                                this.ai[0] = -120f;
                                this.ai[1] += 1f;
                            }
                        }
                        else
                        {
                            if (this.ai[0] >= -30f)
                            {
                                this.aiAction = 1;
                            }
                        }
                    }
                    else
                    {
                        if (this.target < 255 && ((this.direction == 1 && this.velocity.X < 3f) || (this.direction == -1 && this.velocity.X > -3f)))
                        {
                            if ((this.direction == -1 && (double)this.velocity.X < 0.1) || (this.direction == 1 && (double)this.velocity.X > -0.1))
                            {
                                this.velocity.X = this.velocity.X + 0.2f * (float)this.direction;
                            }
                            else
                            {
                                this.velocity.X = this.velocity.X * 0.93f;
                            }
                        }
                    }
                }
                else
                {
                    if (this.aiStyle == 2)
                    {
                        this.noGravity = true;
                        if (this.collideX)
                        {
                            this.velocity.X = this.oldVelocity.X * -0.5f;
                            if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
                            {
                                this.velocity.X = 2f;
                            }
                            if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
                            {
                                this.velocity.X = -2f;
                            }
                        }
                        if (this.collideY)
                        {
                            this.velocity.Y = this.oldVelocity.Y * -0.5f;
                            if (this.velocity.Y > 0f && this.velocity.Y < 1f)
                            {
                                this.velocity.Y = 1f;
                            }
                            if (this.velocity.Y < 0f && this.velocity.Y > -1f)
                            {
                                this.velocity.Y = -1f;
                            }
                        }
                        if (Main.dayTime && (double)this.position.Y <= Main.worldSurface * 16.0 && this.type == 2)
                        {
                            if (this.timeLeft > 10)
                            {
                                this.timeLeft = 10;
                            }
                            this.directionY = -1;
                            if (this.velocity.Y > 0f)
                            {
                                this.direction = 1;
                            }
                            this.direction = -1;
                            if (this.velocity.X > 0f)
                            {
                                this.direction = 1;
                            }
                        }
                        else
                        {
                            this.TargetClosest();
                        }
                        if (this.direction == -1 && this.velocity.X > -4f)
                        {
                            this.velocity.X = this.velocity.X - 0.1f;
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X = this.velocity.X - 0.1f;
                            }
                            else
                            {
                                if (this.velocity.X > 0f)
                                {
                                    this.velocity.X = this.velocity.X + 0.05f;
                                }
                            }
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X = -4f;
                            }
                        }
                        else
                        {
                            if (this.direction == 1 && this.velocity.X < 4f)
                            {
                                this.velocity.X = this.velocity.X + 0.1f;
                                if (this.velocity.X < -4f)
                                {
                                    this.velocity.X = this.velocity.X + 0.1f;
                                }
                                else
                                {
                                    if (this.velocity.X < 0f)
                                    {
                                        this.velocity.X = this.velocity.X - 0.05f;
                                    }
                                }
                                if (this.velocity.X > 4f)
                                {
                                    this.velocity.X = 4f;
                                }
                            }
                        }
                        if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
                        {
                            this.velocity.Y = this.velocity.Y - 0.04f;
                            if ((double)this.velocity.Y > 1.5)
                            {
                                this.velocity.Y = this.velocity.Y - 0.05f;
                            }
                            else
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y = this.velocity.Y + 0.03f;
                                }
                            }
                            if ((double)this.velocity.Y < -1.5)
                            {
                                this.velocity.Y = -1.5f;
                            }
                        }
                        else
                        {
                            if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
                            {
                                this.velocity.Y = this.velocity.Y + 0.04f;
                                if ((double)this.velocity.Y < -1.5)
                                {
                                    this.velocity.Y = this.velocity.Y + 0.05f;
                                }
                                else
                                {
                                    if (this.velocity.Y < 0f)
                                    {
                                        this.velocity.Y = this.velocity.Y - 0.03f;
                                    }
                                }
                                if ((double)this.velocity.Y > 1.5)
                                {
                                    this.velocity.Y = 1.5f;
                                }
                            }
                        }
                        if (this.type == 2 && Main.rand.Next(40) == 0)
                        {
                            int num = 0;
                            Dust expr_A61_cp_0 = Main.dust[num];
                            expr_A61_cp_0.velocity.X = expr_A61_cp_0.velocity.X * 0.5f;
                            Dust expr_A7E_cp_0 = Main.dust[num];
                            expr_A7E_cp_0.velocity.Y = expr_A7E_cp_0.velocity.Y * 0.1f;
                        }
                    }
                    else
                    {
                        if (this.aiStyle == 3)
                        {
                            int num2 = 60;
                            bool flag = false;
                            if (this.velocity.Y == 0f && ((this.velocity.X > 0f && this.direction < 0) || (this.velocity.X < 0f && this.direction > 0)))
                            {
                                flag = true;
                            }
                            if (this.position.X == this.oldPosition.X || this.ai[3] >= (float)num2 || flag)
                            {
                                this.ai[3] += 1f;
                            }
                            else
                            {
                                if ((double)System.Math.Abs(this.velocity.X) > 0.9 && this.ai[3] > 0f)
                                {
                                    this.ai[3] -= 1f;
                                }
                            }
                            if (this.ai[3] > (float)(num2 * 10))
                            {
                                this.ai[3] = 0f;
                            }
                            if (this.ai[3] == (float)num2)
                            {
                                this.netUpdate = true;
                            }
                            if ((!Main.dayTime || (double)this.position.Y > Main.worldSurface * 16.0 || this.type == 26 || this.type == 27 || this.type == 28 || this.type == 31) && this.ai[3] < (float)num2)
                            {
                                if ((this.type != 3 && this.type != 21 && this.type != 31) || Main.rand.Next(1000) != 0)
                                {
                                    goto IL_CA5;
                                }
                            IL_CA5:
                                this.TargetClosest();
                            }
                            else
                            {
                                if (this.timeLeft > 10)
                                {
                                    this.timeLeft = 10;
                                }
                                if (this.velocity.X == 0f)
                                {
                                    if (this.velocity.Y == 0f)
                                    {
                                        this.ai[0] += 1f;
                                        if (this.ai[0] >= 2f)
                                        {
                                            this.direction *= -1;
                                            this.spriteDirection = this.direction;
                                            this.ai[0] = 0f;
                                        }
                                    }
                                }
                                else
                                {
                                    this.ai[0] = 0f;
                                }
                                if (this.direction == 0)
                                {
                                    this.direction = 1;
                                }
                            }
                            if (this.type == 27)
                            {
                                if (this.velocity.X < -2f || this.velocity.X > 2f)
                                {
                                    if (this.velocity.Y == 0f)
                                    {
                                        this.velocity *= 0.8f;
                                    }
                                }
                                else
                                {
                                    if (this.velocity.X < 2f && this.direction == 1)
                                    {
                                        this.velocity.X = this.velocity.X + 0.07f;
                                        if (this.velocity.X > 2f)
                                        {
                                            this.velocity.X = 2f;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.X > -2f && this.direction == -1)
                                        {
                                            this.velocity.X = this.velocity.X - 0.07f;
                                            if (this.velocity.X < -2f)
                                            {
                                                this.velocity.X = -2f;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (this.type == 21 || this.type == 26 || this.type == 31)
                                {
                                    if (this.velocity.X < -1.5f || this.velocity.X > 1.5f)
                                    {
                                        if (this.velocity.Y == 0f)
                                        {
                                            this.velocity *= 0.8f;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.X < 1.5f && this.direction == 1)
                                        {
                                            this.velocity.X = this.velocity.X + 0.07f;
                                            if (this.velocity.X > 1.5f)
                                            {
                                                this.velocity.X = 1.5f;
                                            }
                                        }
                                        else
                                        {
                                            if (this.velocity.X > -1.5f && this.direction == -1)
                                            {
                                                this.velocity.X = this.velocity.X - 0.07f;
                                                if (this.velocity.X < -1.5f)
                                                {
                                                    this.velocity.X = -1.5f;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.velocity.X < -1f || this.velocity.X > 1f)
                                    {
                                        if (this.velocity.Y == 0f)
                                        {
                                            this.velocity *= 0.8f;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.X < 1f && this.direction == 1)
                                        {
                                            this.velocity.X = this.velocity.X + 0.07f;
                                            if (this.velocity.X > 1f)
                                            {
                                                this.velocity.X = 1f;
                                            }
                                        }
                                        else
                                        {
                                            if (this.velocity.X > -1f && this.direction == -1)
                                            {
                                                this.velocity.X = this.velocity.X - 0.07f;
                                                if (this.velocity.X < -1f)
                                                {
                                                    this.velocity.X = -1f;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (this.velocity.Y == 0f)
                            {
                                int num3 = (int)((this.position.X + (float)(this.width / 2) + (float)(15 * this.direction)) / 16f);
                                int num4 = (int)((this.position.Y + (float)this.height - 16f) / 16f);
                                if (Main.tile[num3, num4] == null)
                                {
                                    Main.tile[num3, num4] = new Tile();
                                }
                                if (Main.tile[num3, num4 - 1] == null)
                                {
                                    Main.tile[num3, num4 - 1] = new Tile();
                                }
                                if (Main.tile[num3, num4 - 2] == null)
                                {
                                    Main.tile[num3, num4 - 2] = new Tile();
                                }
                                if (Main.tile[num3, num4 - 3] == null)
                                {
                                    Main.tile[num3, num4 - 3] = new Tile();
                                }
                                if (Main.tile[num3, num4 + 1] == null)
                                {
                                    Main.tile[num3, num4 + 1] = new Tile();
                                }
                                if (Main.tile[num3 + this.direction, num4 - 1] == null)
                                {
                                    Main.tile[num3 + this.direction, num4 - 1] = new Tile();
                                }
                                if (Main.tile[num3 + this.direction, num4 + 1] == null)
                                {
                                    Main.tile[num3 + this.direction, num4 + 1] = new Tile();
                                }
                                if (Main.tile[num3, num4 - 1].active && Main.tile[num3, num4 - 1].type == 10)
                                {
                                    this.ai[2] += 1f;
                                    this.ai[3] = 0f;
                                    if (this.ai[2] >= 60f)
                                    {
                                        if (!Main.bloodMoon && this.type == 3)
                                        {
                                            this.ai[1] = 0f;
                                        }
                                        this.velocity.X = 0.5f * (float)(-(float)this.direction);
                                        this.ai[1] += 1f;
                                        if (this.type == 27)
                                        {
                                            this.ai[1] += 1f;
                                        }
                                        if (this.type == 31)
                                        {
                                            this.ai[1] += 6f;
                                        }
                                        this.ai[2] = 0f;
                                        bool flag2 = false;
                                        if (this.ai[1] >= 10f)
                                        {
                                            flag2 = true;
                                            this.ai[1] = 10f;
                                        }
                                        WorldGen.KillTile(num3, num4 - 1, true, false, false);
                                        if ((Main.netMode != 1 || !flag2) && flag2 && Main.netMode != 1)
                                        {
                                            if (this.type == 26)
                                            {
                                                WorldGen.KillTile(num3, num4 - 1, false, false, false);
                                                if (Main.netMode == 2)
                                                {
                                                    NetMessage.SendData(17, -1, -1, "", 0, (float)num3, (float)(num4 - 1), 0f);
                                                }
                                            }
                                            else
                                            {
                                                bool flag3 = WorldGen.OpenDoor(num3, num4, this.direction);
                                                if (!flag3)
                                                {
                                                    this.ai[3] = (float)num2;
                                                    this.netUpdate = true;
                                                }
                                                if (Main.netMode == 2 && flag3)
                                                {
                                                    NetMessage.SendData(19, -1, -1, "", 0, (float)num3, (float)num4, (float)this.direction);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((this.velocity.X < 0f && this.spriteDirection == -1) || (this.velocity.X > 0f && this.spriteDirection == 1))
                                    {
                                        if (Main.tile[num3, num4 - 2].active && Main.tileSolid[(int)Main.tile[num3, num4 - 2].type])
                                        {
                                            if (Main.tile[num3, num4 - 3].active && Main.tileSolid[(int)Main.tile[num3, num4 - 3].type])
                                            {
                                                this.velocity.Y = -8f;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                this.velocity.Y = -7f;
                                                this.netUpdate = true;
                                            }
                                        }
                                        else
                                        {
                                            if (Main.tile[num3, num4 - 1].active && Main.tileSolid[(int)Main.tile[num3, num4 - 1].type])
                                            {
                                                this.velocity.Y = -6f;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                if (Main.tile[num3, num4].active && Main.tileSolid[(int)Main.tile[num3, num4].type])
                                                {
                                                    this.velocity.Y = -5f;
                                                    this.netUpdate = true;
                                                }
                                                else
                                                {
                                                    if (this.directionY < 0 && (!Main.tile[num3, num4 + 1].active || !Main.tileSolid[(int)Main.tile[num3, num4 + 1].type]) && (!Main.tile[num3 + this.direction, num4 + 1].active || !Main.tileSolid[(int)Main.tile[num3 + this.direction, num4 + 1].type]))
                                                    {
                                                        this.velocity.Y = -8f;
                                                        this.velocity.X = this.velocity.X * 1.5f;
                                                        this.netUpdate = true;
                                                    }
                                                    else
                                                    {
                                                        this.ai[1] = 0f;
                                                        this.ai[2] = 0f;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (this.type == 31 && this.velocity.Y == 0f && System.Math.Abs(this.position.X + (float)(this.width / 2) - (Main.player[this.target].position.X - (float)(Main.player[this.target].width / 2))) < 100f && System.Math.Abs(this.position.Y + (float)(this.height / 2) - (Main.player[this.target].position.Y - (float)(Main.player[this.target].height / 2))) < 50f && ((this.direction > 0 && this.velocity.X > 1f) || (this.direction < 0 && this.velocity.X < -1f)))
                                    {
                                        this.velocity.X = this.velocity.X * 2f;
                                        if (this.velocity.X > 3f)
                                        {
                                            this.velocity.X = 3f;
                                        }
                                        if (this.velocity.X < -3f)
                                        {
                                            this.velocity.X = -3f;
                                        }
                                        this.velocity.Y = -4f;
                                        this.netUpdate = true;
                                    }
                                }
                            }
                            else
                            {
                                this.ai[1] = 0f;
                                this.ai[2] = 0f;
                            }
                        }
                        else
                        {
                            if (this.aiStyle == 4)
                            {
                                if (this.target < 0 || this.target == 255 || Main.player[this.target].dead || !Main.player[this.target].active)
                                {
                                    this.TargetClosest();
                                }
                                bool dead = Main.player[this.target].dead;
                                float num5 = this.position.X + (float)(this.width / 2) - Main.player[this.target].position.X - (float)(Main.player[this.target].width / 2);
                                float num6 = this.position.Y + (float)this.height - 59f - Main.player[this.target].position.Y - (float)(Main.player[this.target].height / 2);
                                float num7 = (float)System.Math.Atan2((double)num6, (double)num5) + 1.57f;
                                if (num7 < 0f)
                                {
                                    num7 += 6.283f;
                                }
                                else
                                {
                                    if ((double)num7 > 6.283)
                                    {
                                        num7 -= 6.283f;
                                    }
                                }
                                float num8 = 0f;
                                if (this.ai[0] == 0f && this.ai[1] == 0f)
                                {
                                    num8 = 0.02f;
                                }
                                if (this.ai[0] == 0f && this.ai[1] == 2f && this.ai[2] > 40f)
                                {
                                    num8 = 0.05f;
                                }
                                if (this.ai[0] == 3f && this.ai[1] == 0f)
                                {
                                    num8 = 0.05f;
                                }
                                if (this.ai[0] == 3f && this.ai[1] == 2f && this.ai[2] > 40f)
                                {
                                    num8 = 0.08f;
                                }
                                if (this.rotation < num7)
                                {
                                    if ((double)(num7 - this.rotation) > 3.1415)
                                    {
                                        this.rotation -= num8;
                                    }
                                    else
                                    {
                                        this.rotation += num8;
                                    }
                                }
                                else
                                {
                                    if (this.rotation > num7)
                                    {
                                        if ((double)(this.rotation - num7) > 3.1415)
                                        {
                                            this.rotation += num8;
                                        }
                                        else
                                        {
                                            this.rotation -= num8;
                                        }
                                    }
                                }
                                if (this.rotation > num7 - num8 && this.rotation < num7 + num8)
                                {
                                    this.rotation = num7;
                                }
                                if (this.rotation < 0f)
                                {
                                    this.rotation += 6.283f;
                                }
                                else
                                {
                                    if ((double)this.rotation > 6.283)
                                    {
                                        this.rotation -= 6.283f;
                                    }
                                }
                                if (this.rotation > num7 - num8 && this.rotation < num7 + num8)
                                {
                                    this.rotation = num7;
                                }
                                if (Main.rand.Next(5) == 0)
                                {
                                    Color color = default(Color);
                                    int num = 0;
                                    Dust expr_1DF8_cp_0 = Main.dust[num];
                                    expr_1DF8_cp_0.velocity.X = expr_1DF8_cp_0.velocity.X * 0.5f;
                                    Dust expr_1E15_cp_0 = Main.dust[num];
                                    expr_1E15_cp_0.velocity.Y = expr_1E15_cp_0.velocity.Y * 0.1f;
                                }
                                if (Main.dayTime || dead)
                                {
                                    this.velocity.Y = this.velocity.Y - 0.04f;
                                    if (this.timeLeft > 10)
                                    {
                                        this.timeLeft = 10;
                                    }
                                }
                                else
                                {
                                    if (this.ai[0] == 0f)
                                    {
                                        if (this.ai[1] == 0f)
                                        {
                                            float num9 = 5f;
                                            float num10 = 0.04f;
                                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                            float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                            float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - 200f - vector.Y;
                                            float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                            float num14 = num13;
                                            num13 = num9 / num13;
                                            num11 *= num13;
                                            num12 *= num13;
                                            if (this.velocity.X < num11)
                                            {
                                                this.velocity.X = this.velocity.X + num10;
                                                if (this.velocity.X < 0f && num11 > 0f)
                                                {
                                                    this.velocity.X = this.velocity.X + num10;
                                                }
                                            }
                                            else
                                            {
                                                if (this.velocity.X > num11)
                                                {
                                                    this.velocity.X = this.velocity.X - num10;
                                                    if (this.velocity.X > 0f && num11 < 0f)
                                                    {
                                                        this.velocity.X = this.velocity.X - num10;
                                                    }
                                                }
                                            }
                                            if (this.velocity.Y < num12)
                                            {
                                                this.velocity.Y = this.velocity.Y + num10;
                                                if (this.velocity.Y < 0f && num12 > 0f)
                                                {
                                                    this.velocity.Y = this.velocity.Y + num10;
                                                }
                                            }
                                            else
                                            {
                                                if (this.velocity.Y > num12)
                                                {
                                                    this.velocity.Y = this.velocity.Y - num10;
                                                    if (this.velocity.Y > 0f && num12 < 0f)
                                                    {
                                                        this.velocity.Y = this.velocity.Y - num10;
                                                    }
                                                }
                                            }
                                            this.ai[2] += 1f;
                                            if (this.ai[2] >= 600f)
                                            {
                                                this.ai[1] = 1f;
                                                this.ai[2] = 0f;
                                                this.ai[3] = 0f;
                                                this.target = 255;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                if (this.position.Y + (float)this.height < Main.player[this.target].position.Y && num14 < 500f)
                                                {
                                                    if (!Main.player[this.target].dead)
                                                    {
                                                        this.ai[3] += 1f;
                                                    }
                                                    if (this.ai[3] >= 90f)
                                                    {
                                                        this.ai[3] = 0f;
                                                        this.rotation = num7;
                                                        float num15 = 5f;
                                                        float num16 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                        float num17 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                        float num18 = (float)System.Math.Sqrt((double)(num16 * num16 + num17 * num17));
                                                        num18 = num15 / num18;
                                                        Vector2 vector2 = vector;
                                                        Vector2 vector3;
                                                        vector3.X = num16 * num18;
                                                        vector3.Y = num17 * num18;
                                                        vector2.X += vector3.X * 10f;
                                                        vector2.Y += vector3.Y * 10f;
                                                        if (Main.netMode != 1)
                                                        {
                                                            int num19 = NPC.NewNPC((int)vector2.X, (int)vector2.Y, 5, 0);
                                                            Main.npc[num19].velocity.X = vector3.X;
                                                            Main.npc[num19].velocity.Y = vector3.Y;
                                                            if (Main.netMode == 2 && num19 < 1000)
                                                            {
                                                                NetMessage.SendData(23, -1, -1, "", num19, 0f, 0f, 0f);
                                                            }
                                                        }
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            Color color = default(Color);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (this.ai[1] == 1f)
                                            {
                                                this.rotation = num7;
                                                float num9 = 7f;
                                                Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                num13 = num9 / num13;
                                                this.velocity.X = num11 * num13;
                                                this.velocity.Y = num12 * num13;
                                                this.ai[1] = 2f;
                                            }
                                            else
                                            {
                                                if (this.ai[1] == 2f)
                                                {
                                                    this.ai[2] += 1f;
                                                    if (this.ai[2] >= 40f)
                                                    {
                                                        this.velocity.X = this.velocity.X * 0.98f;
                                                        this.velocity.Y = this.velocity.Y * 0.98f;
                                                        if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                                                        {
                                                            this.velocity.X = 0f;
                                                        }
                                                        if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
                                                        {
                                                            this.velocity.Y = 0f;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
                                                    }
                                                    if (this.ai[2] >= 120f)
                                                    {
                                                        this.ai[3] += 1f;
                                                        this.ai[2] = 0f;
                                                        this.target = 255;
                                                        this.rotation = num7;
                                                        if (this.ai[3] >= 3f)
                                                        {
                                                            this.ai[1] = 0f;
                                                            this.ai[3] = 0f;
                                                        }
                                                        else
                                                        {
                                                            this.ai[1] = 1f;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if ((double)this.life < (double)this.lifeMax * 0.5)
                                        {
                                            this.ai[0] = 1f;
                                            this.ai[1] = 0f;
                                            this.ai[2] = 0f;
                                            this.ai[3] = 0f;
                                            this.netUpdate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (this.ai[0] == 1f || this.ai[0] == 2f)
                                        {
                                            if (this.ai[0] == 1f)
                                            {
                                                this.ai[2] += 0.005f;
                                                if ((double)this.ai[2] > 0.5)
                                                {
                                                    this.ai[2] = 0.5f;
                                                }
                                            }
                                            else
                                            {
                                                this.ai[2] -= 0.005f;
                                                if (this.ai[2] < 0f)
                                                {
                                                    this.ai[2] = 0f;
                                                }
                                            }
                                            this.rotation += this.ai[2];
                                            this.ai[1] += 1f;
                                            if (this.ai[1] == 100f)
                                            {
                                                this.ai[0] += 1f;
                                                this.ai[1] = 0f;
                                                if (this.ai[0] == 3f)
                                                {
                                                    this.ai[2] = 0f;
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < 2; i++)
                                                    {
                                                        Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8);
                                                        Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
                                                        Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
                                                    }
                                                    for (int i = 0; i < 20; i++)
                                                    {
                                                        Color color = default(Color);
                                                    }
                                                }
                                            }
                                            this.velocity.X = this.velocity.X * 0.98f;
                                            this.velocity.Y = this.velocity.Y * 0.98f;
                                            if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                                            {
                                                this.velocity.X = 0f;
                                            }
                                            if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
                                            {
                                                this.velocity.Y = 0f;
                                            }
                                        }
                                        else
                                        {
                                            this.damage = 30;
                                            this.defense = 6;
                                            if (this.ai[1] == 0f)
                                            {
                                                float num9 = 6f;
                                                float num10 = 0.07f;
                                                Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - 120f - vector.Y;
                                                float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                num13 = num9 / num13;
                                                num11 *= num13;
                                                num12 *= num13;
                                                if (this.velocity.X < num11)
                                                {
                                                    this.velocity.X = this.velocity.X + num10;
                                                    if (this.velocity.X < 0f && num11 > 0f)
                                                    {
                                                        this.velocity.X = this.velocity.X + num10;
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.velocity.X > num11)
                                                    {
                                                        this.velocity.X = this.velocity.X - num10;
                                                        if (this.velocity.X > 0f && num11 < 0f)
                                                        {
                                                            this.velocity.X = this.velocity.X - num10;
                                                        }
                                                    }
                                                }
                                                if (this.velocity.Y < num12)
                                                {
                                                    this.velocity.Y = this.velocity.Y + num10;
                                                    if (this.velocity.Y < 0f && num12 > 0f)
                                                    {
                                                        this.velocity.Y = this.velocity.Y + num10;
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.velocity.Y > num12)
                                                    {
                                                        this.velocity.Y = this.velocity.Y - num10;
                                                        if (this.velocity.Y > 0f && num12 < 0f)
                                                        {
                                                            this.velocity.Y = this.velocity.Y - num10;
                                                        }
                                                    }
                                                }
                                                this.ai[2] += 1f;
                                                if (this.ai[2] >= 200f)
                                                {
                                                    this.ai[1] = 1f;
                                                    this.ai[2] = 0f;
                                                    this.ai[3] = 0f;
                                                    this.target = 255;
                                                    this.netUpdate = true;
                                                }
                                            }
                                            else
                                            {
                                                if (this.ai[1] == 1f)
                                                {
                                                    this.rotation = num7;
                                                    float num9 = 8f;
                                                    Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                    float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                    float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                    float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                    num13 = num9 / num13;
                                                    this.velocity.X = num11 * num13;
                                                    this.velocity.Y = num12 * num13;
                                                    this.ai[1] = 2f;
                                                }
                                                else
                                                {
                                                    if (this.ai[1] == 2f)
                                                    {
                                                        this.ai[2] += 1f;
                                                        if (this.ai[2] >= 40f)
                                                        {
                                                            this.velocity.X = this.velocity.X * 0.97f;
                                                            this.velocity.Y = this.velocity.Y * 0.97f;
                                                            if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                                                            {
                                                                this.velocity.X = 0f;
                                                            }
                                                            if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
                                                            {
                                                                this.velocity.Y = 0f;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
                                                        }
                                                        if (this.ai[2] >= 100f)
                                                        {
                                                            this.ai[3] += 1f;
                                                            this.ai[2] = 0f;
                                                            this.target = 255;
                                                            this.rotation = num7;
                                                            if (this.ai[3] >= 3f)
                                                            {
                                                                this.ai[1] = 0f;
                                                                this.ai[3] = 0f;
                                                            }
                                                            else
                                                            {
                                                                this.ai[1] = 1f;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (this.aiStyle == 5)
                                {
                                    if (this.target < 0 || this.target == 255 || Main.player[this.target].dead)
                                    {
                                        this.TargetClosest();
                                    }
                                    float num9 = 6f;
                                    float num10 = 0.05f;
                                    if (this.type == 6)
                                    {
                                        num9 = 4f;
                                        num10 = 0.02f;
                                    }
                                    else
                                    {
                                        if (this.type == 23)
                                        {
                                            num9 = 2.5f;
                                            num10 = 0.02f;
                                        }
                                    }
                                    Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                    float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                    float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                    float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                    num13 = num9 / num13;
                                    num11 *= num13;
                                    num12 *= num13;
                                    if (Main.player[this.target].dead)
                                    {
                                        num11 = (float)this.direction * num9 / 2f;
                                        num12 = -num9 / 2f;
                                    }
                                    if (this.velocity.X < num11)
                                    {
                                        this.velocity.X = this.velocity.X + num10;
                                        if (this.velocity.X < 0f && num11 > 0f)
                                        {
                                            this.velocity.X = this.velocity.X + num10;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.X > num11)
                                        {
                                            this.velocity.X = this.velocity.X - num10;
                                            if (this.velocity.X > 0f && num11 < 0f)
                                            {
                                                this.velocity.X = this.velocity.X - num10;
                                            }
                                        }
                                    }
                                    if (this.velocity.Y < num12)
                                    {
                                        this.velocity.Y = this.velocity.Y + num10;
                                        if (this.velocity.Y < 0f && num12 > 0f)
                                        {
                                            this.velocity.Y = this.velocity.Y + num10;
                                        }
                                    }
                                    else
                                    {
                                        if (this.velocity.Y > num12)
                                        {
                                            this.velocity.Y = this.velocity.Y - num10;
                                            if (this.velocity.Y > 0f && num12 < 0f)
                                            {
                                                this.velocity.Y = this.velocity.Y - num10;
                                            }
                                        }
                                    }
                                    if (this.type == 23)
                                    {
                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X);
                                    }
                                    else
                                    {
                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
                                    }
                                    if (this.type == 6 || this.type == 23)
                                    {
                                        if (this.collideX)
                                        {
                                            this.netUpdate = true;
                                            this.velocity.X = this.oldVelocity.X * -0.7f;
                                            if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
                                            {
                                                this.velocity.X = 2f;
                                            }
                                            if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
                                            {
                                                this.velocity.X = -2f;
                                            }
                                        }
                                        if (this.collideY)
                                        {
                                            this.netUpdate = true;
                                            this.velocity.Y = this.oldVelocity.Y * -0.7f;
                                            if (this.velocity.Y > 0f && this.velocity.Y < 2f)
                                            {
                                                this.velocity.Y = 2f;
                                            }
                                            if (this.velocity.Y < 0f && this.velocity.Y > -2f)
                                            {
                                                this.velocity.Y = -2f;
                                            }
                                        }
                                        if (this.type == 23)
                                        {
                                            int num = 0;
                                            Main.dust[num].noGravity = true;
                                            Dust expr_3669_cp_0 = Main.dust[num];
                                            expr_3669_cp_0.velocity.X = expr_3669_cp_0.velocity.X * 0.3f;
                                            Dust expr_3686_cp_0 = Main.dust[num];
                                            expr_3686_cp_0.velocity.Y = expr_3686_cp_0.velocity.Y * 0.3f;
                                        }
                                        else
                                        {
                                            if (Main.rand.Next(20) == 0)
                                            {
                                                int num = 0;
                                                Dust expr_36C1_cp_0 = Main.dust[num];
                                                expr_36C1_cp_0.velocity.X = expr_36C1_cp_0.velocity.X * 0.5f;
                                                Dust expr_36DE_cp_0 = Main.dust[num];
                                                expr_36DE_cp_0.velocity.Y = expr_36DE_cp_0.velocity.Y * 0.1f;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Main.rand.Next(40) == 0)
                                        {
                                            int num = 0;
                                            Dust expr_371A_cp_0 = Main.dust[num];
                                            expr_371A_cp_0.velocity.X = expr_371A_cp_0.velocity.X * 0.5f;
                                            Dust expr_3737_cp_0 = Main.dust[num];
                                            expr_3737_cp_0.velocity.Y = expr_3737_cp_0.velocity.Y * 0.1f;
                                        }
                                    }
                                    if ((Main.dayTime && this.type != 6 && this.type != 23) || Main.player[this.target].dead)
                                    {
                                        this.velocity.Y = this.velocity.Y - num10 * 2f;
                                        if (this.timeLeft > 10)
                                        {
                                            this.timeLeft = 10;
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.aiStyle != 6)
                                    {
                                        if (this.aiStyle != 7)
                                        {
                                            if (this.aiStyle == 8)
                                            {
                                                this.TargetClosest();
                                                this.velocity.X = this.velocity.X * 0.93f;
                                                if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
                                                {
                                                    this.velocity.X = 0f;
                                                }
                                                if (this.ai[0] == 0f)
                                                {
                                                    this.ai[0] = 500f;
                                                }
                                                if (this.ai[2] != 0f && this.ai[3] != 0f)
                                                {
                                                    for (int i = 0; i < 50; i++)
                                                    {
                                                        if (this.type == 29 || this.type == 45)
                                                        {
                                                            Color color = default(Color);
                                                            int num = 0;
                                                            Dust dust = Main.dust[num];
                                                            dust.velocity *= 3f;
                                                            if (Main.dust[num].scale > 1f)
                                                            {
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.type == 32)
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Dust dust2 = Main.dust[num];
                                                                dust2.velocity *= 3f;
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                            else
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Dust dust3 = Main.dust[num];
                                                                dust3.velocity *= 3f;
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                        }
                                                    }
                                                    this.position.X = this.ai[2] * 16f - (float)(this.width / 2) + 8f;
                                                    this.position.Y = this.ai[3] * 16f - (float)this.height;
                                                    this.velocity.X = 0f;
                                                    this.velocity.Y = 0f;
                                                    this.ai[2] = 0f;
                                                    this.ai[3] = 0f;
                                                    for (int i = 0; i < 50; i++)
                                                    {
                                                        if (this.type == 29 || this.type == 45)
                                                        {
                                                            Color color = default(Color);
                                                            int num = 0;
                                                            Dust dust4 = Main.dust[num];
                                                            dust4.velocity *= 3f;
                                                            if (Main.dust[num].scale > 1f)
                                                            {
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.type == 32)
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Dust dust5 = Main.dust[num];
                                                                dust5.velocity *= 3f;
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                            else
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Dust dust6 = Main.dust[num];
                                                                dust6.velocity *= 3f;
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                this.ai[0] += 1f;
                                                if (this.ai[0] == 75f || this.ai[0] == 150f || this.ai[0] == 225f)
                                                {
                                                    this.ai[1] = 30f;
                                                    this.netUpdate = true;
                                                }
                                                else
                                                {
                                                    if (this.ai[0] >= 450f && Main.netMode != 1)
                                                    {
                                                        this.ai[0] = 1f;
                                                        int num20 = (int)Main.player[this.target].position.X / 16;
                                                        int num21 = (int)Main.player[this.target].position.Y / 16;
                                                        int num22 = (int)this.position.X / 16;
                                                        int num23 = (int)this.position.Y / 16;
                                                        int num24 = 20;
                                                        int num25 = 0;
                                                        bool flag4 = false;
                                                        if (System.Math.Abs(this.position.X - Main.player[this.target].position.X) + System.Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
                                                        {
                                                            num25 = 100;
                                                            flag4 = true;
                                                        }
                                                        while (!flag4 && num25 < 100)
                                                        {
                                                            num25++;
                                                            int num26 = Main.rand.Next(num20 - num24, num20 + num24);
                                                            for (int j = Main.rand.Next(num21 - num24, num21 + num24); j < num21 + num24; j++)
                                                            {
                                                                if ((j < num21 - 4 || j > num21 + 4 || num26 < num20 - 4 || num26 > num20 + 4) && (j < num23 - 1 || j > num23 + 1 || num26 < num22 - 1 || num26 > num22 + 1) && Main.tile[num26, j].active)
                                                                {
                                                                    bool flag5 = true;
                                                                    if (this.type == 32 && Main.tile[num26, j - 1].wall == 0)
                                                                    {
                                                                        flag5 = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Main.tile[num26, j - 1].lava)
                                                                        {
                                                                            flag5 = false;
                                                                        }
                                                                    }
                                                                    if (flag5 && Main.tileSolid[(int)Main.tile[num26, j].type] && !Collision.SolidTiles(num26 - 1, num26 + 1, j - 4, j - 1))
                                                                    {
                                                                        this.ai[1] = 20f;
                                                                        this.ai[2] = (float)num26;
                                                                        this.ai[3] = (float)j;
                                                                        flag4 = true;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        this.netUpdate = true;
                                                    }
                                                }
                                                if (this.ai[1] > 0f)
                                                {
                                                    this.ai[1] -= 1f;
                                                    if (this.ai[1] == 25f)
                                                    {
                                                        if (Main.netMode != 1)
                                                        {
                                                            if (this.type == 29 || this.type == 45)
                                                            {
                                                                NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y - 8, 30, 0);
                                                            }
                                                            else
                                                            {
                                                                if (this.type == 32)
                                                                {
                                                                    NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y - 8, 33, 0);
                                                                }
                                                                else
                                                                {
                                                                    NPC.NewNPC((int)this.position.X + this.width / 2 + this.direction * 8, (int)this.position.Y + 20, 25, 0);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (this.type == 29 || this.type == 45)
                                                {
                                                    if (Main.rand.Next(5) == 0)
                                                    {
                                                        int num27 = 0;
                                                        Main.dust[num27].noGravity = true;
                                                        Dust expr_401C_cp_0 = Main.dust[num27];
                                                        expr_401C_cp_0.velocity.X = expr_401C_cp_0.velocity.X * 0.5f;
                                                        Main.dust[num27].velocity.Y = -2f;
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.type == 32)
                                                    {
                                                        if (Main.rand.Next(2) == 0)
                                                        {
                                                            int num27 = 0;
                                                            Main.dust[num27].noGravity = true;
                                                            Dust expr_4095_cp_0 = Main.dust[num27];
                                                            expr_4095_cp_0.velocity.X = expr_4095_cp_0.velocity.X * 1f;
                                                            Dust expr_40B3_cp_0 = Main.dust[num27];
                                                            expr_40B3_cp_0.velocity.Y = expr_40B3_cp_0.velocity.Y * 1f;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Main.rand.Next(2) == 0)
                                                        {
                                                            int num27 = 0;
                                                            Main.dust[num27].noGravity = true;
                                                            Dust expr_40FE_cp_0 = Main.dust[num27];
                                                            expr_40FE_cp_0.velocity.X = expr_40FE_cp_0.velocity.X * 1f;
                                                            Dust expr_411C_cp_0 = Main.dust[num27];
                                                            expr_411C_cp_0.velocity.Y = expr_411C_cp_0.velocity.Y * 1f;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 9)
                                                {
                                                    if (this.target == 255)
                                                    {
                                                        this.TargetClosest();
                                                        float num9 = 6f;
                                                        if (this.type == 30)
                                                        {
                                                            NPC.maxSpawns = 8;
                                                        }
                                                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                        float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                        float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                        float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                        num13 = num9 / num13;
                                                        this.velocity.X = num11 * num13;
                                                        this.velocity.Y = num12 * num13;
                                                    }
                                                    if (this.timeLeft > 100)
                                                    {
                                                        this.timeLeft = 100;
                                                    }
                                                    for (int i = 0; i < 2; i++)
                                                    {
                                                        if (this.type == 30)
                                                        {
                                                            Color color = default(Color);
                                                            int num = 0;
                                                            Main.dust[num].noGravity = true;
                                                            Dust dust7 = Main.dust[num];
                                                            dust7.velocity *= 0.3f;
                                                            Dust expr_42ED_cp_0 = Main.dust[num];
                                                            expr_42ED_cp_0.velocity.X = expr_42ED_cp_0.velocity.X - this.velocity.X * 0.2f;
                                                            Dust expr_4316_cp_0 = Main.dust[num];
                                                            expr_4316_cp_0.velocity.Y = expr_4316_cp_0.velocity.Y - this.velocity.Y * 0.2f;
                                                        }
                                                        else
                                                        {
                                                            if (this.type == 33)
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Main.dust[num].noGravity = true;
                                                                Dust expr_4370_cp_0 = Main.dust[num];
                                                                expr_4370_cp_0.velocity.X = expr_4370_cp_0.velocity.X * 0.3f;
                                                                Dust expr_438D_cp_0 = Main.dust[num];
                                                                expr_438D_cp_0.velocity.Y = expr_438D_cp_0.velocity.Y * 0.3f;
                                                            }
                                                            else
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Main.dust[num].noGravity = true;
                                                                Dust expr_43C5_cp_0 = Main.dust[num];
                                                                expr_43C5_cp_0.velocity.X = expr_43C5_cp_0.velocity.X * 0.3f;
                                                                Dust expr_43E2_cp_0 = Main.dust[num];
                                                                expr_43E2_cp_0.velocity.Y = expr_43E2_cp_0.velocity.Y * 0.3f;
                                                            }
                                                        }
                                                    }
                                                    this.rotation += 0.4f * (float)this.direction;
                                                }
                                                else
                                                {
                                                    if (this.aiStyle == 10)
                                                    {
                                                        if (this.collideX)
                                                        {
                                                            this.velocity.X = this.oldVelocity.X * -0.5f;
                                                            if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
                                                            {
                                                                this.velocity.X = 2f;
                                                            }
                                                            if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
                                                            {
                                                                this.velocity.X = -2f;
                                                            }
                                                        }
                                                        if (this.collideY)
                                                        {
                                                            this.velocity.Y = this.oldVelocity.Y * -0.5f;
                                                            if (this.velocity.Y > 0f && this.velocity.Y < 1f)
                                                            {
                                                                this.velocity.Y = 1f;
                                                            }
                                                            if (this.velocity.Y < 0f && this.velocity.Y > -1f)
                                                            {
                                                                this.velocity.Y = -1f;
                                                            }
                                                        }
                                                        this.TargetClosest();
                                                        if (this.direction == -1 && this.velocity.X > -4f)
                                                        {
                                                            this.velocity.X = this.velocity.X - 0.1f;
                                                            if (this.velocity.X > 4f)
                                                            {
                                                                this.velocity.X = this.velocity.X - 0.1f;
                                                            }
                                                            else
                                                            {
                                                                if (this.velocity.X > 0f)
                                                                {
                                                                    this.velocity.X = this.velocity.X + 0.05f;
                                                                }
                                                            }
                                                            if (this.velocity.X < -4f)
                                                            {
                                                                this.velocity.X = -4f;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.direction == 1 && this.velocity.X < 4f)
                                                            {
                                                                this.velocity.X = this.velocity.X + 0.1f;
                                                                if (this.velocity.X < -4f)
                                                                {
                                                                    this.velocity.X = this.velocity.X + 0.1f;
                                                                }
                                                                else
                                                                {
                                                                    if (this.velocity.X < 0f)
                                                                    {
                                                                        this.velocity.X = this.velocity.X - 0.05f;
                                                                    }
                                                                }
                                                                if (this.velocity.X > 4f)
                                                                {
                                                                    this.velocity.X = 4f;
                                                                }
                                                            }
                                                        }
                                                        if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
                                                        {
                                                            this.velocity.Y = this.velocity.Y - 0.04f;
                                                            if ((double)this.velocity.Y > 1.5)
                                                            {
                                                                this.velocity.Y = this.velocity.Y - 0.05f;
                                                            }
                                                            else
                                                            {
                                                                if (this.velocity.Y > 0f)
                                                                {
                                                                    this.velocity.Y = this.velocity.Y + 0.03f;
                                                                }
                                                            }
                                                            if ((double)this.velocity.Y < -1.5)
                                                            {
                                                                this.velocity.Y = -1.5f;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
                                                            {
                                                                this.velocity.Y = this.velocity.Y + 0.04f;
                                                                if ((double)this.velocity.Y < -1.5)
                                                                {
                                                                    this.velocity.Y = this.velocity.Y + 0.05f;
                                                                }
                                                                else
                                                                {
                                                                    if (this.velocity.Y < 0f)
                                                                    {
                                                                        this.velocity.Y = this.velocity.Y - 0.03f;
                                                                    }
                                                                }
                                                                if ((double)this.velocity.Y > 1.5)
                                                                {
                                                                    this.velocity.Y = 1.5f;
                                                                }
                                                            }
                                                        }
                                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
                                                        int num = 0;
                                                        Main.dust[num].noGravity = true;
                                                        Main.dust[num].noLight = true;
                                                        Dust expr_49A3_cp_0 = Main.dust[num];
                                                        expr_49A3_cp_0.velocity.X = expr_49A3_cp_0.velocity.X * 0.3f;
                                                        Dust expr_49C0_cp_0 = Main.dust[num];
                                                        expr_49C0_cp_0.velocity.Y = expr_49C0_cp_0.velocity.Y * 0.3f;
                                                    }
                                                    else
                                                    {
                                                        if (this.aiStyle == 11)
                                                        {
                                                            if (this.ai[0] == 0f && Main.netMode != 1)
                                                            {
                                                                this.TargetClosest();
                                                                this.ai[0] = 1f;
                                                                int num19 = NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 36, this.whoAmI);
                                                                Main.npc[num19].ai[0] = -1f;
                                                                Main.npc[num19].ai[1] = (float)this.whoAmI;
                                                                Main.npc[num19].target = this.target;
                                                                Main.npc[num19].netUpdate = true;
                                                                num19 = NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 36, this.whoAmI);
                                                                Main.npc[num19].ai[0] = 1f;
                                                                Main.npc[num19].ai[1] = (float)this.whoAmI;
                                                                Main.npc[num19].ai[3] = 150f;
                                                                Main.npc[num19].target = this.target;
                                                                Main.npc[num19].netUpdate = true;
                                                            }
                                                            if (Main.player[this.target].dead || System.Math.Abs(this.position.X - Main.player[this.target].position.X) > 2000f || System.Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
                                                            {
                                                                this.TargetClosest();
                                                                if (Main.player[this.target].dead || System.Math.Abs(this.position.X - Main.player[this.target].position.X) > 2000f || System.Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
                                                                {
                                                                    this.ai[1] = 3f;
                                                                }
                                                            }
                                                            if (Main.dayTime && this.ai[1] != 3f && this.ai[1] != 2f)
                                                            {
                                                                this.ai[1] = 2f;
                                                            }
                                                            if (this.ai[1] == 0f)
                                                            {
                                                                this.ai[2] += 1f;
                                                                if (this.ai[2] >= 800f)
                                                                {
                                                                    this.ai[2] = 0f;
                                                                    this.ai[1] = 1f;
                                                                    this.TargetClosest();
                                                                    this.netUpdate = true;
                                                                }
                                                                this.rotation = this.velocity.X / 15f;
                                                                if (this.position.Y > Main.player[this.target].position.Y - 250f)
                                                                {
                                                                    if (this.velocity.Y > 0f)
                                                                    {
                                                                        this.velocity.Y = this.velocity.Y * 0.98f;
                                                                    }
                                                                    this.velocity.Y = this.velocity.Y - 0.02f;
                                                                    if (this.velocity.Y > 2f)
                                                                    {
                                                                        this.velocity.Y = 2f;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (this.position.Y < Main.player[this.target].position.Y - 250f)
                                                                    {
                                                                        if (this.velocity.Y < 0f)
                                                                        {
                                                                            this.velocity.Y = this.velocity.Y * 0.98f;
                                                                        }
                                                                        this.velocity.Y = this.velocity.Y + 0.02f;
                                                                        if (this.velocity.Y < -2f)
                                                                        {
                                                                            this.velocity.Y = -2f;
                                                                        }
                                                                    }
                                                                }
                                                                if (this.position.X + (float)(this.width / 2) > Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))
                                                                {
                                                                    if (this.velocity.X > 0f)
                                                                    {
                                                                        this.velocity.X = this.velocity.X * 0.98f;
                                                                    }
                                                                    this.velocity.X = this.velocity.X - 0.05f;
                                                                    if (this.velocity.X > 8f)
                                                                    {
                                                                        this.velocity.X = 8f;
                                                                    }
                                                                }
                                                                if (this.position.X + (float)(this.width / 2) < Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))
                                                                {
                                                                    if (this.velocity.X < 0f)
                                                                    {
                                                                        this.velocity.X = this.velocity.X * 0.98f;
                                                                    }
                                                                    this.velocity.X = this.velocity.X + 0.05f;
                                                                    if (this.velocity.X < -8f)
                                                                    {
                                                                        this.velocity.X = -8f;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (this.ai[1] == 1f)
                                                                {
                                                                    this.ai[2] += 1f;
                                                                    if (this.ai[2] != 2f)
                                                                    {
                                                                        goto IL_5064;
                                                                    }
                                                                IL_5064:
                                                                    if (this.ai[2] >= 400f)
                                                                    {
                                                                        this.ai[2] = 0f;
                                                                        this.ai[1] = 0f;
                                                                    }
                                                                    this.rotation += (float)this.direction * 0.3f;
                                                                    Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                    float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                                    float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                                    float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                    num13 = 2.5f / num13;
                                                                    this.velocity.X = num11 * num13;
                                                                    this.velocity.Y = num12 * num13;
                                                                }
                                                                else
                                                                {
                                                                    if (this.ai[1] == 2f)
                                                                    {
                                                                        this.damage = 9999;
                                                                        this.defense = 9999;
                                                                        this.rotation += (float)this.direction * 0.3f;
                                                                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                        float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                                        float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                                        float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                        num13 = 8f / num13;
                                                                        this.velocity.X = num11 * num13;
                                                                        this.velocity.Y = num12 * num13;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.ai[1] == 3f)
                                                                        {
                                                                            this.velocity.Y = this.velocity.Y - 0.1f;
                                                                            if (this.velocity.Y > 0f)
                                                                            {
                                                                                this.velocity.Y = this.velocity.Y * 0.95f;
                                                                            }
                                                                            this.velocity.X = this.velocity.X * 0.95f;
                                                                            if (this.timeLeft > 50)
                                                                            {
                                                                                this.timeLeft = 50;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (this.ai[1] != 2f && this.ai[1] != 3f)
                                                            {
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Main.dust[num].noGravity = true;
                                                                Dust expr_53B5_cp_0 = Main.dust[num];
                                                                expr_53B5_cp_0.velocity.X = expr_53B5_cp_0.velocity.X * 1.3f;
                                                                Dust expr_53D2_cp_0 = Main.dust[num];
                                                                expr_53D2_cp_0.velocity.X = expr_53D2_cp_0.velocity.X + this.velocity.X * 0.4f;
                                                                Dust expr_53FB_cp_0 = Main.dust[num];
                                                                expr_53FB_cp_0.velocity.Y = expr_53FB_cp_0.velocity.Y + (2f + this.velocity.Y);
                                                                for (int i = 0; i < 2; i++)
                                                                {
                                                                    color = default(Color);
                                                                    num = 0;
                                                                    Main.dust[num].noGravity = true;
                                                                    Dust dust8 = Main.dust[num];
                                                                    Dust expr_5440 = dust8;
                                                                    expr_5440.velocity -= this.velocity;
                                                                    Dust expr_5462_cp_0 = Main.dust[num];
                                                                    expr_5462_cp_0.velocity.Y = expr_5462_cp_0.velocity.Y + 5f;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.aiStyle == 12)
                                                            {
                                                                this.spriteDirection = -(int)this.ai[0];
                                                                if (!Main.npc[(int)this.ai[1]].active || Main.npc[(int)this.ai[1]].aiStyle != 11)
                                                                {
                                                                    this.ai[2] += 10f;
                                                                    if (this.ai[2] > 50f || Main.netMode != 2)
                                                                    {
                                                                        this.life = -1;
                                                                        this.HitEffect(0, 10.0);
                                                                        this.active = false;
                                                                    }
                                                                }
                                                                if (this.ai[2] == 0f || this.ai[2] == 3f)
                                                                {
                                                                    if (Main.npc[(int)this.ai[1]].ai[1] == 3f && this.timeLeft > 10)
                                                                    {
                                                                        this.timeLeft = 10;
                                                                    }
                                                                    if (Main.npc[(int)this.ai[1]].ai[1] != 0f)
                                                                    {
                                                                        if (this.position.Y > Main.npc[(int)this.ai[1]].position.Y - 100f)
                                                                        {
                                                                            if (this.velocity.Y > 0f)
                                                                            {
                                                                                this.velocity.Y = this.velocity.Y * 0.96f;
                                                                            }
                                                                            this.velocity.Y = this.velocity.Y - 0.07f;
                                                                            if (this.velocity.Y > 6f)
                                                                            {
                                                                                this.velocity.Y = 6f;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y - 100f)
                                                                            {
                                                                                if (this.velocity.Y < 0f)
                                                                                {
                                                                                    this.velocity.Y = this.velocity.Y * 0.96f;
                                                                                }
                                                                                this.velocity.Y = this.velocity.Y + 0.07f;
                                                                                if (this.velocity.Y < -6f)
                                                                                {
                                                                                    this.velocity.Y = -6f;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 120f * this.ai[0])
                                                                        {
                                                                            if (this.velocity.X > 0f)
                                                                            {
                                                                                this.velocity.X = this.velocity.X * 0.96f;
                                                                            }
                                                                            this.velocity.X = this.velocity.X - 0.1f;
                                                                            if (this.velocity.X > 8f)
                                                                            {
                                                                                this.velocity.X = 8f;
                                                                            }
                                                                        }
                                                                        if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 120f * this.ai[0])
                                                                        {
                                                                            if (this.velocity.X < 0f)
                                                                            {
                                                                                this.velocity.X = this.velocity.X * 0.96f;
                                                                            }
                                                                            this.velocity.X = this.velocity.X + 0.1f;
                                                                            if (this.velocity.X < -8f)
                                                                            {
                                                                                this.velocity.X = -8f;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        this.ai[3] += 1f;
                                                                        if (this.ai[3] >= 300f)
                                                                        {
                                                                            this.ai[2] += 1f;
                                                                            this.ai[3] = 0f;
                                                                            this.netUpdate = true;
                                                                        }
                                                                        if (this.position.Y > Main.npc[(int)this.ai[1]].position.Y + 230f)
                                                                        {
                                                                            if (this.velocity.Y > 0f)
                                                                            {
                                                                                this.velocity.Y = this.velocity.Y * 0.96f;
                                                                            }
                                                                            this.velocity.Y = this.velocity.Y - 0.04f;
                                                                            if (this.velocity.Y > 3f)
                                                                            {
                                                                                this.velocity.Y = 3f;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y + 230f)
                                                                            {
                                                                                if (this.velocity.Y < 0f)
                                                                                {
                                                                                    this.velocity.Y = this.velocity.Y * 0.96f;
                                                                                }
                                                                                this.velocity.Y = this.velocity.Y + 0.04f;
                                                                                if (this.velocity.Y < -3f)
                                                                                {
                                                                                    this.velocity.Y = -3f;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0])
                                                                        {
                                                                            if (this.velocity.X > 0f)
                                                                            {
                                                                                this.velocity.X = this.velocity.X * 0.96f;
                                                                            }
                                                                            this.velocity.X = this.velocity.X - 0.07f;
                                                                            if (this.velocity.X > 8f)
                                                                            {
                                                                                this.velocity.X = 8f;
                                                                            }
                                                                        }
                                                                        if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0])
                                                                        {
                                                                            if (this.velocity.X < 0f)
                                                                            {
                                                                                this.velocity.X = this.velocity.X * 0.96f;
                                                                            }
                                                                            this.velocity.X = this.velocity.X + 0.07f;
                                                                            if (this.velocity.X < -8f)
                                                                            {
                                                                                this.velocity.X = -8f;
                                                                            }
                                                                        }
                                                                    }
                                                                    Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                    float num11 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector.X;
                                                                    float num12 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector.Y;
                                                                    float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                    this.rotation = (float)System.Math.Atan2((double)num12, (double)num11) + 1.57f;
                                                                }
                                                                else
                                                                {
                                                                    if (this.ai[2] == 1f)
                                                                    {
                                                                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                        float num11 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector.X;
                                                                        float num12 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector.Y;
                                                                        float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                        this.rotation = (float)System.Math.Atan2((double)num12, (double)num11) + 1.57f;
                                                                        this.velocity.X = this.velocity.X * 0.95f;
                                                                        this.velocity.Y = this.velocity.Y - 0.1f;
                                                                        if (this.velocity.Y < -8f)
                                                                        {
                                                                            this.velocity.Y = -8f;
                                                                        }
                                                                        if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y - 200f)
                                                                        {
                                                                            this.TargetClosest();
                                                                            this.ai[2] = 2f;
                                                                            vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                            num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                                            num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                                            num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                            num13 = 20f / num13;
                                                                            this.velocity.X = num11 * num13;
                                                                            this.velocity.Y = num12 * num13;
                                                                            this.netUpdate = true;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.ai[2] == 2f)
                                                                        {
                                                                            if (this.position.Y > Main.player[this.target].position.Y || this.velocity.Y < 0f)
                                                                            {
                                                                                this.ai[2] = 3f;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.ai[2] == 4f)
                                                                            {
                                                                                Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                                float num11 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector.X;
                                                                                float num12 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector.Y;
                                                                                float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                                this.rotation = (float)System.Math.Atan2((double)num12, (double)num11) + 1.57f;
                                                                                this.velocity.Y = this.velocity.Y * 0.95f;
                                                                                this.velocity.X = this.velocity.X + 0.1f * -this.ai[0];
                                                                                if (this.velocity.X < -8f)
                                                                                {
                                                                                    this.velocity.X = -8f;
                                                                                }
                                                                                if (this.velocity.X > 8f)
                                                                                {
                                                                                    this.velocity.X = 8f;
                                                                                }
                                                                                if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 500f || this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) + 500f)
                                                                                {
                                                                                    this.TargetClosest();
                                                                                    this.ai[2] = 5f;
                                                                                    vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                                    num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                                                                    num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                                                                    num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                                    num13 = 20f / num13;
                                                                                    this.velocity.X = num11 * num13;
                                                                                    this.velocity.Y = num12 * num13;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.ai[2] == 5f && ((this.velocity.X > 0f && this.position.X + (float)(this.width / 2) > Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2)) || (this.velocity.X < 0f && this.position.X + (float)(this.width / 2) < Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))))
                                                                                {
                                                                                    this.ai[2] = 0f;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (this.aiStyle == 13)
                                                                {
                                                                    if (Main.tile[(int)this.ai[0], (int)this.ai[1]] == null)
                                                                    {
                                                                        Main.tile[(int)this.ai[0], (int)this.ai[1]] = new Tile();
                                                                    }
                                                                    if (!Main.tile[(int)this.ai[0], (int)this.ai[1]].active)
                                                                    {
                                                                        this.life = -1;
                                                                        this.HitEffect(0, 10.0);
                                                                        this.active = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        this.TargetClosest();
                                                                        float num10 = 0.05f;
                                                                        Vector2 vector = new Vector2(this.ai[0] * 16f + 8f, this.ai[1] * 16f + 8f);
                                                                        float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - (float)(this.width / 2) - vector.X;
                                                                        float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - (float)(this.height / 2) - vector.Y;
                                                                        float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                                        if (num13 > 150f)
                                                                        {
                                                                            num13 = 150f / num13;
                                                                            num11 *= num13;
                                                                            num12 *= num13;
                                                                        }
                                                                        if (this.position.X < this.ai[0] * 16f + 8f + num11)
                                                                        {
                                                                            this.velocity.X = this.velocity.X + num10;
                                                                            if (this.velocity.X < 0f && num11 > 0f)
                                                                            {
                                                                                this.velocity.X = this.velocity.X + num10 * 2f;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.position.X > this.ai[0] * 16f + 8f + num11)
                                                                            {
                                                                                this.velocity.X = this.velocity.X - num10;
                                                                                if (this.velocity.X > 0f && num11 < 0f)
                                                                                {
                                                                                    this.velocity.X = this.velocity.X - num10 * 2f;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (this.position.Y < this.ai[1] * 16f + 8f + num12)
                                                                        {
                                                                            this.velocity.Y = this.velocity.Y + num10;
                                                                            if (this.velocity.Y < 0f && num12 > 0f)
                                                                            {
                                                                                this.velocity.Y = this.velocity.Y + num10 * 2f;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.position.Y > this.ai[1] * 16f + 8f + num12)
                                                                            {
                                                                                this.velocity.Y = this.velocity.Y - num10;
                                                                                if (this.velocity.Y > 0f && num12 < 0f)
                                                                                {
                                                                                    this.velocity.Y = this.velocity.Y - num10 * 2f;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (this.velocity.X > 2f)
                                                                        {
                                                                            this.velocity.X = 2f;
                                                                        }
                                                                        if (this.velocity.X < -2f)
                                                                        {
                                                                            this.velocity.X = -2f;
                                                                        }
                                                                        if (this.velocity.Y > 2f)
                                                                        {
                                                                            this.velocity.Y = 2f;
                                                                        }
                                                                        if (this.velocity.Y < -2f)
                                                                        {
                                                                            this.velocity.Y = -2f;
                                                                        }
                                                                        if (num11 > 0f)
                                                                        {
                                                                            this.spriteDirection = 1;
                                                                            this.rotation = (float)System.Math.Atan2((double)num12, (double)num11);
                                                                        }
                                                                        if (num11 < 0f)
                                                                        {
                                                                            this.spriteDirection = -1;
                                                                            this.rotation = (float)System.Math.Atan2((double)num12, (double)num11) + 3.14f;
                                                                        }
                                                                        if (this.collideX)
                                                                        {
                                                                            this.netUpdate = true;
                                                                            this.velocity.X = this.oldVelocity.X * -0.7f;
                                                                            if (this.velocity.X > 0f && this.velocity.X < 2f)
                                                                            {
                                                                                this.velocity.X = 2f;
                                                                            }
                                                                            if (this.velocity.X < 0f && this.velocity.X > -2f)
                                                                            {
                                                                                this.velocity.X = -2f;
                                                                            }
                                                                        }
                                                                        if (this.collideY)
                                                                        {
                                                                            this.netUpdate = true;
                                                                            this.velocity.Y = this.oldVelocity.Y * -0.7f;
                                                                            if (this.velocity.Y > 0f && this.velocity.Y < 2f)
                                                                            {
                                                                                this.velocity.Y = 2f;
                                                                            }
                                                                            if (this.velocity.Y < 0f && this.velocity.Y > -2f)
                                                                            {
                                                                                this.velocity.Y = -2f;
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
                                        else
                                        {
                                            int num28 = ((int)this.position.X + this.width / 2) / 16;
                                            int num29 = (int)(this.position.Y + (float)this.height + 1f) / 16;
                                            if (Main.netMode == 1)
                                            {
                                                this.homeTileX = num28;
                                                this.homeTileY = num29;
                                            }
                                            bool flag6 = false;
                                            this.directionY = -1;
                                            if (this.direction == 0)
                                            {
                                                this.direction = 1;
                                            }
                                            for (int k = 0; k < 255; k++)
                                            {
                                                if (Main.player[k].active && Main.player[k].talkNPC == this.whoAmI)
                                                {
                                                    flag6 = true;
                                                    if (this.ai[0] != 0f)
                                                    {
                                                        this.netUpdate = true;
                                                    }
                                                    this.ai[0] = 0f;
                                                    this.ai[1] = 300f;
                                                    this.ai[2] = 100f;
                                                    if (Main.player[k].position.X + (float)(Main.player[k].width / 2) < this.position.X + (float)(this.width / 2))
                                                    {
                                                        this.direction = -1;
                                                    }
                                                    else
                                                    {
                                                        this.direction = 1;
                                                    }
                                                }
                                            }
                                            if (this.ai[3] > 0f)
                                            {
                                                this.life = -1;
                                                this.HitEffect(0, 10.0);
                                                this.active = false;
                                                if (this.type == 37)
                                                {
                                                }
                                            }
                                            if (this.type == 37 && Main.netMode != 1)
                                            {
                                                this.homeless = false;
                                                this.homeTileX = Main.dungeonX;
                                                this.homeTileY = Main.dungeonY;
                                                if (NPC.downedBoss3)
                                                {
                                                    this.ai[3] = 1f;
                                                    this.netUpdate = true;
                                                }
                                                if (!Main.dayTime && flag6 && this.ai[3] == 0f)
                                                {
                                                    bool flag7 = true;
                                                    for (int i = 0; i < 1000; i++)
                                                    {
                                                        if (Main.npc[i].active && Main.npc[i].type == 35)
                                                        {
                                                            flag7 = false;
                                                            break;
                                                        }
                                                    }
                                                    if (flag7)
                                                    {
                                                        int num19 = NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 35, 0);
                                                        Main.npc[num19].netUpdate = true;
                                                        string str = "Skeletron";
                                                        if (Main.netMode != 0)
                                                        {
                                                            if (Main.netMode == 2)
                                                            {
                                                                NetMessage.SendData(25, -1, -1, str + " has awoken!", 255, 175f, 75f, 255f);
                                                            }
                                                        }
                                                    }
                                                    this.ai[3] = 1f;
                                                    this.netUpdate = true;
                                                }
                                            }
                                            if (Main.netMode != 1 && !Main.dayTime && (num28 != this.homeTileX || num29 != this.homeTileY) && !this.homeless)
                                            {
                                                bool flag8 = true;
                                                for (int l = 0; l < 2; l++)
                                                {
                                                    Rectangle rectangle = new Rectangle((int)this.position.X + this.width / 2 - Main.screenWidth / 2 - NPC.safeRangeX, (int)this.position.Y + this.height / 2 - Main.screenHeight / 2 - NPC.safeRangeY, Main.screenWidth + NPC.safeRangeX * 2, Main.screenHeight + NPC.safeRangeY * 2);
                                                    if (l == 1)
                                                    {
                                                        rectangle = new Rectangle(this.homeTileX * 16 + 8 - Main.screenWidth / 2 - NPC.safeRangeX, this.homeTileY * 16 + 8 - Main.screenHeight / 2 - NPC.safeRangeY, Main.screenWidth + NPC.safeRangeX * 2, Main.screenHeight + NPC.safeRangeY * 2);
                                                    }
                                                    for (int i = 0; i < 255; i++)
                                                    {
                                                        if (Main.player[i].active)
                                                        {
                                                            Rectangle rectangle2 = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                                                            if (rectangle2.Intersects(rectangle))
                                                            {
                                                                flag8 = false;
                                                                break;
                                                            }
                                                        }
                                                        if (!flag8)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (flag8)
                                                {
                                                    if (this.type == 37 || !Collision.SolidTiles(this.homeTileX - 1, this.homeTileX + 1, this.homeTileY - 3, this.homeTileY - 1))
                                                    {
                                                        this.velocity.X = 0f;
                                                        this.velocity.Y = 0f;
                                                        this.position.X = (float)(this.homeTileX * 16 + 8 - this.width / 2);
                                                        this.position.Y = (float)(this.homeTileY * 16 - this.height) - 0.1f;
                                                        this.netUpdate = true;
                                                    }
                                                    else
                                                    {
                                                        this.homeless = true;
                                                        WorldGen.QuickFindHome(this.whoAmI);
                                                    }
                                                }
                                            }
                                            if (this.ai[0] == 0f)
                                            {
                                                if (this.ai[2] > 0f)
                                                {
                                                    this.ai[2] -= 1f;
                                                }
                                                if (!Main.dayTime && !flag6)
                                                {
                                                    if (Main.netMode != 1)
                                                    {
                                                        if (num28 == this.homeTileX && num29 == this.homeTileY)
                                                        {
                                                            if (this.velocity.X != 0f)
                                                            {
                                                                this.netUpdate = true;
                                                            }
                                                            if ((double)this.velocity.X > 0.1)
                                                            {
                                                                this.velocity.X = this.velocity.X - 0.1f;
                                                            }
                                                            else
                                                            {
                                                                if ((double)this.velocity.X < -0.1)
                                                                {
                                                                    this.velocity.X = this.velocity.X + 0.1f;
                                                                }
                                                                else
                                                                {
                                                                    this.velocity.X = 0f;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!flag6)
                                                            {
                                                                if (num28 > this.homeTileX)
                                                                {
                                                                    this.direction = -1;
                                                                }
                                                                else
                                                                {
                                                                    this.direction = 1;
                                                                }
                                                                this.ai[0] = 1f;
                                                                this.ai[1] = (float)(200 + Main.rand.Next(200));
                                                                this.ai[2] = 0f;
                                                                this.netUpdate = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if ((double)this.velocity.X > 0.1)
                                                    {
                                                        this.velocity.X = this.velocity.X - 0.1f;
                                                    }
                                                    else
                                                    {
                                                        if ((double)this.velocity.X < -0.1)
                                                        {
                                                            this.velocity.X = this.velocity.X + 0.1f;
                                                        }
                                                        else
                                                        {
                                                            this.velocity.X = 0f;
                                                        }
                                                    }
                                                    if (Main.netMode != 1)
                                                    {
                                                        if (this.ai[1] > 0f)
                                                        {
                                                            this.ai[1] -= 1f;
                                                        }
                                                        if (this.ai[1] <= 0f)
                                                        {
                                                            this.ai[0] = 1f;
                                                            this.ai[1] = (float)(200 + Main.rand.Next(200));
                                                            this.ai[2] = 0f;
                                                            this.netUpdate = true;
                                                        }
                                                    }
                                                }
                                                if (Main.netMode != 1 && (Main.dayTime || (num28 == this.homeTileX && num29 == this.homeTileY)))
                                                {
                                                    if (num28 < this.homeTileX - 25 || num28 > this.homeTileX + 25)
                                                    {
                                                        if (this.ai[2] == 0f)
                                                        {
                                                            if (num28 < this.homeTileX - 50 && this.direction == -1)
                                                            {
                                                                this.direction = 1;
                                                                this.netUpdate = true;
                                                            }
                                                            else
                                                            {
                                                                if (num28 > this.homeTileX + 50 && this.direction == 1)
                                                                {
                                                                    this.direction = -1;
                                                                    this.netUpdate = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Main.rand.Next(80) == 0 && this.ai[2] == 0f)
                                                        {
                                                            this.ai[2] = 200f;
                                                            this.direction *= -1;
                                                            this.netUpdate = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.ai[0] == 1f)
                                                {
                                                    if (Main.netMode != 1 && !Main.dayTime && num28 == this.homeTileX && num29 == this.homeTileY)
                                                    {
                                                        this.ai[0] = 0f;
                                                        this.ai[1] = (float)(200 + Main.rand.Next(200));
                                                        this.ai[2] = 60f;
                                                        this.netUpdate = true;
                                                    }
                                                    else
                                                    {
                                                        if (Main.netMode != 1 && !this.homeless && (num28 < this.homeTileX - 35 || num28 > this.homeTileX + 35))
                                                        {
                                                            if (this.position.X < (float)(this.homeTileX * 16) && this.direction == -1)
                                                            {
                                                                this.direction = 1;
                                                                this.velocity.X = 0.1f;
                                                                this.netUpdate = true;
                                                            }
                                                            else
                                                            {
                                                                if (this.position.X > (float)(this.homeTileX * 16) && this.direction == 1)
                                                                {
                                                                    this.direction = -1;
                                                                    this.velocity.X = -0.1f;
                                                                    this.netUpdate = true;
                                                                }
                                                            }
                                                        }
                                                        this.ai[1] -= 1f;
                                                        if (this.ai[1] <= 0f)
                                                        {
                                                            this.ai[0] = 0f;
                                                            this.ai[1] = (float)(300 + Main.rand.Next(300));
                                                            this.ai[2] = 60f;
                                                            this.netUpdate = true;
                                                        }
                                                        if (this.closeDoor && ((this.position.X + (float)(this.width / 2)) / 16f > (float)(this.doorX + 2) || (this.position.X + (float)(this.width / 2)) / 16f < (float)(this.doorX - 2)))
                                                        {
                                                            if (WorldGen.CloseDoor(this.doorX, this.doorY, false))
                                                            {
                                                                this.closeDoor = false;
                                                                NetMessage.SendData(19, -1, -1, "", 1, (float)this.doorX, (float)this.doorY, (float)this.direction);
                                                            }
                                                            if ((this.position.X + (float)(this.width / 2)) / 16f > (float)(this.doorX + 4) || (this.position.X + (float)(this.width / 2)) / 16f < (float)(this.doorX - 4) || (this.position.Y + (float)(this.height / 2)) / 16f > (float)(this.doorY + 4) || (this.position.Y + (float)(this.height / 2)) / 16f < (float)(this.doorY - 4))
                                                            {
                                                                this.closeDoor = false;
                                                            }
                                                        }
                                                        if (this.velocity.X < -1f || this.velocity.X > 1f)
                                                        {
                                                            if (this.velocity.Y == 0f)
                                                            {
                                                                this.velocity *= 0.8f;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if ((double)this.velocity.X < 1.15 && this.direction == 1)
                                                            {
                                                                this.velocity.X = this.velocity.X + 0.07f;
                                                                if (this.velocity.X > 1f)
                                                                {
                                                                    this.velocity.X = 1f;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (this.velocity.X > -1f && this.direction == -1)
                                                                {
                                                                    this.velocity.X = this.velocity.X - 0.07f;
                                                                    if (this.velocity.X > 1f)
                                                                    {
                                                                        this.velocity.X = 1f;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (this.velocity.Y == 0f)
                                                        {
                                                            if (this.position.X == this.ai[2])
                                                            {
                                                                this.direction *= -1;
                                                            }
                                                            this.ai[2] = -1f;
                                                            int num3 = (int)((this.position.X + (float)(this.width / 2) + (float)(15 * this.direction)) / 16f);
                                                            int num4 = (int)((this.position.Y + (float)this.height - 16f) / 16f);
                                                            if (Main.tile[num3, num4] == null)
                                                            {
                                                                Main.tile[num3, num4] = new Tile();
                                                            }
                                                            if (Main.tile[num3, num4 - 1] == null)
                                                            {
                                                                Main.tile[num3, num4 - 1] = new Tile();
                                                            }
                                                            if (Main.tile[num3, num4 - 2] == null)
                                                            {
                                                                Main.tile[num3, num4 - 2] = new Tile();
                                                            }
                                                            if (Main.tile[num3, num4 - 3] == null)
                                                            {
                                                                Main.tile[num3, num4 - 3] = new Tile();
                                                            }
                                                            if (Main.tile[num3, num4 + 1] == null)
                                                            {
                                                                Main.tile[num3, num4 + 1] = new Tile();
                                                            }
                                                            if (Main.tile[num3 + this.direction, num4 - 1] == null)
                                                            {
                                                                Main.tile[num3 + this.direction, num4 - 1] = new Tile();
                                                            }
                                                            if (Main.tile[num3 + this.direction, num4 + 1] == null)
                                                            {
                                                                Main.tile[num3 + this.direction, num4 + 1] = new Tile();
                                                            }
                                                            if (Main.tile[num3, num4 - 2].active && Main.tile[num3, num4 - 2].type == 10 && (Main.rand.Next(10) == 0 || !Main.dayTime))
                                                            {
                                                                if (Main.netMode != 1)
                                                                {
                                                                    if (WorldGen.OpenDoor(num3, num4 - 2, this.direction))
                                                                    {
                                                                        this.closeDoor = true;
                                                                        this.doorX = num3;
                                                                        this.doorY = num4 - 2;
                                                                        NetMessage.SendData(19, -1, -1, "", 0, (float)num3, (float)(num4 - 2), (float)this.direction);
                                                                        this.netUpdate = true;
                                                                        this.ai[1] += 80f;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (WorldGen.OpenDoor(num3, num4 - 2, -this.direction))
                                                                        {
                                                                            this.closeDoor = true;
                                                                            this.doorX = num3;
                                                                            this.doorY = num4 - 2;
                                                                            NetMessage.SendData(19, -1, -1, "", 0, (float)num3, (float)(num4 - 2), (float)(-(float)this.direction));
                                                                            this.netUpdate = true;
                                                                            this.ai[1] += 80f;
                                                                        }
                                                                        else
                                                                        {
                                                                            this.direction *= -1;
                                                                            this.netUpdate = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if ((this.velocity.X < 0f && this.spriteDirection == -1) || (this.velocity.X > 0f && this.spriteDirection == 1))
                                                                {
                                                                    if (Main.tile[num3, num4 - 2].active && Main.tileSolid[(int)Main.tile[num3, num4 - 2].type] && !Main.tileSolidTop[(int)Main.tile[num3, num4 - 2].type])
                                                                    {
                                                                        if ((this.direction == 1 && !Collision.SolidTiles(num3 - 2, num3 - 1, num4 - 5, num4 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num3 + 1, num3 + 2, num4 - 5, num4 - 1)))
                                                                        {
                                                                            if (!Collision.SolidTiles(num3, num3, num4 - 5, num4 - 3))
                                                                            {
                                                                                this.velocity.Y = -6f;
                                                                                this.netUpdate = true;
                                                                            }
                                                                            else
                                                                            {
                                                                                this.direction *= -1;
                                                                                this.netUpdate = true;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            this.direction *= -1;
                                                                            this.netUpdate = true;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Main.tile[num3, num4 - 1].active && Main.tileSolid[(int)Main.tile[num3, num4 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num3, num4 - 1].type])
                                                                        {
                                                                            if ((this.direction == 1 && !Collision.SolidTiles(num3 - 2, num3 - 1, num4 - 4, num4 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num3 + 1, num3 + 2, num4 - 4, num4 - 1)))
                                                                            {
                                                                                if (!Collision.SolidTiles(num3, num3, num4 - 4, num4 - 2))
                                                                                {
                                                                                    this.velocity.Y = -5f;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                                else
                                                                                {
                                                                                    this.direction *= -1;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                this.direction *= -1;
                                                                                this.netUpdate = true;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Main.tile[num3, num4].active && Main.tileSolid[(int)Main.tile[num3, num4].type] && !Main.tileSolidTop[(int)Main.tile[num3, num4].type])
                                                                            {
                                                                                if ((this.direction == 1 && !Collision.SolidTiles(num3 - 2, num3, num4 - 3, num4 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num3, num3 + 2, num4 - 3, num4 - 1)))
                                                                                {
                                                                                    this.velocity.Y = -3.6f;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                                else
                                                                                {
                                                                                    this.direction *= -1;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (num28 >= this.homeTileX - 35 && num28 <= this.homeTileX + 35 && (!Main.tile[num3, num4 + 1].active || !Main.tileSolid[(int)Main.tile[num3, num4 + 1].type]) && (!Main.tile[num3 - this.direction, num4 + 1].active || !Main.tileSolid[(int)Main.tile[num3 - this.direction, num4 + 1].type]) && (!Main.tile[num3, num4 + 2].active || !Main.tileSolid[(int)Main.tile[num3, num4 + 2].type]) && (!Main.tile[num3 - this.direction, num4 + 2].active || !Main.tileSolid[(int)Main.tile[num3 - this.direction, num4 + 2].type]) && (!Main.tile[num3, num4 + 3].active || !Main.tileSolid[(int)Main.tile[num3, num4 + 3].type]) && (!Main.tile[num3 - this.direction, num4 + 3].active || !Main.tileSolid[(int)Main.tile[num3 - this.direction, num4 + 3].type]) && (!Main.tile[num3, num4 + 4].active || !Main.tileSolid[(int)Main.tile[num3, num4 + 4].type]) && (!Main.tile[num3 - this.direction, num4 + 4].active || !Main.tileSolid[(int)Main.tile[num3 - this.direction, num4 + 4].type]))
                                                                                {
                                                                                    this.direction *= -1;
                                                                                    this.velocity.X = this.velocity.X * -1f;
                                                                                    this.netUpdate = true;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (this.velocity.Y < 0f)
                                                                    {
                                                                        this.ai[2] = this.position.X;
                                                                    }
                                                                }
                                                                if (this.velocity.Y < 0f && this.wet)
                                                                {
                                                                    this.velocity.Y = this.velocity.Y * 1.2f;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.target < 0 || this.target == 255 || Main.player[this.target].dead)
                                        {
                                            this.TargetClosest();
                                        }
                                        if (Main.player[this.target].dead && this.timeLeft > 10)
                                        {
                                            this.timeLeft = 10;
                                        }
                                        if (Main.netMode != 1)
                                        {
                                            if ((this.type == 7 || this.type == 8 || this.type == 10 || this.type == 11 || this.type == 13 || this.type == 14 || this.type == 39 || this.type == 40) && this.ai[0] == 0f)
                                            {
                                                if (this.type == 7 || this.type == 10 || this.type == 13 || this.type == 39)
                                                {
                                                    this.ai[2] = 10f;
                                                    if (this.type == 10)
                                                    {
                                                        this.ai[2] = 5f;
                                                    }
                                                    if (this.type == 13)
                                                    {
                                                        this.ai[2] = 50f;
                                                    }
                                                    if (this.type == 39)
                                                    {
                                                        this.ai[2] = 15f;
                                                    }
                                                    this.ai[0] = (float)NPC.NewNPC((int)this.position.X, (int)this.position.Y, this.type + 1, this.whoAmI);
                                                }
                                                else
                                                {
                                                    if ((this.type == 8 || this.type == 11 || this.type == 14 || this.type == 40) && this.ai[2] > 0f)
                                                    {
                                                        this.ai[0] = (float)NPC.NewNPC((int)this.position.X, (int)this.position.Y, this.type, this.whoAmI);
                                                    }
                                                    else
                                                    {
                                                        this.ai[0] = (float)NPC.NewNPC((int)this.position.X, (int)this.position.Y, this.type + 1, this.whoAmI);
                                                    }
                                                }
                                                Main.npc[(int)this.ai[0]].ai[1] = (float)this.whoAmI;
                                                Main.npc[(int)this.ai[0]].ai[2] = this.ai[2] - 1f;
                                                this.netUpdate = true;
                                            }
                                            if ((this.type == 8 || this.type == 9 || this.type == 11 || this.type == 12 || this.type == 40 || this.type == 41) && !Main.npc[(int)this.ai[1]].active)
                                            {
                                                this.life = 0;
                                                this.HitEffect(0, 10.0);
                                                this.active = false;
                                            }
                                            if ((this.type == 7 || this.type == 8 || this.type == 10 || this.type == 11 || this.type == 39 || this.type == 40) && !Main.npc[(int)this.ai[0]].active)
                                            {
                                                this.life = 0;
                                                this.HitEffect(0, 10.0);
                                                this.active = false;
                                            }
                                            if (this.type == 13 || this.type == 14 || this.type == 15)
                                            {
                                                if (!Main.npc[(int)this.ai[1]].active && !Main.npc[(int)this.ai[0]].active)
                                                {
                                                    this.life = 0;
                                                    this.HitEffect(0, 10.0);
                                                    this.active = false;
                                                }
                                                if (this.type == 13 && !Main.npc[(int)this.ai[0]].active)
                                                {
                                                    this.life = 0;
                                                    this.HitEffect(0, 10.0);
                                                    this.active = false;
                                                }
                                                if (this.type == 15 && !Main.npc[(int)this.ai[1]].active)
                                                {
                                                    this.life = 0;
                                                    this.HitEffect(0, 10.0);
                                                    this.active = false;
                                                }
                                                if (this.type == 14 && !Main.npc[(int)this.ai[1]].active)
                                                {
                                                    this.type = 13;
                                                    int i = this.whoAmI;
                                                    int num30 = this.life;
                                                    float num31 = this.ai[0];
                                                    this.SetDefaults(this.type);
                                                    this.life = num30;
                                                    if (this.life > this.lifeMax)
                                                    {
                                                        this.life = this.lifeMax;
                                                    }
                                                    this.ai[0] = num31;
                                                    this.TargetClosest();
                                                    this.netUpdate = true;
                                                    this.whoAmI = i;
                                                }
                                                if (this.type == 14 && !Main.npc[(int)this.ai[0]].active)
                                                {
                                                    int num30 = this.life;
                                                    int i = this.whoAmI;
                                                    float num32 = this.ai[1];
                                                    this.SetDefaults(this.type);
                                                    this.life = num30;
                                                    if (this.life > this.lifeMax)
                                                    {
                                                        this.life = this.lifeMax;
                                                    }
                                                    this.ai[1] = num32;
                                                    this.TargetClosest();
                                                    this.netUpdate = true;
                                                    this.whoAmI = i;
                                                }
                                                if (this.life == 0)
                                                {
                                                    bool flag9 = true;
                                                    for (int i = 0; i < 1000; i++)
                                                    {
                                                        if (Main.npc[i].active && (Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15))
                                                        {
                                                            flag9 = false;
                                                            break;
                                                        }
                                                    }
                                                    if (flag9)
                                                    {
                                                        this.boss = true;
                                                        this.NPCLoot();
                                                    }
                                                }
                                            }
                                            if (!this.active && Main.netMode == 2)
                                            {
                                                NetMessage.SendData(28, -1, -1, "", this.whoAmI, -1f, 0f, 0f);
                                            }
                                        }
                                        int num33 = (int)(this.position.X / 16f) - 1;
                                        int num34 = (int)((this.position.X + (float)this.width) / 16f) + 2;
                                        int num35 = (int)(this.position.Y / 16f) - 1;
                                        int num36 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
                                        if (num33 < 0)
                                        {
                                            num33 = 0;
                                        }
                                        if (num34 > Main.maxTilesX)
                                        {
                                            num34 = Main.maxTilesX;
                                        }
                                        if (num35 < 0)
                                        {
                                            num35 = 0;
                                        }
                                        if (num36 > Main.maxTilesY)
                                        {
                                            num36 = Main.maxTilesY;
                                        }
                                        bool flag10 = false;
                                        for (int i = num33; i < num34; i++)
                                        {
                                            for (int k = num35; k < num36; k++)
                                            {
                                                if (Main.tile[i, k] != null && ((Main.tile[i, k].active && (Main.tileSolid[(int)Main.tile[i, k].type] || (Main.tileSolidTop[(int)Main.tile[i, k].type] && Main.tile[i, k].frameY == 0))) || Main.tile[i, k].liquid > 64))
                                                {
                                                    Vector2 vector4;
                                                    vector4.X = (float)(i * 16);
                                                    vector4.Y = (float)(k * 16);
                                                    if (this.position.X + (float)this.width > vector4.X && this.position.X < vector4.X + 16f && this.position.Y + (float)this.height > vector4.Y && this.position.Y < vector4.Y + 16f)
                                                    {
                                                        flag10 = true;
                                                        if (Main.rand.Next(40) == 0 && Main.tile[i, k].active)
                                                        {
                                                            WorldGen.KillTile(i, k, true, true, false);
                                                        }
                                                        if (Main.netMode != 1 && Main.tile[i, k].type == 2 && Main.tile[i, k - 1].type != 27)
                                                        {
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        float num9 = 8f;
                                        float num10 = 0.07f;
                                        if (this.type == 10)
                                        {
                                            num9 = 6f;
                                            num10 = 0.05f;
                                        }
                                        if (this.type == 13)
                                        {
                                            num9 = 11f;
                                            num10 = 0.08f;
                                        }
                                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                        float num11 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
                                        float num12 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
                                        float num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                        if (this.ai[1] > 0f)
                                        {
                                            num11 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - vector.X;
                                            num12 = Main.npc[(int)this.ai[1]].position.Y + (float)(Main.npc[(int)this.ai[1]].height / 2) - vector.Y;
                                            this.rotation = (float)System.Math.Atan2((double)num12, (double)num11) + 1.57f;
                                            num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                            num13 = (num13 - (float)this.width) / num13;
                                            num11 *= num13;
                                            num12 *= num13;
                                            this.velocity = default(Vector2);
                                            this.position.X = this.position.X + num11;
                                            this.position.Y = this.position.Y + num12;
                                        }
                                        else
                                        {
                                            if (!flag10)
                                            {
                                                this.TargetClosest();
                                                this.velocity.Y = this.velocity.Y + 0.11f;
                                                if (this.velocity.Y > num9)
                                                {
                                                    this.velocity.Y = num9;
                                                }
                                                if ((double)(System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y)) < (double)num9 * 0.4)
                                                {
                                                    if (this.velocity.X < 0f)
                                                    {
                                                        this.velocity.X = this.velocity.X - num10 * 1.1f;
                                                    }
                                                    else
                                                    {
                                                        this.velocity.X = this.velocity.X + num10 * 1.1f;
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.velocity.Y == num9)
                                                    {
                                                        if (this.velocity.X < num11)
                                                        {
                                                            this.velocity.X = this.velocity.X + num10;
                                                        }
                                                        else
                                                        {
                                                            if (this.velocity.X > num11)
                                                            {
                                                                this.velocity.X = this.velocity.X - num10;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (this.velocity.Y > 4f)
                                                        {
                                                            if (this.velocity.X < 0f)
                                                            {
                                                                this.velocity.X = this.velocity.X + num10 * 0.9f;
                                                            }
                                                            else
                                                            {
                                                                this.velocity.X = this.velocity.X - num10 * 0.9f;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.soundDelay == 0)
                                                {
                                                    float num37 = num13 / 40f;
                                                    if (num37 < 10f)
                                                    {
                                                        num37 = 10f;
                                                    }
                                                    if (num37 > 20f)
                                                    {
                                                        num37 = 20f;
                                                    }
                                                    this.soundDelay = (int)num37;
                                                }
                                                num13 = (float)System.Math.Sqrt((double)(num11 * num11 + num12 * num12));
                                                float num38 = System.Math.Abs(num11);
                                                float num39 = System.Math.Abs(num12);
                                                num13 = num9 / num13;
                                                num11 *= num13;
                                                num12 *= num13;
                                                if ((this.velocity.X > 0f && num11 > 0f) || (this.velocity.X < 0f && num11 < 0f) || (this.velocity.Y > 0f && num12 > 0f) || (this.velocity.Y < 0f && num12 < 0f))
                                                {
                                                    if (this.velocity.X < num11)
                                                    {
                                                        this.velocity.X = this.velocity.X + num10;
                                                    }
                                                    else
                                                    {
                                                        if (this.velocity.X > num11)
                                                        {
                                                            this.velocity.X = this.velocity.X - num10;
                                                        }
                                                    }
                                                    if (this.velocity.Y < num12)
                                                    {
                                                        this.velocity.Y = this.velocity.Y + num10;
                                                    }
                                                    else
                                                    {
                                                        if (this.velocity.Y > num12)
                                                        {
                                                            this.velocity.Y = this.velocity.Y - num10;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (num38 > num39)
                                                    {
                                                        if (this.velocity.X < num11)
                                                        {
                                                            this.velocity.X = this.velocity.X + num10 * 1.1f;
                                                        }
                                                        else
                                                        {
                                                            if (this.velocity.X > num11)
                                                            {
                                                                this.velocity.X = this.velocity.X - num10 * 1.1f;
                                                            }
                                                        }
                                                        if ((double)(System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y)) < (double)num9 * 0.5)
                                                        {
                                                            if (this.velocity.Y > 0f)
                                                            {
                                                                this.velocity.Y = this.velocity.Y + num10;
                                                            }
                                                            else
                                                            {
                                                                this.velocity.Y = this.velocity.Y - num10;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (this.velocity.Y < num12)
                                                        {
                                                            this.velocity.Y = this.velocity.Y + num10 * 1.1f;
                                                        }
                                                        else
                                                        {
                                                            if (this.velocity.Y > num12)
                                                            {
                                                                this.velocity.Y = this.velocity.Y - num10 * 1.1f;
                                                            }
                                                        }
                                                        if ((double)(System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y)) < (double)num9 * 0.5)
                                                        {
                                                            if (this.velocity.X > 0f)
                                                            {
                                                                this.velocity.X = this.velocity.X + num10;
                                                            }
                                                            else
                                                            {
                                                                this.velocity.X = this.velocity.X - num10;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void CheckActive()
        {
            if (this.active && this.type != 8 && this.type != 9 && this.type != 11 && this.type != 12 && this.type != 14 && this.type != 15 && this.type != 40 && this.type != 41)
            {
                if (this.townNPC)
                {
                    if ((double)this.position.Y < Main.worldSurface * 18.0)
                    {
                        Rectangle rectangle = new Rectangle((int)this.position.X + this.width / 2 - NPC.townRangeX, (int)this.position.Y + this.height / 2 - NPC.townRangeY, NPC.townRangeX * 2, NPC.townRangeY * 2);
                        for (int i = 0; i < 255; i++)
                        {
                            if (Main.player[i].active && rectangle.Intersects(new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
                            {
                                Player player = Main.player[i];
                                player.townNPCs++;
                            }
                        }
                    }
                }
                else
                {
                    bool flag = false;
                    Rectangle rectangle2 = new Rectangle((int)this.position.X + this.width / 2 - NPC.activeRangeX, (int)this.position.Y + this.height / 2 - NPC.activeRangeY, NPC.activeRangeX * 2, NPC.activeRangeY * 2);
                    Rectangle rectangle3 = new Rectangle((int)((double)(this.position.X + (float)(this.width / 2)) - (double)Main.screenWidth * 0.5) - this.width, (int)((double)(this.position.Y + (float)(this.height / 2)) - (double)Main.screenHeight * 0.5) - this.height, Main.screenWidth + this.width * 2, Main.screenHeight + this.height * 2);
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active)
                        {
                            if (rectangle2.Intersects(new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
                            {
                                flag = true;
                                if (this.type != 25 && this.type != 30 && this.type != 33)
                                {
                                    Player player2 = Main.player[i];
                                    player2.activeNPCs++;
                                }
                            }
                            if (rectangle3.Intersects(new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
                            {
                                this.timeLeft = NPC.activeTime;
                            }
                            if (this.type == 7 || this.type == 10 || this.type == 13)
                            {
                                flag = true;
                            }
                            if (this.boss || this.type == 35 || this.type == 36)
                            {
                                flag = true;
                            }
                        }
                    }
                    this.timeLeft--;
                    if (this.timeLeft <= 0)
                    {
                        flag = false;
                    }
                    if (!flag && Main.netMode != 1)
                    {
                        this.active = false;
                        if (Main.netMode == 2)
                        {
                            this.life = 0;
                            NetMessage.SendData(23, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                        }
                    }
                }
            }
        }
        public void FindFrame()
        {
            int num = 1;
            if (!Main.dedServ)
            {
                num = Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type];
            }
            int num2 = 0;
            if (this.aiAction == 0)
            {
                if (this.velocity.Y < 0f)
                {
                    num2 = 2;
                }
                else
                {
                    if (this.velocity.Y > 0f)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        if (this.velocity.X != 0f)
                        {
                            num2 = 1;
                        }
                        else
                        {
                            num2 = 0;
                        }
                    }
                }
            }
            else
            {
                if (this.aiAction == 1)
                {
                    num2 = 4;
                }
            }
            if (this.type == 1 || this.type == 16)
            {
                this.frameCounter += 1.0;
                if (num2 > 0)
                {
                    this.frameCounter += 1.0;
                }
                if (num2 == 4)
                {
                    this.frameCounter += 1.0;
                }
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y = this.frame.Y + num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= num * Main.npcFrameCount[this.type])
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 2 || this.type == 23)
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X);
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 3.14f;
                }
                this.frameCounter += 1.0;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y = this.frame.Y + num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= num * Main.npcFrameCount[this.type])
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 42)
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X);
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 3.14f;
                }
                this.frameCounter += 1.0;
                if (this.frameCounter < 4.0)
                {
                    this.frame.Y = 0;
                }
                else
                {
                    if (this.frameCounter < 8.0)
                    {
                        this.frame.Y = num;
                    }
                    else
                    {
                        if (this.frameCounter < 12.0)
                        {
                            this.frame.Y = num * 2;
                        }
                        else
                        {
                            if (this.frameCounter < 16.0)
                            {
                                this.frame.Y = num;
                            }
                        }
                    }
                }
                if (this.frameCounter == 15.0)
                {
                    this.frameCounter = 0.0;
                }
            }
            if (this.type == 43)
            {
                this.frameCounter += 1.0;
                if (this.frameCounter < 6.0)
                {
                    this.frame.Y = 0;
                }
                else
                {
                    if (this.frameCounter < 12.0)
                    {
                        this.frame.Y = num;
                    }
                    else
                    {
                        if (this.frameCounter < 18.0)
                        {
                            this.frame.Y = num * 2;
                        }
                        else
                        {
                            if (this.frameCounter < 24.0)
                            {
                                this.frame.Y = num;
                            }
                        }
                    }
                }
                if (this.frameCounter == 23.0)
                {
                    this.frameCounter = 0.0;
                }
            }
            if (this.type == 17 || this.type == 18 || this.type == 19 || this.type == 20 || this.type == 22 || this.type == 38 || this.type == 26 || this.type == 27 || this.type == 28 || this.type == 31 || this.type == 21 || this.type == 44)
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                    if (this.velocity.X == 0f)
                    {
                        this.frame.Y = 0;
                        this.frameCounter = 0.0;
                    }
                    else
                    {
                        this.frameCounter += (double)(System.Math.Abs(this.velocity.X) * 2f);
                        this.frameCounter += 1.0;
                        if (this.frameCounter > 6.0)
                        {
                            this.frame.Y = this.frame.Y + num;
                            this.frameCounter = 0.0;
                        }
                        if (this.frame.Y / num >= Main.npcFrameCount[this.type])
                        {
                            this.frame.Y = num * 2;
                        }
                    }
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num;
                    if (this.type == 44 || this.type == 31 || this.type == 21)
                    {
                        this.frame.Y = num * 9;
                    }
                }
            }
            else
            {
                if (this.type == 3 || this.townNPC || this.type == 21 || this.type == 26 || this.type == 27 || this.type == 28 || this.type == 31)
                {
                    if (this.velocity.Y == 0f)
                    {
                        if (this.direction == 1)
                        {
                            this.spriteDirection = 1;
                        }
                        if (this.direction == -1)
                        {
                            this.spriteDirection = -1;
                        }
                    }
                    if (this.velocity.Y != 0f || (this.direction == -1 && this.velocity.X > 0f) || (this.direction == 1 && this.velocity.X < 0f))
                    {
                        this.frameCounter = 0.0;
                        this.frame.Y = num * 2;
                    }
                    else
                    {
                        if (this.velocity.X == 0f)
                        {
                            this.frameCounter = 0.0;
                            this.frame.Y = 0;
                        }
                        else
                        {
                            this.frameCounter += (double)System.Math.Abs(this.velocity.X);
                            if (this.frameCounter < 8.0)
                            {
                                this.frame.Y = 0;
                            }
                            else
                            {
                                if (this.frameCounter < 16.0)
                                {
                                    this.frame.Y = num;
                                }
                                else
                                {
                                    if (this.frameCounter < 24.0)
                                    {
                                        this.frame.Y = num * 2;
                                    }
                                    else
                                    {
                                        if (this.frameCounter < 32.0)
                                        {
                                            this.frame.Y = num;
                                        }
                                        else
                                        {
                                            this.frameCounter = 0.0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.type == 4)
                    {
                        this.frameCounter += 1.0;
                        if (this.frameCounter < 7.0)
                        {
                            this.frame.Y = 0;
                        }
                        else
                        {
                            if (this.frameCounter < 14.0)
                            {
                                this.frame.Y = num;
                            }
                            else
                            {
                                if (this.frameCounter < 21.0)
                                {
                                    this.frame.Y = num * 2;
                                }
                                else
                                {
                                    this.frameCounter = 0.0;
                                    this.frame.Y = 0;
                                }
                            }
                        }
                        if (this.ai[0] > 1f)
                        {
                            this.frame.Y = this.frame.Y + num * 3;
                        }
                    }
                    else
                    {
                        if (this.type == 5)
                        {
                            this.frameCounter += 1.0;
                            if (this.frameCounter >= 8.0)
                            {
                                this.frame.Y = this.frame.Y + num;
                                this.frameCounter = 0.0;
                            }
                            if (this.frame.Y >= num * Main.npcFrameCount[this.type])
                            {
                                this.frame.Y = 0;
                            }
                        }
                        else
                        {
                            if (this.type == 6)
                            {
                                this.frameCounter += 1.0;
                                if (this.frameCounter >= 8.0)
                                {
                                    this.frame.Y = this.frame.Y + num;
                                    this.frameCounter = 0.0;
                                }
                                if (this.frame.Y >= num * Main.npcFrameCount[this.type])
                                {
                                    this.frame.Y = 0;
                                }
                            }
                            else
                            {
                                if (this.type == 24)
                                {
                                    if (this.velocity.Y == 0f)
                                    {
                                        if (this.direction == 1)
                                        {
                                            this.spriteDirection = 1;
                                        }
                                        if (this.direction == -1)
                                        {
                                            this.spriteDirection = -1;
                                        }
                                    }
                                    if (this.ai[1] > 0f)
                                    {
                                        if (this.frame.Y < 4)
                                        {
                                            this.frameCounter = 0.0;
                                        }
                                        this.frameCounter += 1.0;
                                        if (this.frameCounter <= 4.0)
                                        {
                                            this.frame.Y = num * 4;
                                        }
                                        else
                                        {
                                            if (this.frameCounter <= 8.0)
                                            {
                                                this.frame.Y = num * 5;
                                            }
                                            else
                                            {
                                                if (this.frameCounter <= 12.0)
                                                {
                                                    this.frame.Y = num * 6;
                                                }
                                                else
                                                {
                                                    if (this.frameCounter <= 16.0)
                                                    {
                                                        this.frame.Y = num * 7;
                                                    }
                                                    else
                                                    {
                                                        if (this.frameCounter <= 20.0)
                                                        {
                                                            this.frame.Y = num * 8;
                                                        }
                                                        else
                                                        {
                                                            this.frame.Y = num * 9;
                                                            this.frameCounter = 100.0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.frameCounter += 1.0;
                                        if (this.frameCounter <= 4.0)
                                        {
                                            this.frame.Y = 0;
                                        }
                                        else
                                        {
                                            if (this.frameCounter <= 8.0)
                                            {
                                                this.frame.Y = num;
                                            }
                                            else
                                            {
                                                if (this.frameCounter <= 12.0)
                                                {
                                                    this.frame.Y = num * 2;
                                                }
                                                else
                                                {
                                                    this.frame.Y = num * 3;
                                                    if (this.frameCounter >= 16.0)
                                                    {
                                                        this.frameCounter = 0.0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.type == 29 || this.type == 32 || this.type == 45)
                                    {
                                        if (this.velocity.Y == 0f)
                                        {
                                            if (this.direction == 1)
                                            {
                                                this.spriteDirection = 1;
                                            }
                                            if (this.direction == -1)
                                            {
                                                this.spriteDirection = -1;
                                            }
                                        }
                                        this.frame.Y = 0;
                                        if (this.velocity.Y != 0f)
                                        {
                                            this.frame.Y = this.frame.Y + num;
                                        }
                                        else
                                        {
                                            if (this.ai[1] > 0f)
                                            {
                                                this.frame.Y = this.frame.Y + num * 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (this.type == 34)
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = -1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X);
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = 1;
                    this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 3.14f;
                }
                this.frameCounter += 1.0;
                if (this.frameCounter >= 4.0)
                {
                    this.frame.Y = this.frame.Y + num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= num * Main.npcFrameCount[this.type])
                {
                    this.frame.Y = 0;
                }
            }
        }
        public Color GetAlpha(Color newColor)
        {
            int r = (int)newColor.R - this.alpha;
            int g = (int)newColor.G - this.alpha;
            int b = (int)newColor.B - this.alpha;
            int num = (int)newColor.A - this.alpha;
            if (this.type == 25 || this.type == 30 || this.type == 33)
            {
                r = (int)newColor.R;
                g = (int)newColor.G;
                b = (int)newColor.B;
            }
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
        public string GetChat()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.npc[i].type == 17)
                {
                    flag = true;
                }
                else
                {
                    if (Main.npc[i].type == 18)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        if (Main.npc[i].type == 19)
                        {
                            flag3 = true;
                        }
                        else
                        {
                            if (Main.npc[i].type == 20)
                            {
                                flag4 = true;
                            }
                            else
                            {
                                if (Main.npc[i].type == 37)
                                {
                                    flag5 = true;
                                }
                                else
                                {
                                    if (Main.npc[i].type == 38)
                                    {
                                        flag6 = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            string text = "";
            string result;
            if (this.type == 17)
            {
                if (Main.dayTime)
                {
                    if (Main.time < 16200.0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            result = "Sword beats paper, get one today.";
                        }
                        else
                        {
                            result = "Lovely morning, wouldn't you say? Was there something you needed?";
                        }
                    }
                    else
                    {
                        if (Main.time > 37800.0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                result = "Night be upon us soon, friend. Make your choices while you can.";
                            }
                            else
                            {
                                result = "Ah, they will tell tales of " + Main.player[Main.myPlayer].name + " some day... good ones I'm sure.";
                            }
                        }
                        else
                        {
                            switch (Main.rand.Next(3))
                            {
                                case 0:
                                    {
                                        text = "Check out my dirt blocks, they are extra dirty.";
                                        break;
                                    }
                                case 1:
                                    {
                                        result = "Boy, that sun is hot! I do have some perfectly ventilated armor.";
                                        return result;
                                    }
                            }
                            result = "The sun is high, but my prices are not.";
                        }
                    }
                }
                else
                {
                    if (Main.bloodMoon)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            result = "Have you seen Chith...Shith.. Chat... The big eye?";
                        }
                        else
                        {
                            result = "Keep your eye on the prize, buy a lense!";
                        }
                    }
                    else
                    {
                        if (Main.time < 9720.0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                result = "Kosh, kapleck Mog. Oh sorry, thats klingon for 'Buy something or die.'";
                            }
                            else
                            {
                                result = Main.player[Main.myPlayer].name + " is it? I've heard good things, friend!";
                            }
                        }
                        else
                        {
                            if (Main.time > 22680.0)
                            {
                                if (Main.rand.Next(2) == 0)
                                {
                                    result = "I hear there's a secret treasure... oh never mind.";
                                }
                                else
                                {
                                    result = "Angel Statue you say? I'm sorry, I'm not a junk dealer.";
                                }
                            }
                            else
                            {
                                int num = Main.rand.Next(3);
                                if (num == 0)
                                {
                                    text = "The last guy who was here left me some junk..er I mean.. treasures!";
                                }
                                if (num == 1)
                                {
                                    result = "I wonder if the moon is made of cheese...huh, what? Oh yes, buy something!";
                                }
                                else
                                {
                                    result = "Did you say gold?  I'll take that off of ya'.";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.type == 18)
                {
                    if (flag6 && Main.rand.Next(4) == 0)
                    {
                        result = "I wish that bomb maker would be more careful.  I'm getting tired of having to sew his limbs back on every day.";
                    }
                    else
                    {
                        if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.33)
                        {
                            switch (Main.rand.Next(5))
                            {
                                case 0:
                                    {
                                        result = "I think you look better this way.";
                                        break;
                                    }
                                case 1:
                                    {
                                        result = "Eww.. What happened to your face?";
                                        break;
                                    }
                                case 2:
                                    {
                                        result = "MY GOODNESS! I'm good but I'm not THAT good.";
                                        break;
                                    }
                                case 3:
                                    {
                                        result = "Dear friends we are gathered here today to bid farewell... oh, you'll be fine.";
                                        break;
                                    }
                                default:
                                    {
                                        result = "You left your arm over there. Let me get that for you..";
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.66)
                            {
                                switch (Main.rand.Next(4))
                                {
                                    case 0:
                                        {
                                            result = "Quit being such a baby! I've seen worse.";
                                            break;
                                        }
                                    case 1:
                                        {
                                            result = "That's gonna need stitches!";
                                            break;
                                        }
                                    case 2:
                                        {
                                            result = "Trouble with those bullies again?";
                                            break;
                                        }
                                    default:
                                        {
                                            result = "You look half digested. Have you been chasing slimes again?";
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                switch (Main.rand.Next(3))
                                {
                                    case 0:
                                        {
                                            result = "Turn your head and cough.";
                                            break;
                                        }
                                    case 1:
                                        {
                                            result = "Thats not the biggest I've ever seen... Yes, I've seen bigger wounds for sure.";
                                            break;
                                        }
                                    default:
                                        {
                                            result = "Show me where it hurts.";
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.type == 19)
                    {
                        if (flag2 && Main.rand.Next(4) == 0)
                        {
                            result = "Make it quick! I've got a date with the nurse in an hour.";
                        }
                        else
                        {
                            if (flag4 && Main.rand.Next(4) == 0)
                            {
                                result = "That dryad is a looker.  Too bad she's such a prude.";
                            }
                            else
                            {
                                if (flag6 && Main.rand.Next(4) == 0)
                                {
                                    result = "Don't bother with that firework vendor, I've got all you need right here.";
                                }
                                else
                                {
                                    if (Main.bloodMoon)
                                    {
                                        result = "I love nights like tonight.  There is never a shortage of things to kill!";
                                    }
                                    else
                                    {
                                        switch (Main.rand.Next(2))
                                        {
                                            case 0:
                                                {
                                                    result = "I see you're eyeballin' the Minishark.. You really don't want to know how it was made.";
                                                    return result;
                                                }
                                            case 1:
                                                {
                                                    text = "Keep your hands off my gun, buddy!";
                                                    break;
                                                }
                                        }
                                        result = text;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.type == 20)
                        {
                            if (flag3 && Main.rand.Next(4) == 0)
                            {
                                result = "I wish that gun seller would stop talking to me. Doesn't he realize I'm 500 years old?";
                            }
                            else
                            {
                                if (flag && Main.rand.Next(4) == 0)
                                {
                                    result = "That merchant keeps trying to sell me an angel statue. Everyone knows that they don't do anything.";
                                }
                                else
                                {
                                    if (flag5 && Main.rand.Next(4) == 0)
                                    {
                                        result = "Have you seen the old man walking around the dungeon? He doesn't look well at all...";
                                    }
                                    else
                                    {
                                        if (Main.bloodMoon)
                                        {
                                            result = "It is an evil moon tonight. Be careful.";
                                        }
                                        else
                                        {
                                            switch (Main.rand.Next(6))
                                            {
                                                case 0:
                                                    {
                                                        result = "You must cleanse the world of this corruption.";
                                                        break;
                                                    }
                                                case 1:
                                                    {
                                                        result = "Be safe; Terraria needs you!";
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        result = "The sands of time are flowing. And well, you are not aging very gracefully.";
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        result = "Whats this about me having more 'bark' than bite?";
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        result = "So two goblins walk into a bar, and one says to the other, 'Want to get a Gobblet of beer?!'";
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        result = "Be safe; Terraria needs you!";
                                                        break;
                                                    }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.type == 22)
                            {
                                if (Main.bloodMoon)
                                {
                                    result = "You can tell a Blood Moon is out when the sky turns red. There is something about it that causes monsters to swarm.";
                                }
                                else
                                {
                                    if (!Main.dayTime)
                                    {
                                        result = "You should stay indoors at night. It is very dangerous to be wandering around in the dark.";
                                    }
                                    else
                                    {
                                        switch (Main.rand.Next(3))
                                        {
                                            case 0:
                                                {
                                                    result = "Greetings, " + Main.player[Main.myPlayer].name + ". Is there something I can help you with?";
                                                    return result;
                                                }
                                            case 1:
                                                {
                                                    result = "I am here to give you advice on what to do next.  It is recommended that you talk with me anytime you get stuck.";
                                                    return result;
                                                }
                                            case 2:
                                                {
                                                    text = "They say there is a person who will tell you how to survive in this land... oh wait. Thats me.";
                                                    break;
                                                }
                                        }
                                        result = text;
                                    }
                                }
                            }
                            else
                            {
                                if (this.type == 37)
                                {
                                    if (Main.dayTime)
                                    {
                                        switch (Main.rand.Next(2))
                                        {
                                            case 0:
                                                {
                                                    result = "I cannot let you enter until you free me of my curse.";
                                                    return result;
                                                }
                                            case 1:
                                                {
                                                    text = "Come back at night if you wish to enter.";
                                                    break;
                                                }
                                        }
                                    }
                                    result = text;
                                }
                                else
                                {
                                    if (this.type != 38)
                                    {
                                        result = text;
                                    }
                                    else
                                    {
                                        if (Main.bloodMoon)
                                        {
                                            result = "I've got something for them zombies alright!";
                                        }
                                        else
                                        {
                                            if (flag3 && Main.rand.Next(4) == 0)
                                            {
                                                result = "Even the gun dealer wants what I'm selling!";
                                            }
                                            else
                                            {
                                                if (flag2 && Main.rand.Next(4) == 0)
                                                {
                                                    result = "I'm sure the nurse will help if you accidentally lose a limb to these.";
                                                }
                                                else
                                                {
                                                    if (flag4 && Main.rand.Next(4) == 0)
                                                    {
                                                        result = "Why purify the world when you can just blow it up?";
                                                    }
                                                    else
                                                    {
                                                        int num = Main.rand.Next(4);
                                                        if (num == 0)
                                                        {
                                                            result = "Explosives are da' bomb these days.  Buy some now!";
                                                        }
                                                        else
                                                        {
                                                            if (num == 1)
                                                            {
                                                                result = "It's a good day to die!";
                                                            }
                                                            else
                                                            {
                                                                if (num == 2)
                                                                {
                                                                    result = "I wonder what happens if I... (BOOM!)... Oh, sorry, did you need that leg?";
                                                                }
                                                                else
                                                                {
                                                                    if (num == 3)
                                                                    {
                                                                        result = "Dynamite, my own special cure-all for what ails ya.";
                                                                    }
                                                                    else
                                                                    {
                                                                        result = "Check out my goods; they have explosive prices!";
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
            return result;
        }
        public Color GetColor(Color newColor)
        {
            int num = (int)(this.color.R - (255 - newColor.R));
            int num2 = (int)(this.color.G - (255 - newColor.G));
            int num3 = (int)(this.color.B - (255 - newColor.B));
            int num4 = (int)(this.color.A - (255 - newColor.A));
            if (num < 0)
            {
                num = 0;
            }
            if (num > 255)
            {
                num = 255;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num2 > 255)
            {
                num2 = 255;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num3 > 255)
            {
                num3 = 255;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num4 > 255)
            {
                num4 = 255;
            }
            return new Color(num, num2, num3, num4);
        }
        public void HitEffect(int hitDirection = 0, double dmg = 10.0)
        {
            if (this.type == 1 || this.type == 16)
            {
                if (this.life > 0)
                {
                    int i = 0;
                    while ((double)i < dmg / (double)this.lifeMax * 100.0)
                    {
                        i++;
                    }
                }
                else
                {
                    for (int i = 0; i < 50; i++)
                    {
                    }
                    if (Main.netMode != 1 && this.type == 16)
                    {
                        int num = Main.rand.Next(2) + 2;
                        for (int j = 0; j < num; j++)
                        {
                            int num2 = NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y + this.height, 1, 0);
                            Main.npc[num2].SetDefaults("Baby Slime");
                            Main.npc[num2].velocity.X = this.velocity.X * 2f;
                            Main.npc[num2].velocity.Y = this.velocity.Y;
                            NPC expr_13E_cp_0 = Main.npc[num2];
                            expr_13E_cp_0.velocity.X = expr_13E_cp_0.velocity.X + ((float)Main.rand.Next(-20, 20) * 0.1f + (float)(j * this.direction) * 0.3f);
                            NPC expr_17B_cp_0 = Main.npc[num2];
                            expr_17B_cp_0.velocity.Y = expr_17B_cp_0.velocity.Y - ((float)Main.rand.Next(0, 10) * 0.1f + (float)j);
                            Main.npc[num2].ai[1] = (float)j;
                            if (Main.netMode == 2 && num2 < 1000)
                            {
                                NetMessage.SendData(23, -1, -1, "", num2, 0f, 0f, 0f);
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.type == 2)
                {
                    if (this.life > 0)
                    {
                        int i = 0;
                        while ((double)i < dmg / (double)this.lifeMax * 100.0)
                        {
                            Color color = default(Color);
                            i++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Color color = default(Color);
                        }
                        Gore.NewGore(this.position, this.velocity, 1);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 2);
                    }
                }
                else
                {
                    if (this.type == 3)
                    {
                        if (this.life > 0)
                        {
                            int i = 0;
                            while ((double)i < dmg / (double)this.lifeMax * 100.0)
                            {
                                Color color = default(Color);
                                i++;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 50; i++)
                            {
                                Color color = default(Color);
                            }
                            Gore.NewGore(this.position, this.velocity, 3);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                        }
                    }
                    else
                    {
                        if (this.type == 4)
                        {
                            if (this.life > 0)
                            {
                                int i = 0;
                                while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                {
                                    Color color = default(Color);
                                    i++;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 150; i++)
                                {
                                    Color color = default(Color);
                                }
                                for (int i = 0; i < 2; i++)
                                {
                                    Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 2);
                                    Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
                                    Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 9);
                                    Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 10);
                                }
                            }
                        }
                        else
                        {
                            if (this.type == 5)
                            {
                                if (this.life > 0)
                                {
                                    int i = 0;
                                    while ((double)i < dmg / (double)this.lifeMax * 50.0)
                                    {
                                        Color color = default(Color);
                                        i++;
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 20; i++)
                                    {
                                        Color color = default(Color);
                                    }
                                    Gore.NewGore(this.position, this.velocity, 6);
                                    Gore.NewGore(this.position, this.velocity, 7);
                                }
                            }
                            else
                            {
                                if (this.type == 6)
                                {
                                    if (this.life > 0)
                                    {
                                        int i = 0;
                                        while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                        {
                                            i++;
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < 50; i++)
                                        {
                                        }
                                        int num3 = Gore.NewGore(this.position, this.velocity, 14);
                                        Main.gore[num3].alpha = this.alpha;
                                        num3 = Gore.NewGore(this.position, this.velocity, 15);
                                        Main.gore[num3].alpha = this.alpha;
                                    }
                                }
                                else
                                {
                                    if (this.type == 7 || this.type == 8 || this.type == 9)
                                    {
                                        if (this.life > 0)
                                        {
                                            int i = 0;
                                            while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                            {
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < 50; i++)
                                            {
                                            }
                                            int num3 = Gore.NewGore(this.position, this.velocity, this.type - 7 + 18);
                                            Main.gore[num3].alpha = this.alpha;
                                        }
                                    }
                                    else
                                    {
                                        if (this.type == 10 || this.type == 11 || this.type == 12)
                                        {
                                            if (this.life > 0)
                                            {
                                                int i = 0;
                                                while ((double)i < dmg / (double)this.lifeMax * 50.0)
                                                {
                                                    Color color = default(Color);
                                                    i++;
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < 10; i++)
                                                {
                                                    Color color = default(Color);
                                                }
                                                Gore.NewGore(this.position, this.velocity, this.type - 7 + 18);
                                            }
                                        }
                                        else
                                        {
                                            if (this.type == 13 || this.type == 14 || this.type == 15)
                                            {
                                                if (this.life > 0)
                                                {
                                                    int i = 0;
                                                    while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                    {
                                                        i++;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < 50; i++)
                                                    {
                                                    }
                                                    if (this.type == 13)
                                                    {
                                                        Gore.NewGore(this.position, this.velocity, 24);
                                                        Gore.NewGore(this.position, this.velocity, 25);
                                                    }
                                                    else
                                                    {
                                                        if (this.type == 14)
                                                        {
                                                            Gore.NewGore(this.position, this.velocity, 26);
                                                            Gore.NewGore(this.position, this.velocity, 27);
                                                        }
                                                        else
                                                        {
                                                            Gore.NewGore(this.position, this.velocity, 28);
                                                            Gore.NewGore(this.position, this.velocity, 29);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.type == 17)
                                                {
                                                    if (this.life > 0)
                                                    {
                                                        int i = 0;
                                                        while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                        {
                                                            Color color = default(Color);
                                                            i++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        for (int i = 0; i < 50; i++)
                                                        {
                                                            Color color = default(Color);
                                                        }
                                                        Gore.NewGore(this.position, this.velocity, 30);
                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 31);
                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 31);
                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 32);
                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 32);
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.type == 22)
                                                    {
                                                        if (this.life > 0)
                                                        {
                                                            int i = 0;
                                                            while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                            {
                                                                Color color = default(Color);
                                                                i++;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            for (int i = 0; i < 50; i++)
                                                            {
                                                                Color color = default(Color);
                                                            }
                                                            Gore.NewGore(this.position, this.velocity, 73);
                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 74);
                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 74);
                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 75);
                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 75);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (this.type == 37)
                                                        {
                                                            if (this.life > 0)
                                                            {
                                                                int i = 0;
                                                                while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                {
                                                                    Color color = default(Color);
                                                                    i++;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                for (int i = 0; i < 50; i++)
                                                                {
                                                                    Color color = default(Color);
                                                                }
                                                                Gore.NewGore(this.position, this.velocity, 58);
                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 59);
                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 59);
                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this.type == 18)
                                                            {
                                                                if (this.life > 0)
                                                                {
                                                                    int i = 0;
                                                                    while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                    {
                                                                        Color color = default(Color);
                                                                        i++;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    for (int i = 0; i < 50; i++)
                                                                    {
                                                                        Color color = default(Color);
                                                                    }
                                                                    Gore.NewGore(this.position, this.velocity, 33);
                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 34);
                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 34);
                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 35);
                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 35);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (this.type == 19)
                                                                {
                                                                    if (this.life > 0)
                                                                    {
                                                                        int i = 0;
                                                                        while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                        {
                                                                            Color color = default(Color);
                                                                            i++;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        for (int i = 0; i < 50; i++)
                                                                        {
                                                                            Color color = default(Color);
                                                                        }
                                                                        Gore.NewGore(this.position, this.velocity, 36);
                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 37);
                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 37);
                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 38);
                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 38);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (this.type == 38)
                                                                    {
                                                                        if (this.life > 0)
                                                                        {
                                                                            int i = 0;
                                                                            while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                            {
                                                                                Color color = default(Color);
                                                                                i++;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            for (int i = 0; i < 50; i++)
                                                                            {
                                                                                Color color = default(Color);
                                                                            }
                                                                            Gore.NewGore(this.position, this.velocity, 64);
                                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 65);
                                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 65);
                                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 66);
                                                                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 66);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.type == 20)
                                                                        {
                                                                            if (this.life > 0)
                                                                            {
                                                                                int i = 0;
                                                                                while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                {
                                                                                    Color color = default(Color);
                                                                                    i++;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                for (int i = 0; i < 50; i++)
                                                                                {
                                                                                    Color color = default(Color);
                                                                                }
                                                                                Gore.NewGore(this.position, this.velocity, 39);
                                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 41);
                                                                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 41);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.type == 21 || this.type == 31 || this.type == 32 || this.type == 44 || this.type == 45)
                                                                            {
                                                                                if (this.life > 0)
                                                                                {
                                                                                    int i = 0;
                                                                                    while ((double)i < dmg / (double)this.lifeMax * 50.0)
                                                                                    {
                                                                                        Color color = default(Color);
                                                                                        i++;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    for (int i = 0; i < 20; i++)
                                                                                    {
                                                                                        Color color = default(Color);
                                                                                    }
                                                                                    Gore.NewGore(this.position, this.velocity, 42);
                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 43);
                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 43);
                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 44);
                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 44);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.type == 39 || this.type == 40 || this.type == 41)
                                                                                {
                                                                                    if (this.life > 0)
                                                                                    {
                                                                                        int i = 0;
                                                                                        while ((double)i < dmg / (double)this.lifeMax * 50.0)
                                                                                        {
                                                                                            Color color = default(Color);
                                                                                            i++;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        for (int i = 0; i < 20; i++)
                                                                                        {
                                                                                            Color color = default(Color);
                                                                                        }
                                                                                        Gore.NewGore(this.position, this.velocity, this.type - 39 + 67);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (this.type == 34)
                                                                                    {
                                                                                        if (this.life > 0)
                                                                                        {
                                                                                            int i = 0;
                                                                                            while ((double)i < dmg / (double)this.lifeMax * 50.0)
                                                                                            {
                                                                                                Color color = default(Color);
                                                                                                int num4 = 0;
                                                                                                Main.dust[num4].noLight = true;
                                                                                                Main.dust[num4].noGravity = true;
                                                                                                Dust dust = Main.dust[num4];
                                                                                                dust.velocity *= 2f;
                                                                                                color = default(Color);
                                                                                                num4 = 0;
                                                                                                Main.dust[num4].noLight = true;
                                                                                                Dust dust2 = Main.dust[num4];
                                                                                                dust2.velocity *= 2f;
                                                                                                i++;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            for (int i = 0; i < 20; i++)
                                                                                            {
                                                                                                Color color = default(Color);
                                                                                                int num4 = 0;
                                                                                                Main.dust[num4].noLight = true;
                                                                                                Main.dust[num4].noGravity = true;
                                                                                                Dust dust3 = Main.dust[num4];
                                                                                                dust3.velocity *= 2f;
                                                                                                color = default(Color);
                                                                                                num4 = 0;
                                                                                                Main.dust[num4].noLight = true;
                                                                                                Dust dust4 = Main.dust[num4];
                                                                                                dust4.velocity *= 2f;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (this.type == 35 || this.type == 36)
                                                                                        {
                                                                                            if (this.life > 0)
                                                                                            {
                                                                                                int i = 0;
                                                                                                while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                {
                                                                                                    Color color = default(Color);
                                                                                                    i++;
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                for (int i = 0; i < 150; i++)
                                                                                                {
                                                                                                    Color color = default(Color);
                                                                                                }
                                                                                                if (this.type == 35)
                                                                                                {
                                                                                                    Gore.NewGore(this.position, this.velocity, 54);
                                                                                                    Gore.NewGore(this.position, this.velocity, 55);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Gore.NewGore(this.position, this.velocity, 56);
                                                                                                    Gore.NewGore(this.position, this.velocity, 57);
                                                                                                    Gore.NewGore(this.position, this.velocity, 57);
                                                                                                    Gore.NewGore(this.position, this.velocity, 57);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (this.type == 23)
                                                                                            {
                                                                                                if (this.life > 0)
                                                                                                {
                                                                                                    int i = 0;
                                                                                                    while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                    {
                                                                                                        if (Main.rand.Next(2) != 0)
                                                                                                        {
                                                                                                            goto IL_182E;
                                                                                                        }
                                                                                                    IL_182E:
                                                                                                        Color color = default(Color);
                                                                                                        color = default(Color);
                                                                                                        int num4 = 0;
                                                                                                        Main.dust[num4].noGravity = true;
                                                                                                        i++;
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    for (int i = 0; i < 50; i++)
                                                                                                    {
                                                                                                        if (Main.rand.Next(2) != 0)
                                                                                                        {
                                                                                                            goto IL_189D;
                                                                                                        }
                                                                                                    IL_189D:
                                                                                                        Color color = default(Color);
                                                                                                    }
                                                                                                    for (int i = 0; i < 50; i++)
                                                                                                    {
                                                                                                        Color color = default(Color);
                                                                                                        int num4 = 0;
                                                                                                        Dust dust5 = Main.dust[num4];
                                                                                                        dust5.velocity *= 6f;
                                                                                                        Main.dust[num4].noGravity = true;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (this.type == 24)
                                                                                                {
                                                                                                    if (this.life > 0)
                                                                                                    {
                                                                                                        int i = 0;
                                                                                                        while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                        {
                                                                                                            Color color = default(Color);
                                                                                                            int num4 = 0;
                                                                                                            Main.dust[num4].noGravity = true;
                                                                                                            i++;
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        for (int i = 0; i < 50; i++)
                                                                                                        {
                                                                                                            Color color = default(Color);
                                                                                                            int num4 = 0;
                                                                                                            Main.dust[num4].noGravity = true;
                                                                                                            Dust dust6 = Main.dust[num4];
                                                                                                            dust6.velocity *= 2f;
                                                                                                        }
                                                                                                        Gore.NewGore(this.position, this.velocity, 45);
                                                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 46);
                                                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 46);
                                                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 47);
                                                                                                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 47);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (this.type == 25)
                                                                                                    {
                                                                                                        for (int k = 0; k < 20; k++)
                                                                                                        {
                                                                                                            Color color = default(Color);
                                                                                                            int num4 = 0;
                                                                                                            Main.dust[num4].noGravity = true;
                                                                                                            Dust dust7 = Main.dust[num4];
                                                                                                            dust7.velocity *= 2f;
                                                                                                            color = default(Color);
                                                                                                            num4 = 0;
                                                                                                            Dust dust8 = Main.dust[num4];
                                                                                                            dust8.velocity *= 2f;
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (this.type == 33)
                                                                                                        {
                                                                                                            for (int k = 0; k < 20; k++)
                                                                                                            {
                                                                                                                Color color = default(Color);
                                                                                                                int num4 = 0;
                                                                                                                Main.dust[num4].noGravity = true;
                                                                                                                Dust dust9 = Main.dust[num4];
                                                                                                                dust9.velocity *= 2f;
                                                                                                                color = default(Color);
                                                                                                                num4 = 0;
                                                                                                                Dust dust10 = Main.dust[num4];
                                                                                                                dust10.velocity *= 2f;
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (this.type == 26 || this.type == 27 || this.type == 28 || this.type == 29)
                                                                                                            {
                                                                                                                if (this.life > 0)
                                                                                                                {
                                                                                                                    int i = 0;
                                                                                                                    while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                                    {
                                                                                                                        Color color = default(Color);
                                                                                                                        i++;
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    for (int i = 0; i < 50; i++)
                                                                                                                    {
                                                                                                                        Color color = default(Color);
                                                                                                                    }
                                                                                                                    Gore.NewGore(this.position, this.velocity, 48);
                                                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 49);
                                                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 49);
                                                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                                                                                                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (this.type == 30)
                                                                                                                {
                                                                                                                    for (int k = 0; k < 20; k++)
                                                                                                                    {
                                                                                                                        Color color = default(Color);
                                                                                                                        int num4 = 0;
                                                                                                                        Main.dust[num4].noGravity = true;
                                                                                                                        Dust dust11 = Main.dust[num4];
                                                                                                                        dust11.velocity *= 2f;
                                                                                                                        color = default(Color);
                                                                                                                        num4 = 0;
                                                                                                                        Dust dust12 = Main.dust[num4];
                                                                                                                        dust12.velocity *= 2f;
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (this.type == 42)
                                                                                                                    {
                                                                                                                        if (this.life > 0)
                                                                                                                        {
                                                                                                                            int i = 0;
                                                                                                                            while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                                            {
                                                                                                                                i++;
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            for (int i = 0; i < 50; i++)
                                                                                                                            {
                                                                                                                            }
                                                                                                                            Gore.NewGore(this.position, this.velocity, 70);
                                                                                                                            Gore.NewGore(this.position, this.velocity, 71);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (this.type == 43)
                                                                                                                        {
                                                                                                                            if (this.life > 0)
                                                                                                                            {
                                                                                                                                int i = 0;
                                                                                                                                while ((double)i < dmg / (double)this.lifeMax * 100.0)
                                                                                                                                {
                                                                                                                                    i++;
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                for (int i = 0; i < 50; i++)
                                                                                                                                {
                                                                                                                                }
                                                                                                                                Gore.NewGore(this.position, this.velocity, 72);
                                                                                                                                Gore.NewGore(this.position, this.velocity, 72);
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
        public static int NewNPC(int X, int Y, int Type, int Start = 0)
        {
            int num = -1;
            for (int i = Start; i < 1000; i++)
            {
                if (!Main.npc[i].active)
                {
                    num = i;
                    break;
                }
            }
            int result;
            if (num >= 0)
            {
                Main.npc[num] = new NPC();
                Main.npc[num].SetDefaults(Type);
                Main.npc[num].position.X = (float)(X - Main.npc[num].width / 2);
                Main.npc[num].position.Y = (float)(Y - Main.npc[num].height);
                Main.npc[num].active = true;
                Main.npc[num].timeLeft = (int)((double)NPC.activeTime * 1.25);
                Main.npc[num].wet = Collision.WetCollision(Main.npc[num].position, Main.npc[num].width, Main.npc[num].height);
                result = num;
            }
            else
            {
                result = 1000;
            }
            return result;
        }
        public void NPCLoot()
        {
            int num;
            if ((this.type == 1) || (this.type == 0x10))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x17, Main.rand.Next(1, 3), false);
            }
            if (this.type == 2)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x26, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xec, 1, false);
                }
            }
            if ((this.type == 3) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xd8, 1, false);
            }
            if (this.type == 4)
            {
                num = Main.rand.Next(30) + 20;
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x2f, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(3) + 1;
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x3b, num, false);
            }
            if ((this.type == 6) && (Main.rand.Next(3) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x44, 1, false);
            }
            if (((this.type == 7) || (this.type == 8)) || (this.type == 9))
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x44, Main.rand.Next(1, 3), false);
                }
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x45, Main.rand.Next(3, 9), false);
            }
            if ((((this.type == 10) || (this.type == 11)) || (this.type == 12)) && (Main.rand.Next(500) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xd7, 1, false);
            }
            if (((this.type == 0x27) || (this.type == 40)) || (this.type == 0x29))
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 220, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xda, 1, false);
                }
            }
            if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
            {
                num = Main.rand.Next(1, 4);
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x56, num, false);
                if (Main.rand.Next(2) == 0)
                {
                    num = Main.rand.Next(2, 6);
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                }
                if (this.boss)
                {
                    num = Main.rand.Next(15, 30);
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                    num = Main.rand.Next(15, 0x1f);
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x38, num, false);
                    int type = Main.rand.Next(100, 0x67);
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type, 1, false);
                }
            }
            if ((this.type == 0x15) || (this.type == 0x2c))
            {
                if (Main.rand.Next(0x19) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x76, 1, false);
                }
                else if (this.type == 0x2c)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xa6, Main.rand.Next(1, 4), false);
                }
            }
            if (this.type == 0x2d)
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xee, 1, false);
            }
            if ((this.type == 0x17) && (Main.rand.Next(3) == 0))
            {
            }
            if ((this.type == 0x18) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x70, 1, false);
            }
            if ((this.type == 0x1f) || (this.type == 0x20))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x9a, 1, false);
            }
            if ((((this.type == 0x1a) || (this.type == 0x1b)) || (this.type == 0x1c)) || (this.type == 0x1d))
            {
                if (Main.rand.Next(400) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x80, 1, false);
                }
                else if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 160, 1, false);
                }
                else if (Main.rand.Next(2) == 0)
                {
                    num = Main.rand.Next(1, 6);
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xa1, num, false);
                }
            }
            if (this.type == 0x2a)
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xd1, 1, false);
            }
            if ((this.type == 0x2b) && (Main.rand.Next(5) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 210, 1, false);
            }
            if (((this.type == 0x2a) || (this.type == 0x2b)) && (Main.rand.Next(150) == 0))
            {
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.rand.Next(0xe4, 0xe7), 1, false);
            }
            if (this.boss)
            {
                if (this.type == 4)
                {
                    downedBoss1 = true;
                }
                if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                {
                    downedBoss2 = true;
                    this.name = "Eater of Worlds";
                }
                if (this.type == 0x23)
                {
                    downedBoss3 = true;
                    this.name = "Skeletron";
                }
                num = Main.rand.Next(5, 0x10);
                Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x1c, num, false);
                int num3 = Main.rand.Next(5) + 5;
                for (int i = 0; i < num3; i++)
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
                if ((Main.netMode != 0) && (Main.netMode == 2))
                {
                    NetMessage.SendData(0x19, -1, -1, this.name + " has been defeated!", 0xff, 175f, 75f, 255f);
                }
            }
            if (Main.rand.Next(7) == 0)
            {
                if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statMana < Main.player[Player.FindClosest(this.position, this.width, this.height)].statManaMax))
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0xb8, 1, false);
                }
                else if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statLife < Main.player[Player.FindClosest(this.position, this.width, this.height)].statLifeMax))
                {
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
            }
            float num5 = this.value * (1f + (Main.rand.Next(-20, 0x15) * 0.01f));
            if (Main.rand.Next(5) == 0)
            {
                num5 *= 1f + (Main.rand.Next(5, 11) * 0.01f);
            }
            if (Main.rand.Next(10) == 0)
            {
                num5 *= 1f + (Main.rand.Next(10, 0x15) * 0.01f);
            }
            if (Main.rand.Next(15) == 0)
            {
                num5 *= 1f + (Main.rand.Next(15, 0x1f) * 0.01f);
            }
            if (Main.rand.Next(20) == 0)
            {
                num5 *= 1f + (Main.rand.Next(20, 0x29) * 0.01f);
            }
            while (((int)num5) > 0)
            {
                if (num5 > 1000000f)
                {
                    num = (int)(num5 / 1000000f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 0xf4240 * num;
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x4a, num, false);
                }
                else if (num5 > 10000f)
                {
                    num = (int)(num5 / 10000f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 0x2710 * num;
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x49, num, false);
                }
                else if (num5 > 100f)
                {
                    num = (int)(num5 / 100f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 100 * num;
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x48, num, false);
                }
                else
                {
                    num = (int)num5;
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(4) + 1;
                    }
                    if (num < 1)
                    {
                        num = 1;
                    }
                    num5 -= num;
                    Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 0x47, num, false);
                }
            }
        }
        public void SetDefaults(int Type)
        {
            this.lavaWet = false;
            this.wetCount = 0;
            this.wet = false;
            this.townNPC = false;
            this.homeless = false;
            this.homeTileX = -1;
            this.homeTileY = -1;
            this.friendly = false;
            this.behindTiles = false;
            this.boss = false;
            this.noTileCollide = false;
            this.rotation = 0f;
            this.active = true;
            this.alpha = 0;
            this.color = default(Color);
            this.collideX = false;
            this.collideY = false;
            this.direction = 0;
            this.oldDirection = this.direction;
            this.frameCounter = 0.0;
            this.netUpdate = false;
            this.knockBackResist = 1f;
            this.name = "";
            this.noGravity = false;
            this.scale = 1f;
            this.soundHit = 0;
            this.soundKilled = 0;
            this.spriteDirection = -1;
            this.target = 255;
            this.oldTarget = this.target;
            this.targetRect = default(Rectangle);
            this.timeLeft = NPC.activeTime;
            this.type = Type;
            this.value = 0f;
            for (int i = 0; i < NPC.maxAI; i++)
            {
                this.ai[i] = 0f;
            }
            if (this.type == 1)
            {
                this.name = "Blue Slime";
                this.width = 24;
                this.height = 18;
                this.aiStyle = 1;
                this.damage = 7;
                this.defense = 2;
                this.lifeMax = 25;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 175;
                this.color = new Color(0, 80, 255, 100);
                this.value = 25f;
            }
            if (this.type == 2)
            {
                this.name = "Demon Eye";
                this.width = 30;
                this.height = 32;
                this.aiStyle = 2;
                this.damage = 18;
                this.defense = 2;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 75f;
            }
            if (this.type == 3)
            {
                this.name = "Zombie";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 14;
                this.defense = 6;
                this.lifeMax = 45;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 60f;
            }
            if (this.type == 4)
            {
                this.name = "Eye of Cthulhu";
                this.width = 100;
                this.height = 110;
                this.aiStyle = 4;
                this.damage = 18;
                this.defense = 12;
                this.lifeMax = 3000;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0f;
                this.noGravity = true;
                this.noTileCollide = true;
                this.timeLeft = NPC.activeTime * 30;
                this.boss = true;
                this.value = 30000f;
            }
            if (this.type == 5)
            {
                this.name = "Servant of Cthulhu";
                this.width = 20;
                this.height = 20;
                this.aiStyle = 5;
                this.damage = 23;
                this.defense = 0;
                this.lifeMax = 8;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
            }
            if (this.type == 6)
            {
                this.name = "Eater of Souls";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 5;
                this.damage = 15;
                this.defense = 8;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.knockBackResist = 0.5f;
                this.value = 90f;
            }
            if (this.type == 7)
            {
                this.name = "Devourer Head";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 28;
                this.defense = 2;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 8)
            {
                this.name = "Devourer Body";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 18;
                this.defense = 6;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 9)
            {
                this.name = "Devourer Tail";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 13;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 10)
            {
                this.name = "Giant Worm Head";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 8;
                this.defense = 0;
                this.lifeMax = 10;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 11)
            {
                this.name = "Giant Worm Body";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 4;
                this.lifeMax = 15;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 12)
            {
                this.name = "Giant Worm Tail";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 6;
                this.lifeMax = 20;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 13)
            {
                this.name = "Eater of Worlds Head";
                this.width = 38;
                this.height = 38;
                this.aiStyle = 6;
                this.damage = 40;
                this.defense = 0;
                this.lifeMax = 120;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 14)
            {
                this.name = "Eater of Worlds Body";
                this.width = 38;
                this.height = 38;
                this.aiStyle = 6;
                this.damage = 15;
                this.defense = 4;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 15)
            {
                this.name = "Eater of Worlds Tail";
                this.width = 38;
                this.height = 38;
                this.aiStyle = 6;
                this.damage = 10;
                this.defense = 8;
                this.lifeMax = 300;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 16)
            {
                this.name = "Mother Slime";
                this.width = 36;
                this.height = 24;
                this.aiStyle = 1;
                this.damage = 20;
                this.defense = 7;
                this.lifeMax = 90;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 120;
                this.color = new Color(0, 0, 0, 50);
                this.value = 75f;
                this.scale = 1.25f;
                this.knockBackResist = 0.6f;
            }
            if (this.type == 17)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Merchant";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 18)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Nurse";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 19)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Arms Dealer";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 20)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Dryad";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 21)
            {
                this.name = "Skeleton";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 8;
                this.lifeMax = 60;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 250f;
            }
            if (this.type == 22)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Guide";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 23)
            {
                this.name = "Meteor Head";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 5;
                this.damage = 25;
                this.defense = 10;
                this.lifeMax = 50;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 300f;
                this.knockBackResist = 0.8f;
            }
            else
            {
                if (this.type == 24)
                {
                    this.name = "Fire Imp";
                    this.width = 18;
                    this.height = 40;
                    this.aiStyle = 8;
                    this.damage = 30;
                    this.defense = 20;
                    this.lifeMax = 80;
                    this.soundHit = 1;
                    this.soundKilled = 1;
                    this.knockBackResist = 0.5f;
                    this.value = 800f;
                }
            }
            if (this.type == 25)
            {
                this.name = "Burning Sphere";
                this.width = 16;
                this.height = 16;
                this.aiStyle = 9;
                this.damage = 25;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.alpha = 100;
            }
            if (this.type == 26)
            {
                this.name = "Goblin Peon";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 12;
                this.defense = 4;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.8f;
                this.value = 250f;
            }
            if (this.type == 27)
            {
                this.name = "Goblin Thief";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 6;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.7f;
                this.value = 600f;
            }
            if (this.type == 28)
            {
                this.name = "Goblin Warrior";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 25;
                this.defense = 8;
                this.lifeMax = 110;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
                this.value = 500f;
            }
            else
            {
                if (this.type == 29)
                {
                    this.name = "Goblin Sorcerer";
                    this.width = 18;
                    this.height = 40;
                    this.aiStyle = 8;
                    this.damage = 20;
                    this.defense = 2;
                    this.lifeMax = 40;
                    this.soundHit = 1;
                    this.soundKilled = 1;
                    this.knockBackResist = 0.6f;
                    this.value = 800f;
                }
                else
                {
                    if (this.type == 30)
                    {
                        this.name = "Chaos Ball";
                        this.width = 16;
                        this.height = 16;
                        this.aiStyle = 9;
                        this.damage = 20;
                        this.defense = 0;
                        this.lifeMax = 1;
                        this.soundHit = 3;
                        this.soundKilled = 3;
                        this.noGravity = true;
                        this.noTileCollide = true;
                        this.alpha = 100;
                        this.knockBackResist = 0f;
                    }
                    else
                    {
                        if (this.type == 31)
                        {
                            this.name = "Angry Bones";
                            this.width = 18;
                            this.height = 40;
                            this.aiStyle = 3;
                            this.damage = 30;
                            this.defense = 10;
                            this.lifeMax = 100;
                            this.soundHit = 2;
                            this.soundKilled = 2;
                            this.knockBackResist = 0.7f;
                            this.value = 500f;
                        }
                        else
                        {
                            if (this.type == 32)
                            {
                                this.name = "Dark Caster";
                                this.width = 18;
                                this.height = 40;
                                this.aiStyle = 8;
                                this.damage = 20;
                                this.defense = 4;
                                this.lifeMax = 50;
                                this.soundHit = 2;
                                this.soundKilled = 2;
                                this.knockBackResist = 0.6f;
                                this.value = 800f;
                            }
                            else
                            {
                                if (this.type == 33)
                                {
                                    this.name = "Water Sphere";
                                    this.width = 16;
                                    this.height = 16;
                                    this.aiStyle = 9;
                                    this.damage = 20;
                                    this.defense = 0;
                                    this.lifeMax = 1;
                                    this.soundHit = 3;
                                    this.soundKilled = 3;
                                    this.noGravity = true;
                                    this.noTileCollide = true;
                                    this.alpha = 100;
                                    this.knockBackResist = 0f;
                                }
                            }
                        }
                    }
                }
            }
            if (this.type == 34)
            {
                this.name = "Burning Skull";
                this.width = 26;
                this.height = 28;
                this.aiStyle = 10;
                this.damage = 25;
                this.defense = 30;
                this.lifeMax = 30;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.value = 300f;
                this.knockBackResist = 1.2f;
            }
            if (this.type == 35)
            {
                this.name = "Skeletron Head";
                this.width = 80;
                this.height = 102;
                this.aiStyle = 11;
                this.damage = 35;
                this.defense = 12;
                this.lifeMax = 6000;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 50000f;
                this.knockBackResist = 0f;
                this.boss = true;
            }
            if (this.type == 36)
            {
                this.name = "Skeletron Hand";
                this.width = 52;
                this.height = 52;
                this.aiStyle = 12;
                this.damage = 30;
                this.defense = 18;
                this.lifeMax = 1200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
            }
            if (this.type == 37)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Old Man";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 100;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 38)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Demolitionist";
                this.width = 18;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 39)
            {
                this.name = "Bone Serpent Head";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 40;
                this.defense = 10;
                this.lifeMax = 120;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 40)
            {
                this.name = "Bone Serpent Body";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 30;
                this.defense = 12;
                this.lifeMax = 150;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 41)
            {
                this.name = "Bone Serpent Tail";
                this.width = 22;
                this.height = 22;
                this.aiStyle = 6;
                this.damage = 20;
                this.defense = 18;
                this.lifeMax = 200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 42)
            {
                this.name = "Hornet";
                this.width = 34;
                this.height = 32;
                this.aiStyle = 2;
                this.damage = 40;
                this.defense = 14;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 750f;
            }
            if (this.type == 43)
            {
                this.noGravity = true;
                this.name = "Man Eater";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 13;
                this.damage = 60;
                this.defense = 18;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.knockBackResist = 0.7f;
                this.soundKilled = 1;
                this.value = 750f;
            }
            else
            {
                if (this.type == 44)
                {
                    this.name = "Dead Miner";
                    this.width = 18;
                    this.height = 40;
                    this.aiStyle = 3;
                    this.damage = 20;
                    this.defense = 8;
                    this.lifeMax = 60;
                    this.soundHit = 2;
                    this.soundKilled = 2;
                    this.knockBackResist = 0.5f;
                    this.value = 250f;
                }
                else
                {
                    if (this.type == 45)
                    {
                        this.name = "Tim";
                        this.width = 18;
                        this.height = 40;
                        this.aiStyle = 8;
                        this.damage = 20;
                        this.defense = 4;
                        this.lifeMax = 50;
                        this.soundHit = 2;
                        this.soundKilled = 2;
                        this.knockBackResist = 0.6f;
                        this.value = 800f;
                    }
                }
            }
            if (Main.dedServ)
            {
                this.frame = default(Rectangle);
            }
            else
            {
                this.frame = new Rectangle(0, 0, Main.npcTexture[this.type].Width, Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type]);
            }
            this.width = (int)((float)this.width * this.scale);
            this.height = (int)((float)this.height * this.scale);
            this.life = this.lifeMax;
            if (Main.dumbAI)
            {
                this.aiStyle = 0;
            }
        }
        public void SetDefaults(string Name)
        {
            this.SetDefaults(0);
            if (Name == "Green Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.9f;
                this.damage = 8;
                this.defense = 2;
                this.life = 15;
                this.knockBackResist = 1.1f;
                this.color = new Color(0, 220, 40, 100);
                this.value = 3f;
            }
            else
            {
                if (Name == "Pinky")
                {
                    this.SetDefaults(1);
                    this.name = Name;
                    this.scale = 0.6f;
                    this.damage = 5;
                    this.defense = 5;
                    this.life = 150;
                    this.knockBackResist = 1.4f;
                    this.color = new Color(250, 30, 90, 90);
                    this.value = 10000f;
                }
                else
                {
                    if (Name == "Baby Slime")
                    {
                        this.SetDefaults(1);
                        this.name = Name;
                        this.scale = 0.9f;
                        this.damage = 13;
                        this.defense = 4;
                        this.life = 30;
                        this.knockBackResist = 0.95f;
                        this.alpha = 120;
                        this.color = new Color(0, 0, 0, 50);
                        this.value = 10f;
                    }
                    else
                    {
                        if (Name == "Black Slime")
                        {
                            this.SetDefaults(1);
                            this.name = Name;
                            this.damage = 15;
                            this.defense = 4;
                            this.life = 45;
                            this.color = new Color(0, 0, 0, 50);
                            this.value = 20f;
                        }
                        else
                        {
                            if (Name == "Purple Slime")
                            {
                                this.SetDefaults(1);
                                this.name = Name;
                                this.scale = 1.2f;
                                this.damage = 12;
                                this.defense = 6;
                                this.life = 40;
                                this.knockBackResist = 0.9f;
                                this.color = new Color(200, 0, 255, 150);
                                this.value = 10f;
                            }
                            else
                            {
                                if (Name == "Red Slime")
                                {
                                    this.SetDefaults(1);
                                    this.name = Name;
                                    this.damage = 12;
                                    this.defense = 4;
                                    this.life = 35;
                                    this.color = new Color(255, 30, 0, 100);
                                    this.value = 8f;
                                }
                                else
                                {
                                    if (Name == "Yellow Slime")
                                    {
                                        this.SetDefaults(1);
                                        this.name = Name;
                                        this.scale = 1.2f;
                                        this.damage = 15;
                                        this.defense = 7;
                                        this.life = 45;
                                        this.color = new Color(255, 255, 0, 100);
                                        this.value = 10f;
                                    }
                                    else
                                    {
                                        if (Name != "")
                                        {
                                            for (int i = 1; i < 46; i++)
                                            {
                                                this.SetDefaults(i);
                                                if (this.name == Name)
                                                {
                                                    break;
                                                }
                                                if (i == 45)
                                                {
                                                    this.SetDefaults(0);
                                                    this.active = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.active = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.lifeMax = this.life;
        }
        public static void SpawnNPC()
        {
            if (!Main.stopSpawns)
            {
                bool flag = false;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active)
                    {
                        num3++;
                    }
                }
                for (int i = 0; i < 255; i++)
                {
                    bool flag2 = false;
                    if (Main.player[i].active && Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0 && (double)Main.player[i].position.Y < Main.worldSurface * 16.0 + (double)Main.screenHeight)
                    {
                        int num4 = 3000;
                        if ((double)Main.player[i].position.X > Main.invasionX * 16.0 - (double)num4 && (double)Main.player[i].position.X < Main.invasionX * 16.0 + (double)num4)
                        {
                            flag2 = true;
                        }
                    }
                    flag = false;
                    NPC.spawnRate = NPC.defaultSpawnRate;
                    NPC.maxSpawns = NPC.defaultMaxSpawns;
                    if (Main.player[i].position.Y > (float)((Main.maxTilesY - 200) * 16))
                    {
                        NPC.spawnRate = (int)((float)NPC.spawnRate * 1.5f);
                        NPC.maxSpawns = (int)((float)NPC.maxSpawns * 0.5f);
                    }
                    else
                    {
                        if ((double)Main.player[i].position.Y > Main.rockLayer * 16.0 + (double)Main.screenHeight)
                        {
                            NPC.spawnRate = (int)((double)NPC.spawnRate * 0.7);
                            NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.35f);
                        }
                        else
                        {
                            if ((double)Main.player[i].position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
                            {
                                NPC.spawnRate = (int)((double)NPC.spawnRate * 0.8);
                                NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.1f);
                            }
                            else
                            {
                                if (!Main.dayTime)
                                {
                                    NPC.spawnRate = (int)((double)NPC.spawnRate * 0.6);
                                    NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.3f);
                                    if (Main.bloodMoon)
                                    {
                                        NPC.spawnRate = (int)((double)NPC.spawnRate * 0.3);
                                        NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.8f);
                                    }
                                }
                            }
                        }
                    }
                    if (Main.player[i].zoneDungeon)
                    {
                        NPC.spawnRate = (int)((double)NPC.defaultSpawnRate * 0.1);
                        NPC.maxSpawns = (int)((double)NPC.defaultMaxSpawns * 2.1);
                    }
                    else
                    {
                        if (Main.player[i].zoneEvil)
                        {
                            NPC.spawnRate = (int)((double)NPC.spawnRate * 0.5);
                            NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.4f);
                        }
                        else
                        {
                            if (Main.player[i].zoneMeteor)
                            {
                                NPC.spawnRate = (int)((double)NPC.spawnRate * 0.5);
                            }
                            else
                            {
                                if (Main.player[i].zoneJungle)
                                {
                                    NPC.spawnRate = (int)((double)NPC.spawnRate * 0.3);
                                    NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.6f);
                                }
                            }
                        }
                    }
                    if ((double)NPC.spawnRate < (double)NPC.defaultSpawnRate * 0.1)
                    {
                        NPC.spawnRate = (int)((double)NPC.defaultSpawnRate * 0.1);
                    }
                    if ((double)NPC.maxSpawns > (double)NPC.defaultMaxSpawns * 2.5)
                    {
                        NPC.maxSpawns = (int)((double)NPC.defaultMaxSpawns * 2.5);
                    }
                    if (Main.player[i].inventory[Main.player[i].selectedItem].type == 49)
                    {
                        NPC.spawnRate = (int)((double)NPC.spawnRate * 0.75);
                        NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.5f);
                    }
                    if (flag2)
                    {
                        NPC.maxSpawns = (int)((double)NPC.defaultMaxSpawns * (1.0 + 0.4 * (double)num3));
                        NPC.spawnRate = 30;
                    }
                    if (!flag2 && (!Main.bloodMoon || Main.dayTime) && !Main.player[i].zoneDungeon && !Main.player[i].zoneEvil && !Main.player[i].zoneMeteor)
                    {
                        if (Main.player[i].townNPCs == 1)
                        {
                            NPC.maxSpawns = (int)((double)NPC.maxSpawns * 0.6);
                            NPC.spawnRate = (int)((float)NPC.spawnRate * 2f);
                        }
                        else
                        {
                            if (Main.player[i].townNPCs == 2)
                            {
                                NPC.maxSpawns = (int)((double)NPC.maxSpawns * 0.3);
                                NPC.spawnRate = (int)((float)NPC.spawnRate * 3f);
                            }
                            else
                            {
                                if (Main.player[i].townNPCs >= 3)
                                {
                                    NPC.maxSpawns = 0;
                                    NPC.spawnRate = 99999;
                                }
                            }
                        }
                    }
                    if (Main.player[i].active && !Main.player[i].dead && Main.player[i].activeNPCs < NPC.maxSpawns && Main.rand.Next(NPC.spawnRate) == 0)
                    {
                        int num5 = (int)(Main.player[i].position.X / 16f) - NPC.spawnRangeX;
                        int num6 = (int)(Main.player[i].position.X / 16f) + NPC.spawnRangeX;
                        int num7 = (int)(Main.player[i].position.Y / 16f) - NPC.spawnRangeY;
                        int num8 = (int)(Main.player[i].position.Y / 16f) + NPC.spawnRangeY;
                        int num9 = (int)(Main.player[i].position.X / 16f) - NPC.safeRangeX;
                        int num10 = (int)(Main.player[i].position.X / 16f) + NPC.safeRangeX;
                        int num11 = (int)(Main.player[i].position.Y / 16f) - NPC.safeRangeY;
                        int num12 = (int)(Main.player[i].position.Y / 16f) + NPC.safeRangeY;
                        if (num5 < 0)
                        {
                            num5 = 0;
                        }
                        if (num6 > Main.maxTilesX)
                        {
                            num6 = Main.maxTilesX;
                        }
                        if (num7 < 0)
                        {
                            num7 = 0;
                        }
                        if (num8 > Main.maxTilesY)
                        {
                            num8 = Main.maxTilesY;
                        }
                        int j = 0;
                        while (j < 50)
                        {
                            int k = Main.rand.Next(num5, num6);
                            int num13 = Main.rand.Next(num7, num8);
                            if (Main.tile[k, num13].active && Main.tileSolid[(int)Main.tile[k, num13].type])
                            {
                                goto IL_9C4;
                            }
                            if (!Main.wallHouse[(int)Main.tile[k, num13].wall])
                            {
                                for (int l = num13; l < Main.maxTilesY; l++)
                                {
                                    if (Main.tile[k, l].active && Main.tileSolid[(int)Main.tile[k, l].type])
                                    {
                                        if (k < num9 || k > num10 || l < num11 || l > num12)
                                        {
                                            int num14 = (int)Main.tile[k, l].type;
                                            num = k;
                                            num2 = l;
                                            flag = true;
                                        }
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    int num15 = num - NPC.spawnSpaceX / 2;
                                    int num16 = num + NPC.spawnSpaceX / 2;
                                    int num17 = num2 - NPC.spawnSpaceY;
                                    int num18 = num2;
                                    if (num15 < 0)
                                    {
                                        flag = false;
                                    }
                                    if (num16 > Main.maxTilesX)
                                    {
                                        flag = false;
                                    }
                                    if (num17 < 0)
                                    {
                                        flag = false;
                                    }
                                    if (num18 > Main.maxTilesY)
                                    {
                                        flag = false;
                                    }
                                    if (flag)
                                    {
                                        for (int m = num15; m < num16; m++)
                                        {
                                            for (int l = num17; l < num18; l++)
                                            {
                                                if (Main.tile[m, l].active && Main.tileSolid[(int)Main.tile[m, l].type])
                                                {
                                                    flag = false;
                                                    break;
                                                }
                                                if (Main.tile[m, l].lava && l < Main.maxTilesY - 200)
                                                {
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                goto IL_9C4;
                            }
                        IL_9D7:
                            j++;
                            continue;
                        IL_9C4:
                            if (flag || flag)
                            {
                                break;
                            }
                            goto IL_9D7;
                        }
                    }
                    if (flag)
                    {
                        Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
                        for (int k = 0; k < 255; k++)
                        {
                            if (Main.player[k].active)
                            {
                                Rectangle rectangle2 = new Rectangle((int)Main.player[k].position.X + Main.player[k].width / 2 - Main.screenWidth / 2 - NPC.safeRangeX, (int)Main.player[k].position.Y + Main.player[k].height / 2 - Main.screenHeight / 2 - NPC.safeRangeY, Main.screenWidth + NPC.safeRangeX * 2, Main.screenHeight + NPC.safeRangeY * 2);
                                if (rectangle.Intersects(rectangle2))
                                {
                                    flag = false;
                                }
                            }
                        }
                    }
                    if (flag && Main.player[i].zoneDungeon && (!Main.tileDungeon[(int)Main.tile[num, num2].type] || Main.tile[num, num2 - 1].wall == 0))
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        flag = false;
                        int num19 = (int)Main.tile[num, num2].type;
                        int num20 = 1000;
                        if (flag2)
                        {
                            if (Main.rand.Next(9) == 0)
                            {
                                NPC.NewNPC(num * 16 + 8, num2 * 16, 29, 0);
                            }
                            else
                            {
                                if (Main.rand.Next(5) == 0)
                                {
                                    NPC.NewNPC(num * 16 + 8, num2 * 16, 26, 0);
                                }
                                else
                                {
                                    if (Main.rand.Next(3) == 0)
                                    {
                                        NPC.NewNPC(num * 16 + 8, num2 * 16, 27, 0);
                                    }
                                    else
                                    {
                                        NPC.NewNPC(num * 16 + 8, num2 * 16, 28, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Main.player[i].zoneDungeon)
                            {
                                if (!NPC.downedBoss3)
                                {
                                    num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 35, 0);
                                    Main.npc[num20].ai[0] = 1f;
                                    Main.npc[num20].ai[2] = 2f;
                                }
                                else
                                {
                                    if (Main.rand.Next(4) == 0)
                                    {
                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 34, 0);
                                    }
                                    else
                                    {
                                        if (Main.rand.Next(5) == 0)
                                        {
                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 32, 0);
                                        }
                                        else
                                        {
                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 31, 0);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Main.player[i].zoneMeteor)
                                {
                                    num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 23, 0);
                                }
                                else
                                {
                                    if (Main.player[i].zoneEvil && Main.rand.Next(50) == 0)
                                    {
                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 7, 1);
                                    }
                                    else
                                    {
                                        if (num19 == 60)
                                        {
                                            if (Main.rand.Next(3) == 0)
                                            {
                                                num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 43, 0);
                                                Main.npc[num20].ai[0] = (float)num;
                                                Main.npc[num20].ai[1] = (float)num2;
                                                Main.npc[num20].netUpdate = true;
                                            }
                                            else
                                            {
                                                num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 42, 0);
                                            }
                                        }
                                        else
                                        {
                                            if ((double)num2 <= Main.worldSurface)
                                            {
                                                if (num19 == 23 || num19 == 25)
                                                {
                                                    num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 6, 0);
                                                }
                                                else
                                                {
                                                    if (Main.dayTime)
                                                    {
                                                        int num21 = System.Math.Abs(num - Main.spawnTileX);
                                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
                                                        if (Main.rand.Next(3) == 0 || num21 < 200)
                                                        {
                                                            Main.npc[num20].SetDefaults("Green Slime");
                                                        }
                                                        else
                                                        {
                                                            if (Main.rand.Next(10) == 0 && num21 > 400)
                                                            {
                                                                Main.npc[num20].SetDefaults("Purple Slime");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Main.rand.Next(6) == 0 || (Main.moonPhase == 4 && Main.rand.Next(2) == 0))
                                                        {
                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 2, 0);
                                                        }
                                                        else
                                                        {
                                                            NPC.NewNPC(num * 16 + 8, num2 * 16, 3, 0);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if ((double)num2 <= Main.rockLayer)
                                                {
                                                    if (Main.rand.Next(30) == 0)
                                                    {
                                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 10, 1);
                                                    }
                                                    else
                                                    {
                                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
                                                        if (Main.rand.Next(5) == 0)
                                                        {
                                                            Main.npc[num20].SetDefaults("Yellow Slime");
                                                        }
                                                        else
                                                        {
                                                            if (Main.rand.Next(2) == 0)
                                                            {
                                                                Main.npc[num20].SetDefaults("Blue Slime");
                                                            }
                                                            else
                                                            {
                                                                Main.npc[num20].SetDefaults("Red Slime");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (num2 > Main.maxTilesY - 190)
                                                    {
                                                        if (Main.rand.Next(5) == 0)
                                                        {
                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 39, 1);
                                                        }
                                                        else
                                                        {
                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 24, 0);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Main.rand.Next(35) == 0)
                                                        {
                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 10, 1);
                                                        }
                                                        else
                                                        {
                                                            if (Main.rand.Next(5) == 0)
                                                            {
                                                                num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 16, 0);
                                                            }
                                                            else
                                                            {
                                                                if (Main.rand.Next(2) == 0)
                                                                {
                                                                    if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(100) == 0)
                                                                    {
                                                                        num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 45, 0);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Main.rand.Next(8) == 0)
                                                                        {
                                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 44, 0);
                                                                        }
                                                                        else
                                                                        {
                                                                            num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 21, 0);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    num20 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
                                                                    Main.npc[num20].SetDefaults("Black Slime");
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
                        if (Main.npc[num20].type == 1 && Main.rand.Next(250) == 0)
                        {
                            Main.npc[num20].SetDefaults("Pinky");
                        }
                        if (Main.netMode == 2 && num20 < 1000)
                        {
                            NetMessage.SendData(23, -1, -1, "", num20, 0f, 0f, 0f);
                        }
                        break;
                    }
                }
            }
        }
        public static void SpawnOnPlayer(int plr, int Type)
        {
            bool flag = false;
            int num = 0;
            int num2 = 0;
            int num3 = (int)(Main.player[plr].position.X / 16f) - NPC.spawnRangeX * 3;
            int num4 = (int)(Main.player[plr].position.X / 16f) + NPC.spawnRangeX * 3;
            int num5 = (int)(Main.player[plr].position.Y / 16f) - NPC.spawnRangeY * 3;
            int num6 = (int)(Main.player[plr].position.Y / 16f) + NPC.spawnRangeY * 3;
            int num7 = (int)(Main.player[plr].position.X / 16f) - NPC.safeRangeX;
            int num8 = (int)(Main.player[plr].position.X / 16f) + NPC.safeRangeX;
            int num9 = (int)(Main.player[plr].position.Y / 16f) - NPC.safeRangeY;
            int num10 = (int)(Main.player[plr].position.Y / 16f) + NPC.safeRangeY;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesX)
            {
                num4 = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesY)
            {
                num6 = Main.maxTilesY;
            }
            for (int i = 0; i < 1000; i++)
            {
                int j = 0;
                while (j < 100)
                {
                    int k = Main.rand.Next(num3, num4);
                    int num11 = Main.rand.Next(num5, num6);
                    if (Main.tile[k, num11].active && Main.tileSolid[(int)Main.tile[k, num11].type])
                    {
                        goto IL_3A2;
                    }
                    if (Main.tile[k, num11].wall != 1)
                    {
                        for (int l = num11; l < Main.maxTilesY; l++)
                        {
                            if (Main.tile[k, l].active && Main.tileSolid[(int)Main.tile[k, l].type])
                            {
                                if (k < num7 || k > num8 || l < num9 || l > num10)
                                {
                                    int num12 = (int)Main.tile[k, l].type;
                                    num = k;
                                    num2 = l;
                                    flag = true;
                                }
                                break;
                            }
                        }
                        if (flag)
                        {
                            int num13 = num - NPC.spawnSpaceX / 2;
                            int num14 = num + NPC.spawnSpaceX / 2;
                            int num15 = num2 - NPC.spawnSpaceY;
                            int num16 = num2;
                            if (num13 < 0)
                            {
                                flag = false;
                            }
                            if (num14 > Main.maxTilesX)
                            {
                                flag = false;
                            }
                            if (num15 < 0)
                            {
                                flag = false;
                            }
                            if (num16 > Main.maxTilesY)
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                for (int m = num13; m < num14; m++)
                                {
                                    for (int l = num15; l < num16; l++)
                                    {
                                        if (Main.tile[m, l].active && Main.tileSolid[(int)Main.tile[m, l].type])
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        goto IL_3A2;
                    }
                IL_3B6:
                    j++;
                    continue;
                IL_3A2:
                    if (flag || flag)
                    {
                        break;
                    }
                    goto IL_3B6;
                }
                if (flag)
                {
                    Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
                    for (int k = 0; k < 255; k++)
                    {
                        if (Main.player[k].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.player[k].position.X + Main.player[k].width / 2 - Main.screenWidth / 2 - NPC.safeRangeX, (int)Main.player[k].position.Y + Main.player[k].height / 2 - Main.screenHeight / 2 - NPC.safeRangeY, Main.screenWidth + NPC.safeRangeX * 2, Main.screenHeight + NPC.safeRangeY * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag = false;
                            }
                        }
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            if (flag)
            {
                int num17 = NPC.NewNPC(num * 16 + 8, num2 * 16, Type, 1);
                Main.npc[num17].target = plr;
                string str = Main.npc[num17].name;
                if (Main.npc[num17].type == 13)
                {
                    str = "Eater of Worlds";
                }
                if (Main.npc[num17].type == 35)
                {
                    str = "Skeletron";
                }
                if (Main.netMode == 2 && num17 < 1000)
                {
                    NetMessage.SendData(23, -1, -1, "", num17, 0f, 0f, 0f);
                }
                if (Main.netMode != 0)
                {
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(25, -1, -1, str + " has awoken!", 255, 175f, 75f, 255f);
                    }
                }
            }
        }
        public double StrikeNPC(int Damage, float knockBack, int hitDirection)
        {
            double result;
            if (!this.active || this.life <= 0)
            {
                result = 0.0;
            }
            else
            {
                double num = Main.CalculateDamage(Damage, this.defense);
                if (num < 1.0)
                {
                    result = 0.0;
                }
                else
                {
                    if (this.townNPC)
                    {
                        this.ai[0] = 1f;
                        this.ai[1] = (float)(300 + Main.rand.Next(300));
                        this.ai[2] = 0f;
                        this.direction = hitDirection;
                        this.netUpdate = true;
                    }
                    if (this.aiStyle == 8 && Main.netMode != 1)
                    {
                        this.ai[0] = 400f;
                        this.TargetClosest();
                    }
                    this.life -= (int)num;
                    if (knockBack > 0f && this.knockBackResist > 0f)
                    {
                        if (!this.noGravity)
                        {
                            this.velocity.Y = -knockBack * 0.75f * this.knockBackResist;
                        }
                        else
                        {
                            this.velocity.Y = -knockBack * 0.5f * this.knockBackResist;
                        }
                        this.velocity.X = knockBack * (float)hitDirection * this.knockBackResist;
                    }
                    this.HitEffect(hitDirection, num);
                    if (this.soundHit <= 0)
                    {
                        goto IL_181;
                    }
                IL_181:
                    if (this.life <= 0)
                    {
                        if (this.townNPC && this.type != 37)
                        {
                            if (Main.netMode != 0)
                            {
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(25, -1, -1, this.name + " was slain...", 255, 255f, 25f, 25f);
                                }
                            }
                        }
                        if (this.townNPC && Main.netMode != 1 && this.homeless && WorldGen.spawnNPC == this.type)
                        {
                            WorldGen.spawnNPC = 0;
                        }
                        if (this.soundKilled <= 0)
                        {
                            goto IL_248;
                        }
                    IL_248:
                        this.NPCLoot();
                        this.active = false;
                        if (this.type == 26 || this.type == 27 || this.type == 28 || this.type == 29)
                        {
                            Main.invasionSize--;
                        }
                    }
                    result = num;
                }
            }
            return result;
        }
        public void TargetClosest()
        {
            float num = -1f;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead && (num == -1f || System.Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - this.position.X + (float)(this.width / 2)) + System.Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - this.position.Y + (float)(this.height / 2)) < num))
                {
                    num = System.Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - this.position.X + (float)(this.width / 2)) + System.Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - this.position.Y + (float)(this.height / 2));
                    this.target = i;
                }
            }
            if (this.target < 0 || this.target >= 255)
            {
                this.target = 0;
            }
            this.targetRect = new Rectangle((int)Main.player[this.target].position.X, (int)Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
            this.direction = 1;
            if ((float)(this.targetRect.X + this.targetRect.Width / 2) < this.position.X + (float)(this.width / 2))
            {
                this.direction = -1;
            }
            this.directionY = 1;
            if ((float)(this.targetRect.Y + this.targetRect.Height / 2) < this.position.Y + (float)(this.height / 2))
            {
                this.directionY = -1;
            }
            if (this.direction != this.oldDirection || this.directionY != this.oldDirectionY || this.target != this.oldTarget)
            {
                this.netUpdate = true;
            }
        }
        public void UpdateNPC(int i)
        {
            this.whoAmI = i;
            if (this.active)
            {
                float num = 10f;
                float num2 = 0.3f;
                if (this.wet)
                {
                    num2 = 0.2f;
                    num = 7f;
                }
                if (this.soundDelay > 0)
                {
                    this.soundDelay--;
                }
                if (this.life <= 0)
                {
                    this.active = false;
                }
                this.oldTarget = this.target;
                this.oldDirection = this.direction;
                this.oldDirectionY = this.directionY;
                this.AI();
                if (this.type == 44)
                {
                    Lighting.addLight(((int)this.position.X + this.width / 2) / 16, (int)(this.position.Y + 4f) / 16, 0.6f);
                }
                for (int j = 0; j < 256; j++)
                {
                    if (this.immune[j] > 0)
                    {
                        this.immune[j]--;
                    }
                }
                if (!this.noGravity)
                {
                    this.velocity.Y = this.velocity.Y + num2;
                    if (this.velocity.Y > num)
                    {
                        this.velocity.Y = num;
                    }
                }
                if ((double)this.velocity.X < 0.005 && (double)this.velocity.X > -0.005)
                {
                    this.velocity.X = 0f;
                }
                if (Main.netMode != 1 && this.friendly && this.type != 37)
                {
                    if (this.life < this.lifeMax)
                    {
                        this.friendlyRegen++;
                        if (this.friendlyRegen > 300)
                        {
                            this.friendlyRegen = 0;
                            this.life++;
                            this.netUpdate = true;
                        }
                    }
                    if (this.immune[255] == 0)
                    {
                        Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.npc[j].active && !Main.npc[j].friendly)
                            {
                                Rectangle rectangle2 = new Rectangle((int)Main.npc[j].position.X, (int)Main.npc[j].position.Y, Main.npc[j].width, Main.npc[j].height);
                                if (rectangle.Intersects(rectangle2))
                                {
                                    int num3 = Main.npc[j].damage;
                                    int num4 = 6;
                                    int num5 = 1;
                                    if (Main.npc[j].position.X + (float)(Main.npc[j].width / 2) > this.position.X + (float)(this.width / 2))
                                    {
                                        num5 = -1;
                                    }
                                    Main.npc[i].StrikeNPC(num3, (float)num4, num5);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(28, -1, -1, "", i, (float)num3, (float)num4, (float)num5);
                                    }
                                    this.netUpdate = true;
                                    this.immune[255] = 30;
                                }
                            }
                        }
                    }
                }
                if (!this.noTileCollide)
                {
                    bool flag = Collision.LavaCollision(this.position, this.width, this.height);
                    if (flag)
                    {
                        this.lavaWet = true;
                        if (Main.netMode != 1 && this.immune[255] == 0)
                        {
                            this.immune[255] = 30;
                            this.StrikeNPC(50, 0f, 0);
                            if (Main.netMode == 2 && Main.netMode != 0)
                            {
                                NetMessage.SendData(28, -1, -1, "", this.whoAmI, 50f, 0f, 0f);
                            }
                        }
                    }
                    if (Collision.WetCollision(this.position, this.width, this.height))
                    {
                        if (!this.wet && this.wetCount == 0)
                        {
                            this.wetCount = 10;
                            if (!flag)
                            {
                                for (int k = 0; k < 50; k++)
                                {
                                    Color color = default(Color);
                                    int num6 = 0;
                                    Dust expr_520_cp_0 = Main.dust[num6];
                                    expr_520_cp_0.velocity.Y = expr_520_cp_0.velocity.Y - 4f;
                                    Dust expr_53E_cp_0 = Main.dust[num6];
                                    expr_53E_cp_0.velocity.X = expr_53E_cp_0.velocity.X * 2.5f;
                                    Main.dust[num6].scale = 1.3f;
                                    Main.dust[num6].alpha = 100;
                                    Main.dust[num6].noGravity = true;
                                }
                            }
                            else
                            {
                                for (int k = 0; k < 20; k++)
                                {
                                    Color color = default(Color);
                                    int num6 = 0;
                                    Dust expr_5B9_cp_0 = Main.dust[num6];
                                    expr_5B9_cp_0.velocity.Y = expr_5B9_cp_0.velocity.Y - 1.5f;
                                    Dust expr_5D7_cp_0 = Main.dust[num6];
                                    expr_5D7_cp_0.velocity.X = expr_5D7_cp_0.velocity.X * 2.5f;
                                    Main.dust[num6].scale = 1.3f;
                                    Main.dust[num6].alpha = 100;
                                    Main.dust[num6].noGravity = true;
                                }
                            }
                        }
                        this.wet = true;
                    }
                    else
                    {
                        if (this.wet)
                        {
                            this.velocity.X = this.velocity.X * 0.5f;
                            this.wet = false;
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!this.lavaWet)
                                {
                                    for (int k = 0; k < 50; k++)
                                    {
                                        Color color = default(Color);
                                        int num6 = 0;
                                        Dust expr_6B9_cp_0 = Main.dust[num6];
                                        expr_6B9_cp_0.velocity.Y = expr_6B9_cp_0.velocity.Y - 4f;
                                        Dust expr_6D7_cp_0 = Main.dust[num6];
                                        expr_6D7_cp_0.velocity.X = expr_6D7_cp_0.velocity.X * 2.5f;
                                        Main.dust[num6].scale = 1.3f;
                                        Main.dust[num6].alpha = 100;
                                        Main.dust[num6].noGravity = true;
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < 20; k++)
                                    {
                                        Color color = default(Color);
                                        int num6 = 0;
                                        Dust expr_752_cp_0 = Main.dust[num6];
                                        expr_752_cp_0.velocity.Y = expr_752_cp_0.velocity.Y - 1.5f;
                                        Dust expr_770_cp_0 = Main.dust[num6];
                                        expr_770_cp_0.velocity.X = expr_770_cp_0.velocity.X * 2.5f;
                                        Main.dust[num6].scale = 1.3f;
                                        Main.dust[num6].alpha = 100;
                                        Main.dust[num6].noGravity = true;
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
                    bool flag2 = false;
                    if (this.aiStyle == 10)
                    {
                        flag2 = true;
                    }
                    if (this.aiStyle == 3 && this.directionY == 1)
                    {
                        flag2 = true;
                    }
                    this.oldVelocity = this.velocity;
                    this.collideX = false;
                    this.collideY = false;
                    if (this.wet)
                    {
                        Vector2 vector = this.velocity;
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag2, flag2);
                        Vector2 b = this.velocity * 0.5f;
                        if (this.velocity.X != vector.X)
                        {
                            b.X = this.velocity.X;
                            this.collideX = true;
                        }
                        if (this.velocity.Y != vector.Y)
                        {
                            b.Y = this.velocity.Y;
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += b;
                    }
                    else
                    {
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag2, flag2);
                        if (this.oldVelocity.X != this.velocity.X)
                        {
                            this.collideX = true;
                        }
                        if (this.oldVelocity.Y != this.velocity.Y)
                        {
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += this.velocity;
                    }
                }
                else
                {
                    this.oldPosition = this.position;
                    this.position += this.velocity;
                }
                if (!this.active)
                {
                    this.netUpdate = true;
                }
                if (Main.netMode == 2 && this.netUpdate)
                {
                    NetMessage.SendData(23, -1, -1, "", i, 0f, 0f, 0f);
                }
                this.FindFrame();
                this.CheckActive();
                this.netUpdate = false;
            }
        }
    }
}

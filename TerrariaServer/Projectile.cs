using System;
namespace Terraria
{
    public class Projectile
    {
        public bool active = false;
        public float[] ai = new float[Projectile.maxAI];
        public int aiStyle;
        public int alpha;
        public int damage = 0;
        public int direction;
        public bool friendly = false;
        public int height;
        public bool hostile;
        public int identity = 0;
        public bool ignoreWater;
        public float knockBack = 0f;
        public bool lavaWet;
        public float light = 0f;
        public static int maxAI = 2;
        public int maxUpdates = 0;
        public string name = "";
        public bool netUpdate = false;
        public int numUpdates = 0;
        public int owner = 255;
        public int penetrate = 1;
        public int[] playerImmune = new int[255];
        public Vector2 position;
        public int restrikeDelay = 0;
        public float rotation = 0f;
        public float scale = 1f;
        public int soundDelay = 0;
        public bool tileCollide;
        public int timeLeft = 0;
        public int type = 0;
        public Vector2 velocity;
        public bool wet;
        public byte wetCount = 0;
        public int whoAmI;
        public int width;
        public void AI()
        {
            if (this.aiStyle == 1)
            {
                if (this.type == 20 || this.type == 14 || this.type == 36)
                {
                    if (this.alpha > 0)
                    {
                        this.alpha -= 15;
                    }
                    if (this.alpha < 0)
                    {
                        this.alpha = 0;
                    }
                }
                if (this.type != 5 && this.type != 14 && this.type != 20 && this.type != 36)
                {
                    this.ai[0] += 1f;
                }
                if (this.ai[0] >= 15f)
                {
                    this.ai[0] = 15f;
                    this.velocity.Y = this.velocity.Y + 0.1f;
                }
                this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
                if (this.velocity.Y > 16f)
                {
                    this.velocity.Y = 16f;
                }
            }
            else
            {
                if (this.aiStyle == 2)
                {
                    this.ai[0] += 1f;
                    if (this.ai[0] >= 20f)
                    {
                        this.velocity.Y = this.velocity.Y + 0.4f;
                        this.velocity.X = this.velocity.X * 0.97f;
                    }
                    this.rotation += (System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y)) * 0.03f * (float)this.direction;
                    if (this.velocity.Y > 16f)
                    {
                        this.velocity.Y = 16f;
                    }
                }
                else
                {
                    if (this.aiStyle == 3)
                    {
                        if (this.soundDelay == 0)
                        {
                            this.soundDelay = 8;
                        }
                        if (this.type == 19)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Color color = default(Color);
                                int num = 0;
                                Main.dust[num].noGravity = true;
                                Dust expr_2BD_cp_0 = Main.dust[num];
                                expr_2BD_cp_0.velocity.X = expr_2BD_cp_0.velocity.X * 0.3f;
                                Dust expr_2DA_cp_0 = Main.dust[num];
                                expr_2DA_cp_0.velocity.Y = expr_2DA_cp_0.velocity.Y * 0.3f;
                            }
                        }
                        else
                        {
                            if (this.type == 33)
                            {
                                if (Main.rand.Next(1) == 0)
                                {
                                    int num = 0;
                                    Main.dust[num].noGravity = true;
                                }
                            }
                            else
                            {
                                if (Main.rand.Next(5) == 0)
                                {
                                }
                            }
                        }
                        if (this.ai[0] == 0f)
                        {
                            this.ai[1] += 1f;
                            if (this.ai[1] >= 30f)
                            {
                                this.ai[0] = 1f;
                                this.ai[1] = 0f;
                                this.netUpdate = true;
                            }
                        }
                        else
                        {
                            this.tileCollide = false;
                            float num2 = 9f;
                            float num3 = 0.4f;
                            if (this.type == 19)
                            {
                                num2 = 13f;
                                num3 = 0.6f;
                            }
                            else
                            {
                                if (this.type == 33)
                                {
                                    num2 = 15f;
                                    num3 = 0.8f;
                                }
                            }
                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                            float num4 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
                            float num5 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
                            float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                            num6 = num2 / num6;
                            num4 *= num6;
                            num5 *= num6;
                            if (this.velocity.X < num4)
                            {
                                this.velocity.X = this.velocity.X + num3;
                                if (this.velocity.X < 0f && num4 > 0f)
                                {
                                    this.velocity.X = this.velocity.X + num3;
                                }
                            }
                            else
                            {
                                if (this.velocity.X > num4)
                                {
                                    this.velocity.X = this.velocity.X - num3;
                                    if (this.velocity.X > 0f && num4 < 0f)
                                    {
                                        this.velocity.X = this.velocity.X - num3;
                                    }
                                }
                            }
                            if (this.velocity.Y < num5)
                            {
                                this.velocity.Y = this.velocity.Y + num3;
                                if (this.velocity.Y < 0f && num5 > 0f)
                                {
                                    this.velocity.Y = this.velocity.Y + num3;
                                }
                            }
                            else
                            {
                                if (this.velocity.Y > num5)
                                {
                                    this.velocity.Y = this.velocity.Y - num3;
                                    if (this.velocity.Y > 0f && num5 < 0f)
                                    {
                                        this.velocity.Y = this.velocity.Y - num3;
                                    }
                                }
                            }
                            if (Main.myPlayer == this.owner)
                            {
                                Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
                                Rectangle value = new Rectangle((int)Main.player[this.owner].position.X, (int)Main.player[this.owner].position.Y, Main.player[this.owner].width, Main.player[this.owner].height);
                                if (rectangle.Intersects(value))
                                {
                                    this.Kill();
                                }
                            }
                        }
                        this.rotation += 0.4f * (float)this.direction;
                    }
                    else
                    {
                        if (this.aiStyle == 4)
                        {
                            this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
                            if (this.ai[0] == 0f)
                            {
                                this.alpha -= 50;
                                if (this.alpha <= 0)
                                {
                                    this.alpha = 0;
                                    this.ai[0] = 1f;
                                    if (this.ai[1] == 0f)
                                    {
                                        this.ai[1] += 1f;
                                        this.position += this.velocity * 1f;
                                    }
                                    if (this.type == 7 && Main.myPlayer == this.owner)
                                    {
                                        int num7 = this.type;
                                        if (this.ai[1] >= 6f)
                                        {
                                            num7++;
                                        }
                                        int num8 = Projectile.NewProjectile(this.position.X + this.velocity.X + (float)(this.width / 2), this.position.Y + this.velocity.Y + (float)(this.height / 2), this.velocity.X, this.velocity.Y, num7, this.damage, this.knockBack, this.owner);
                                        Main.projectile[num8].damage = this.damage;
                                        Main.projectile[num8].ai[1] = this.ai[1] + 1f;
                                        NetMessage.SendData(27, -1, -1, "", num8, 0f, 0f, 0f);
                                    }
                                }
                            }
                            else
                            {
                                if (this.alpha < 170 && this.alpha + 5 >= 170)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        Color color = default(Color);
                                    }
                                }
                                this.alpha += 5;
                                if (this.alpha >= 255)
                                {
                                    this.Kill();
                                }
                            }
                        }
                        else
                        {
                            if (this.aiStyle == 5)
                            {
                                if (this.soundDelay == 0)
                                {
                                    this.soundDelay = 20 + Main.rand.Next(40);
                                }
                                if (this.ai[0] == 0f)
                                {
                                    this.ai[0] = 1f;
                                }
                                this.alpha += (int)(25f * this.ai[0]);
                                if (this.alpha > 200)
                                {
                                    this.alpha = 200;
                                    this.ai[0] = -1f;
                                }
                                if (this.alpha < 0)
                                {
                                    this.alpha = 0;
                                    this.ai[0] = 1f;
                                }
                                this.rotation += (System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y)) * 0.01f * (float)this.direction;
                                if (Main.rand.Next(10) != 0)
                                {
                                    goto IL_B1B;
                                }
                            IL_B1B:
                                if (Main.rand.Next(20) == 0)
                                {
                                    Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.2f, this.velocity.Y * 0.2f), Main.rand.Next(16, 18));
                                }
                            }
                            else
                            {
                                if (this.aiStyle == 6)
                                {
                                    this.velocity *= 0.95f;
                                    this.ai[0] += 1f;
                                    if (this.ai[0] == 180f)
                                    {
                                        this.Kill();
                                    }
                                    if (this.ai[1] == 0f)
                                    {
                                        this.ai[1] = 1f;
                                        for (int i = 0; i < 30; i++)
                                        {
                                            Color color = default(Color);
                                        }
                                    }
                                    if (this.type == 10)
                                    {
                                        int num9 = (int)(this.position.X / 16f) - 1;
                                        int num10 = (int)((this.position.X + (float)this.width) / 16f) + 2;
                                        int num11 = (int)(this.position.Y / 16f) - 1;
                                        int num12 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
                                        if (num9 < 0)
                                        {
                                            num9 = 0;
                                        }
                                        if (num10 > Main.maxTilesX)
                                        {
                                            num10 = Main.maxTilesX;
                                        }
                                        if (num11 < 0)
                                        {
                                            num11 = 0;
                                        }
                                        if (num12 > Main.maxTilesY)
                                        {
                                            num12 = Main.maxTilesY;
                                        }
                                        for (int i = num9; i < num10; i++)
                                        {
                                            for (int j = num11; j < num12; j++)
                                            {
                                                Vector2 vector2;
                                                vector2.X = (float)(i * 16);
                                                vector2.Y = (float)(j * 16);
                                                if (this.position.X + (float)this.width > vector2.X && this.position.X < vector2.X + 16f && this.position.Y + (float)this.height > vector2.Y && this.position.Y < vector2.Y + 16f && Main.myPlayer == this.owner && Main.tile[i, j].active)
                                                {
                                                    if (Main.tile[i, j].type == 23)
                                                    {
                                                        Main.tile[i, j].type = 2;
                                                        WorldGen.SquareTileFrame(i, j, true);
                                                        if (Main.netMode == 1)
                                                        {
                                                            NetMessage.SendTileSquare(-1, i - 1, j - 1, 3);
                                                        }
                                                    }
                                                    if (Main.tile[i, j].type == 25)
                                                    {
                                                        Main.tile[i, j].type = 1;
                                                        WorldGen.SquareTileFrame(i, j, true);
                                                        if (Main.netMode == 1)
                                                        {
                                                            NetMessage.SendTileSquare(-1, i - 1, j - 1, 3);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.aiStyle == 7)
                                    {
                                        if (Main.player[this.owner].dead)
                                        {
                                            this.Kill();
                                        }
                                        else
                                        {
                                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                            float num4 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
                                            float num5 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
                                            float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                            this.rotation = (float)System.Math.Atan2((double)num5, (double)num4) - 1.57f;
                                            if (this.ai[0] == 0f)
                                            {
                                                if ((num6 > 300f && this.type == 13) || (num6 > 400f && this.type == 32))
                                                {
                                                    this.ai[0] = 1f;
                                                }
                                                int num9 = (int)(this.position.X / 16f) - 1;
                                                int num10 = (int)((this.position.X + (float)this.width) / 16f) + 2;
                                                int num11 = (int)(this.position.Y / 16f) - 1;
                                                int num12 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
                                                if (num9 < 0)
                                                {
                                                    num9 = 0;
                                                }
                                                if (num10 > Main.maxTilesX)
                                                {
                                                    num10 = Main.maxTilesX;
                                                }
                                                if (num11 < 0)
                                                {
                                                    num11 = 0;
                                                }
                                                if (num12 > Main.maxTilesY)
                                                {
                                                    num12 = Main.maxTilesY;
                                                }
                                                for (int i = num9; i < num10; i++)
                                                {
                                                    for (int j = num11; j < num12; j++)
                                                    {
                                                        if (Main.tile[i, j] == null)
                                                        {
                                                            Main.tile[i, j] = new Tile();
                                                        }
                                                        Vector2 vector2;
                                                        vector2.X = (float)(i * 16);
                                                        vector2.Y = (float)(j * 16);
                                                        if (this.position.X + (float)this.width > vector2.X && this.position.X < vector2.X + 16f && this.position.Y + (float)this.height > vector2.Y && this.position.Y < vector2.Y + 16f && Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type])
                                                        {
                                                            if (Main.player[this.owner].grapCount < 10)
                                                            {
                                                                Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
                                                                Player player = Main.player[this.owner];
                                                                player.grapCount++;
                                                            }
                                                            if (Main.myPlayer == this.owner)
                                                            {
                                                                int num13 = 0;
                                                                int num14 = -1;
                                                                int num15 = 100000;
                                                                for (int k = 0; k < 1000; k++)
                                                                {
                                                                    if (Main.projectile[k].active && Main.projectile[k].owner == this.owner && Main.projectile[k].aiStyle == 7)
                                                                    {
                                                                        if (Main.projectile[k].timeLeft < num15)
                                                                        {
                                                                            num14 = k;
                                                                            num15 = Main.projectile[k].timeLeft;
                                                                        }
                                                                        num13++;
                                                                    }
                                                                }
                                                                if (num13 > 3)
                                                                {
                                                                    Main.projectile[num14].Kill();
                                                                }
                                                            }
                                                            WorldGen.KillTile(i, j, true, true, false);
                                                            this.velocity.X = 0f;
                                                            this.velocity.Y = 0f;
                                                            this.ai[0] = 2f;
                                                            this.position.X = (float)(i * 16 + 8 - this.width / 2);
                                                            this.position.Y = (float)(j * 16 + 8 - this.height / 2);
                                                            this.damage = 0;
                                                            this.netUpdate = true;
                                                            if (Main.myPlayer == this.owner)
                                                            {
                                                                NetMessage.SendData(13, -1, -1, "", this.owner, 0f, 0f, 0f);
                                                            }
                                                            break;
                                                        }
                                                    }
                                                    if (this.ai[0] == 2f)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.ai[0] == 1f)
                                                {
                                                    float num16 = 11f;
                                                    if (this.type == 32)
                                                    {
                                                        num16 = 15f;
                                                    }
                                                    if (num6 < 24f)
                                                    {
                                                        this.Kill();
                                                    }
                                                    num6 = num16 / num6;
                                                    num4 *= num6;
                                                    num5 *= num6;
                                                    this.velocity.X = num4;
                                                    this.velocity.Y = num5;
                                                }
                                                else
                                                {
                                                    if (this.ai[0] == 2f)
                                                    {
                                                        int num9 = (int)(this.position.X / 16f) - 1;
                                                        int num10 = (int)((this.position.X + (float)this.width) / 16f) + 2;
                                                        int num11 = (int)(this.position.Y / 16f) - 1;
                                                        int num12 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
                                                        if (num9 < 0)
                                                        {
                                                            num9 = 0;
                                                        }
                                                        if (num10 > Main.maxTilesX)
                                                        {
                                                            num10 = Main.maxTilesX;
                                                        }
                                                        if (num11 < 0)
                                                        {
                                                            num11 = 0;
                                                        }
                                                        if (num12 > Main.maxTilesY)
                                                        {
                                                            num12 = Main.maxTilesY;
                                                        }
                                                        bool flag = true;
                                                        for (int i = num9; i < num10; i++)
                                                        {
                                                            for (int j = num11; j < num12; j++)
                                                            {
                                                                if (Main.tile[i, j] == null)
                                                                {
                                                                    Main.tile[i, j] = new Tile();
                                                                }
                                                                Vector2 vector2;
                                                                vector2.X = (float)(i * 16);
                                                                vector2.Y = (float)(j * 16);
                                                                if (this.position.X + (float)(this.width / 2) > vector2.X && this.position.X + (float)(this.width / 2) < vector2.X + 16f && this.position.Y + (float)(this.height / 2) > vector2.Y && this.position.Y + (float)(this.height / 2) < vector2.Y + 16f && Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type])
                                                                {
                                                                    flag = false;
                                                                }
                                                            }
                                                        }
                                                        if (flag)
                                                        {
                                                            this.ai[0] = 1f;
                                                        }
                                                        else
                                                        {
                                                            if (Main.player[this.owner].grapCount < 10)
                                                            {
                                                                Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
                                                                Player player2 = Main.player[this.owner];
                                                                player2.grapCount++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.aiStyle == 8)
                                        {
                                            if (this.type == 27)
                                            {
                                                Color color = default(Color);
                                                int num = 0;
                                                Main.dust[num].noGravity = true;
                                            }
                                            else
                                            {
                                                for (int i = 0; i < 2; i++)
                                                {
                                                    Color color = default(Color);
                                                    int num = 0;
                                                    Main.dust[num].noGravity = true;
                                                    Dust expr_17F2_cp_0 = Main.dust[num];
                                                    expr_17F2_cp_0.velocity.X = expr_17F2_cp_0.velocity.X * 0.3f;
                                                    Dust expr_180F_cp_0 = Main.dust[num];
                                                    expr_180F_cp_0.velocity.Y = expr_180F_cp_0.velocity.Y * 0.3f;
                                                }
                                            }
                                            if (this.type != 27)
                                            {
                                                this.ai[1] += 1f;
                                            }
                                            if (this.ai[1] >= 20f)
                                            {
                                                this.velocity.Y = this.velocity.Y + 0.2f;
                                            }
                                            this.rotation += 0.3f * (float)this.direction;
                                            if (this.velocity.Y > 16f)
                                            {
                                                this.velocity.Y = 16f;
                                            }
                                        }
                                        else
                                        {
                                            if (this.aiStyle == 9)
                                            {
                                                if (this.type == 34)
                                                {
                                                    Color color = default(Color);
                                                    int num = 0;
                                                    Main.dust[num].noGravity = true;
                                                    Dust dust = Main.dust[num];
                                                    dust.velocity *= 1.4f;
                                                }
                                                else
                                                {
                                                    if (this.soundDelay == 0 && System.Math.Abs(this.velocity.X) + System.Math.Abs(this.velocity.Y) > 2f)
                                                    {
                                                        this.soundDelay = 10;
                                                    }
                                                    int num = 0;
                                                    Dust dust2 = Main.dust[num];
                                                    dust2.velocity *= 0.3f;
                                                    Main.dust[num].position.X = this.position.X + (float)(this.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
                                                    Main.dust[num].position.Y = this.position.Y + (float)(this.height / 2) + (float)Main.rand.Next(-4, 5);
                                                    Main.dust[num].noGravity = true;
                                                }
                                                if (Main.myPlayer == this.owner && this.ai[0] == 0f)
                                                {
                                                    if (Main.player[this.owner].channel)
                                                    {
                                                        float num3 = 12f;
                                                        Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                        float num4 = 0f + Main.screenPosition.X - vector.X;
                                                        float num5 = 0f + Main.screenPosition.Y - vector.Y;
                                                        float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                        num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                        if (num6 > num3)
                                                        {
                                                            num6 = num3 / num6;
                                                            num4 *= num6;
                                                            num5 *= num6;
                                                            if (num4 != this.velocity.X || num5 != this.velocity.Y)
                                                            {
                                                                this.netUpdate = true;
                                                            }
                                                            this.velocity.X = num4;
                                                            this.velocity.Y = num5;
                                                        }
                                                        else
                                                        {
                                                            if (num4 != this.velocity.X || num5 != this.velocity.Y)
                                                            {
                                                                this.netUpdate = true;
                                                            }
                                                            this.velocity.X = num4;
                                                            this.velocity.Y = num5;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        this.Kill();
                                                    }
                                                }
                                                if (this.type == 34)
                                                {
                                                    this.rotation += 0.3f * (float)this.direction;
                                                }
                                                else
                                                {
                                                    if (this.velocity.X != 0f || this.velocity.Y != 0f)
                                                    {
                                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 2.355f;
                                                    }
                                                }
                                                if (this.velocity.Y > 16f)
                                                {
                                                    this.velocity.Y = 16f;
                                                }
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 10)
                                                {
                                                    if (this.type == 31)
                                                    {
                                                        if (Main.rand.Next(2) == 0)
                                                        {
                                                            int num = 0;
                                                            Dust expr_1CEA_cp_0 = Main.dust[num];
                                                            expr_1CEA_cp_0.velocity.X = expr_1CEA_cp_0.velocity.X * 0.4f;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Main.rand.Next(20) == 0)
                                                        {
                                                        }
                                                    }
                                                    if (Main.myPlayer == this.owner && this.ai[0] == 0f)
                                                    {
                                                        if (Main.player[this.owner].channel)
                                                        {
                                                            float num3 = 12f;
                                                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                            float num4 = 0f + Main.screenPosition.X - vector.X;
                                                            float num5 = 0f + Main.screenPosition.Y - vector.Y;
                                                            float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                            num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                            if (num6 > num3)
                                                            {
                                                                num6 = num3 / num6;
                                                                num4 *= num6;
                                                                num5 *= num6;
                                                                if (num4 != this.velocity.X || num5 != this.velocity.Y)
                                                                {
                                                                    this.netUpdate = true;
                                                                }
                                                                this.velocity.X = num4;
                                                                this.velocity.Y = num5;
                                                            }
                                                            else
                                                            {
                                                                if (num4 != this.velocity.X || num5 != this.velocity.Y)
                                                                {
                                                                    this.netUpdate = true;
                                                                }
                                                                this.velocity.X = num4;
                                                                this.velocity.Y = num5;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this.ai[0] = 1f;
                                                            this.netUpdate = true;
                                                        }
                                                    }
                                                    if (this.ai[0] == 1f)
                                                    {
                                                        this.velocity.Y = this.velocity.Y + 0.41f;
                                                    }
                                                    this.rotation += 0.1f;
                                                    if (this.velocity.Y > 10f)
                                                    {
                                                        this.velocity.Y = 10f;
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.aiStyle == 11)
                                                    {
                                                        this.rotation += 0.02f;
                                                        if (Main.myPlayer == this.owner)
                                                        {
                                                            if (!Main.player[this.owner].dead)
                                                            {
                                                                float num3 = 4f;
                                                                Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                float num4 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
                                                                float num5 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
                                                                float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                                num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                                if (num6 > (float)Main.screenWidth)
                                                                {
                                                                    this.position.X = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - (float)(this.width / 2);
                                                                    this.position.Y = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - (float)(this.height / 2);
                                                                }
                                                                else
                                                                {
                                                                    if (num6 > 64f)
                                                                    {
                                                                        num6 = num3 / num6;
                                                                        num4 *= num6;
                                                                        num5 *= num6;
                                                                        if (num4 != this.velocity.X || num5 != this.velocity.Y)
                                                                        {
                                                                            this.netUpdate = true;
                                                                        }
                                                                        this.velocity.X = num4;
                                                                        this.velocity.Y = num5;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.velocity.X != 0f || this.velocity.Y != 0f)
                                                                        {
                                                                            this.netUpdate = true;
                                                                        }
                                                                        this.velocity.X = 0f;
                                                                        this.velocity.Y = 0f;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                this.Kill();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (this.aiStyle == 12)
                                                        {
                                                            this.scale -= 0.05f;
                                                            if (this.scale <= 0f)
                                                            {
                                                                this.Kill();
                                                            }
                                                            if (this.ai[0] > 4f)
                                                            {
                                                                this.alpha = 150;
                                                                this.light = 0.8f;
                                                                Color color = default(Color);
                                                                int num = 0;
                                                                Main.dust[num].noGravity = true;
                                                            }
                                                            else
                                                            {
                                                                this.ai[0] += 1f;
                                                            }
                                                            this.rotation += 0.3f * (float)this.direction;
                                                        }
                                                        else
                                                        {
                                                            if (this.aiStyle == 13)
                                                            {
                                                                if (Main.player[this.owner].dead)
                                                                {
                                                                    this.Kill();
                                                                }
                                                                else
                                                                {
                                                                    Main.player[this.owner].itemAnimation = 5;
                                                                    Main.player[this.owner].itemTime = 5;
                                                                    if (this.position.X + (float)(this.width / 2) > Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2))
                                                                    {
                                                                        Main.player[this.owner].direction = 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        Main.player[this.owner].direction = -1;
                                                                    }
                                                                    Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                    float num4 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
                                                                    float num5 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
                                                                    float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                                    if (this.ai[0] == 0f)
                                                                    {
                                                                        if (num6 > 600f)
                                                                        {
                                                                            this.ai[0] = 1f;
                                                                        }
                                                                        this.rotation = (float)System.Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
                                                                        this.ai[1] += 1f;
                                                                        if (this.ai[1] > 2f)
                                                                        {
                                                                            this.alpha = 0;
                                                                        }
                                                                        if (this.ai[1] >= 10f)
                                                                        {
                                                                            this.ai[1] = 15f;
                                                                            this.velocity.Y = this.velocity.Y + 0.3f;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.ai[0] == 1f)
                                                                        {
                                                                            this.tileCollide = false;
                                                                            this.rotation = (float)System.Math.Atan2((double)num5, (double)num4) - 1.57f;
                                                                            float num16 = 11f;
                                                                            if (num6 < 50f)
                                                                            {
                                                                                this.Kill();
                                                                            }
                                                                            num6 = num16 / num6;
                                                                            num4 *= num6;
                                                                            num5 *= num6;
                                                                            this.velocity.X = num4;
                                                                            this.velocity.Y = num5;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (this.aiStyle == 14)
                                                                {
                                                                    this.ai[0] += 1f;
                                                                    if (this.ai[0] > 5f)
                                                                    {
                                                                        this.ai[0] = 5f;
                                                                        if (this.velocity.Y == 0f && this.velocity.X != 0f)
                                                                        {
                                                                            this.velocity.X = this.velocity.X * 0.97f;
                                                                            if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
                                                                            {
                                                                                this.velocity.X = 0f;
                                                                                this.netUpdate = true;
                                                                            }
                                                                        }
                                                                        this.velocity.Y = this.velocity.Y + 0.2f;
                                                                    }
                                                                    this.rotation += this.velocity.X * 0.1f;
                                                                }
                                                                else
                                                                {
                                                                    if (this.aiStyle == 15)
                                                                    {
                                                                        if (this.type == 25)
                                                                        {
                                                                            if (Main.rand.Next(15) == 0)
                                                                            {
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.type == 26)
                                                                            {
                                                                                int num = 0;
                                                                                Main.dust[num].noGravity = true;
                                                                                Dust expr_2778_cp_0 = Main.dust[num];
                                                                                expr_2778_cp_0.velocity.X = expr_2778_cp_0.velocity.X / 2f;
                                                                                Dust expr_2795_cp_0 = Main.dust[num];
                                                                                expr_2795_cp_0.velocity.Y = expr_2795_cp_0.velocity.Y / 2f;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.type == 35)
                                                                                {
                                                                                    int num = 0;
                                                                                    Main.dust[num].noGravity = true;
                                                                                    Dust expr_27D8_cp_0 = Main.dust[num];
                                                                                    expr_27D8_cp_0.velocity.X = expr_27D8_cp_0.velocity.X * 2f;
                                                                                    Dust expr_27F5_cp_0 = Main.dust[num];
                                                                                    expr_27F5_cp_0.velocity.Y = expr_27F5_cp_0.velocity.Y * 2f;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (Main.player[this.owner].dead)
                                                                        {
                                                                            this.Kill();
                                                                        }
                                                                        else
                                                                        {
                                                                            Main.player[this.owner].itemAnimation = 5;
                                                                            Main.player[this.owner].itemTime = 5;
                                                                            if (this.position.X + (float)(this.width / 2) > Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2))
                                                                            {
                                                                                Main.player[this.owner].direction = 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                Main.player[this.owner].direction = -1;
                                                                            }
                                                                            Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
                                                                            float num4 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
                                                                            float num5 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
                                                                            float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
                                                                            if (this.ai[0] == 0f)
                                                                            {
                                                                                this.tileCollide = true;
                                                                                if (num6 > 300f)
                                                                                {
                                                                                    this.ai[0] = 1f;
                                                                                }
                                                                                else
                                                                                {
                                                                                    this.ai[1] += 1f;
                                                                                    if (this.ai[1] > 2f)
                                                                                    {
                                                                                        this.alpha = 0;
                                                                                    }
                                                                                    if (this.ai[1] >= 5f)
                                                                                    {
                                                                                        this.ai[1] = 15f;
                                                                                        this.velocity.Y = this.velocity.Y + 0.5f;
                                                                                        this.velocity.X = this.velocity.X * 0.95f;
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.ai[0] == 1f)
                                                                                {
                                                                                    this.tileCollide = false;
                                                                                    float num16 = 11f;
                                                                                    if (num6 < 20f)
                                                                                    {
                                                                                        this.Kill();
                                                                                    }
                                                                                    num6 = num16 / num6;
                                                                                    num4 *= num6;
                                                                                    num5 *= num6;
                                                                                    this.velocity.X = num4;
                                                                                    this.velocity.Y = num5;
                                                                                }
                                                                            }
                                                                            this.rotation += this.velocity.X * 0.03f;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.aiStyle == 16)
                                                                        {
                                                                            if (this.owner == Main.myPlayer && this.timeLeft <= 3 && this.ai[1] == 0f)
                                                                            {
                                                                                this.ai[1] = 1f;
                                                                                this.netUpdate = true;
                                                                            }
                                                                            if (this.type == 37)
                                                                            {
                                                                                try
                                                                                {
                                                                                    int num9 = (int)(this.position.X / 16f) - 1;
                                                                                    int num10 = (int)((this.position.X + (float)this.width) / 16f) + 2;
                                                                                    int num11 = (int)(this.position.Y / 16f) - 1;
                                                                                    int num12 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
                                                                                    if (num9 < 0)
                                                                                    {
                                                                                        num9 = 0;
                                                                                    }
                                                                                    if (num10 > Main.maxTilesX)
                                                                                    {
                                                                                        num10 = Main.maxTilesX;
                                                                                    }
                                                                                    if (num11 < 0)
                                                                                    {
                                                                                        num11 = 0;
                                                                                    }
                                                                                    if (num12 > Main.maxTilesY)
                                                                                    {
                                                                                        num12 = Main.maxTilesY;
                                                                                    }
                                                                                    for (int i = num9; i < num10; i++)
                                                                                    {
                                                                                        for (int j = num11; j < num12; j++)
                                                                                        {
                                                                                            if (Main.tile[i, j] != null && Main.tile[i, j].active && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
                                                                                            {
                                                                                                Vector2 vector2;
                                                                                                vector2.X = (float)(i * 16);
                                                                                                vector2.Y = (float)(j * 16);
                                                                                                if (this.position.X + (float)this.width - 4f > vector2.X && this.position.X + 4f < vector2.X + 16f && this.position.Y + (float)this.height - 4f > vector2.Y && this.position.Y + 4f < vector2.Y + 16f)
                                                                                                {
                                                                                                    this.velocity.X = 0f;
                                                                                                    this.velocity.Y = -0.2f;
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                catch
                                                                                {
                                                                                }
                                                                            }
                                                                            if (this.ai[1] > 0f)
                                                                            {
                                                                                this.alpha = 255;
                                                                                if (this.type == 28 || this.type == 37)
                                                                                {
                                                                                    this.position.X = this.position.X + (float)(this.width / 2);
                                                                                    this.position.Y = this.position.Y + (float)(this.height / 2);
                                                                                    this.width = 128;
                                                                                    this.height = 128;
                                                                                    this.position.X = this.position.X - (float)(this.width / 2);
                                                                                    this.position.Y = this.position.Y - (float)(this.height / 2);
                                                                                    this.damage = 100;
                                                                                    this.knockBack = 8f;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (this.type == 29)
                                                                                    {
                                                                                        this.position.X = this.position.X + (float)(this.width / 2);
                                                                                        this.position.Y = this.position.Y + (float)(this.height / 2);
                                                                                        this.width = 250;
                                                                                        this.height = 250;
                                                                                        this.position.X = this.position.X - (float)(this.width / 2);
                                                                                        this.position.Y = this.position.Y - (float)(this.height / 2);
                                                                                        this.damage = 250;
                                                                                        this.knockBack = 10f;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (this.type == 30)
                                                                                        {
                                                                                            this.position.X = this.position.X + (float)(this.width / 2);
                                                                                            this.position.Y = this.position.Y + (float)(this.height / 2);
                                                                                            this.width = 128;
                                                                                            this.height = 128;
                                                                                            this.position.X = this.position.X - (float)(this.width / 2);
                                                                                            this.position.Y = this.position.Y - (float)(this.height / 2);
                                                                                            this.knockBack = 8f;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.type != 30 && Main.rand.Next(4) == 0)
                                                                                {
                                                                                    if (this.type != 30)
                                                                                    {
                                                                                        this.damage = 0;
                                                                                    }
                                                                                }
                                                                            }
                                                                            this.ai[0] += 1f;
                                                                            if ((this.type == 30 && this.ai[0] > 10f) || (this.type != 30 && this.ai[0] > 5f))
                                                                            {
                                                                                this.ai[0] = 10f;
                                                                                if (this.velocity.Y == 0f && this.velocity.X != 0f)
                                                                                {
                                                                                    this.velocity.X = this.velocity.X * 0.97f;
                                                                                    if (this.type == 29)
                                                                                    {
                                                                                        this.velocity.X = this.velocity.X * 0.99f;
                                                                                    }
                                                                                    if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
                                                                                    {
                                                                                        this.velocity.X = 0f;
                                                                                        this.netUpdate = true;
                                                                                    }
                                                                                }
                                                                                this.velocity.Y = this.velocity.Y + 0.2f;
                                                                            }
                                                                            this.rotation += this.velocity.X * 0.1f;
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
        public Color GetAlpha(Color newColor)
        {
            int r;
            int g;
            int b;
            if (this.type == 9 || this.type == 15 || this.type == 34)
            {
                r = (int)newColor.R - this.alpha / 3;
                g = (int)newColor.G - this.alpha / 3;
                b = (int)newColor.B - this.alpha / 3;
            }
            else
            {
                if (this.type == 16 || this.type == 18)
                {
                    r = (int)newColor.R;
                    g = (int)newColor.G;
                    b = (int)newColor.B;
                }
                else
                {
                    r = (int)newColor.R - this.alpha;
                    g = (int)newColor.G - this.alpha;
                    b = (int)newColor.B - this.alpha;
                }
            }
            int num = (int)newColor.A - this.alpha;
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
        public void Kill()
        {
            if (this.active)
            {
                if (this.owner == Main.myPlayer)
                {
                    if (this.type == 28 || this.type == 29 || this.type == 37)
                    {
                        int num = 3;
                        if (this.type == 29)
                        {
                            num = 7;
                        }
                        int num2 = (int)(this.position.X / 16f) - num;
                        int num3 = (int)(this.position.X / 16f) + num;
                        int num4 = (int)(this.position.Y / 16f) - num;
                        int num5 = (int)(this.position.Y / 16f) + num;
                        if (num2 < 0)
                        {
                            num2 = 0;
                        }
                        if (num3 > Main.maxTilesX)
                        {
                            num3 = Main.maxTilesX;
                        }
                        if (num4 < 0)
                        {
                            num4 = 0;
                        }
                        if (num5 > Main.maxTilesY)
                        {
                            num5 = Main.maxTilesY;
                        }
                        bool flag = false;
                        for (int num6 = num2; num6 <= num3; num6++)
                        {
                            for (int num7 = num4; num7 <= num5; num7++)
                            {
                                float num8 = System.Math.Abs((float)num6 - this.position.X / 16f);
                                float num9 = System.Math.Abs((float)num7 - this.position.Y / 16f);
                                if (System.Math.Sqrt((double)(num8 * num8 + num9 * num9)) < (double)num && Main.tile[num6, num7] != null && Main.tile[num6, num7].wall == 0)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        for (int num6 = num2; num6 <= num3; num6++)
                        {
                            for (int num7 = num4; num7 <= num5; num7++)
                            {
                                float num8 = System.Math.Abs((float)num6 - this.position.X / 16f);
                                float num9 = System.Math.Abs((float)num7 - this.position.Y / 16f);
                                if (System.Math.Sqrt((double)(num8 * num8 + num9 * num9)) < (double)num)
                                {
                                    bool flag2 = true;
                                    if (Main.tile[num6, num7] != null && Main.tile[num6, num7].active)
                                    {
                                        flag2 = false;
                                        if (this.type == 28 || this.type == 37)
                                        {
                                            if (!Main.tileSolid[(int)Main.tile[num6, num7].type] || Main.tileSolidTop[(int)Main.tile[num6, num7].type] || Main.tile[num6, num7].type == 0 || Main.tile[num6, num7].type == 1 || Main.tile[num6, num7].type == 2 || Main.tile[num6, num7].type == 23 || Main.tile[num6, num7].type == 30 || Main.tile[num6, num7].type == 40 || Main.tile[num6, num7].type == 6 || Main.tile[num6, num7].type == 7 || Main.tile[num6, num7].type == 8 || Main.tile[num6, num7].type == 9 || Main.tile[num6, num7].type == 10 || Main.tile[num6, num7].type == 53 || Main.tile[num6, num7].type == 54 || Main.tile[num6, num7].type == 57 || Main.tile[num6, num7].type == 59 || Main.tile[num6, num7].type == 60 || Main.tile[num6, num7].type == 63 || Main.tile[num6, num7].type == 64 || Main.tile[num6, num7].type == 65 || Main.tile[num6, num7].type == 66 || Main.tile[num6, num7].type == 67 || Main.tile[num6, num7].type == 68 || Main.tile[num6, num7].type == 70 || Main.tile[num6, num7].type == 37)
                                            {
                                                flag2 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (this.type == 29)
                                            {
                                                flag2 = true;
                                            }
                                        }
                                        if (Main.tileDungeon[(int)Main.tile[num6, num7].type] || Main.tile[num6, num7].type == 26 || Main.tile[num6, num7].type == 58 || Main.tile[num6, num7].type == 21)
                                        {
                                            flag2 = false;
                                        }
                                        if (flag2)
                                        {
                                            WorldGen.KillTile(num6, num7, false, false, false);
                                            if (!Main.tile[num6, num7].active && Main.netMode == 1)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 0, (float)num6, (float)num7, 0f);
                                            }
                                        }
                                    }
                                    if (flag2 && Main.tile[num6, num7] != null && Main.tile[num6, num7].wall > 0 && flag)
                                    {
                                        WorldGen.KillWall(num6, num7, false);
                                        if (Main.tile[num6, num7].wall == 0 && Main.netMode == 1)
                                        {
                                            NetMessage.SendData(17, -1, -1, "", 2, (float)num6, (float)num7, 0f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(29, -1, -1, "", this.identity, (float)this.owner, 0f, 0f);
                    }
                    int num10 = -1;
                    if (this.aiStyle == 10)
                    {
                        int num11 = ((int)this.position.X + this.width / 2) / 16;
                        int num12 = ((int)this.position.Y + this.width / 2) / 16;
                        int num13 = 0;
                        int num14 = 2;
                        if (this.type == 31)
                        {
                            num13 = 53;
                            num14 = 169;
                        }
                        if (!Main.tile[num11, num12].active)
                        {
                            WorldGen.PlaceTile(num11, num12, num13, false, true, -1);
                            if (Main.tile[num11, num12].active && (int)Main.tile[num11, num12].type == num13)
                            {
                                NetMessage.SendData(17, -1, -1, "", 1, (float)num11, (float)num12, (float)num13);
                            }
                            else
                            {
                                num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num14, 1, false);
                            }
                        }
                        else
                        {
                            num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num14, 1, false);
                        }
                    }
                    if (this.type == 1 && Main.rand.Next(3) < 2)
                    {
                        num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 40, 1, false);
                    }
                    if (this.type == 2 && Main.rand.Next(5) < 4)
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 41, 1, false);
                        }
                        else
                        {
                            num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 40, 1, false);
                        }
                    }
                    if (this.type == 3 && Main.rand.Next(5) < 4)
                    {
                        num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 42, 1, false);
                    }
                    if (this.type == 4 && Main.rand.Next(5) < 4)
                    {
                        num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 47, 1, false);
                    }
                    if (this.type == 12 && this.damage > 100)
                    {
                        num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 75, 1, false);
                    }
                    if (this.type == 21 && Main.rand.Next(5) < 4)
                    {
                        num10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 154, 1, false);
                    }
                    if (Main.netMode == 1 && num10 >= 0)
                    {
                        NetMessage.SendData(21, -1, -1, "", num10, 0f, 0f, 0f);
                    }
                }
                this.active = false;
            }
        }
        public static int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255)
        {
            int num = 1000;
            for (int i = 0; i < 1000; i++)
            {
                if (!Main.projectile[i].active)
                {
                    num = i;
                    break;
                }
            }
            if (num != 1000)
            {
                Main.projectile[num].SetDefaults(Type);
                Main.projectile[num].position.X = X - (float)Main.projectile[num].width * 0.5f;
                Main.projectile[num].position.Y = Y - (float)Main.projectile[num].height * 0.5f;
                Main.projectile[num].owner = Owner;
                Main.projectile[num].velocity.X = SpeedX;
                Main.projectile[num].velocity.Y = SpeedY;
                Main.projectile[num].damage = Damage;
                Main.projectile[num].knockBack = KnockBack;
                Main.projectile[num].identity = num;
                Main.projectile[num].wet = Collision.WetCollision(Main.projectile[num].position, Main.projectile[num].width, Main.projectile[num].height);
                if (Main.netMode != 0 && Owner == Main.myPlayer)
                {
                    NetMessage.SendData(27, -1, -1, "", num, 0f, 0f, 0f);
                }
                if (Owner == Main.myPlayer)
                {
                    if (Type == 28)
                    {
                        Main.projectile[num].timeLeft = 180;
                    }
                    if (Type == 29)
                    {
                        Main.projectile[num].timeLeft = 300;
                    }
                    if (Type == 30)
                    {
                        Main.projectile[num].timeLeft = 180;
                    }
                    if (Type == 37)
                    {
                        Main.projectile[num].timeLeft = 180;
                    }
                }
            }
            return num;
        }
        public void SetDefaults(int Type)
        {
            for (int i = 0; i < Projectile.maxAI; i++)
            {
                this.ai[i] = 0f;
            }
            for (int i = 0; i < 255; i++)
            {
                this.playerImmune[i] = 0;
            }
            this.lavaWet = false;
            this.wetCount = 0;
            this.wet = false;
            this.ignoreWater = false;
            this.hostile = false;
            this.netUpdate = false;
            this.numUpdates = 0;
            this.maxUpdates = 0;
            this.identity = 0;
            this.restrikeDelay = 0;
            this.light = 0f;
            this.penetrate = 1;
            this.tileCollide = true;
            this.position = default(Vector2);
            this.velocity = default(Vector2);
            this.aiStyle = 0;
            this.alpha = 0;
            this.type = Type;
            this.active = true;
            this.rotation = 0f;
            this.scale = 1f;
            this.owner = 255;
            this.timeLeft = 3600;
            this.name = "";
            this.friendly = false;
            this.damage = 0;
            this.knockBack = 0f;
            if (this.type == 1)
            {
                this.name = "Wooden Arrow";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 1;
                this.friendly = true;
            }
            else
            {
                if (this.type == 2)
                {
                    this.name = "Fire Arrow";
                    this.width = 10;
                    this.height = 10;
                    this.aiStyle = 1;
                    this.friendly = true;
                    this.light = 1f;
                }
                else
                {
                    if (this.type == 3)
                    {
                        this.name = "Shuriken";
                        this.width = 22;
                        this.height = 22;
                        this.aiStyle = 2;
                        this.friendly = true;
                        this.penetrate = 4;
                    }
                    else
                    {
                        if (this.type == 4)
                        {
                            this.name = "Unholy Arrow";
                            this.width = 10;
                            this.height = 10;
                            this.aiStyle = 1;
                            this.friendly = true;
                            this.light = 0.2f;
                            this.penetrate = 3;
                        }
                        else
                        {
                            if (this.type == 5)
                            {
                                this.name = "Jester's Arrow";
                                this.width = 10;
                                this.height = 10;
                                this.aiStyle = 1;
                                this.friendly = true;
                                this.light = 0.4f;
                                this.penetrate = -1;
                                this.timeLeft = 40;
                                this.alpha = 100;
                                this.ignoreWater = true;
                            }
                            else
                            {
                                if (this.type == 6)
                                {
                                    this.name = "Enchanted Boomerang";
                                    this.width = 22;
                                    this.height = 22;
                                    this.aiStyle = 3;
                                    this.friendly = true;
                                    this.penetrate = -1;
                                }
                                else
                                {
                                    if (this.type == 7 || this.type == 8)
                                    {
                                        this.name = "Vilethorn";
                                        this.width = 28;
                                        this.height = 28;
                                        this.aiStyle = 4;
                                        this.friendly = true;
                                        this.penetrate = -1;
                                        this.tileCollide = false;
                                        this.alpha = 255;
                                        this.ignoreWater = true;
                                    }
                                    else
                                    {
                                        if (this.type == 9)
                                        {
                                            this.name = "Starfury";
                                            this.width = 24;
                                            this.height = 24;
                                            this.aiStyle = 5;
                                            this.friendly = true;
                                            this.penetrate = 2;
                                            this.alpha = 50;
                                            this.scale = 0.8f;
                                            this.light = 1f;
                                        }
                                        else
                                        {
                                            if (this.type == 10)
                                            {
                                                this.name = "Purification Powder";
                                                this.width = 64;
                                                this.height = 64;
                                                this.aiStyle = 6;
                                                this.friendly = true;
                                                this.tileCollide = false;
                                                this.penetrate = -1;
                                                this.alpha = 255;
                                                this.ignoreWater = true;
                                            }
                                            else
                                            {
                                                if (this.type == 11)
                                                {
                                                    this.name = "Vile Powder";
                                                    this.width = 48;
                                                    this.height = 48;
                                                    this.aiStyle = 6;
                                                    this.friendly = true;
                                                    this.tileCollide = false;
                                                    this.penetrate = -1;
                                                    this.alpha = 255;
                                                    this.ignoreWater = true;
                                                }
                                                else
                                                {
                                                    if (this.type == 12)
                                                    {
                                                        this.name = "Fallen Star";
                                                        this.width = 16;
                                                        this.height = 16;
                                                        this.aiStyle = 5;
                                                        this.friendly = true;
                                                        this.penetrate = -1;
                                                        this.alpha = 50;
                                                        this.light = 1f;
                                                    }
                                                    else
                                                    {
                                                        if (this.type == 13)
                                                        {
                                                            this.name = "Hook";
                                                            this.width = 18;
                                                            this.height = 18;
                                                            this.aiStyle = 7;
                                                            this.friendly = true;
                                                            this.penetrate = -1;
                                                            this.tileCollide = false;
                                                        }
                                                        else
                                                        {
                                                            if (this.type == 14)
                                                            {
                                                                this.name = "Musket Ball";
                                                                this.width = 4;
                                                                this.height = 4;
                                                                this.aiStyle = 1;
                                                                this.friendly = true;
                                                                this.penetrate = 1;
                                                                this.light = 0.5f;
                                                                this.alpha = 255;
                                                                this.maxUpdates = 1;
                                                                this.scale = 1.2f;
                                                                this.timeLeft = 600;
                                                            }
                                                            else
                                                            {
                                                                if (this.type == 15)
                                                                {
                                                                    this.name = "Ball of Fire";
                                                                    this.width = 16;
                                                                    this.height = 16;
                                                                    this.aiStyle = 8;
                                                                    this.friendly = true;
                                                                    this.light = 0.8f;
                                                                    this.alpha = 100;
                                                                }
                                                                else
                                                                {
                                                                    if (this.type == 16)
                                                                    {
                                                                        this.name = "Magic Missile";
                                                                        this.width = 10;
                                                                        this.height = 10;
                                                                        this.aiStyle = 9;
                                                                        this.friendly = true;
                                                                        this.light = 0.8f;
                                                                        this.alpha = 100;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (this.type == 17)
                                                                        {
                                                                            this.name = "Dirt Ball";
                                                                            this.width = 10;
                                                                            this.height = 10;
                                                                            this.aiStyle = 10;
                                                                            this.friendly = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (this.type == 18)
                                                                            {
                                                                                this.name = "Orb of Light";
                                                                                this.width = 32;
                                                                                this.height = 32;
                                                                                this.aiStyle = 11;
                                                                                this.friendly = true;
                                                                                this.light = 1f;
                                                                                this.alpha = 150;
                                                                                this.tileCollide = false;
                                                                                this.penetrate = -1;
                                                                                this.timeLeft *= 5;
                                                                                this.ignoreWater = true;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (this.type == 19)
                                                                                {
                                                                                    this.name = "Flamarang";
                                                                                    this.width = 22;
                                                                                    this.height = 22;
                                                                                    this.aiStyle = 3;
                                                                                    this.friendly = true;
                                                                                    this.penetrate = -1;
                                                                                    this.light = 1f;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (this.type == 20)
                                                                                    {
                                                                                        this.name = "Green Laser";
                                                                                        this.width = 4;
                                                                                        this.height = 4;
                                                                                        this.aiStyle = 1;
                                                                                        this.friendly = true;
                                                                                        this.penetrate = -1;
                                                                                        this.light = 0.75f;
                                                                                        this.alpha = 255;
                                                                                        this.maxUpdates = 2;
                                                                                        this.scale = 1.4f;
                                                                                        this.timeLeft = 600;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (this.type == 21)
                                                                                        {
                                                                                            this.name = "Bone";
                                                                                            this.width = 16;
                                                                                            this.height = 16;
                                                                                            this.aiStyle = 2;
                                                                                            this.scale = 1.2f;
                                                                                            this.friendly = true;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (this.type == 22)
                                                                                            {
                                                                                                this.name = "Water Stream";
                                                                                                this.width = 12;
                                                                                                this.height = 12;
                                                                                                this.aiStyle = 12;
                                                                                                this.friendly = true;
                                                                                                this.alpha = 255;
                                                                                                this.penetrate = -1;
                                                                                                this.maxUpdates = 1;
                                                                                                this.ignoreWater = true;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (this.type == 23)
                                                                                                {
                                                                                                    this.name = "Harpoon";
                                                                                                    this.width = 4;
                                                                                                    this.height = 4;
                                                                                                    this.aiStyle = 13;
                                                                                                    this.friendly = true;
                                                                                                    this.penetrate = -1;
                                                                                                    this.alpha = 255;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (this.type == 24)
                                                                                                    {
                                                                                                        this.name = "Spiky Ball";
                                                                                                        this.width = 14;
                                                                                                        this.height = 14;
                                                                                                        this.aiStyle = 14;
                                                                                                        this.friendly = true;
                                                                                                        this.penetrate = 3;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (this.type == 25)
                                                                                                        {
                                                                                                            this.name = "Ball 'O Hurt";
                                                                                                            this.width = 22;
                                                                                                            this.height = 22;
                                                                                                            this.aiStyle = 15;
                                                                                                            this.friendly = true;
                                                                                                            this.penetrate = -1;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (this.type == 26)
                                                                                                            {
                                                                                                                this.name = "Blue Moon";
                                                                                                                this.width = 22;
                                                                                                                this.height = 22;
                                                                                                                this.aiStyle = 15;
                                                                                                                this.friendly = true;
                                                                                                                this.penetrate = -1;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (this.type == 27)
                                                                                                                {
                                                                                                                    this.name = "Water Bolt";
                                                                                                                    this.width = 16;
                                                                                                                    this.height = 16;
                                                                                                                    this.aiStyle = 8;
                                                                                                                    this.friendly = true;
                                                                                                                    this.light = 0.8f;
                                                                                                                    this.alpha = 200;
                                                                                                                    this.timeLeft /= 2;
                                                                                                                    this.penetrate = 10;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (this.type == 28)
                                                                                                                    {
                                                                                                                        this.name = "Bomb";
                                                                                                                        this.width = 22;
                                                                                                                        this.height = 22;
                                                                                                                        this.aiStyle = 16;
                                                                                                                        this.friendly = true;
                                                                                                                        this.penetrate = -1;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (this.type == 29)
                                                                                                                        {
                                                                                                                            this.name = "Dynamite";
                                                                                                                            this.width = 10;
                                                                                                                            this.height = 10;
                                                                                                                            this.aiStyle = 16;
                                                                                                                            this.friendly = true;
                                                                                                                            this.penetrate = -1;
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (this.type == 30)
                                                                                                                            {
                                                                                                                                this.name = "Grenade";
                                                                                                                                this.width = 14;
                                                                                                                                this.height = 14;
                                                                                                                                this.aiStyle = 16;
                                                                                                                                this.friendly = true;
                                                                                                                                this.penetrate = -1;
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (this.type == 31)
                                                                                                                                {
                                                                                                                                    this.name = "Sand Ball";
                                                                                                                                    this.knockBack = 6f;
                                                                                                                                    this.width = 10;
                                                                                                                                    this.height = 10;
                                                                                                                                    this.aiStyle = 10;
                                                                                                                                    this.friendly = true;
                                                                                                                                    this.hostile = true;
                                                                                                                                    this.penetrate = -1;
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (this.type == 32)
                                                                                                                                    {
                                                                                                                                        this.name = "Ivy Whip";
                                                                                                                                        this.width = 18;
                                                                                                                                        this.height = 18;
                                                                                                                                        this.aiStyle = 7;
                                                                                                                                        this.friendly = true;
                                                                                                                                        this.penetrate = -1;
                                                                                                                                        this.tileCollide = false;
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (this.type == 33)
                                                                                                                                        {
                                                                                                                                            this.name = "Thorn Chakrum";
                                                                                                                                            this.width = 28;
                                                                                                                                            this.height = 28;
                                                                                                                                            this.aiStyle = 3;
                                                                                                                                            this.friendly = true;
                                                                                                                                            this.scale = 0.9f;
                                                                                                                                            this.penetrate = -1;
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (this.type == 34)
                                                                                                                                            {
                                                                                                                                                this.name = "Flamelash";
                                                                                                                                                this.width = 14;
                                                                                                                                                this.height = 14;
                                                                                                                                                this.aiStyle = 9;
                                                                                                                                                this.friendly = true;
                                                                                                                                                this.light = 0.8f;
                                                                                                                                                this.alpha = 100;
                                                                                                                                                this.penetrate = 2;
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (this.type == 35)
                                                                                                                                                {
                                                                                                                                                    this.name = "Sunfury";
                                                                                                                                                    this.width = 22;
                                                                                                                                                    this.height = 22;
                                                                                                                                                    this.aiStyle = 15;
                                                                                                                                                    this.friendly = true;
                                                                                                                                                    this.penetrate = -1;
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (this.type == 36)
                                                                                                                                                    {
                                                                                                                                                        this.name = "Meteor Shot";
                                                                                                                                                        this.width = 4;
                                                                                                                                                        this.height = 4;
                                                                                                                                                        this.aiStyle = 1;
                                                                                                                                                        this.friendly = true;
                                                                                                                                                        this.penetrate = 2;
                                                                                                                                                        this.light = 0.6f;
                                                                                                                                                        this.alpha = 255;
                                                                                                                                                        this.maxUpdates = 1;
                                                                                                                                                        this.scale = 1.4f;
                                                                                                                                                        this.timeLeft = 600;
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (this.type == 37)
                                                                                                                                                        {
                                                                                                                                                            this.name = "Bomb";
                                                                                                                                                            this.width = 22;
                                                                                                                                                            this.height = 22;
                                                                                                                                                            this.aiStyle = 16;
                                                                                                                                                            this.friendly = true;
                                                                                                                                                            this.penetrate = -1;
                                                                                                                                                            this.tileCollide = false;
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
            this.width = (int)((float)this.width * this.scale);
            this.height = (int)((float)this.height * this.scale);
        }
        public void Update(int i)
        {
            if (this.active)
            {
                Vector2 b = this.velocity;
                if (this.position.X <= Main.leftWorld || this.position.X + (float)this.width >= Main.rightWorld || this.position.Y <= Main.topWorld || this.position.Y + (float)this.height >= Main.bottomWorld)
                {
                    this.active = false;
                }
                else
                {
                    this.whoAmI = i;
                    if (this.soundDelay > 0)
                    {
                        this.soundDelay--;
                    }
                    this.netUpdate = false;
                    for (int j = 0; j < 255; j++)
                    {
                        if (this.playerImmune[j] > 0)
                        {
                            this.playerImmune[j]--;
                        }
                    }
                    this.AI();
                    if (this.owner < 255 && !Main.player[this.owner].active)
                    {
                        this.Kill();
                    }
                    if (!this.ignoreWater)
                    {
                        bool flag;
                        bool flag2;
                        try
                        {
                            flag = Collision.LavaCollision(this.position, this.width, this.height);
                            flag2 = Collision.WetCollision(this.position, this.width, this.height);
                            if (flag)
                            {
                                this.lavaWet = true;
                            }
                        }
                        catch
                        {
                            this.active = false;
                            return;
                        }
                        if (flag2)
                        {
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!this.wet)
                                {
                                    if (!flag)
                                    {
                                        for (int k = 0; k < 10; k++)
                                        {
                                            int num = 0;
                                            Dust expr_1FC_cp_0 = Main.dust[num];
                                            expr_1FC_cp_0.velocity.Y = expr_1FC_cp_0.velocity.Y - 4f;
                                            Dust expr_21A_cp_0 = Main.dust[num];
                                            expr_21A_cp_0.velocity.X = expr_21A_cp_0.velocity.X * 2.5f;
                                            Main.dust[num].scale = 1.3f;
                                            Main.dust[num].alpha = 100;
                                            Main.dust[num].noGravity = true;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < 10; k++)
                                        {
                                            int num = 0;
                                            Dust expr_28D_cp_0 = Main.dust[num];
                                            expr_28D_cp_0.velocity.Y = expr_28D_cp_0.velocity.Y - 1.5f;
                                            Dust expr_2AB_cp_0 = Main.dust[num];
                                            expr_2AB_cp_0.velocity.X = expr_2AB_cp_0.velocity.X * 2.5f;
                                            Main.dust[num].scale = 1.3f;
                                            Main.dust[num].alpha = 100;
                                            Main.dust[num].noGravity = true;
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
                                if (this.wetCount == 0)
                                {
                                    this.wetCount = 10;
                                    if (!this.lavaWet)
                                    {
                                        for (int k = 0; k < 10; k++)
                                        {
                                            int num = 0;
                                            Dust expr_36F_cp_0 = Main.dust[num];
                                            expr_36F_cp_0.velocity.Y = expr_36F_cp_0.velocity.Y - 4f;
                                            Dust expr_38D_cp_0 = Main.dust[num];
                                            expr_38D_cp_0.velocity.X = expr_38D_cp_0.velocity.X * 2.5f;
                                            Main.dust[num].scale = 1.3f;
                                            Main.dust[num].alpha = 100;
                                            Main.dust[num].noGravity = true;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < 10; k++)
                                        {
                                            int num = 0;
                                            Dust expr_400_cp_0 = Main.dust[num];
                                            expr_400_cp_0.velocity.Y = expr_400_cp_0.velocity.Y - 1.5f;
                                            Dust expr_41E_cp_0 = Main.dust[num];
                                            expr_41E_cp_0.velocity.X = expr_41E_cp_0.velocity.X * 2.5f;
                                            Main.dust[num].scale = 1.3f;
                                            Main.dust[num].alpha = 100;
                                            Main.dust[num].noGravity = true;
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
                    }
                    if (this.tileCollide)
                    {
                        Vector2 a = this.velocity;
                        bool flag3 = true;
                        if (this.type == 9 || this.type == 12 || this.type == 15 || this.type == 13 || this.type == 31)
                        {
                            flag3 = false;
                        }
                        if (this.aiStyle == 10)
                        {
                            this.velocity = Collision.AnyCollision(this.position, this.velocity, this.width, this.height);
                        }
                        else
                        {
                            if (this.wet)
                            {
                                Vector2 vector = this.velocity;
                                this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
                                b = this.velocity * 0.5f;
                                if (this.velocity.X != vector.X)
                                {
                                    b.X = this.velocity.X;
                                }
                                if (this.velocity.Y != vector.Y)
                                {
                                    b.Y = this.velocity.Y;
                                }
                            }
                            else
                            {
                                this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
                            }
                        }
                        if (a != this.velocity)
                        {
                            if (this.type == 36)
                            {
                                if (this.penetrate > 1)
                                {
                                    Collision.HitTiles(this.position, this.velocity, this.width, this.height);
                                    this.penetrate--;
                                    if (this.velocity.X != a.X)
                                    {
                                        this.velocity.X = -a.X;
                                    }
                                    if (this.velocity.Y != a.Y)
                                    {
                                        this.velocity.Y = -a.Y;
                                    }
                                }
                                else
                                {
                                    this.Kill();
                                }
                            }
                            else
                            {
                                if (this.aiStyle == 3 || this.aiStyle == 13 || this.aiStyle == 15)
                                {
                                    Collision.HitTiles(this.position, this.velocity, this.width, this.height);
                                    if (this.type == 33)
                                    {
                                        if (this.velocity.X != a.X)
                                        {
                                            this.velocity.X = -a.X;
                                        }
                                        if (this.velocity.Y != a.Y)
                                        {
                                            this.velocity.Y = -a.Y;
                                        }
                                    }
                                    else
                                    {
                                        this.ai[0] = 1f;
                                        if (this.aiStyle == 3)
                                        {
                                            this.velocity.X = -a.X;
                                            this.velocity.Y = -a.Y;
                                        }
                                    }
                                    this.netUpdate = true;
                                }
                                else
                                {
                                    if (this.aiStyle == 8)
                                    {
                                        this.ai[0] += 1f;
                                        if (this.ai[0] >= 5f)
                                        {
                                            this.position += this.velocity;
                                            this.Kill();
                                        }
                                        else
                                        {
                                            if (this.velocity.Y != a.Y)
                                            {
                                                this.velocity.Y = -a.Y;
                                            }
                                            if (this.velocity.X != a.X)
                                            {
                                                this.velocity.X = -a.X;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.aiStyle == 14)
                                        {
                                            if (this.velocity.X != a.X)
                                            {
                                                this.velocity.X = a.X * -0.5f;
                                            }
                                            if (this.velocity.Y != a.Y && a.Y > 1f)
                                            {
                                                this.velocity.Y = a.Y * -0.5f;
                                            }
                                        }
                                        else
                                        {
                                            if (this.aiStyle == 16)
                                            {
                                                if (this.velocity.X != a.X)
                                                {
                                                    this.velocity.X = a.X * -0.4f;
                                                    if (this.type == 29)
                                                    {
                                                        this.velocity.X = this.velocity.X * 0.8f;
                                                    }
                                                }
                                                if (this.velocity.Y != a.Y && (double)a.Y > 0.7)
                                                {
                                                    this.velocity.Y = a.Y * -0.4f;
                                                    if (this.type == 29)
                                                    {
                                                        this.velocity.Y = this.velocity.Y * 0.8f;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                this.position += this.velocity;
                                                this.Kill();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (this.type != 7 && this.type != 8)
                    {
                        if (this.wet)
                        {
                            this.position += b;
                        }
                        else
                        {
                            this.position += this.velocity;
                        }
                    }
                    if ((this.aiStyle != 3 || this.ai[0] != 1f) && (this.aiStyle != 7 || this.ai[0] != 1f) && (this.aiStyle != 13 || this.ai[0] != 1f) && (this.aiStyle != 15 || this.ai[0] != 1f))
                    {
                        if (this.velocity.X < 0f)
                        {
                            this.direction = -1;
                        }
                        else
                        {
                            this.direction = 1;
                        }
                    }
                    if (this.active)
                    {
                        if (this.light > 0f)
                        {
                            Lighting.addLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), this.light);
                        }
                        if (this.type != 2)
                        {
                            if (this.type == 4)
                            {
                                if (Main.rand.Next(5) == 0)
                                {
                                }
                            }
                            else
                            {
                                if (this.type == 5)
                                {
                                }
                            }
                        }
                        Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
                        if (this.hostile && Main.myPlayer < 255 && this.damage > 0)
                        {
                            int j = Main.myPlayer;
                            if (Main.player[j].active && !Main.player[j].dead && !Main.player[j].immune)
                            {
                                Rectangle value = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
                                if (rectangle.Intersects(value))
                                {
                                    int hitDirection = this.direction;
                                    if (Main.player[j].position.X + (float)(Main.player[j].width / 2) < this.position.X + (float)(this.width / 2))
                                    {
                                        hitDirection = -1;
                                    }
                                    else
                                    {
                                        hitDirection = 1;
                                    }
                                    Main.player[j].Hurt(this.damage * 2, hitDirection, false, false);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(26, -1, -1, "", j, (float)this.direction, (float)(this.damage * 2), 0f);
                                    }
                                }
                            }
                        }
                        if (this.friendly && this.type != 18 && this.owner == Main.myPlayer)
                        {
                            if (this.aiStyle == 16 && this.ai[1] > 0f)
                            {
                                for (int j = 0; j < 255; j++)
                                {
                                    if (Main.player[j].active && !Main.player[j].dead && !Main.player[j].immune)
                                    {
                                        Rectangle value = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
                                        if (rectangle.Intersects(value))
                                        {
                                            if (Main.player[j].position.X + (float)(Main.player[j].width / 2) < this.position.X + (float)(this.width / 2))
                                            {
                                                this.direction = -1;
                                            }
                                            else
                                            {
                                                this.direction = 1;
                                            }
                                            Main.player[j].Hurt(this.damage, this.direction, true, false);
                                            if (Main.netMode != 0)
                                            {
                                                NetMessage.SendData(26, -1, -1, "", j, (float)this.direction, (float)this.damage, 1f);
                                            }
                                        }
                                    }
                                }
                            }
                            int num2 = (int)(this.position.X / 16f);
                            int num3 = (int)((this.position.X + (float)this.width) / 16f) + 1;
                            int num4 = (int)(this.position.Y / 16f);
                            int num5 = (int)((this.position.Y + (float)this.height) / 16f) + 1;
                            if (num2 < 0)
                            {
                                num2 = 0;
                            }
                            if (num3 > Main.maxTilesX)
                            {
                                num3 = Main.maxTilesX;
                            }
                            if (num4 < 0)
                            {
                                num4 = 0;
                            }
                            if (num5 > Main.maxTilesY)
                            {
                                num5 = Main.maxTilesY;
                            }
                            for (int l = num2; l < num3; l++)
                            {
                                for (int m = num4; m < num5; m++)
                                {
                                    if (Main.tile[l, m] != null && (Main.tile[l, m].type == 3 || Main.tile[l, m].type == 24 || Main.tile[l, m].type == 28 || Main.tile[l, m].type == 32 || Main.tile[l, m].type == 51 || Main.tile[l, m].type == 52 || Main.tile[l, m].type == 61 || Main.tile[l, m].type == 62 || Main.tile[l, m].type == 69 || Main.tile[l, m].type == 71 || Main.tile[l, m].type == 73 || Main.tile[l, m].type == 74))
                                    {
                                        WorldGen.KillTile(l, m, false, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(17, -1, -1, "", 0, (float)l, (float)m, 0f);
                                        }
                                    }
                                }
                            }
                            if (this.damage > 0)
                            {
                                for (int j = 0; j < 1000; j++)
                                {
                                    if (Main.npc[j].active && !Main.npc[j].friendly && (this.owner < 0 || Main.npc[j].immune[this.owner] == 0))
                                    {
                                        Rectangle value2 = new Rectangle((int)Main.npc[j].position.X, (int)Main.npc[j].position.Y, Main.npc[j].width, Main.npc[j].height);
                                        if (rectangle.Intersects(value2))
                                        {
                                            if (this.aiStyle == 3)
                                            {
                                                if (this.ai[0] == 0f)
                                                {
                                                    this.velocity.X = -this.velocity.X;
                                                    this.velocity.Y = -this.velocity.Y;
                                                    this.netUpdate = true;
                                                }
                                                this.ai[0] = 1f;
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 16)
                                                {
                                                    if (this.timeLeft > 3)
                                                    {
                                                        this.timeLeft = 3;
                                                    }
                                                    if (Main.npc[j].position.X + (float)(Main.npc[j].width / 2) < this.position.X + (float)(this.width / 2))
                                                    {
                                                        this.direction = -1;
                                                    }
                                                    else
                                                    {
                                                        this.direction = 1;
                                                    }
                                                }
                                            }
                                            Main.npc[j].StrikeNPC(this.damage, this.knockBack, this.direction);
                                            if (Main.netMode != 0)
                                            {
                                                NetMessage.SendData(28, -1, -1, "", j, (float)this.damage, this.knockBack, (float)this.direction);
                                            }
                                            if (this.penetrate != 1)
                                            {
                                                Main.npc[j].immune[this.owner] = 10;
                                            }
                                            if (this.penetrate > 0)
                                            {
                                                this.penetrate--;
                                                if (this.penetrate == 0)
                                                {
                                                    break;
                                                }
                                            }
                                            if (this.aiStyle == 7)
                                            {
                                                this.ai[0] = 1f;
                                                this.damage = 0;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 13)
                                                {
                                                    this.ai[0] = 1f;
                                                    this.netUpdate = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (this.damage > 0 && Main.player[Main.myPlayer].hostile)
                            {
                                for (int j = 0; j < 255; j++)
                                {
                                    if (j != this.owner && Main.player[j].active && !Main.player[j].dead && !Main.player[j].immune && Main.player[j].hostile && this.playerImmune[j] <= 0 && (Main.player[Main.myPlayer].team == 0 || Main.player[Main.myPlayer].team != Main.player[j].team))
                                    {
                                        Rectangle value = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
                                        if (rectangle.Intersects(value))
                                        {
                                            if (this.aiStyle == 3)
                                            {
                                                if (this.ai[0] == 0f)
                                                {
                                                    this.velocity.X = -this.velocity.X;
                                                    this.velocity.Y = -this.velocity.Y;
                                                    this.netUpdate = true;
                                                }
                                                this.ai[0] = 1f;
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 16)
                                                {
                                                    if (this.timeLeft > 3)
                                                    {
                                                        this.timeLeft = 3;
                                                    }
                                                    if (Main.player[j].position.X + (float)(Main.player[j].width / 2) < this.position.X + (float)(this.width / 2))
                                                    {
                                                        this.direction = -1;
                                                    }
                                                    else
                                                    {
                                                        this.direction = 1;
                                                    }
                                                }
                                            }
                                            Main.player[j].Hurt(this.damage, this.direction, true, false);
                                            if (Main.netMode != 0)
                                            {
                                                NetMessage.SendData(26, -1, -1, "", j, (float)this.direction, (float)this.damage, 1f);
                                            }
                                            this.playerImmune[j] = 40;
                                            if (this.penetrate > 0)
                                            {
                                                this.penetrate--;
                                                if (this.penetrate == 0)
                                                {
                                                    break;
                                                }
                                            }
                                            if (this.aiStyle == 7)
                                            {
                                                this.ai[0] = 1f;
                                                this.damage = 0;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                if (this.aiStyle == 13)
                                                {
                                                    this.ai[0] = 1f;
                                                    this.netUpdate = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        this.timeLeft--;
                        if (this.timeLeft <= 0)
                        {
                            this.Kill();
                        }
                        if (this.penetrate == 0)
                        {
                            this.Kill();
                        }
                        if (this.active && this.netUpdate && this.owner == Main.myPlayer)
                        {
                            NetMessage.SendData(27, -1, -1, "", i, 0f, 0f, 0f);
                        }
                        if (this.active && this.maxUpdates > 0)
                        {
                            this.numUpdates--;
                            if (this.numUpdates >= 0)
                            {
                                this.Update(i);
                            }
                            else
                            {
                                this.numUpdates = this.maxUpdates;
                            }
                        }
                        this.netUpdate = false;
                    }
                }
            }
        }
    }
}

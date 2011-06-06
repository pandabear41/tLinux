using System;
namespace Terraria
{
    public class Cloud
    {
        public bool active = false;
        public int height = 0;
        public Vector2 position;
        private static System.Random rand = new System.Random();
        public float rotation;
        public float rSpeed;
        public float scale;
        public float sSpeed;
        public int type = 0;
        public int width = 0;
        public static void addCloud()
        {
            int num = -1;
            for (int i = 0; i < 100; i++)
            {
                if (!Main.cloud[i].active)
                {
                    num = i;
                    break;
                }
            }
            if (num >= 0)
            {
                Main.cloud[num].rSpeed = 0f;
                Main.cloud[num].sSpeed = 0f;
                Main.cloud[num].type = Cloud.rand.Next(4);
                Main.cloud[num].scale = (float)Cloud.rand.Next(8, 13) * 0.1f;
                Main.cloud[num].rotation = (float)Cloud.rand.Next(-10, 11) * 0.01f;
                Main.cloud[num].width = (int)((float)Main.cloudTexture[Main.cloud[num].type].Width * Main.cloud[num].scale);
                Main.cloud[num].height = (int)((float)Main.cloudTexture[Main.cloud[num].type].Height * Main.cloud[num].scale);
                if (Main.windSpeed > 0f)
                {
                    Main.cloud[num].position.X = (float)(-(float)Main.cloud[num].width - Main.cloudTexture[Main.cloud[num].type].Width - Cloud.rand.Next(Main.screenWidth * 2));
                }
                else
                {
                    Main.cloud[num].position.X = (float)(Main.screenWidth + Main.cloudTexture[Main.cloud[num].type].Width + Cloud.rand.Next(Main.screenWidth * 2));
                }
                Main.cloud[num].position.Y = (float)Cloud.rand.Next((int)((float)(-(float)Main.screenHeight) * 0.25f), (int)((double)Main.screenHeight * 1.25));
                Cloud expr_207_cp_0 = Main.cloud[num];
                expr_207_cp_0.position.Y = expr_207_cp_0.position.Y - (float)Cloud.rand.Next((int)((float)Main.screenHeight * 0.25f));
                Cloud expr_237_cp_0 = Main.cloud[num];
                expr_237_cp_0.position.Y = expr_237_cp_0.position.Y - (float)Cloud.rand.Next((int)((float)Main.screenHeight * 0.25f));
                Cloud cloud = Main.cloud[num];
                cloud.scale *= 2.2f - (float)((double)(Main.cloud[num].position.Y + (float)Main.screenHeight * 0.25f) / ((double)Main.screenHeight * 1.5) + 0.699999988079071);
                if ((double)Main.cloud[num].scale > 1.4)
                {
                    Main.cloud[num].scale = 1.4f;
                }
                if ((double)Main.cloud[num].scale < 0.6)
                {
                    Main.cloud[num].scale = 0.6f;
                }
                Main.cloud[num].active = true;
                Rectangle rectangle = new Rectangle((int)Main.cloud[num].position.X, (int)Main.cloud[num].position.Y, Main.cloud[num].width, Main.cloud[num].height);
                for (int i = 0; i < 100; i++)
                {
                    if (num != i && Main.cloud[i].active)
                    {
                        Rectangle value = new Rectangle((int)Main.cloud[i].position.X, (int)Main.cloud[i].position.Y, Main.cloud[i].width, Main.cloud[i].height);
                        if (rectangle.Intersects(value))
                        {
                            Main.cloud[num].active = false;
                        }
                    }
                }
            }
        }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
        public static void resetClouds()
        {
            if (Main.cloudLimit >= 10)
            {
                Main.numClouds = Cloud.rand.Next(10, Main.cloudLimit);
                Main.windSpeed = 0f;
                while (Main.windSpeed == 0f)
                {
                    Main.windSpeed = (float)Cloud.rand.Next(-100, 101) * 0.01f;
                }
                for (int i = 0; i < 100; i++)
                {
                    Main.cloud[i].active = false;
                }
                for (int i = 0; i < Main.numClouds; i++)
                {
                    Cloud.addCloud();
                }
                for (int i = 0; i < Main.numClouds; i++)
                {
                    if (Main.windSpeed < 0f)
                    {
                        Cloud expr_C1_cp_0 = Main.cloud[i];
                        expr_C1_cp_0.position.X = expr_C1_cp_0.position.X - (float)(Main.screenWidth * 2);
                    }
                    else
                    {
                        Cloud expr_E5_cp_0 = Main.cloud[i];
                        expr_E5_cp_0.position.X = expr_E5_cp_0.position.X + (float)(Main.screenWidth * 2);
                    }
                }
            }
        }
        public void Update()
        {
            if (Main.gameMenu)
            {
                this.position.X = this.position.X + Main.windSpeed * this.scale * 3f;
            }
            else
            {
                this.position.X = this.position.X + (Main.windSpeed - Main.player[Main.myPlayer].velocity.X * 0.1f) * this.scale;
            }
            if (Main.windSpeed > 0f)
            {
                if (this.position.X - (float)Main.cloudTexture[this.type].Width > (float)Main.screenWidth)
                {
                    this.active = false;
                }
            }
            else
            {
                if (this.position.X + (float)this.width + (float)Main.cloudTexture[this.type].Width < 0f)
                {
                    this.active = false;
                }
            }
            this.rSpeed += (float)Cloud.rand.Next(-10, 11) * 2E-05f;
            if ((double)this.rSpeed > 0.0007)
            {
                this.rSpeed = 0.0007f;
            }
            if ((double)this.rSpeed < -0.0007)
            {
                this.rSpeed = -0.0007f;
            }
            if ((double)this.rotation > 0.05)
            {
                this.rotation = 0.05f;
            }
            if ((double)this.rotation < -0.05)
            {
                this.rotation = -0.05f;
            }
            this.sSpeed += (float)Cloud.rand.Next(-10, 11) * 2E-05f;
            if ((double)this.sSpeed > 0.0007)
            {
                this.sSpeed = 0.0007f;
            }
            if ((double)this.sSpeed < -0.0007)
            {
                this.sSpeed = -0.0007f;
            }
            if ((double)this.scale > 1.4)
            {
                this.scale = 1.4f;
            }
            if ((double)this.scale < 0.6)
            {
                this.scale = 0.6f;
            }
            this.rotation += this.rSpeed;
            this.scale += this.sSpeed;
            this.width = (int)((float)Main.cloudTexture[this.type].Width * this.scale);
            this.height = (int)((float)Main.cloudTexture[this.type].Height * this.scale);
        }
        public static void UpdateClouds()
        {
            int num = 0;
            for (int i = 0; i < 100; i++)
            {
                if (Main.cloud[i].active)
                {
                    Main.cloud[i].Update();
                    num++;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Main.cloud[i].active)
                {
                    if (i > 1 && (!Main.cloud[i - 1].active || (double)Main.cloud[i - 1].scale > (double)Main.cloud[i].scale + 0.02))
                    {
                        Cloud cloud = (Cloud)Main.cloud[i - 1].Clone();
                        Main.cloud[i - 1] = (Cloud)Main.cloud[i].Clone();
                        Main.cloud[i] = cloud;
                    }
                    if (i < 99 && (!Main.cloud[i].active || (double)Main.cloud[i + 1].scale < (double)Main.cloud[i].scale - 0.02))
                    {
                        Cloud cloud = (Cloud)Main.cloud[i + 1].Clone();
                        Main.cloud[i + 1] = (Cloud)Main.cloud[i].Clone();
                        Main.cloud[i] = cloud;
                    }
                }
            }
            if (num < Main.numClouds)
            {
                Cloud.addCloud();
            }
        }
    }
}

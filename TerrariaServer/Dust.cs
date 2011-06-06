using System;
namespace Terraria
{
    public class Dust
    {
        public bool active = false;
        public int alpha;
        public Color color;
        public Rectangle frame;
        public bool noGravity = false;
        public bool noLight = false;
        public Vector2 position;
        public float rotation;
        public float scale;
        public int type = 0;
        public Vector2 velocity;
        public Color GetAlpha(Color newColor)
        {
            int r;
            int g;
            int b;
            if (this.type == 15 || this.type == 20 || this.type == 21 || this.type == 29 || this.type == 35 || this.type == 41)
            {
                r = (int)newColor.R - this.alpha / 3;
                g = (int)newColor.G - this.alpha / 3;
                b = (int)newColor.B - this.alpha / 3;
            }
            else
            {
                r = (int)newColor.R - this.alpha;
                g = (int)newColor.G - this.alpha;
                b = (int)newColor.B - this.alpha;
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
        public static void UpdateDust()
        {
            for (int i = 0; i < 2000; i++)
            {
                if (Main.dust[i].active)
                {
                    Dust dust = Main.dust[i];
                    Dust expr_2B = dust;
                    expr_2B.position += Main.dust[i].velocity;
                    if (Main.dust[i].type == 6 || Main.dust[i].type == 29)
                    {
                        if (!Main.dust[i].noGravity)
                        {
                            Dust expr_95_cp_0 = Main.dust[i];
                            expr_95_cp_0.velocity.Y = expr_95_cp_0.velocity.Y + 0.05f;
                        }
                        if (!Main.dust[i].noLight)
                        {
                            float num = Main.dust[i].scale * 1.6f;
                            if (Main.dust[i].type == 29)
                            {
                                num *= 0.3f;
                            }
                            if (num > 1f)
                            {
                                num = 1f;
                            }
                            Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                        }
                    }
                    else
                    {
                        if (Main.dust[i].type == 14 || Main.dust[i].type == 16 || Main.dust[i].type == 31)
                        {
                            Dust expr_193_cp_0 = Main.dust[i];
                            expr_193_cp_0.velocity.Y = expr_193_cp_0.velocity.Y * 0.98f;
                            Dust expr_1B0_cp_0 = Main.dust[i];
                            expr_1B0_cp_0.velocity.X = expr_1B0_cp_0.velocity.X * 0.98f;
                        }
                        else
                        {
                            if (Main.dust[i].type == 32)
                            {
                                Dust dust2 = Main.dust[i];
                                dust2.scale -= 0.01f;
                                Dust expr_207_cp_0 = Main.dust[i];
                                expr_207_cp_0.velocity.X = expr_207_cp_0.velocity.X * 0.96f;
                                Dust expr_224_cp_0 = Main.dust[i];
                                expr_224_cp_0.velocity.Y = expr_224_cp_0.velocity.Y + 0.1f;
                            }
                            else
                            {
                                if (Main.dust[i].type == 15)
                                {
                                    Dust expr_264_cp_0 = Main.dust[i];
                                    expr_264_cp_0.velocity.Y = expr_264_cp_0.velocity.Y * 0.98f;
                                    Dust expr_281_cp_0 = Main.dust[i];
                                    expr_281_cp_0.velocity.X = expr_281_cp_0.velocity.X * 0.98f;
                                    float num = Main.dust[i].scale;
                                    if (num > 1f)
                                    {
                                        num = 1f;
                                    }
                                    Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                                }
                                else
                                {
                                    if (Main.dust[i].type == 20 || Main.dust[i].type == 21)
                                    {
                                        Dust dust3 = Main.dust[i];
                                        dust3.scale += 0.005f;
                                        Dust expr_34D_cp_0 = Main.dust[i];
                                        expr_34D_cp_0.velocity.Y = expr_34D_cp_0.velocity.Y * 0.94f;
                                        Dust expr_36A_cp_0 = Main.dust[i];
                                        expr_36A_cp_0.velocity.X = expr_36A_cp_0.velocity.X * 0.94f;
                                        float num = Main.dust[i].scale * 0.8f;
                                        if (Main.dust[i].type == 21)
                                        {
                                            num = Main.dust[i].scale * 0.4f;
                                        }
                                        if (num > 1f)
                                        {
                                            num = 1f;
                                        }
                                        Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                                    }
                                    else
                                    {
                                        if (Main.dust[i].type == 27)
                                        {
                                            Dust dust4 = Main.dust[i];
                                            dust4.velocity *= 0.94f;
                                            Dust dust5 = Main.dust[i];
                                            dust5.scale += 0.002f;
                                            float num = Main.dust[i].scale;
                                            if (num > 1f)
                                            {
                                                num = 1f;
                                            }
                                            Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                                        }
                                        else
                                        {
                                            if (!Main.dust[i].noGravity && Main.dust[i].type != 41)
                                            {
                                                Dust expr_500_cp_0 = Main.dust[i];
                                                expr_500_cp_0.velocity.Y = expr_500_cp_0.velocity.Y + 0.1f;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Main.dust[i].type == 5 && Main.dust[i].noGravity)
                    {
                        Dust dust6 = Main.dust[i];
                        dust6.scale -= 0.04f;
                    }
                    if (Main.dust[i].type == 33)
                    {
                        if (Collision.WetCollision(new Vector2(Main.dust[i].position.X, Main.dust[i].position.Y), 4, 4))
                        {
                            Dust dust7 = Main.dust[i];
                            dust7.alpha += 20;
                            Dust dust8 = Main.dust[i];
                            dust8.scale -= 0.1f;
                        }
                        Dust dust9 = Main.dust[i];
                        dust9.alpha += 2;
                        Dust dust10 = Main.dust[i];
                        dust10.scale -= 0.005f;
                        if (Main.dust[i].alpha > 255)
                        {
                            Main.dust[i].scale = 0f;
                        }
                        Dust expr_651_cp_0 = Main.dust[i];
                        expr_651_cp_0.velocity.X = expr_651_cp_0.velocity.X * 0.93f;
                        if (Main.dust[i].velocity.Y > 4f)
                        {
                            Main.dust[i].velocity.Y = 4f;
                        }
                        if (Main.dust[i].noGravity)
                        {
                            if (Main.dust[i].velocity.X < 0f)
                            {
                                Dust dust11 = Main.dust[i];
                                dust11.rotation -= 0.2f;
                            }
                            else
                            {
                                Dust dust12 = Main.dust[i];
                                dust12.rotation += 0.2f;
                            }
                            Dust dust13 = Main.dust[i];
                            dust13.scale += 0.03f;
                            Dust expr_73B_cp_0 = Main.dust[i];
                            expr_73B_cp_0.velocity.X = expr_73B_cp_0.velocity.X * 1.05f;
                            Dust expr_758_cp_0 = Main.dust[i];
                            expr_758_cp_0.velocity.Y = expr_758_cp_0.velocity.Y + 0.15f;
                        }
                    }
                    if (Main.dust[i].type == 35 && Main.dust[i].noGravity)
                    {
                        Dust dust14 = Main.dust[i];
                        dust14.scale += 0.02f;
                        if (Main.dust[i].scale < 1f)
                        {
                            Dust expr_7DC_cp_0 = Main.dust[i];
                            expr_7DC_cp_0.velocity.Y = expr_7DC_cp_0.velocity.Y + 0.075f;
                        }
                        Dust expr_7FA_cp_0 = Main.dust[i];
                        expr_7FA_cp_0.velocity.X = expr_7FA_cp_0.velocity.X * 1.08f;
                        if (Main.dust[i].velocity.X > 0f)
                        {
                            Dust dust15 = Main.dust[i];
                            dust15.rotation += 0.01f;
                        }
                        else
                        {
                            Dust dust16 = Main.dust[i];
                            dust16.rotation -= 0.01f;
                        }
                    }
                    else
                    {
                        if (Main.dust[i].type == 34 || Main.dust[i].type == 35)
                        {
                            if (!Collision.WetCollision(new Vector2(Main.dust[i].position.X, Main.dust[i].position.Y - 8f), 4, 4))
                            {
                                Main.dust[i].scale = 0f;
                            }
                            else
                            {
                                Dust dust17 = Main.dust[i];
                                dust17.alpha += Main.rand.Next(2);
                                if (Main.dust[i].alpha > 255)
                                {
                                    Main.dust[i].scale = 0f;
                                }
                                Main.dust[i].velocity.Y = -0.5f;
                                if (Main.dust[i].type == 34)
                                {
                                    Dust dust18 = Main.dust[i];
                                    dust18.scale += 0.005f;
                                }
                                else
                                {
                                    Dust dust19 = Main.dust[i];
                                    dust19.alpha++;
                                    Dust dust20 = Main.dust[i];
                                    dust20.scale -= 0.01f;
                                    Main.dust[i].velocity.Y = -0.2f;
                                }
                                Dust expr_9EB_cp_0 = Main.dust[i];
                                expr_9EB_cp_0.velocity.X = expr_9EB_cp_0.velocity.X + (float)Main.rand.Next(-10, 10) * 0.002f;
                                if ((double)Main.dust[i].velocity.X < -0.25)
                                {
                                    Main.dust[i].velocity.X = -0.25f;
                                }
                                if ((double)Main.dust[i].velocity.X > 0.25)
                                {
                                    Main.dust[i].velocity.X = 0.25f;
                                }
                            }
                            if (Main.dust[i].type == 35)
                            {
                                float num = Main.dust[i].scale * 1.6f;
                                if (num > 1f)
                                {
                                    num = 1f;
                                }
                                Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                            }
                        }
                    }
                    if (Main.dust[i].type == 41)
                    {
                        Dust expr_B31_cp_0 = Main.dust[i];
                        expr_B31_cp_0.velocity.X = expr_B31_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.01f;
                        Dust expr_B5E_cp_0 = Main.dust[i];
                        expr_B5E_cp_0.velocity.Y = expr_B5E_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.01f;
                        if ((double)Main.dust[i].velocity.X > 0.75)
                        {
                            Main.dust[i].velocity.X = 0.75f;
                        }
                        if ((double)Main.dust[i].velocity.X < -0.75)
                        {
                            Main.dust[i].velocity.X = -0.75f;
                        }
                        if ((double)Main.dust[i].velocity.Y > 0.75)
                        {
                            Main.dust[i].velocity.Y = 0.75f;
                        }
                        if ((double)Main.dust[i].velocity.Y < -0.75)
                        {
                            Main.dust[i].velocity.Y = -0.75f;
                        }
                        Dust dust21 = Main.dust[i];
                        dust21.scale += 0.007f;
                        float num = Main.dust[i].scale * 0.7f;
                        if (num > 1f)
                        {
                            num = 1f;
                        }
                        Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num);
                    }
                    else
                    {
                        Dust expr_D06_cp_0 = Main.dust[i];
                        expr_D06_cp_0.velocity.X = expr_D06_cp_0.velocity.X * 0.99f;
                    }
                    Dust dust22 = Main.dust[i];
                    dust22.rotation += Main.dust[i].velocity.X * 0.5f;
                    Dust dust23 = Main.dust[i];
                    dust23.scale -= 0.01f;
                    if (Main.dust[i].noGravity)
                    {
                        Dust dust24 = Main.dust[i];
                        dust24.velocity *= 0.92f;
                        Dust dust25 = Main.dust[i];
                        dust25.scale -= 0.04f;
                    }
                    if (Main.dust[i].position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
                    {
                        Main.dust[i].active = false;
                    }
                    if ((double)Main.dust[i].scale < 0.1)
                    {
                        Main.dust[i].active = false;
                    }
                }
            }
        }
    }
}

﻿using System;
namespace Terraria
{
    public class Star
    {
        public Vector2 position;
        public float rotation;
        public float rotationSpeed;
        public float scale;
        public float twinkle;
        public float twinkleSpeed;
        public int type;
        public static void SpawnStars()
        {
            Main.numStars = Main.rand.Next(65, 130);
            Main.numStars = 130;
            for (int i = 0; i < Main.numStars; i++)
            {
                Main.star[i] = new Star();
                Main.star[i].position.X = (float)Main.rand.Next(-12, Main.screenWidth + 1);
                Main.star[i].position.Y = (float)Main.rand.Next(-12, (int)((double)Main.screenHeight * 1.35));
                Main.star[i].rotation = (float)Main.rand.Next(628) * 0.01f;
                Main.star[i].scale = (float)Main.rand.Next(50, 120) * 0.01f;
                Main.star[i].type = Main.rand.Next(0, 5);
                Main.star[i].twinkle = (float)Main.rand.Next(101) * 0.01f;
                Main.star[i].twinkleSpeed = (float)Main.rand.Next(40, 100) * 0.0001f;
                if (Main.rand.Next(2) == 0)
                {
                    Star star = Main.star[i];
                    star.twinkleSpeed *= -1f;
                }
                Main.star[i].rotationSpeed = (float)Main.rand.Next(10, 40) * 0.0001f;
                if (Main.rand.Next(2) == 0)
                {
                    Star star2 = Main.star[i];
                    star2.rotationSpeed *= -1f;
                }
            }
        }
        public static void UpdateStars()
        {
            for (int i = 0; i < Main.numStars; i++)
            {
                Star star = Main.star[i];
                star.twinkle += Main.star[i].twinkleSpeed;
                if (Main.star[i].twinkle > 1f)
                {
                    Main.star[i].twinkle = 1f;
                    Star star2 = Main.star[i];
                    star2.twinkleSpeed *= -1f;
                }
                else
                {
                    if ((double)Main.star[i].twinkle < 0.5)
                    {
                        Main.star[i].twinkle = 0.5f;
                        Star star3 = Main.star[i];
                        star3.twinkleSpeed *= -1f;
                    }
                }
                Star star4 = Main.star[i];
                star4.rotation += Main.star[i].rotationSpeed;
                if ((double)Main.star[i].rotation > 6.28)
                {
                    Star star5 = Main.star[i];
                    star5.rotation -= 6.28f;
                }
                if (Main.star[i].rotation < 0f)
                {
                    Star star6 = Main.star[i];
                    star6.rotation += 6.28f;
                }
            }
        }
    }
}
